Import-Module "$PSScriptRoot\..\..\PSPlus.psd1" -Force

Describe "Win32" {
    Context "When trying to create the win32 data structures" {
        It "Should be able to create Win32 point" {
            $win32Point = New-Win32Point 100 200
            $win32Point.X | Should Be 100
            $win32Point.Y | Should Be 200
        }

        It "Should be able to create Win32 rect" {
            $win32Rect = New-Win32Rect 100 200 300 400
            $win32Rect.Left | Should Be 100
            $win32Rect.Top | Should Be 200
            $win32Rect.Right | Should Be 300
            $win32Rect.Bottom | Should Be 400
        }

        It "Should be able to create Win32 scroll info" {
            $win32ScrollInfo = New-Win32ScrollInfo 100 $Win32Consts::SIF_ALL 10 200 800 150 120
            $win32ScrollInfo.Size | Should Be 100
            $win32ScrollInfo.Mask | Should Be $Win32Consts::SIF_ALL
            $win32ScrollInfo.Min | Should Be 10
            $win32ScrollInfo.Max | Should Be 200
            $win32ScrollInfo.Page | Should Be 800
            $win32ScrollInfo.Pos | Should Be 150
            $win32ScrollInfo.TrackPos | Should Be 120
        }

        It "Should be able to create Win32 size" {
            $win32Size = New-Win32Size 100 200
            $win32Size.Cx | Should be 100
            $win32Size.Cy | Should be 200
        }

        It "Should be able to create Win32 window placement" {
            $win32WindowPlacement = New-Win32WindowPlacement 
            $win32WindowPlacement | Should Not Be $null
            $win32WindowPlacement.Length | Should Not Be 0

            # We cannot assign the values in the nested structures directly:
            # See MSDN: https://msdn.microsoft.com/en-us/library/aa288471(v=vs.71).aspx
            $win32WindowPlacement.MinPosition.X = 1
            $win32WindowPlacement.MinPosition.X | Should Be 0
            $win32WindowPlacement.MinPosition.Y = 2
            $win32WindowPlacement.MinPosition.Y | Should Be 0

            $win32WindowPlacement.MinPosition = New-Win32Point 1 2
            $win32WindowPlacement.MinPosition.X | Should Be 1
            $win32WindowPlacement.MinPosition.Y | Should Be 2
        }
    }

    Context "When trying to get the desktop window" {
        It "Should be able to get the desktop window" {
            $desktopWindow = Get-DesktopWindow
            $desktopWindow | Should Not Be $null
            $desktopWindow.IsWindow() | Should Be $true
        }
    }

    Context "When trying to enumerate all the top level windows" {
        It "Should be able to enumerate all the top level windows" {
            $topLevelWindows = Get-Windows
            foreach ($topLevelWindow in $topLevelWindows) {
                $topLevelWindow.IsWindow() | Should Be $true

                # We need to check both the parent and the child style to make sure a window is a top level window,
                # because some windows like "ComboLBox" will have WS_CHILD style but no parent.
                $hasChildStyle = ($topLevelWindow.GetWindowLong($Win32Consts::GWL_STYLE) -band $Win32Consts::WS_CHILD) -ne 0
                $hasParent = $topLevelWindow.GetWindowLongPtr($Win32Consts::GWLP_HWNDPARENT) -ne 0
                $hasChildStyle -and $hasParent | Should Be $false
            }
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

        It "Should be able to get the process id and thread id" {
            $notepadWindowControl.GetWindowProcessId() | Should Be $notepad.Id
            $notepadWindowControl.GetWindowThreadId() | Should Not Be 0
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

        It "Should be able to enumerate the child windows by calling GetChildren" {
            foreach ($childWindow in $notepadWindowControl.GetChildren()) {
                $childWindow.IsWindow() | Should Be $true
                $childWindow.GetParent().Hwnd | Should Be $notepadWindowControl.Hwnd
            }
        }

        It "Should be able to enumerate the child windows calling Get-Windows" {
            $childWindows = Get-Windows -Parent $notepadWindowControl.Hwnd
            foreach ($childWindow in $childWindows) {
                $childWindow.IsWindow() | Should Be $true
                $childWindow.GetParent().Hwnd | Should Be $notepadWindowControl.Hwnd
            }
        }

        It "Should be able to get and set the window text" {
            $titleLength = $notepadWindowControl.GetWindowTextLength()
            $titleLength | Should Not Be 0

            $title = $notepadWindowControl.GetWindowText()
            $title | Should Not Be $null
            $title | Should Not Be ''

            $newTitle = $title + "_test";
            $notepadWindowControl.SetWindowText($newTitle) | Should Be $true

            $title = $notepadWindowControl.GetWindowText()
            $title | Should Not Be $null
            $title | Should Not Be ''
            $title | Should Be $newTitle
            $title.EndsWith("_test") | Should Be $true
        }

        It "Should be able to get the class name" {
            $className = $notepadWindowControl.GetClassName()
            $className | Should Not Be $null
            $className | Should Not Be ''
        }

        It "Should be able to get the class long" {
            $wndProc = $notepadWindowControl.GetClassLongPtr($Win32Consts::GCLP_WNDPROC)
            $wndProc | Should Not Be $null
        }

        It "Should be able to get menu" {
            $menu = $notepadWindowControl.GetMenu()
            $menu | Should Not Be $null

            $sysmenu = $notepadWindowControl.GetSystemMenu($false)
            $sysmenu | Should Not Be $null
        }

        It "Should be able to get and change the visibility state" {
            $notepadWindowControl.ShowWindow($Win32Consts::SW_SHOWNORMAL);
            $isVisible = $notepadWindowControl.IsWindowVisible()
            $isVisible | Should Be $true

            $notepadWindowControl.ShowWindow($Win32Consts::SW_HIDE);
            $isVisible = $notepadWindowControl.IsWindowVisible()
            $isVisible | Should Be $false

            $notepadWindowControl.ShowWindow($Win32Consts::SW_SHOWNORMAL);
            $isVisible = $notepadWindowControl.IsWindowVisible()
            $isVisible | Should Be $true
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

        It "Should be able to resize the window" {
            $notepadWindowControl.MoveWindow(0, 0, 640, 480, $true)
            $windowRect = $notepadWindowControl.GetWindowRect()
            $windowRect.Left | Should Be 0
            $windowRect.Top | Should Be 0
            $windowRect.Right | Should Be 640
            $windowRect.Bottom | Should Be 480

            $notepadWindowControl.SetWindowPos(0, 100, 200, 300, 200, $Win32Consts.SWP_NOZORDER)
            $windowRect = $notepadWindowControl.GetWindowRect()
            $windowRect.Left | Should Be 100
            $windowRect.Top | Should Be 200
            $windowRect.Right | Should Be 400
            $windowRect.Bottom | Should Be 400
        }

        It "Should be able to send the message" {
            $notepadWindowControl.IsWindow() | Should Be $true
            #$notepadWindowControl.SendMessageW($Win32MsgIds::WM_CLOSE, 0, 0);
            #$notepadWindowControl.IsWindow() | Should Be $false
        }

        $notepad.Kill()
    }

    Context "When trying to use desktop layout" {
        $notepad = Start-Process notepad -PassThru

        do
        {
            Sleep 1
        }
        while ($notepad.MainWindowHandle -eq 0)

        $notepadWindowControl = New-WindowControl $notepad.MainWindowHandle

        It "Should be able to generate the layout rules and restore from it" {
            $notepadWindowControl.MoveWindow(0, 0, 640, 480, $true)

            $layoutRules = New-DesktopLayoutRulesFromCurrentLayout -IncludeProcessNames "notepad" -ExcludeClassNames "IME"
            $layoutRules.Count | Should Not Be 0

            $notepadWindowControl.MoveWindow(100, 100, 320, 240, $true)

            Restore-DesktopLayoutFromLayoutRules $layoutRules
            
            $windowRect = $notepadWindowControl.GetWindowRect()
            $windowRect.Left | Should Be 0
            $windowRect.Top | Should Be 0
            $windowRect.Right | Should Be 640
            $windowRect.Bottom | Should Be 480
        }

        It "Should be able to generate the layout rules to file" {
            $layoutRulesFile = New-TemporaryFile

            $notepadWindowControl.MoveWindow(0, 0, 640, 480, $true)

            Save-DesktopLayout -FilePath $layoutRulesFile -IncludeProcessNames "notepad" -ExcludeClassNames "IME"
            $savedLayoutRules = Get-Content $layoutRulesFile | ConvertFrom-Json
            $savedLayoutRules.Count | Should Not Be 0

            $notepadWindowControl.MoveWindow(100, 100, 320, 240, $true)

            Restore-DesktopLayout $layoutRulesFile

            $windowRect = $notepadWindowControl.GetWindowRect()
            $windowRect.Left | Should Be 0
            $windowRect.Top | Should Be 0
            $windowRect.Right | Should Be 640
            $windowRect.Bottom | Should Be 480

            Remove-Item $layoutRulesFile
        }

        $notepad.Kill()
    }
}