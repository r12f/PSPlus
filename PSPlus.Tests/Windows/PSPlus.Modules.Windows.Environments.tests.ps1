Import-Module "$PSScriptRoot\..\..\Output\Publish\PSPlus.psd1" -Force

Describe "Windows.Environments" {
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