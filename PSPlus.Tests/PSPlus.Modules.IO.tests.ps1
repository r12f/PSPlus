Import-Module "$PSScriptRoot\..\Output\Publish\PSPlus.psd1" -Force

Describe "IO" {
    Context "When trying to get or change file extension" {
        $testPath = Resolve-Path "$PSScriptRoot\..\Output\Publish\PSPlus.psd1"

        It "Should be able to get the file extension." {
            Get-FileExtension $testPath | Should Be ".psd1"
        }

        It "Should be able to change the file extension." {
            $newFilePath = Rename-FileExtension $testPath ".test"
            Get-FileExtension $newFilePath | Should Be ".test"
        }
    }
}