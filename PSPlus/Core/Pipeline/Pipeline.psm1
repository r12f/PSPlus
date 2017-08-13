#
# Pipeline.psm1
#

$pipelineControl = $null

function Stop-UpstreamCommands($cmdlet)
{
    if ($pipelineControl -eq $null)
    {
        $pipelineControl = Add-Type -Passthru -TypeDefinition "
            using System.Management.Automation;
            namespace PSPlus {
                public class PipelineControl {
                    public static void Stop(Cmdlet cmdlet) {
                        throw (System.Exception) System.Activator.CreateInstance(typeof(Cmdlet).Assembly.GetType(`"System.Management.Automation.StopUpstreamCommandsException`"), cmdlet);
                    }
                }
            }"
    }

    $pipelineControl::Stop($cmdlet)
}