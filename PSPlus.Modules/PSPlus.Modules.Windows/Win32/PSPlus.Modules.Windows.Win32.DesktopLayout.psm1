#
# PSPlus.Modules.Windows.Win32.DesktopLayout.psm1
#

$LayoutRuleFieldMatch = "Match"
$LayoutRuleMatchFieldProcessName = "ProcessName"
$LayoutRuleMatchFieldProcessNamePattern = "ProcessNamePattern"
$LayoutRuleMatchFieldClassName = "ClassName"
$LayoutRuleMatchFieldClassNamePattern = "ClassNamePattern"
$LayoutRuleMatchFieldWindowTitlePattern = "WindowTitlePattern"

$LayoutRuleFieldPlacement = "Placement"
$LayoutRulePlacementFieldWindowFlags = "Flags"
$LayoutRulePlacementFieldWindowShowCmd = "ShowCmd"
$LayoutRulePlacementFieldWindowMinPositionX = "MinPositionX"
$LayoutRulePlacementFieldWindowMinPositionY = "MinPositionY"
$LayoutRulePlacementFieldWindowMaxPositionX = "MaxPositionX"
$LayoutRulePlacementFieldWindowMaxPositionY = "MaxPositionY"
$LayoutRulePlacementFieldWindowNormalPositionLeft = "NormalPositionLeft"
$LayoutRulePlacementFieldWindowNormalPositionTop = "NormalPositionTop"
$LayoutRulePlacementFieldWindowNormalPositionRight = "NormalPositionRight"
$LayoutRulePlacementFieldWindowNormalPositionBottom = "NormalPositionBottom"

function Save-DesktopLayout
{
    [CmdletBinding()]
    param(
        [Parameter(Position = 0, Mandatory = $true)]
        [string] $FilePath,

        [Parameter(Mandatory = $false)]
        [string[]] $IncludeProcessNames = @(),

        [Parameter(Mandatory = $false)]
        [string[]] $ExcludeProcessNames = @(),

        [Parameter(Mandatory = $false)]
        [string[]] $IncludeClassNames = @(),

        [Parameter(Mandatory = $false)]
        [string[]] $ExcludeClassNames = @()
    )

    $layoutRules = New-DesktopLayoutRulesFromCurrentLayout -IncludeProcessNames $IncludeProcessNames -ExcludeProcessNames $ExcludeProcessNames -IncludeClassNames $IncludeClassNames -ExcludeClassNames $ExcludeClassNames
    ConvertTo-Json -InputObject  @($layoutRules) | Out-File -FilePath $FilePath
}

