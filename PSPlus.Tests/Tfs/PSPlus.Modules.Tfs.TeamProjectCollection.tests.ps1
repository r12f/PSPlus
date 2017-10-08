Import-Module "$PSScriptRoot\..\..\Publish\PSPlus.Tfs\PSPlus.Modules.Tfs.psd1" -Force

Describe "PSPlus.Tfs.TeamProjectCollection" {
    Context "When operating on team project collection" {
        $tpcURL = Read-Host "Team Project Collection URL: "
        It "Should be able to get team project collection." {
            $tpc = Get-TfsTeamProjectCollection -URL $tpcURL
            $tpc | Should Not Be $null
        }
    }
}