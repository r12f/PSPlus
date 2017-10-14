using PSPlus.Core.Windows.Interop.User32;
using System;

namespace PSPlus.Core.Windows.Window
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
            _attached = User32APIs.AttachThreadInput(_attachThreadId, _attachToThreadId, true);
        }

        public void Dispose()
        {
            if (_attached)
            {
                User32APIs.AttachThreadInput(_attachThreadId, _attachToThreadId, false);
            }
        }
    }
}
