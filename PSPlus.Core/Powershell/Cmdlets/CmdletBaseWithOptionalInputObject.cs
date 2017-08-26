using System.Management.Automation;

namespace PSPlus.Core.Powershell.Cmdlets
{
    public class CmdletBaseWithOptionalInputObject<T> : CmdletBase
    {
        [Parameter(Position = 0, ValueFromPipeline = true, Mandatory = false)]
        public T InputObject { get; set; }
    }
}
