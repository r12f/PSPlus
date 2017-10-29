Import-Module "$PSScriptRoot\..\..\Publish\PSPlus.Tfs\PSPlus.Modules.Tfs.psd1" -Force

Describe "PSPlus.Tfs.Work" {
    $tpcURL = Read-Host "Team Project Collection URL"
    $tpName = Read-Host "Team Project Name"

    $tpc = Get-TfsTeamProjectCollection -URL $tpcURL
    $tp = Get-TfsTeamProject -Collection $tpc -Name $tpName

    Context "When manipulating on work item types" {
        It "Should be able to get work item types." {
            $workItemTypes = Get-TfsWorkItemType -Project $tp
            $workItemTypes | Should Not Be $null
        }
    }

    Context "When manipulating on registered types" {
        It "Should be able to get registered types." {
            $registeredLinkTypes = Get-TfsRegisteredLinkType -Collection $tpc
            $registeredLinkTypes | Should Not Be $null

            $registeredLinkTypes = @($registeredLinkTypes)
            $registeredLinkTypes.Count | Should Not Be 0
        }
    }

    Context "When manipulating on work item link types" {
        It "Should be able to get work item link types." {
            $workItemLinkTypeEnds = Get-TfsWorkItemLinkTypeEnd -Collection $tpc
            $workItemLinkTypeEnds | Should Not Be $null

            $workItemLinkTypeEnds = @($workItemLinkTypeEnds)
            $workItemLinkTypeEnds.Count | Should Not Be 0
        }
    }

    Context "When creating and removing work items" {
        $workItemName = "Test work item " + (Get-Random)
        $childWorkItemName = "Child test work item " + (Get-Random)

        It "Should be able to create work item and remove it by id." {
            $workItem = New-TfsWorkItem Task $workItemName -Project $tp
            $workItem | Should Not Be $null
            $workItem.Id | Should Not Be 0
            $workItem.Title | Should Be $workItemName

            $removeResults = Remove-TfsWorkItem -Id $workItem.Id -Collection $tpc
            $removeResults | Should Not Be $null
            $removeResults.WorkItem | Should Not Be $null
            $removeResults.WorkItem.Id | Should Be $workItem.Id
            $removeResults.Error | Should Be $null
        }

        It "Should be able to create work with tags and priority, and be able to remove work item by work item reference." {
            $workItem = New-TfsWorkItem Task $workItemName -Tags "tag1;tag2" -Priority 1 -Project $tp
            $workItem | Should Not Be $null
            $workItem.Id | Should Not Be 0
            $workItem.Title | Should Be $workItemName
            $workItem.Tags | Should Be "tag1; tag2"
            $workItem.Fields[[PSPlus.Tfs.WIQLUtils.WIQLSystemFieldNames]::Priority].Value | Should Be 1

            $removeResults = Remove-TfsWorkItem -w $workItem -Collection $tpc
            $removeResults | Should Not Be $null
            $removeResults.WorkItem | Should Not Be $null
            $removeResults.WorkItem.Id | Should Be $workItem.Id
            $removeResults.Error | Should Be $null
        }

        It "Should be able to create work with assigned to as email." {
            $workItem = New-TfsWorkItem Task $workItemName -AssignedTo $tpc.AuthorizedIdentity.UniqueName -Project $tp
            $workItem | Should Not Be $null
            $workItem.Id | Should Not Be 0

            $removeResults = Remove-TfsWorkItem -w $workItem -Collection $tpc
            $removeResults | Should Not Be $null
            $removeResults.WorkItem | Should Not Be $null
            $removeResults.WorkItem.Id | Should Be $workItem.Id
            $removeResults.Error | Should Be $null
        }

        It "Should be able to create work with assigned to as identity." {
            $userIdentity = Get-TfsUserIdentity $tpc.AuthorizedIdentity.UniqueName -Collection $tpc
            $userIdentity | Should Not Be $null

            $workItem = New-TfsWorkItem Task $workItemName -AssignedTo $userIdentity -Project $tp
            $workItem | Should Not Be $null
            $workItem.Id | Should Not Be 0

            $removeResults = Remove-TfsWorkItem -w $workItem -Collection $tpc
            $removeResults | Should Not Be $null
            $removeResults.WorkItem | Should Not Be $null
            $removeResults.WorkItem.Id | Should Be $workItem.Id
            $removeResults.Error | Should Be $null
        }

        It "Should be able to create and link child work item." {
            # Create parent work item
            $workItem = New-TfsWorkItem Task $workItemName -Project $tp
            $workItem | Should Not Be $null
            $workItem.Id | Should Not Be 0
            $workItem.Title | Should Be $workItemName

            # Create child work item
            $childWorkItem = New-TfsWorkItem Task $childWorkItemName -Parent $workItem.Id -Project $tp
            $childWorkItem | Should Not Be $null
            $childWorkItem.Id | Should Not Be 0
            $childWorkItem.Title | Should Be $childWorkItemName
            $childWorkItem.Links.Count | Should Be 1
            $childWorkItem.Links[0].RelatedWorkItemId | Should Be $workItem.Id

            $workItem.SyncToLatest()
            $workItem.Links.Count | Should Be 1
            $workItem.Links[0].RelatedWorkItemId | Should Be $childWorkItem.Id

            # Remove parent work item link
            Remove-TfsWorkItemLink -WorkItem $childWorkItem -WorkItemRelationType Parent -Collection $tpc
            $childWorkItem.Links.Count | Should Be 0
            $workItem.Links.Count | Should Be 1
            $workItem.SyncToLatest()
            $workItem.Links.Count | Should Be 0

            # Create parent work item link
            New-TfsWorkItemLink -WorkItem $childWorkItem -WorkItemRelationType Parent -RelatedWorkItemId $workItem.Id -Collection $tpc
            $childWorkItem.Links.Count | Should Be 1
            $workItem.Links.Count | Should Be 0
            $workItem.SyncToLatest()
            $workItem.Links.Count | Should Be 1

            # Clean up
            $removeResults = Remove-TfsWorkItem -WorkItem $workItem,$childWorkItem -Collection $tpc
        }

        It "Should fail when removing work item without filters." {
            $failure = $null

            try {
                Remove-TfsWorkItem -Collection $tpc
            } catch {
                $failure = $_
            }

            $failure | Should Not Be $null
        }
    }

    Context "When querying work items" {
        $workItemName = "Test work item " + (Get-Random)
        $workItem = New-TfsWorkItem Task $workItemName -Priority 1 -Project $tp

        It "Get work item by id" {
            $workItemFromQuery = Get-TfsWorkItem -Collection $tpc -Id $workItem.Id
            $workItemFromQuery | Should Not Be $null
            $workItemFromQuery.Id | Should Be $workItem.Id
            $workItemFromQuery.Title | Should Be $workItemName
        }

        It "Get work item by title" {
            $workItemFromQuery = Get-TfsWorkItem -Collection $tpc -Title $workItemName
            $workItemFromQuery | Should Not Be $null
            $workItemFromQuery.Id | Should Be $workItem.Id
            $workItemFromQuery.Title | Should Be $workItemName
        }

        It "Get work item by type" {
            $workItemFromQuery = Get-TfsWorkItem -Collection $tpc -Type Task -Title $workItemName
            $workItemFromQuery | Should Not Be $null
            $workItemFromQuery.Id | Should Be $workItem.Id

            $workItemFromQuery = Get-TfsWorkItem -Collection $tpc -Type Bug -Title $workItemName
            $workItemFromQuery | Should Be $null
        }

        It "Get work item by priority" {
            $workItemFromQuery = Get-TfsWorkItem -Collection $tpc -Priority 1 -Title $workItemName
            $workItemFromQuery | Should Not Be $null
            $workItemFromQuery.Id | Should Be $workItem.Id

            $workItemFromQuery = Get-TfsWorkItem -Collection $tpc -Priority 2 -Title $workItemName
            $workItemFromQuery | Should Be $null
        }

        $removeResults = Remove-TfsWorkItem -Id $workItem.Id -Collection $tpc
    }
}