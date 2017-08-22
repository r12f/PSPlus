Import-Module "$PSScriptRoot\..\Output\Publish\PSPlus.psd1" -Force

Describe "Functional" {
    Context "When test any" {
        It "Should return true if any object matches the predicate" {
            1..10 | Test-Any { $_ -gt 5 } | Should Be $true
        }

        It "Should return false if all objects do not match the predicate" {
            1..10 | Test-Any { $_ -gt 10 } | Should Be $false
        }

        It "Should only check the numbers from the direct pipe" {
            1..10 | %{ 11..20 | Test-Any { $_ -gt 15 } } | Should Be $true
            1..10 | %{ 11..20 | Test-Any { $_ -lt 5 } } | Should Be $false
        }
    }

    Context "When test all" {
        It "Should return true if all objects match the predicate" {
            1..10 | Test-All { $_ -le 10 } | Should Be $true
        }

        It "Should return false if any object does not match the predicate" {
            1..10 | Test-All { $_ -le 5 } | Should Be $false
        }

        It "Should only check the numbers from the direct pipe" {
            1..10 | %{ 11..20 | Test-All { $_ -gt 10 } } | Should Be $true
            1..10 | %{ 11..20 | Test-All { $_ -le 10 } } | Should Be $false
        }
    }
}
