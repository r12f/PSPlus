using System;
using System.Management.Automation;

namespace PSPlus.Core.Runtime
{
    public static class PipelineControl
    {
        public static void StopUpstreamCommands(Cmdlet cmdlet)
        {
            Type exceptionType = typeof(Cmdlet).Assembly.GetType("System.Management.Automation.StopUpstreamCommandsException");
            throw (System.Exception) System.Activator.CreateInstance(exceptionType, cmdlet);
        }
    }
}
