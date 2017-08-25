#
# PSPlus.Modules.Runtime.Pipeline.psm1
#

# This function cannot be a cmdlet. Because it is usually not being called in the pipeline, the pipeline it stops actually contains nothing.
function Stop-UpstreamCommands($cmdlet)
{
    [PSPlus.Core.Runtime.PipelineControl]::StopUpstreamCommands($cmdlet)
}