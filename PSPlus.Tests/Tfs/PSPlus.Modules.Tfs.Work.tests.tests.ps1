Import-Module "$PSScriptRoot\..\..\Publish\PSPlus.Tfs\PSPlus.Modules.Tfs.psd1" -Force

Describe "PSPlus.Tfs.Work" {
    $tpcURL = Read-Host "Team Project Collection URL"
    $tpName = Read-Host "Team Project Name"

    $tpc = Get-TfsTeamProjectCollection -URL $tpcURL
    $tp = Get-TfsTeamProject -Collection $tpc -Name $tpName

    Context "When manipulating on work item types" {
        It "Should be able to get get work item types." {
            $workItemTypes = Get-TfsWorkItemType -Project $tp
            $workItemTypes | Should Not Be $null
        }
    }

    Context "When manipulating on work items" {
        $workItemName = "Test work item " + (Get-Random)
        $childWorkItemName = "Child test work item " + (Get-Random)

        It "Should be able to get and delete work item by id." {
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

        It "Should be able to delete work item by work item reference." {
            $workItem = New-TfsWorkItem Task $workItemName -AssignedTo $tpc.AuthorizedIdentity.DisplayName -Tags "tag1;tag2" -Project $tp
            $workItem | Should Not Be $null
            $workItem.Id | Should Not Be 0
            $workItem.Title | Should Be $workItemName

            $removeResults = Remove-TfsWorkItem -w $workItem -Collection $tpc
            $removeResults | Should Not Be $null
            $removeResults.WorkItem | Should Not Be $null
            $removeResults.WorkItem.Id | Should Be $workItem.Id
            $removeResults.Error | Should Be $null
        }

        It "Should be able to create child work item." {
            $workItem = New-TfsWorkItem Task $workItemName -Project $tp
            $workItem | Should Not Be $null
            $workItem.Id | Should Not Be 0
            $workItem.Title | Should Be $workItemName

            $childWorkItem = New-TfsWorkItem Task $childWorkItemName -Parent $workItem.Id -Project $tp
            $childWorkItem | Should Not Be $null
            $childWorkItem.Id | Should Not Be 0
            $childWorkItem.Title | Should Be $childWorkItemName

            $removeResults = Remove-TfsWorkItem -WorkItems $workItem,$childWorkItem -Collection $tpc
        }

        It "Should fail when deleting work item without filters." {
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
        $workItem = New-TfsWorkItem Task $workItemName -Project $tp

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

        $removeResults = Remove-TfsWorkItem -Id $workItem.Id -Collection $tpc
    }
}