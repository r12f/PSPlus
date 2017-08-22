Import-Module "$PSScriptRoot\..\Output\Publish\PSPlus.psd1" -Force

Describe "Loader" {
    Context "When trying to call native functions" {
        It "Should be able to register a single native function." {
            $win32FunctionSigs = @(New-NativeFunctionSignature "user32.dll" "IntPtr GetDesktopWindow()")

            $win32Functions = Import-NativeFunctions "Win32It0" $win32FunctionSigs
            $win32Functions | Should Not BeNullOrEmpty

            $win32Functions::GetDesktopWindow | Should Not BeNullOrEmpty
        }

        It "Should be able to register multiple native functions." {
            $functionSigs = @(
                New-NativeFunctionSignature "user32.dll" "IntPtr GetDesktopWindow()"
                New-NativeFunctionSignature "user32.dll" "bool ShowWindow(IntPtr hWnd, int nCmdShow)"
            )

            $win32Functions = Import-NativeFunctions "Win32It2" $functionSigs
            $win32Functions | Should Not BeNullOrEmpty

            $win32Functions::GetDesktopWindow | Should Not BeNullOrEmpty
            $win32Functions::ShowWindow | Should Not BeNullOrEmpty
        }

        It "Should be able to register multiple native functions with multiple class names." {
            $win32FunctionSigs = @(
                New-NativeFunctionSignature "user32.dll" "IntPtr GetDesktopWindow()"
                New-NativeFunctionSignature "user32.dll" "bool ShowWindow(IntPtr hWnd, int nCmdShow)"
            )
            $win32Functions = Import-NativeFunctions "Win32It3" $win32FunctionSigs
            $win32Functions | Should Not BeNullOrEmpty

            $eventFunctionSigs = @(
                New-NativeFunctionSignature "kernal32.dll" "IntPtr CreateEvent(IntPtr lpEventAttributes, bool bManualReset, bool bInitialState, string lpName)"
            )
            $eventFunctionSigs = Import-NativeFunctions "EventIt3" $eventFunctionSigs
            $eventFunctionSigs | Should Not BeNullOrEmpty

            $win32Functions::GetDesktopWindow | Should Not BeNullOrEmpty
            $win32Functions::ShowWindow | Should Not BeNullOrEmpty
            $eventFunctionSigs::CreateEvent | Should Not BeNullOrEmpty
        }

        It "Should be able to call the native function." {
            $win32FunctionSigs = @(New-NativeFunctionSignature "user32.dll" "IntPtr GetDesktopWindow()")

            $win32Functions = Import-NativeFunctions "Win32It4" $win32FunctionSigs
            $desktopWindow = $win32Functions::GetDesktopWindow()
            $desktopWindow | Should Not Be 0
        }
    }
}