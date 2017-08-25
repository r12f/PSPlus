Import-Module "$PSScriptRoot\..\Output\Publish\PSPlus.psd1" -Force

Describe "Runtime.Reflection" {
    Context "When trying to get the object type" {
        It "Should be able to get the first object type." {
            $type = 1..5 | Get-Type
            $type | Should Be (1).GetType()
        }

        It "Should be able to get all object types." {
            $types = 1..5 | Get-Type -All
            $types.Count | Should Be 5
            $types[0] | Should Be (1).GetType()
        }
    }
}