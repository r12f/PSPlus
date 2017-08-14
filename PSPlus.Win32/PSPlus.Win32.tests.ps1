Import-Module "$PSScriptRoot\..\PSPlus.psd1" -Force

Describe "Win32" {
    Context "When trying to use window control" {
        It "Should be able to create window control" {
            New-WindowControl 0x10010 | Should Not Be $null
        }
    }
}