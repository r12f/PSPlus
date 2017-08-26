using System;
using System.Management.Automation;

namespace PSPlus.Core.Powershell.Cmdlets
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
                if (e.GetType().FullName.StartsWith("System.Management.Automation"))
                {
                    // If the exception is a powershell control exception, rethrow it.
                    throw;
                }
                else
                {
                    ThrowTerminatingError(new ErrorRecord(e, ErrorType.InvalidOperation, ErrorCategory.InvalidOperation, this));
                }
            }
        }
    }
}
