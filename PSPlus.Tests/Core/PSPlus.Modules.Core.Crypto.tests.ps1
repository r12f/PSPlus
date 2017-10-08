Import-Module "$PSScriptRoot\..\..\Publish\PSPlus\PSPlus.psd1" -Force

Describe "Core.Crypto" {
    Context "When trying to use base64 encoding" {
        $decodedString = "test string"
        $encodedStringUtf8 = "dGVzdCBzdHJpbmc="
        $encodedStringUnicode = "dABlAHMAdAAgAHMAdAByAGkAbgBnAA=="

        It "Should be able to encode string with base64 encoding." {
            $resultUtf8 = ConvertTo-Base64 $decodedString
            $resultUtf8 | Should Be $encodedStringUtf8

            $resultUnicode = ConvertTo-Base64 $decodedString "unicode"
            $resultUnicode | Should Be $encodedStringUnicode
        }

        It "Should be able to decode string with base64 encoding." {
            $resultUtf8 = ConvertFrom-Base64 $encodedStringUtf8
            $resultUtf8 | Should Be $decodedString

            $resultUnicode = ConvertFrom-Base64 $encodedStringUnicode "unicode"
            $resultUnicode | Should Be $decodedString
        }
    }

    Context "When trying to hash a string" {
        $testString = "test string"

        It "Should be able to get MD5 hash." {
            $hashResult = Get-StringHash $testString "MD5"
            $hashResult.Hash | Should Be "6f8db599de986fab7a21625b7916589c"
        }

        It "Should be able to get SHA1 hash." {
            $hashResult = Get-StringHash $testString "SHA1"
            $hashResult.Hash | Should Be "661295c9cbf9d6b2f6428414504a8deed3020641"
        }

        It "Should be able to get SHA256 hash." {
            $hashResult = Get-StringHash $testString "SHA256"
            $hashResult.Hash | Should Be "d5579c46dfcc7f18207013e65b44e4cb4e2c2298f4ac457ba8f82743f31e930b"
        }

        It "Should be able to get SHA384 hash." {
            $hashResult = Get-StringHash $testString "SHA384"
            $hashResult.Hash | Should Be "e213dccb3221e0b8fdd995dcc1d04e218fc649981038bfac81abc98932369bac0efb758b92eccd80321df8eb64efae87"
        }

        It "Should be able to get SHA512 hash." {
            $hashResult = Get-StringHash $testString "SHA512"
            $hashResult.Hash | Should Be "10e6d647af44624442f388c2c14a787ff8b17e6165b83d767ec047768d8cbcb71a1a3226e7cc7816bc79c0427d94a9da688c41a3992c7bf5e4d7cc3e0be5dbac"
        }

        It "Should be able to get RIPEMD160 hash." {
            $hashResult = Get-StringHash $testString "RIPEMD160"
            $hashResult.Hash | Should Be "055e60d209a7480cb329896fbbf9e91c43c582c1"
        }
    }
}