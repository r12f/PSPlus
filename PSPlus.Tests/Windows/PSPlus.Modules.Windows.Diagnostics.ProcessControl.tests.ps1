Import-Module "$PSScriptRoot\..\..\Output\Publish\PSPlus.psd1" -Force

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

        It "Should be able to get the process command line" {
            $testProgram = "$env:WINDIR\system32\cmd.exe"
            $testArguments = "/?"
            $testProcess = Start-Process $testProgram -ArgumentList $testArguments -PassThru

            Get-ProcessCommandLine $testProcess | Should Be """C:\WINDOWS\system32\cmd.exe"" /? "

            $testProcess.Kill()
            $testProcess.Close()
        }
    }
}