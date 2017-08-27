Import-Module "$PSScriptRoot\..\..\Output\Publish\PSPlus.psd1" -Force

Describe "Core.Collection" {
    Context "When using list" {
        It "Should be able to use list" {
            $collection = New-GenericList "int"
            $collection.Count | Should Be 0

            $collection.Add(1)
            $collection.Count | Should Be 1

            $collection.Clear()
            $collection.Count | Should Be 0
        }
    }

    Context "When using queue" {
        It "Should be able to use queue" {
            $collection = New-GenericQueue "int"
            $collection.Count | Should Be 0

            $collection.Enqueue(1)
            $collection.Count | Should Be 1

            $collection.Dequeue() | Should Be 1
            $collection.Count | Should Be 0
        }
    }

    Context "When using stack" {
        It "Should be able to use stack" {
            $collection = New-GenericStack "int"
            $collection.Count | Should Be 0

            $collection.Push(1)
            $collection.Count | Should Be 1

            $collection.Pop() | Should Be 1
            $collection.Count | Should Be 0
        }
    }

    Context "When using set" {
        It "Should be able to use stack" {
            $collection = New-GenericSet "int"
            $collection.Count | Should Be 0
            $collection.Contains(1) | Should Be $false

            $collection.Add(1)
            $collection.Count | Should Be 1
            $collection.Contains(1) | Should Be $true

            $collection.Remove(1) | Should Be 1
            $collection.Count | Should Be 0
            $collection.Contains(1) | Should Be $false
        }

        It "Should be able to convert objects from pipeline to a set with automated type detection" {
            $processIds = Get-Process | % { $_.Id } | ConvertTo-Set
            $processIds.Count | Should Not Be 0
        }

        It "Should be able to convert objects from pipeline to an set with type set to System.Object" {
            $processIds = Get-Process | % { $_.Id } | ConvertTo-Set -GenericValue
            $processIds.Count | Should Not Be 0
        }
    }

    Context "When using dictionary" {
        It "Should be able to use dictionary" {
            $collection = New-GenericDictionary "int" "string"
            $collection.Count | Should Be 0
            $collection.ContainsKey(1) | Should Be $false

            $collection.Add(1, "test")
            $collection.Count | Should Be 1
            $collection.ContainsKey(1) | Should Be $true
            $collection[1] | Should Be "test"

            $collection.Remove(1)
            $collection.Count | Should Be 0
            $collection.ContainsKey(1) | Should Be $false
        }

        It "Should be able to convert an object list to an dictionary with automated type detection" {
            $processes = @(Get-Process)
            $processes.Count | Should Not Be 0

            $collection = $processes | ConvertTo-Dictionary { $_.Id } { $_ }
            $collection.Count | Should Not Be 0
        }

        It "Should be able to convert an object list to an dictionary with type set to System.Object" {
            $processes = @(Get-Process)
            $processes.Count | Should Not Be 0

            $collection = $processes | ConvertTo-Dictionary { $_.Id } { $_ } -GenericValue
            $collection.Count | Should Not Be 0
        }
    }
}
