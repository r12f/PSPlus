Import-Module "$PSScriptRoot\..\..\Output\Publish\PSPlus.psd1" -Force

Describe "Windows.Environments" {
    Context "When trying to check the architecture" {
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
    }

    Context "When trying to change environment variables" {
        It "Should be able to set environment variable in current process." {
            Set-EnvironmentVariable "TestKey" "TestValue"
            $env:TestKey | Should Be "TestValue"

            Remove-EnvironmentVariable "TestKey"
            $env:TestKey | Should Be $null
        }

        It "Should be able to set environment variable for current user." {
            # Set-EnvironmentVariable "TestKey" "TestValue" -Target "User"
            # $env:TestKey | Should Be "TestValue"

            # Remove-EnvironmentVariable "TestKey" -Target "User"
            # $env:TestKey | Should Be $null
        }

        It "Should be able to set environment variable for machine." {
            # Set-EnvironmentVariable "TestKey" "TestValue" -Target "Machine"
            # $env:TestKey | Should Be "TestValue"

            # Remove-EnvironmentVariable "TestKey" -Target "Machine"
            # $env:TestKey | Should Be $null
        }
    }
}