function New-DesktopLayoutRulesFromCurrentLayout
{
    [CmdletBinding()]
    param(
        [Parameter(Mandatory = $false)]
        [string[]] $IncludeProcessNames = @(),

        [Parameter(Mandatory = $false)]
        [string[]] $ExcludeProcessNames = @(),

        [Parameter(Mandatory = $false)]
        [string[]] $IncludeClassNames = @(),

        [Parameter(Mandatory = $false)]
        [string[]] $ExcludeClassNames = @()
    )

    # Snap all processes
    $pidToProcessNameMap = Get-Process | ConvertTo-Dictionary { $_.Id } { $_.ProcessName }

    # Snap all top level windows
    $topLevelWindows = Get-Windows

    # Generate the window layout rules
    $windowLayoutRules = $topLevelWindows | ForEach-Object {
        $window = $_

        # Make sure the window is still alive while we are getting all the data
        if (-not $window.IsWindow()) {
            return
        }

        $isVisible = $window.IsWindowVisible()
        $windowPid = $window.GetWindowProcessId()
        $className = $window.GetClassName()
        $windowPlacement = $window.GetWindowPlacement()

        if (-not $window.IsWindow()) {
            return
        }

        # If the window is not visible, ignore. (Minimized windows will still have WS_VISIBLE style)
        if (-not $isVisible) {
            return
        }

        $windowProcessName = [string]::Empty;
        if (-not $pidToProcessNameMap.TryGetValue($windowPid, [ref] $windowProcessName)) {
            return
        }

        $isWindowProcessIncluded = Test-IsStringIncluded $windowProcessName $IncludeProcessNames $ExcludeProcessNames
        if (!$isWindowProcessIncluded) {
            return
        }

        $isClassNameIncluded = Test-IsStringIncluded $className $includeClassNames $excludeClassNames
        if (!$isClassNameIncluded) {
            return
        }

        $windowLayoutRule = @{
            $LayoutRuleFieldMatch = @{
                $LayoutRuleMatchFieldProcessName = $windowProcessName
                $LayoutRuleMatchFieldProcessNamePattern = $null
                $LayoutRuleMatchFieldClassName = $className
                $LayoutRuleMatchFieldClassNamePattern = $null
                $LayoutRuleMatchFieldWindowTitlePattern = $null
            }
            $LayoutRuleFieldPlacement = @{
                $LayoutRulePlacementFieldWindowFlags = $windowPlacement.Flags
                $LayoutRulePlacementFieldWindowShowCmd = $windowPlacement.ShowCmd
                $LayoutRulePlacementFieldWindowMinPositionX = $windowPlacement.MinPosition.X
                $LayoutRulePlacementFieldWindowMinPositionY = $windowPlacement.MinPosition.Y
                $LayoutRulePlacementFieldWindowMaxPositionX = $windowPlacement.MaxPosition.X
                $LayoutRulePlacementFieldWindowMaxPositionY = $windowPlacement.MaxPosition.Y
                $LayoutRulePlacementFieldWindowNormalPositionLeft = $windowPlacement.NormalPosition.Left
                $LayoutRulePlacementFieldWindowNormalPositionTop = $windowPlacement.NormalPosition.Top
                $LayoutRulePlacementFieldWindowNormalPositionRight = $windowPlacement.NormalPosition.Right
                $LayoutRulePlacementFieldWindowNormalPositionBottom = $windowPlacement.NormalPosition.Bottom
            }
        }

        return $windowLayoutRule
    } 

    return $windowLayoutRules
}

function Test-IsClassNameIncluded($window, $includeClassNames, $excludeClassNames) {
    if (-not $window.IsWindow()) {
        return $false
    }

    $className = $window.GetClassName()
    return Test-IsStringIncluded $className $includeClassNames $excludeClassNames
}

function Test-IsStringIncluded([string] $s, [string[]] $includeRegexes, [string[]] $excludeRegexes) {
    if ($includeRegexes -ne $null -and $includeRegexes.Count -gt 0) {
        $isStringIncluded = $includeRegexes | Test-Any { Select-String -InputObject $s -Pattern $_ -Quiet }
        if (!$isStringIncluded) {
            return $false
        }
    }

    if ($excludeRegexes -ne $null -and $excludeRegexes.Count -gt 0) {
        $isStringExcluded = $excludeRegexes | Test-Any { Select-String -InputObject $s -Pattern $_ -Quiet }
        if ($isStringExcluded) {
            return $false
        }
    }

    return $true
}

function Restore-DesktopLayout
{
    [CmdletBinding()]
    param(
        [Parameter(Position = 0, Mandatory = $true)]
        [string] $FilePath
    )

    $layoutRules = Get-Content $FilePath | ConvertFrom-Json
    Restore-DesktopLayoutFromLayoutRules $layoutRules
}

