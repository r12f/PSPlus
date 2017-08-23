#
# Pipeline.psm1
#

$pipelineControl = $null

function Stop-UpstreamCommands($cmdlet)
{
    [PSPlus.Core.Runtime.PipelineControl]::Stop($cmdlet)
}