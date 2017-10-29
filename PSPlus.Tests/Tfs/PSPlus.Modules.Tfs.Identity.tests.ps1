Import-Module "$PSScriptRoot\..\..\Publish\PSPlus.Tfs\PSPlus.Modules.Tfs.psd1" -Force

Describe "PSPlus.Tfs.Identity" {
    $tpcURL = Read-Host "Team Project Collection URL"

    $tpc = Get-TfsTeamProjectCollection -URL $tpcURL

    Context "When getting user identity" {
        It "Should be able to get user identity." {
            $userIdentity = Get-TfsUserIdentity $tpc.AuthorizedIdentity.UniqueName -Collection $tpc
            $userIdentity | Should Not Be $null
            $userIdentity.UniqueName | Should Be $tpc.AuthorizedIdentity.UniqueName
        }
    }
}