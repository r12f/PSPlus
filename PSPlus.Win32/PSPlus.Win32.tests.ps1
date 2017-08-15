Import-Module "$PSScriptRoot\..\PSPlus.psd1" -Force

Describe "Win32" {
    Context "When trying to use window control" {
        $notepad = Start-Process notepad -PassThru
        do
        {
            Sleep 1
        }
        while ($notepad.MainWindowHandle -eq 0)

        $notepadWindowControl = New-WindowControl $notepad.MainWindowHandle

        It "Should be able to create window control" {
            $notepadWindowControl | Should Not Be $null
            $notepadWindowControl.Hwnd | Should Be $notepad.MainWindowHandle
        }

        It "Should be able to check if the window is availble" {
            $notepadWindowControl.IsWindow() | Should Be $true
        }

        It "Should be able to get window data" {
            $style32 = $notepadWindowControl.GetWindowLong(-16)
            $style32 | Should Not Be 0

            $newStyle32 = $style32 -band (-bnot 0x10000);
            $notepadWindowControl.SetWindowLong(-16, $newStyle32) | Should Be $style32

            $style32 = $notepadWindowControl.GetWindowLong(-16)
            $style32 | Should Be $newStyle32

            $style64 = $notepadWindowControl.GetWindowLongPtr(-16)
            $style64 | Should Not Be 0

            $newStyle64 = $style64 -bor 0x10000;
            $notepadWindowControl.SetWindowLongPtr(-16, $newStyle64) | Should Be $style64

            $style64 = $notepadWindowControl.GetWindowLongPtr(-16)
            $style64 | Should Be $newStyle64
        }

        It "Should be able to check and change the enabled state" {
            $enabled = $notepadWindowControl.IsWindowEnabled()
            $enabled | Should Be $true

            # If the window was not previously disabled, the return value is zero.
            $notepadWindowControl.EnableWindow($false) | Should Be $false
            $enabled = $notepadWindowControl.IsWindowEnabled()
            $enabled | Should Be $false

            # If the window was previously disabled, the return value is nonzero.
            $notepadWindowControl.EnableWindow($true) | Should Be $true
            $enabled = $notepadWindowControl.IsWindowEnabled()
            $enabled | Should Be $true
        }
    }
}