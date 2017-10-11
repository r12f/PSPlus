Import-Module "$PSScriptRoot\..\..\Publish\PSPlus.Tfs\PSPlus.Modules.Tfs.psd1" -Force

Describe "PSPlus.Tfs.TeamProject" {
    Context "When operating on team project" {
        $tpcURL = Read-Host "Team Project Collection URL"
        $tpc = Get-TfsTeamProjectCollection -URL $tpcURL

        It "Should be able to get team projects." {
            $projects = Get-TfsTeamProject -Collection $tpc -Name *
            $projects | Should Not Be $null

            $projects = @($projects)
            $projects.Count | Should Not Be 0
        }
    }
}