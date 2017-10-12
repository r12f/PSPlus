Import-Module "$PSScriptRoot\..\..\Publish\PSPlus.Tfs\PSPlus.Modules.Tfs.psd1" -Force

Describe "PSPlus.Tfs.TeamProject" {
    Context "When operating on team project" {
        $tpcURL = Read-Host "Team Project Collection URL"
        $tpName = Read-Host "Team Project Name"

        $tpc = Get-TfsTeamProjectCollection -URL $tpcURL

        It "Should be able to get team projects." {
            $projects = Get-TfsTeamProject -Collection $tpc -Name *
            $projects | Should Not Be $null

            $projects = @($projects)
            $projects.Count | Should Not Be 0
        }

        It "Should be able to connect to team project collection." {
            $project = Connect-TfsTeamProject -Collection $tpc -Name $tpName -PassThru
            $project | Should Not Be $null
        }
    }
}