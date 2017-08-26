Import-Module "$PSScriptRoot\..\Output\Publish\PSPlus.psd1" -Force

Describe "Runtime.Text" {
    Context "When trying to convert object to hex string" {
        It "Should be able to convert byte to hex string." {
            $v = [byte](0x1)
            $hexString = ConvertTo-HexString $v
            $hexString | Should Be "01"
        }

        It "Should be able to convert byte array to hex string." {
            $v = [byte[]]::new(2)
            $v[0] = 1
            $v[1] = 2

            $hexString = ConvertTo-HexString $v
            $hexString | Should Be "0102"
        }

        It "Should be able to convert int16 to hex string." {
            $v = [Int16](0x1)
            $hexString = ConvertTo-HexString $v
            $hexString | Should Be "0100"
        }

        It "Should be able to convert int16 array to hex string." {
            $v = [Int16[]]::new(2)
            $v[0] = 1
            $v[1] = 2

            $hexString = ConvertTo-HexString $v
            $hexString | Should Be "01000200"
        }

        It "Should be able to convert uint16 to hex string." {
            $v = [UInt16](0x1)
            $hexString = ConvertTo-HexString $v
            $hexString | Should Be "0100"
        }

        It "Should be able to convert uint16 array to hex string." {
            $v = [UInt16[]]::new(2)
            $v[0] = 1
            $v[1] = 2

            $hexString = ConvertTo-HexString $v
            $hexString | Should Be "01000200"
        }

        It "Should be able to convert int32 to hex string." {
            $v = [Int32](0x1)
            $hexString = ConvertTo-HexString $v
            $hexString | Should Be "01000000"
        }

        It "Should be able to convert int32 array to hex string." {
            $v = [Int32[]]::new(2)
            $v[0] = 1
            $v[1] = 2

            $hexString = ConvertTo-HexString $v
            $hexString | Should Be "0100000002000000"
        }

        It "Should be able to convert uint32 to hex string." {
            $v = [UInt32](0x1)
            $hexString = ConvertTo-HexString $v
            $hexString | Should Be "01000000"
        }

        It "Should be able to convert uint32 array to hex string." {
            $v = [UInt32[]]::new(2)
            $v[0] = 1
            $v[1] = 2

            $hexString = ConvertTo-HexString $v
            $hexString | Should Be "0100000002000000"
        }

        It "Should be able to convert int64 to hex string." {
            $v = [Int64](0x1)
            $hexString = ConvertTo-HexString $v
            $hexString | Should Be "0100000000000000"
        }

        It "Should be able to convert int64 array to hex string." {
            $v = [Int64[]]::new(2)
            $v[0] = 1
            $v[1] = 2

            $hexString = ConvertTo-HexString $v
            $hexString | Should Be "01000000000000000200000000000000"
        }

        It "Should be able to convert uint64 to hex string." {
            $v = [UInt64](0x1)
            $hexString = ConvertTo-HexString $v
            $hexString | Should Be "0100000000000000"
        }

        It "Should be able to convert uint64 array to hex string." {
            $v = [UInt64[]]::new(2)
            $v[0] = 1
            $v[1] = 2

            $hexString = ConvertTo-HexString $v
            $hexString | Should Be "01000000000000000200000000000000"
        }

        It "Should be able to convert uint64 to hex string." {
            $v = "test"
            $hexString = ConvertTo-HexString $v
            $hexString | Should Be "74657374"
        }
    }
}