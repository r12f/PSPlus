using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;

namespace PSPlus.Core.Cmdlets
{
    public class CmdletBase : Cmdlet
    {
        protected override void BeginProcessing()
        {
            RunWithEH(BegingProcessingInEH);
        }

        protected override void ProcessRecord()
        {
            RunWithEH(ProcessRecordInEH);
        }

        protected override void EndProcessing()
        {
            RunWithEH(EndProcessingInEH);
        }

        protected override void StopProcessing()
        {
            RunWithEH(StopProcessingInEH);
        }

        protected virtual void BegingProcessingInEH() { }
        protected virtual void ProcessRecordInEH() { }
        protected virtual void EndProcessingInEH() { }
        protected virtual void StopProcessingInEH() { }

        protected void RunWithEH(Action action)
        {
            try
            {
                action();
            }
            catch (ArgumentException e)
            {
                ThrowTerminatingError(new ErrorRecord(e, ErrorType.InvalidArgument, ErrorCategory.InvalidArgument, this));
            }
            catch (Exception e)
            {
                ThrowTerminatingError(new ErrorRecord(e, ErrorType.InvalidOperation, ErrorCategory.InvalidOperation, this));
            }
        }
    }
}
