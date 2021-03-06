#
# PSPlus.Modules.Core.Pipeline.psm1
#

# This function cannot be a cmdlet. Because it is usually not being called in the pipeline, the pipeline it stops actually contains nothing.
function Stop-UpstreamCommands($cmdlet)
{
    [PSPlus.Core.Powershell.PipelineControl]::StopUpstreamCommands($cmdlet)
}