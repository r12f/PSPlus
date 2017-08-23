using System.Management.Automation;

namespace PSPlus.Core.Runtime
{
    public class PipelineControl
    {
        public static void Stop(Cmdlet cmdlet)
        {
            throw (System.Exception) System.Activator.CreateInstance(typeof(Cmdlet).Assembly.GetType("System.Management.Automation.StopUpstreamCommandsException"), cmdlet);
        }
    }
}
