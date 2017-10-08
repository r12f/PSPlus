Import-Module "$PSScriptRoot\..\..\Publish\PSPlus\PSPlus.psd1" -Force

Describe "Core.IO" {
    Context "When trying to get or change file extension" {
        $testPath = Resolve-Path "$PSScriptRoot\..\..\Publish\PSPlus\PSPlus.psd1"

        It "Should be able to get the file extension." {
            Get-FileExtension $testPath | Should Be ".psd1"
        }

        It "Should be able to change the file extension." {
            $newFilePath = Rename-FileExtension $testPath ".test"
            Get-FileExtension $newFilePath | Should Be ".test"
        }
    }
}