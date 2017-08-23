using System.Management.Automation;

namespace PSPlus.Core
{
    public class CmdletBaseWithInputObject<T> : Cmdlet
    {
        [Parameter(Position = 0, ValueFromPipeline = true, Mandatory = true)]
        public T InputObject;
    }
}
