function Restore-DesktopLayoutFromLayoutRules
{
    [CmdletBinding()]
    param(
        [Parameter(Position = 0, Mandatory = $true)]
        [Object[]] $LayoutRules
    )

    $LayoutRuleFieldNames = [PSPlus.Core.Windows.Window.DesktopLayout.LayoutRuleFieldNames]

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
        $windowTitle = $window.GetWindowText()
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
            $matchRule = $_.($LayoutRuleFieldNames::Match)
            if ($matchRule.($LayoutRuleFieldNames::MatchProcessName) -ne $null) {
                $isProcessNameMatches = Select-String -InputObject $windowProcessName -Pattern $matchRule.($LayoutRuleFieldNames::MatchProcessName) -SimpleMatch -Quiet
                if (-not $isProcessNameMatches) {
                    return $false
                }
            }

            if ($matchRule.($LayoutRuleFieldNames::MatchProcessNamePattern) -ne $null) {
                $isProcessNameMatches = Select-String -InputObject $windowProcessName -Pattern $matchRule.($LayoutRuleFieldNames::MatchProcessNamePattern) -Quiet
                if (-not $isProcessNameMatches) {
                    return $false
                }
            }

            if ($matchRule.($LayoutRuleFieldNames::MatchClassName) -ne $null) {
                $isClassNameMatches = Select-String -InputObject $className -Pattern $matchRule.($LayoutRuleFieldNames::MatchClassName) -SimpleMatch -Quiet
                if (-not $isClassNameMatches) {
                    return $false
                }
            }

            if ($matchRule.($LayoutRuleFieldNames::MatchClassNamePattern) -ne $null) {
                $isClassNameMatches = Select-String -InputObject $className -Pattern $matchRule.($LayoutRuleFieldNames::MatchClassNamePattern)  -Quiet
                if (-not $isClassNameMatches) {
                    return $false
                }
            }

            if ($matchRule.($LayoutRuleFieldNames::MatchWindowTitlePattern) -ne $null) {
                $isWindowTitleMatches = Select-String -InputObject $windowTitle -Pattern $matchRule.($LayoutRuleFieldNames::MatchWindowTitlePattern)  -Quiet
                if (-not $isWindowTitleMatches) {
                    return $false
                }
            }

            return $true
        } | Select-Object -First 1

        if ($firstMatchedRule -eq $null) {
            return
        }

        $windowPlacementFromRule = $firstMatchedRule.($LayoutRuleFieldNames::Placement)

        $windowPlacement = New-User32WindowPlacement
        $windowPlacement.Flags = $windowPlacementFromRule.($LayoutRuleFieldNames::PlacementWindowFlags)
        $windowPlacement.ShowCmd = $windowPlacementFromRule.($LayoutRuleFieldNames::PlacementWindowShowCmd)
        $windowPlacement.MinPosition = New-User32Point $windowPlacementFromRule.($LayoutRuleFieldNames::PlacementWindowMinPositionX) $windowPlacementFromRule.($LayoutRuleFieldNames::PlacementWindowMinPositionY)
        $windowPlacement.MaxPosition = New-User32Point $windowPlacementFromRule.($LayoutRuleFieldNames::PlacementWindowMaxPositionX) $windowPlacementFromRule.($LayoutRuleFieldNames::PlacementWindowMaxPositionY)
        $windowPlacement.NormalPosition = New-User32Rect $windowPlacementFromRule.($LayoutRuleFieldNames::PlacementWindowNormalPositionLeft) $windowPlacementFromRule.($LayoutRuleFieldNames::PlacementWindowNormalPositionTop) $windowPlacementFromRule.($LayoutRuleFieldNames::PlacementWindowNormalPositionRight) $windowPlacementFromRule.($LayoutRuleFieldNames::PlacementWindowNormalPositionBottom) 

        Write-Host ("Restoring window {0} of process {1} at ({2}, {3}) - ({4}, {5})." -f $className, $windowProcessName, $windowPlacement.NormalPosition.Left, $windowPlacement.NormalPosition.Top, $windowPlacement.NormalPosition.Right, $windowPlacement.NormalPosition.Bottom)
        if (-not $window.SetWindowPlacement($windowPlacement)) {
            Write-Host "Restoring failed."
        }
    }
}
