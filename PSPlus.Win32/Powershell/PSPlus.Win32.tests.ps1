Import-Module "$PSScriptRoot\..\..\PSPlus.psd1" -Force

Describe "Win32" {
    Context "When trying to get the desktop window" {
        It "Should be able to get the desktop window" {
            $desktopWindow = Get-DesktopWindow
            $desktopWindow | Should Not Be $null
            $desktopWindow.IsWindow() | Should Be $true
        }
    }

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
            $style32 = $notepadWindowControl.GetWindowLong($Win32Consts::GWL_STYLE)
            $style32 | Should Not Be 0

            $newStyle32 = $style32 -band (-bnot 0x10000);
            $notepadWindowControl.SetWindowLong(-16, $newStyle32) | Should Be $style32

            $style32 = $notepadWindowControl.GetWindowLong($Win32Consts::GWL_STYLE)
            $style32 | Should Be $newStyle32

            $style64 = $notepadWindowControl.GetWindowLongPtr($Win32Consts::GWL_STYLE)
            $style64 | Should Not Be 0

            $newStyle64 = $style64 -bor 0x10000;
            $notepadWindowControl.SetWindowLongPtr($Win32Consts::GWL_STYLE, $newStyle64) | Should Be $style64

            $style64 = $notepadWindowControl.GetWindowLongPtr($Win32Consts::GWL_STYLE)
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

        It "Should be able to get foreground window" {
            $window = Get-ForegroundWindow
            $window | Should Not Be $null
            $window.IsWindow() | Should Be $true
        }

        It "Should be able to get the active window" {
            $activeWindow = $notepadWindowControl.GetActiveWindow()
            $activeWindow.IsWindow() | Should Be $true
        }

        It "Should be able to get and set the capture window" {
            $captureWindow = $notepadWindowControl.GetCapture()
            $captureWindow.IsWindow() | Should Be $false

            $notepadWindowControl.SetCapture()

            $captureWindow = $notepadWindowControl.GetCapture()
            $captureWindow.IsWindow() | Should Be $true

            $notepadWindowControl.ReleaseCapture() | Should Be $true
        }

        It "Should be able to get the focus window" {
            $focusWindow = $notepadWindowControl.GetFocus()
            $focusWindow.IsWindow() | Should Be $true
        }

        It "Should be able to get the top window" {
            $topWindow = $notepadWindowControl.GetTopWindow()
            $topWindow.IsWindow() | Should Be $true
        }

        It "Should be able to get the child window" {
            $childWindow = $notepadWindowControl.GetWindow($Win32Consts::GW_CHILD)
            $childWindow.IsWindow() | Should Be $true

            $notepadWindowControl.IsChild($childWindow.Hwnd) | Should Be $true

            $childParentWindow = $childWindow.GetParent()
            $childParentWindow.IsWindow() | Should Be $true
            $childParentWindow.Hwnd | Should Be $notepadWindowControl.Hwnd
        }

        It "Should be able to enumerate the sibiling windows" {
            $nextWindow = $notepadWindowControl.GetWindow($Win32Consts::GW_HWNDNEXT)
            $nextWindow.IsWindow() | Should Be $true

            $currentWindow = $nextWindow.GetWindow($Win32Consts::GW_HWNDPREV)
            $currentWindow.IsWindow() | Should Be $true
            $currentWindow.Hwnd | Should Be $notepadWindowControl.Hwnd
        }

        It "Should be able to get and set the window text" {
            $titleLength = $notepadWindowControl.GetWindowTextLength()
            $titleLength | Should Not Be 0

            $title = $notepadWindowControl.GetWindowText()
            $title | Should Not Be $null
            $title | Should Not Be ''

            $newTitle = "Test_" + $title;
            $notepadWindowControl.SetWindowText($newTitle) | Should Be $true

            $title = $notepadWindowControl.GetWindowText()
            $title | Should Not Be $null
            $title | Should Not Be ''
            $title | Should Be $newTitle
            $title.StartsWith("Test_") | Should Be $true
        }

        It "Should be able to get menu" {
            $menu = $notepadWindowControl.GetMenu()
            $menu | Should Not Be $null

            $sysmenu = $notepadWindowControl.GetSystemMenu($false)
            $sysmenu | Should Not Be $null
        }

        It "Should be able to check and change the minimized and maximized state" {
            $notepadWindowControl.ShowWindow($Win32Consts::SW_SHOWMAXIMIZED);
            $isMaximized = $notepadWindowControl.IsZoomed()
            $isMaximized | Should Be $true

            $notepadWindowControl.ShowWindow($Win32Consts::SW_SHOWNORMAL);
            $isMaximized = $notepadWindowControl.IsZoomed()
            $isMaximized | Should Be $false

            $notepadWindowControl.ShowWindow($Win32Consts::SW_SHOWMINIMIZED);
            $isMinimized = $notepadWindowControl.IsIconic()
            $isMinimized | Should Be $true

            $notepadWindowControl.ShowWindow($Win32Consts::SW_SHOWNORMAL);
            $isMinimized = $notepadWindowControl.IsIconic()
            $isMinimized | Should Be $false
        }

        It "Should be able to get window and client rect" {
            $windowRect = $notepadWindowControl.GetWindowRect()
            $windowRect.Right - $windowRect.Left | Should Not Be 0
            $windowRect.Bottom - $windowRect.Top| Should Not Be 0

            $clientRect = $notepadWindowControl.GetClientRect()
            $clientRect.Right - $clientRect.Left | Should Not Be 0
            $clientRect.Bottom - $clientRect.Top| Should Not Be 0

            $clientRectInScreenCoord = $notepadWindowControl.ClientToScreen($clientRect)
            $clientRectInScreenCoord.Right - $clientRectInScreenCoord.Left | Should Be $clientRect.Right - $clientRect.Left
            $clientRectInScreenCoord.Bottom - $clientRectInScreenCoord.Top | Should Be $clientRect.Bottom - $clientRect.Top

            $clientRectInClientCoord = $notepadWindowControl.ScreenToClient($clientRectInScreenCoord)
            $clientRectInClientCoord.Left | Should Be $clientRect.Left
            $clientRectInClientCoord.Right | Should Be $clientRect.Right
            $clientRectInClientCoord.Top | Should Be $clientRect.Top
            $clientRectInClientCoord.Bottom | Should Be $clientRect.Bottom

            $screenPoint = [PSPlus.Win32.Interop.Win32Point]@{X = $clientRectInScreenCoord.Left; Y = $clientRectInScreenCoord.Top}
            $clientPoint = $notepadWindowControl.ScreenToClient($screenPoint)
            $clientPoint.X | Should Be $clientRect.Left
            $clientPoint.Y | Should Be $clientRect.Top

            $clientPointInScreenCoord = $notepadWindowControl.ClientToScreen($clientPoint)
            $clientPointInScreenCoord.X | Should Be $clientRectInScreenCoord.Left
            $clientPointInScreenCoord.Y | Should Be $clientRectInScreenCoord.Top
        }

        $notepad.Kill()
    }
}