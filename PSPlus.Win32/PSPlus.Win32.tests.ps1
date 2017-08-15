Import-Module "$PSScriptRoot\..\PSPlus.psd1" -Force

Describe "Win32" {
    Context "When trying to use window control" {
        $notepad = Start-Process notepad -PassThru
        $notepadWindowControl = New-WindowControl $notepad.MainWindowHandle

        It "Should be able to create window control" {
            $notepadWindowControl | Should Not Be $null
        }

        $notepad.Kill()
    }
}