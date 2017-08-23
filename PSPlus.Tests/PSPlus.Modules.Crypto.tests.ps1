Import-Module "$PSScriptRoot\..\Output\Publish\PSPlus.psd1" -Force

Describe "Crypto" {
    Context "When trying to use base64 encoding" {
        $decodedString = "test string"
        $encodedString = "dGVzdCBzdHJpbmc="

        It "Should be able to encode string with base64 encoding." {
            ConvertTo-Base64 $decodedString "utf-8" | Should Be $encodedString
        }

        It "Should be able to decode string with base64 encoding." {
            ConvertFrom-Base64 $encodedString "utf-8" | Should Be $decodedString
        }
    }
}