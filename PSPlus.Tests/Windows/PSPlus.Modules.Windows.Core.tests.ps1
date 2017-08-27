Import-Module "$PSScriptRoot\..\..\Output\Publish\PSPlus.psd1" -Force

Describe "Windows.Core" {
    Context "When trying to check the architecture" {
        $notepadNativePath = "$env:WINDIR\sysnative\notepad.exe"
        $notepadWowPath = "$env:WINDIR\SysWOW64\notepad.exe"

        It "Should be able to check if system is 64 bits." {
            $is32BitsOS = Test-Is32BitsOS
            $is64BitsOS = Test-Is64BitsOS
            $is32BitsOS -xor $is64BitsOS | Should Be $true

            if ($is32BitsOS) {
                Test-Path $notepadWowPath | Should Be $false
            } else {
                Test-Path $notepadWowPath | Should Be $true
            }
        }

        It "Should be able to check if this powershell process is 64 bits." {
            $is32BitPowershell = Test-Is32BitsPowershell
            $is64BitPowershell = Test-Is64BitsPowershell
            $is32BitPowershell -xor $is64BitPowershell | Should Be $true
        }

        It "Should be able to check if other process is 64 bits." {
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
    }
}