function Restore-DesktopLayoutFromLayoutRules
{
    [CmdletBinding()]
    param(
        [Parameter(Position = 0, Mandatory = $true)]
        [Object[]] $LayoutRules
    )

    # Snap all processes
    $pidToProcessNameMap = Get-Process | ConvertTo-Dictionary { $_.Id } { $_.ProcessName }

    # Snap all top level windows
    $topLevelWindows = Get-Windows

    $topLevelWindows | ForEach-Object {
        $window = $_

        # Make sure the window is still alive while we are getting all the data
        if (-not $window.IsWindow()) {
            return
        }

        $isVisible = $window.IsWindowVisible()
        $windowPid = $window.GetWindowProcessId()
        $className = $window.GetClassName()
        $windowPlacement = $window.GetWindowPlacement()

        if (-not $window.IsWindow()) {
            return
        }

        # If the window is not visible, ignore. (Minimized windows will still have WS_VISIBLE style)
        if (-not $isVisible) {
            return
        }

        $windowProcessName = [string]::Empty;
        if (-not $pidToProcessNameMap.TryGetValue($windowPid, [ref] $windowProcessName)) {
            return
        }

        $firstMatchedRule = $LayoutRules | Where-Object {
            $matchRule = $_.$LayoutRuleFieldMatch
            if ($matchRule.$LayoutRuleMatchFieldProcessName -ne $null) {
                $isProcessNameMatches = Select-String -InputObject $windowProcessName -Pattern $matchRule.$LayoutRuleMatchFieldProcessName -SimpleMatch -Quiet
                if (-not $isProcessNameMatches) {
                    return $false
                }
            }

            if ($matchRule.$LayoutRuleMatchFieldProcessNamePattern -ne $null) {
                $isProcessNameMatches = Select-String -InputObject $windowProcessName -Pattern $matchRule.$LayoutRuleMatchFieldProcessNamePattern -Quiet
                if (-not $isProcessNameMatches) {
                    return $false
                }
            }

            if ($matchRule.$LayoutRuleMatchFieldClassName -ne $null) {
                $isClassNameMatches = Select-String -InputObject $className -Pattern $matchRule.$LayoutRuleMatchFieldClassName -SimpleMatch -Quiet
                if (-not $isClassNameMatches) {
                    return $false
                }
            }

            if ($matchRule.$LayoutRuleMatchFieldClassNamePattern -ne $null) {
                $isClassNameMatches = Select-String -InputObject $className -Pattern $matchRule.$LayoutRuleMatchFieldClassNamePattern  -Quiet
                if (-not $isClassNameMatches) {
                    return $false
                }
            }

            if ($matchRule.$LayoutRuleMatchFieldWindowTitlePattern -ne $null) {
                $isWindowTitleMatches = Select-String -InputObject $className -Pattern $matchRule.$LayoutRuleMatchFieldWindowTitlePattern  -Quiet
                if (-not $isWindowTitleMatches) {
                    return $false
                }
            }

            return $true
        } | Select-Object -First 1

        if ($firstMatchedRule -eq $null) {
            return
        }

        $windowPlacementFromRule = $firstMatchedRule.$LayoutRuleFieldPlacement

        $windowPlacement = New-Win32WindowPlacement
        $windowPlacement.Flags = $windowPlacementFromRule.$LayoutRulePlacementFieldWindowFlags
        $windowPlacement.ShowCmd = $windowPlacementFromRule.$LayoutRulePlacementFieldWindowShowCmd
        $windowPlacement.MinPosition = New-Win32Point $windowPlacementFromRule.$LayoutRulePlacementFieldWindowMinPositionX $windowPlacementFromRule.$LayoutRulePlacementFieldWindowMinPositionY
        $windowPlacement.MaxPosition = New-Win32Point $windowPlacementFromRule.$LayoutRulePlacementFieldWindowMaxPositionX $windowPlacementFromRule.$LayoutRulePlacementFieldWindowMaxPositionY
        $windowPlacement.NormalPosition = New-Win32Rect $windowPlacementFromRule.$LayoutRulePlacementFieldWindowNormalPositionLeft $windowPlacementFromRule.$LayoutRulePlacementFieldWindowNormalPositionTop $windowPlacementFromRule.$LayoutRulePlacementFieldWindowNormalPositionRight $windowPlacementFromRule.$LayoutRulePlacementFieldWindowNormalPositionBottom 
        $window.SetWindowPlacement($windowPlacement)
    }
}
