Import-Module "$PSScriptRoot\..\..\Publish\PSPlus\PSPlus.psd1" -Force

Describe "Windows.Diagnostics.ProcessControl" {
    Context "When trying to do process related operations" {
        It "Should be able to check if other process is 64 bits." {
            $notepadNativePath = "$env:WINDIR\system32\notepad.exe"
            if ((Test-Is64BitsOS) -and (Test-Is32BitsPowershell)) {
                $notepadNativePath = "$env:WINDIR\sysnative\notepad.exe"
            }

            $notepad = Start-Process $notepadNativePath -PassThru

            $is32BitsProcess = Test-Is32BitsProcess $notepad
            $is64BitsProcess = Test-Is64BitsProcess $notepad
            $is32BitsProcess -xor $is64BitsProcess | Should Be $true

            if (Test-Is32BitsOS) {
                $is64BitsProcess | Should Be $false
            } else {
                $is64BitsProcess | Should Be $true
            }

            $notepad.Kill()
        }

        It "Should be able to get the process command line in the same arch" {
            $testProgram = "$env:WINDIR\system32\cmd.exe"
            $testArguments = "/?"
            $testProcess = Start-Process $testProgram -ArgumentList $testArguments -PassThru

            Get-ProcessCommandLine $testProcess | Should Be """C:\WINDOWS\system32\cmd.exe"" /? "

            $testProcess.Kill()
            $testProcess.Close()
        }

        It "Should be able to get the process command line of wow64 process." {
            if (-not (Test-Is64BitsOS)) {
                return
            }

            $testProgram = "$env:WINDIR\SysWOW64\cmd.exe"
            $testArguments = "/?"
            $testProcess = Start-Process $testProgram -ArgumentList $testArguments -PassThru

            Get-ProcessCommandLine $testProcess | Should Be """C:\WINDOWS\SysWOW64\cmd.exe"" /? "

            $testProcess.Kill()
            $testProcess.Close()
        }

        It "Should be unable to get the process command line of amd64 process from x86 process." {
            if (-not (Test-Is64BitsOS)) {
                return
            }

            if (-not (Test-Is32BitsPowershell)) {
                return
            }

            $testProgram = "$env:WINDIR\sysnative\cmd.exe"
            $testArguments = "/?"
            $testProcess = Start-Process $testProgram -ArgumentList $testArguments -PassThru

            $exceptionThrown = $false
            try
            {
                Get-ProcessCommandLine $testProcess
            }
            catch
            {
                $exceptionThrown = $true
            }
            $exceptionThrown | Should Be $true

            $testProcess.Kill()
            $testProcess.Close()
        }
    }
}