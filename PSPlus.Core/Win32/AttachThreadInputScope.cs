using PSPlus.Core.Win32.Interop;
using System;

namespace PSPlus.Core.Win32
{
    public class AttachThreadInputScope : IDisposable
    {
        private bool _attached;
        private uint _attachThreadId;
        private uint _attachToThreadId;

        public AttachThreadInputScope(uint threadId)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            _attachThreadId = (uint)AppDomain.GetCurrentThreadId();
#pragma warning restore CS0618 // Type or member is obsolete

            _attachToThreadId = threadId;
            _attached = Win32APIs.AttachThreadInput(_attachThreadId, _attachToThreadId, true);
        }

        public void Dispose()
        {
            if (_attached)
            {
                Win32APIs.AttachThreadInput(_attachThreadId, _attachToThreadId, false);
            }
        }
    }
}
