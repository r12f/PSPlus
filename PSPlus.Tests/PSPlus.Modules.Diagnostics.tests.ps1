Import-Module "$PSScriptRoot\..\Output\Publish\PSPlus.psd1" -Force

Describe "Diagnostics" {
    Context "When trying to create event source definition" {
        It "Should be able to create event source definition with positioned parameters" {
            $eventSource = New-EventSourceDefinition "TestProvider" 0x0000000000100000 -Ids 123
            $eventSource | Should Not Be $null

            $eventSource.ProviderName | Should Be "TestProvider"
            $eventSource.Keywords | Should Be 0x0000000000100000
            
            $eventSource.Ids.Count | Should Be 1
            $eventSource.Ids[0] | Should Be 123
            
            $eventSource.ProcessIds | Should Be $null
            $eventSource.ProcessNames | Should Be $null
        }

        It "Should be able to create event source definition with named parameters" {
            $eventSource = New-EventSourceDefinition -ProviderName "TestProvider" -Keywords 0x0000000000100000 -Ids 1,2,3 -ProcessIds 4,5,6 -ProcessNames "notepad", "devenv"
            $eventSource | Should Not Be $null

            $eventSource.ProviderName | Should Be "TestProvider"
            $eventSource.Keywords | Should Be 0x0000000000100000

            $eventSource.Ids.Count | Should Be 3
            $eventSource.Ids[0] | Should Be 1
            $eventSource.Ids[1] | Should Be 2
            $eventSource.Ids[2] | Should Be 3

            $eventSource.ProcessIds.Count | Should Be 3
            $eventSource.ProcessIds[0] | Should Be 4
            $eventSource.ProcessIds[1] | Should Be 5
            $eventSource.ProcessIds[2] | Should Be 6

            $eventSource.ProcessNames.Count | Should Be 2
            $eventSource.ProcessNames[0] | Should Be "notepad"
            $eventSource.ProcessNames[1] | Should Be "devenv"
        }
    }

    Context "When trying to create watcher options" {
        It "Should be able to create watcher options with positioned parameters" {
            $watcherOptions = New-EventWatcherOptions "TestSession" "TestProvider" 0x0000000000100000 -Ids 123 { Write-Host 123 }
            $watcherOptions | Should Not Be $null

            $watcherOptions.SessionName | Should Be "TestSession"
            $watcherOptions.EventSources | Should Not Be $null
            $watcherOptions.EventSources.Count | Should Be 1
            $watcherOptions.OnEvent | Should Not Be $null

            $eventSource = $watcherOptions.EventSources[0]
            $eventSource.ProviderName | Should Be "TestProvider"
            $eventSource.Keywords | Should Be 0x0000000000100000
            
            $eventSource.Ids.Count | Should Be 1
            $eventSource.Ids[0] | Should Be 123
            
            $eventSource.ProcessIds | Should Be $null
            $eventSource.ProcessNames | Should Be $null
        }

        It "Should be able to create watcher options with named parameters" {
            $watcherOptions = New-EventWatcherOptions -SessionName "TestSession" -ProviderName "TestProvider" -Keywords 0x0000000000100000 -Ids 1,2,3 -ProcessIds 4,5,6 -ProcessNames "notepad", "devenv" { Write-Host 123 }
            $watcherOptions | Should Not Be $null

            $watcherOptions.SessionName | Should Be "TestSession"
            $watcherOptions.EventSources | Should Not Be $null
            $watcherOptions.EventSources.Count | Should Be 1
            $watcherOptions.OnEvent | Should Not Be $null

            $eventSource = $watcherOptions.EventSources[0]
            $eventSource.ProviderName | Should Be "TestProvider"
            $eventSource.Keywords | Should Be 0x0000000000100000

            $eventSource.Ids.Count | Should Be 3
            $eventSource.Ids[0] | Should Be 1
            $eventSource.Ids[1] | Should Be 2
            $eventSource.Ids[2] | Should Be 3

            $eventSource.ProcessIds.Count | Should Be 3
            $eventSource.ProcessIds[0] | Should Be 4
            $eventSource.ProcessIds[1] | Should Be 5
            $eventSource.ProcessIds[2] | Should Be 6

            $eventSource.ProcessNames.Count | Should Be 2
            $eventSource.ProcessNames[0] | Should Be "notepad"
            $eventSource.ProcessNames[1] | Should Be "devenv"
        }

        It "Should be able to create watcher options with parameter and event source" {
            $extraEventSource = New-EventSourceDefinition "TestProvider2" 0x0000000100000000 -Ids 456
            $watcherOptions = New-EventWatcherOptions "TestSession" "TestProvider1" 0x0000000000100000 -Ids 123 -EventSource $extraEventSource { Write-Host 123 }
            $watcherOptions | Should Not Be $null

            $watcherOptions.SessionName | Should Be "TestSession"
            $watcherOptions.EventSources | Should Not Be $null
            $watcherOptions.EventSources.Count | Should Be 2
            $watcherOptions.OnEvent | Should Not Be $null

            $eventSource = $watcherOptions.EventSources[0]
            $eventSource.ProviderName | Should Be "TestProvider1"
            $eventSource.Keywords | Should Be 0x0000000000100000
            
            $eventSource.Ids.Count | Should Be 1
            $eventSource.Ids[0] | Should Be 123
            
            $eventSource.ProcessIds | Should Be $null
            $eventSource.ProcessNames | Should Be $null

            $eventSource = $watcherOptions.EventSources[1]
            $eventSource.ProviderName | Should Be "TestProvider2"
            $eventSource.Keywords | Should Be 0x0000000100000000
            
            $eventSource.Ids.Count | Should Be 1
            $eventSource.Ids[0] | Should Be 456
            
            $eventSource.ProcessIds | Should Be $null
            $eventSource.ProcessNames | Should Be $null
        }
    }

    Context "When trying to watch events" {
        It "Should be able to watch events" {
            Get-EtwTraceSession -Name "TestSession" | Stop-EtwTraceSession

            $onEvent = {
                param($e, $watcherManager, $watchOptions)
                $e | Should Not Be $null
                $watcherManager | Should Not Be $null
                $watchOptions | Should Not Be $null

                $watcherManager.RequestStop()
            }

            $watcherOptions = New-EventWatcherOptions "TestSession" "Microsoft-Windows-Win32k" 0x0000000000100000 $onEvent
            Watch-ETWEvents -WatcherOptions $watcherOptions
        }
    }
}