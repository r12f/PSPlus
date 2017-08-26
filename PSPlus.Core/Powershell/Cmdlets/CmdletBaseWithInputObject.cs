using System.Management.Automation;

namespace PSPlus.Core.Powershell.Cmdlets
{
    public class CmdletBaseWithInputObject<T> : CmdletBase
    {
        [Parameter(Position = 0, ValueFromPipeline = true, Mandatory = true)]
        public T InputObject { get; set; }
    }
}
