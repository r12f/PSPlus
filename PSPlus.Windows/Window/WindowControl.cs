using PSPlus.Core.Windows.Interop.User32;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSPlus.Core.Windows.Window
{
    public class WindowControl
    {
        public IntPtr Hwnd { get; set; }

        public static WindowControl GetForegroundWindow()
        {
            IntPtr hwnd = User32APIs.GetForegroundWindow();
            return new WindowControl(hwnd);
        }

        public static WindowControl GetDesktopWindow()
        {
            IntPtr hwnd = User32APIs.GetDesktopWindow();
            return new WindowControl(hwnd);
        }

        public static WindowControl WindowFromPoint(User32Point point)
        {
            IntPtr hwnd = User32APIs.WindowFromPoint(point);
            return new WindowControl(hwnd);
        }

        public WindowControl(IntPtr hwnd)
        {
            Hwnd = hwnd;
        }

        public bool IsWindow()
        {
            return Hwnd != null && User32APIs.IsWindow(Hwnd);
        }

        public bool DestroyWindow()
        {
            return User32APIs.DestroyWindow(Hwnd);
        }

        public uint GetWindowThreadID()
        {
            unsafe
            {
                return User32APIs.GetWindowThreadProcessId(Hwnd, null);
            }
        }

        public uint GetWindowProcessID()
        {
            uint processId = 0;

            unsafe
            {
                User32APIs.GetWindowThreadProcessId(Hwnd, &processId);
            }

            return processId;
        }

        public int GetWindowLong(int nIndex)
        {
            return User32APIs.GetWindowLongW(Hwnd, nIndex);
        }

        public IntPtr GetWindowLongPtr(int nIndex)
        {
            if (!Environment.Is64BitProcess)
            {
                return (IntPtr)GetWindowLong(nIndex);
            }

            return User32APIs.GetWindowLongPtrW(Hwnd, nIndex);
        }

        public int SetWindowLong(int nIndex, int dwNewLong)
        {
            return User32APIs.SetWindowLongW(Hwnd, nIndex, dwNewLong);
        }

        public IntPtr SetWindowLongPtr(int nIndex, IntPtr dwNewLong)
        {
            if (!Environment.Is64BitProcess)
            {
                return (IntPtr)SetWindowLong(nIndex, (int)dwNewLong);
            }

            return User32APIs.SetWindowLongPtrW(Hwnd, nIndex, dwNewLong);
        }

        public bool IsWindowEnabled()
        {
            return User32APIs.IsWindowEnabled(Hwnd);
        }

        public bool EnableWindow(bool bEnable)
        {
            return User32APIs.EnableWindow(Hwnd, bEnable);
        }

        public WindowControl GetActiveWindow()
        {
            using (AttachThreadInputScope scope = new AttachThreadInputScope(GetWindowThreadID()))
            {
                return new WindowControl(User32APIs.GetActiveWindow());
            }
        }

        public IntPtr SetActiveWindow()
        {
            using (AttachThreadInputScope scope = new AttachThreadInputScope(GetWindowThreadID()))
            {
                return User32APIs.SetActiveWindow(Hwnd);
            }
        }

        public WindowControl GetCapture()
        {
            using (AttachThreadInputScope scope = new AttachThreadInputScope(GetWindowThreadID()))
            {
                return new WindowControl(User32APIs.GetCapture());
            }
        }

        public IntPtr SetCapture()
        {
            using (AttachThreadInputScope scope = new AttachThreadInputScope(GetWindowThreadID()))
            {
                return User32APIs.SetCapture(Hwnd);
            }
        }

        public bool ReleaseCapture()
        {
            using (AttachThreadInputScope scope = new AttachThreadInputScope(GetWindowThreadID()))
            {
                return User32APIs.ReleaseCapture();
            }
        }

        public WindowControl GetFocus()
        {
            using (AttachThreadInputScope scope = new AttachThreadInputScope(GetWindowThreadID()))
            {
                return new WindowControl(User32APIs.GetFocus());
            }
        }

        public IntPtr SetFocus()
        {
            using (AttachThreadInputScope scope = new AttachThreadInputScope(GetWindowThreadID()))
            {
                return User32APIs.SetFocus(Hwnd);
            }
        }

        public WindowControl ChildWindowFromPoint(User32Point point)
        {
            return new WindowControl(User32APIs.ChildWindowFromPoint(Hwnd, point));
        }

        public WindowControl ChildWindowFromPointEx(User32Point point, uint uFlags)
        {
            return new WindowControl(User32APIs.ChildWindowFromPointEx(Hwnd, point, uFlags));
        }

        public WindowControl GetTopWindow()
        {
            return new WindowControl(User32APIs.GetTopWindow(Hwnd));
        }

        public WindowControl GetWindow(uint nCmd)
        {
            return new WindowControl(User32APIs.GetWindow(Hwnd, nCmd));
        }

        public IEnumerable<WindowControl> GetChildren()
        {
            WindowControl childWindow = GetWindow(User32Consts.GW_CHILD);
            while (childWindow.IsWindow())
            {
                yield return childWindow;
                childWindow = childWindow.GetWindow(User32Consts.GW_HWNDNEXT);
            }
        }

        public WindowControl GetLastActivePopup()
        {
            return new WindowControl(User32APIs.GetLastActivePopup(Hwnd));
        }

        public bool IsChild(IntPtr hWnd)
        {
            return User32APIs.IsChild(Hwnd, hWnd);
        }

        public WindowControl GetParent()
        {
            return new WindowControl(User32APIs.GetParent(Hwnd));
        }

        public IntPtr SetParent(IntPtr hWndNewParent)
        {
            return User32APIs.SetParent(Hwnd, hWndNewParent);
        }

        public WindowControl GetAncestor(uint flags)
        {
            return new WindowControl(User32APIs.GetAncestor(Hwnd, flags));
        }

        public IntPtr SendMessageA(uint message, IntPtr wParam, IntPtr lParam)
        {
            return User32APIs.SendMessageA(Hwnd, message, wParam, lParam);
        }

        public IntPtr SendMessageW(uint message, IntPtr wParam, IntPtr lParam)
        {
            return User32APIs.SendMessageW(Hwnd, message, wParam, lParam);
        }

        public IntPtr SendMessageTimeoutA(uint message, IntPtr wParam, IntPtr lParam, uint flags, uint timeout, ref IntPtr result)
        {
            unsafe
            {
                fixed (IntPtr* resultPtr = &result)
                {
                    return User32APIs.SendMessageTimeoutA(Hwnd, message, wParam, lParam, flags, timeout, resultPtr);
                }
            }
        }

        public IntPtr SendMessageTimeoutW(uint message, IntPtr wParam, IntPtr lParam, uint flags, uint timeout, ref IntPtr result)
        {
            unsafe
            {
                fixed (IntPtr* resultPtr = &result)
                {
                    return User32APIs.SendMessageTimeoutW(Hwnd, message, wParam, lParam, flags, timeout, resultPtr);
                }
            }
        }

        public bool PostMessageA(uint message, IntPtr wParam, IntPtr lParam)
        {
            return User32APIs.PostMessageA(Hwnd, message, wParam, lParam);
        }

        public bool PostMessageW(uint message, IntPtr wParam, IntPtr lParam)
        {
            return User32APIs.PostMessageW(Hwnd, message, wParam, lParam);
        }

        public bool SendNotifyMessageA(uint message, IntPtr wParam, IntPtr lParam)
        {
            return User32APIs.SendNotifyMessageA(Hwnd, message, wParam, lParam);
        }

        public bool SendNotifyMessageW(uint message, IntPtr wParam, IntPtr lParam)
        {
            return User32APIs.SendNotifyMessageW(Hwnd, message, wParam, lParam);
        }

        public bool SetWindowText(string text)
        {
            byte[] textBuffer = Encoding.Unicode.GetBytes(text);

            unsafe
            {
                fixed (byte* textBufferPtr = textBuffer)
                {
                    return User32APIs.SetWindowTextW(Hwnd, new IntPtr(textBufferPtr));
                }
            }
        }

        public string GetWindowText()
        {
            int textLength = GetWindowTextLength();

            unsafe
            {
                byte[] textBuffer = new byte[(textLength + 1) * 2];
                fixed (byte* textBufferPtr = textBuffer)
                {
                    User32APIs.GetWindowTextW(Hwnd, new IntPtr(textBufferPtr), textLength + 1);
                }
                return Encoding.Unicode.GetString(textBuffer, 0, textLength * 2);
            }
        }

        public int GetWindowTextLength()
        {
            return User32APIs.GetWindowTextLength(Hwnd);
        }

        public IntPtr GetMenu()
        {
            return User32APIs.GetMenu(Hwnd);
        }

        public bool SetMenu(IntPtr hMenu)
        {
            return User32APIs.SetMenu(Hwnd, hMenu);
        }

        public bool DrawMenuBar()
        {
            return User32APIs.DrawMenuBar(Hwnd);
        }

        public IntPtr GetSystemMenu(bool bRevert)
        {
            return User32APIs.GetSystemMenu(Hwnd, bRevert);
        }

        public bool HiliteMenuItem(IntPtr hMenu, uint uItemHilite, uint uHilite)
        {
            return User32APIs.HiliteMenuItem(Hwnd, hMenu, uItemHilite, uHilite);
        }

        public bool IsIconic()
        {
            return User32APIs.IsIconic(Hwnd);
        }

        public bool IsZoomed()
        {
            return User32APIs.IsZoomed(Hwnd);
        }

        public bool MoveWindow(int x, int y, int nWidth, int nHeight, bool bRepaint)
        {
            return User32APIs.MoveWindow(Hwnd, x, y, nWidth, nHeight, bRepaint);
        }

        public bool MoveWindow(User32Rect rect, bool bRepaint)
        {
            return MoveWindow(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top, bRepaint);
        }

        public bool SetWindowPos(IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint nFlags)
        {
            return User32APIs.SetWindowPos(Hwnd, hWndInsertAfter, x, y, cx, cy, nFlags);
        }

        public bool SetWindowPos(IntPtr hWndInsertAfter, User32Rect rect, uint nFlags)
        {
            return SetWindowPos(hWndInsertAfter, rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top, nFlags);
        }

        public uint ArrangeIconicWindows()
        {
            return User32APIs.ArrangeIconicWindows(Hwnd);
        }

        public bool BringWindowToTop()
        {
            return User32APIs.BringWindowToTop(Hwnd);
        }

        public User32Rect GetWindowRect()
        {
            User32Rect rect;

            unsafe
            {
                User32APIs.GetWindowRect(Hwnd, &rect);
            }

            return rect;
        }

        public User32Rect GetClientRect()
        {
            User32Rect rect;

            unsafe
            {
                User32APIs.GetClientRect(Hwnd, &rect);
            }

            return rect;
        }

        public User32WindowPlacement GetWindowPlacement()
        {
            User32WindowPlacement wndpl;

            unsafe
            {
                User32APIs.GetWindowPlacement(Hwnd, &wndpl);
            }

            return wndpl;
        }

        public bool SetWindowPlacement(User32WindowPlacement wndpl)
        {
            unsafe
            {
                return User32APIs.SetWindowPlacement(Hwnd, &wndpl);
            }
        }

        public User32Point ClientToScreen(User32Point point)
        {
            User32Point screenPoint = point;
            ClientToScreenInPlace(ref screenPoint);
            return screenPoint;
        }

        public bool ClientToScreenInPlace(ref User32Point point)
        {
            unsafe
            {
                fixed (User32Point* lpPoint = &point)
                {
                    return User32APIs.ClientToScreen(Hwnd, lpPoint);
                }
            }
        }

        public User32Rect ClientToScreen(User32Rect rect)
        {
            User32Rect screenRect = rect;
            ClientToScreenInPlace(ref screenRect);
            return screenRect;
        }

        public bool ClientToScreenInPlace(ref User32Rect rect)
        {
            unsafe
            {
                fixed (User32Rect* lpRect = &rect)
                {
                    User32Point* lpPoint = (User32Point*)lpRect;
                    if (!User32APIs.ClientToScreen(Hwnd, lpPoint))
                    {
                        return false;
                    }
                    return User32APIs.ClientToScreen(Hwnd, lpPoint + 1);
                }
            }
        }

        public User32Point ScreenToClient(User32Point rect)
        {
            User32Point clientPoint = rect;
            ScreenToClientInPlace(ref clientPoint);
            return clientPoint;
        }

        public bool ScreenToClientInPlace(ref User32Point point)
        {
            unsafe
            {
                fixed (User32Point* lpPoint = &point)
                {
                    return User32APIs.ScreenToClient(Hwnd, lpPoint);
                }
            }
        }

        public User32Rect ScreenToClient(User32Rect rect)
        {
            User32Rect clientRect = rect;
            ScreenToClientInPlace(ref clientRect);
            return clientRect;
        }

        public bool ScreenToClientInPlace(ref User32Rect rect)
        {
            unsafe
            {
                fixed (User32Rect* lpRect = &rect)
                {
                    User32Point* lpPoint = (User32Point*)lpRect;
                    if (!User32APIs.ScreenToClient(Hwnd, lpPoint))
                    {
                        return false;
                    }
                    return User32APIs.ScreenToClient(Hwnd, lpPoint + 1);
                }
            }
        }

        public int MapWindowPoints(IntPtr hWndTo, User32Point[] point)
        {
            unsafe
            {
                fixed (User32Point* pointPtr = point)
                {
                    return User32APIs.MapWindowPoints(Hwnd, hWndTo, pointPtr, (uint)point.Length);
                }
            }
        }

        public IntPtr GetDC()
        {
            return User32APIs.GetDC(Hwnd);
        }

        public IntPtr GetWindowDC()
        {
            return User32APIs.GetWindowDC(Hwnd);
        }

        public int ReleaseDC(IntPtr hDC)
        {
            return User32APIs.ReleaseDC(Hwnd, hDC);
        }

        public void Print(IntPtr hDC, uint dwFlags)
        {
            User32APIs.Print(Hwnd, hDC, dwFlags);
        }

        public void PrintClient(IntPtr hDC, uint dwFlags)
        {
            User32APIs.PrintClient(Hwnd, hDC, dwFlags);
        }

        public bool UpdateWindow()
        {
            return User32APIs.UpdateWindow(Hwnd);
        }

        public void SetRedraw(bool bRedraw)
        {
            User32APIs.SetRedraw(Hwnd, bRedraw);
        }

        public User32Rect GetUpdateRect(bool bErase)
        {
            User32Rect rect;

            unsafe
            {
                User32APIs.GetUpdateRect(Hwnd, &rect, bErase);
            }

            return rect;
        }

        public int GetUpdateRgn(IntPtr hRgn, bool bErase)
        {
            return User32APIs.GetUpdateRgn(Hwnd, hRgn, bErase);
        }

        public bool Invalidate(bool bErase)
        {
            return User32APIs.Invalidate(Hwnd, bErase);
        }

        public bool InvalidateRect(User32Rect rect, bool bErase)
        {
            unsafe
            {
                return User32APIs.InvalidateRect(Hwnd, &rect, bErase);
            }
        }

        public bool ValidateRect(User32Rect rect)
        {
            unsafe
            {
                return User32APIs.ValidateRect(Hwnd, &rect);
            }
        }

        public void InvalidateRgn(IntPtr hRgn, bool bErase)
        {
            User32APIs.InvalidateRgn(Hwnd, hRgn, bErase);
        }

        public bool ValidateRgn(IntPtr hRgn)
        {
            return User32APIs.ValidateRgn(Hwnd, hRgn);
        }

        public bool ShowWindow(int nCmdShow)
        {
            return User32APIs.ShowWindow(Hwnd, nCmdShow);
        }

        public bool ShowWindowAsync(int nCmdShow)
        {
            return User32APIs.ShowWindowAsync(Hwnd, nCmdShow);
        }

        public bool IsWindowVisible()
        {
            return User32APIs.IsWindowVisible(Hwnd);
        }

        public bool ShowOwnedPopups(bool bShow)
        {
            return User32APIs.ShowOwnedPopups(Hwnd, bShow);
        }

        public IntPtr GetDCEx(IntPtr hRgnClip, uint flags)
        {
            return User32APIs.GetDCEx(Hwnd, hRgnClip, flags);
        }

        public bool LockWindowUpdate(bool bLock)
        {
            return User32APIs.LockWindowUpdate(Hwnd, bLock);
        }

        public bool RedrawWindow(User32Rect rectUpdate, IntPtr hRgnUpdate, uint flags)
        {
            unsafe
            {
                return User32APIs.RedrawWindow(Hwnd, &rectUpdate, hRgnUpdate, flags);
            }
        }

        public IntPtr SetTimer(UIntPtr nIDEvent, uint uElapse, IntPtr lpTimerProc)
        {
            return User32APIs.SetTimer(Hwnd, nIDEvent, uElapse, lpTimerProc);
        }

        public bool KillTimer(UIntPtr nIDEvent)
        {
            return User32APIs.KillTimer(Hwnd, nIDEvent);
        }

        public bool CheckDlgButton(int nIDButton, uint nCheck)
        {
            return User32APIs.CheckDlgButton(Hwnd, nIDButton, nCheck);
        }

        public bool CheckRadioButton(int nIDFirstButton, int nIDLastButton, int nIDCheckButton)
        {
            return User32APIs.CheckRadioButton(Hwnd, nIDFirstButton, nIDLastButton, nIDCheckButton);
        }

        public uint GetDlgItemInt(int nID, ref bool? translated, bool bSigned)
        {
            unsafe
            {
                bool tempTranslated = false;

                uint result = User32APIs.GetDlgItemInt(Hwnd, nID, &tempTranslated, bSigned);
                if (translated.HasValue)
                {
                    translated = tempTranslated;
                }

                return result;
            }
        }

        public string GetDlgItemText(int nID)
        {
            unsafe
            {
                int textLength = (int)User32APIs.GetDlgItemTextW(Hwnd, nID, new IntPtr(0), 0);
                byte[] textBuffer = new byte[(textLength + 1) * 2];
                fixed (byte* textBufferPtr = textBuffer)
                {
                    User32APIs.GetDlgItemTextW(Hwnd, nID, new IntPtr(textBufferPtr), textLength + 1);
                }

                return Encoding.Unicode.GetString(textBuffer, 0, textLength * 2);
            }
        }

        public IntPtr GetNextDlgGroupItem(IntPtr hWndCtl, bool bPrevious)
        {
            return User32APIs.GetNextDlgGroupItem(Hwnd, hWndCtl, bPrevious);
        }

        public IntPtr GetNextDlgTabItem(IntPtr hWndCtl, bool bPrevious)
        {
            return User32APIs.GetNextDlgTabItem(Hwnd, hWndCtl, bPrevious);
        }

        public uint IsDlgButtonChecked(int nIDButton)
        {
            return User32APIs.IsDlgButtonChecked(Hwnd, nIDButton);
        }

        public IntPtr SendDlgItemMessage(int nID, uint message, IntPtr wParam, IntPtr lParam)
        {
            return User32APIs.SendDlgItemMessage(Hwnd, nID, message, wParam, lParam);
        }

        public bool SetDlgItemInt(int nID, uint nValue, bool bSigned)
        {
            return User32APIs.SetDlgItemInt(Hwnd, nID, nValue, bSigned);
        }

        public bool SetDlgItemText(int nID, string text)
        {
            byte[] textBuffer = Encoding.Unicode.GetBytes(text);

            unsafe
            {
                fixed (byte* textBufferPtr = textBuffer)
                {
                    return User32APIs.SetDlgItemTextW(Hwnd, nID, new IntPtr(textBufferPtr));
                }
            }
        }

        public int GetScrollPos(int nBar)
        {
            return User32APIs.GetScrollPos(Hwnd, nBar);
        }

        public bool GetScrollRange(int nBar, ref int minPos, ref int maxPos)
        {
            unsafe
            {
                fixed (int* minPosPtr = &minPos)
                {
                    fixed (int* maxPosPtr = &maxPos)
                    {
                        return User32APIs.GetScrollRange(Hwnd, nBar, minPosPtr, maxPosPtr);
                    }
                }
            }
        }

        public bool ScrollWindow(int xAmount, int yAmount, User32Rect rect, User32Rect clipRect)
        {
            unsafe
            {
                return User32APIs.ScrollWindow(Hwnd, xAmount, yAmount, &rect, &clipRect);
            }
        }

        public int ScrollWindowEx(int dx, int dy, User32Rect rectScroll, User32Rect rectClip, IntPtr hRgnUpdate, ref User32Rect rectUpdate, uint uFlags)
        {
            unsafe
            {
                fixed (User32Rect* rectUpdatePtr = &rectUpdate)
                {
                    return User32APIs.ScrollWindowEx(Hwnd, dx, dy, &rectScroll, &rectClip, hRgnUpdate, rectUpdatePtr, uFlags);
                }
            }
        }

        public int SetScrollPos(int nBar, int nPos, bool bRedraw)
        {
            return User32APIs.SetScrollPos(Hwnd, nBar, nPos, bRedraw);
        }

        public bool SetScrollRange(int nBar, int nMinPos, int nMaxPos, bool bRedraw)
        {
            return User32APIs.SetScrollRange(Hwnd, nBar, nMinPos, nMaxPos, bRedraw);
        }

        public bool ShowScrollBar(uint nBar, bool bShow)
        {
            return User32APIs.ShowScrollBar(Hwnd, nBar, bShow);
        }

        public bool EnableScrollBar(uint uSBFlags, uint uArrowFlags)
        {
            return User32APIs.EnableScrollBar(Hwnd, uSBFlags, uArrowFlags);
        }

        public int GetDlgCtrlID()
        {
            return User32APIs.GetDlgCtrlID(Hwnd);
        }

        public int SetDlgCtrlID(int nID)
        {
            return User32APIs.SetDlgCtrlID(Hwnd, nID);
        }

        public IntPtr GetDlgItem(int nID)
        {
            return User32APIs.GetDlgItem(Hwnd, nID);
        }

        public bool FlashWindow(bool bInvert)
        {
            return User32APIs.FlashWindow(Hwnd, bInvert);
        }

        public int MessageBox(string text, string caption, uint uType)
        {
            byte[] textBuffer = Encoding.Unicode.GetBytes(text);
            byte[] captionBuffer = Encoding.Unicode.GetBytes(caption);

            unsafe
            {
                fixed (byte* textBufferPtr = textBuffer)
                {
                    fixed (byte* captionBufferPtr = captionBuffer)
                    {
                        return User32APIs.MessageBoxW(Hwnd, new IntPtr(textBufferPtr), new IntPtr(captionBufferPtr), uType);
                    }
                }
            }
        }

        public bool ChangeClipboardChain(IntPtr hWndNewNext)
        {
            return User32APIs.ChangeClipboardChain(Hwnd, hWndNewNext);
        }

        public IntPtr SetClipboardViewer()
        {
            return User32APIs.SetClipboardViewer(Hwnd);
        }

        public bool OpenClipboard()
        {
            return User32APIs.OpenClipboard(Hwnd);
        }

        public bool CreateCaret(IntPtr hBitmap)
        {
            return User32APIs.CreateCaret(Hwnd, hBitmap);
        }

        public bool CreateSolidCaret(int nWidth, int nHeight)
        {
            return User32APIs.CreateSolidCaret(Hwnd, nWidth, nHeight);
        }

        public bool CreateGrayCaret(int nWidth, int nHeight)
        {
            return User32APIs.CreateGrayCaret(Hwnd, nWidth, nHeight);
        }

        public bool HideCaret()
        {
            return User32APIs.HideCaret(Hwnd);
        }

        public bool ShowCaret()
        {
            return User32APIs.ShowCaret(Hwnd);
        }

        public void DragAcceptFiles(bool bAccept)
        {
            User32APIs.DragAcceptFiles(Hwnd, bAccept);
        }

        public IntPtr SetIcon(IntPtr hIcon, bool bBigIcon)
        {
            return User32APIs.SetIcon(Hwnd, hIcon, bBigIcon);
        }

        public IntPtr GetIcon(bool bBigIcon)
        {
            return User32APIs.GetIcon(Hwnd, bBigIcon);
        }

        public bool WinHelp(string help, uint nCmd, uint dwData)
        {
            byte[] helpBuffer = Encoding.Unicode.GetBytes(help);

            unsafe
            {
                fixed (byte* helpBufferPtr = helpBuffer)
                {
                    return User32APIs.WinHelpW(Hwnd, new IntPtr(helpBufferPtr), nCmd, dwData);
                }
            }
        }

        public bool SetWindowContextHelpId(uint dwContextHelpId)
        {
            return User32APIs.SetWindowContextHelpId(Hwnd, dwContextHelpId);
        }

        public uint GetWindowContextHelpId()
        {
            return User32APIs.GetWindowContextHelpId(Hwnd);
        }

        public User32ScrollInfo GetScrollInfo(int nBar)
        {
            User32ScrollInfo scrollInfo;

            unsafe
            {
                User32APIs.GetScrollInfo(Hwnd, nBar, &scrollInfo);
            }

            return scrollInfo;
        }

        public int SetScrollInfo(int nBar, User32ScrollInfo scrollInfo, bool bRedraw)
        {
            unsafe
            {
                return User32APIs.SetScrollInfo(Hwnd, nBar, &scrollInfo, bRedraw);
            }
        }

        public bool IsDialogMessage(User32Msg msg)
        {
            unsafe
            {
                return User32APIs.IsDialogMessage(Hwnd, &msg);
            }
        }

        public int GetWindowRgn(IntPtr hRgn)
        {
            return User32APIs.GetWindowRgn(Hwnd, hRgn);
        }

        public int SetWindowRgn(IntPtr hRgn, bool bRedraw)
        {
            return User32APIs.SetWindowRgn(Hwnd, hRgn, bRedraw);
        }

        public IntPtr DeferWindowPos(IntPtr hWinPosInfo, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags)
        {
            return User32APIs.DeferWindowPos(Hwnd, hWinPosInfo, hWndInsertAfter, x, y, cx, cy, uFlags);
        }

        public bool IsWindowUnicode()
        {
            return User32APIs.IsWindowUnicode(Hwnd);
        }

        public string GetClassName()
        {
            unsafe
            {
                // The maximum length of a classname is 256.
                int textLength = 0;
                byte[] textBuffer = new byte[257 * 2];
                fixed (byte* textBufferPtr = textBuffer)
                {
                    textLength = User32APIs.GetClassNameW(Hwnd, new IntPtr(textBufferPtr), 257);
                }
                return Encoding.Unicode.GetString(textBuffer, 0, textLength * 2);
            }
        }

        public int GetClassLong(int nIndex)
        {
            return User32APIs.GetClassLongW(Hwnd, nIndex);
        }

        public int SetClassLong(int nIndex, int dwNewLong)
        {
            return User32APIs.SetClassLongW(Hwnd, nIndex, dwNewLong);
        }

        public IntPtr GetClassLongPtr(int nIndex)
        {
            if (!Environment.Is64BitProcess)
            {
                return (IntPtr)GetClassLong(nIndex);
            }

            var ret = User32APIs.GetClassLongPtrW(Hwnd, nIndex);
            return ret;
        }

        public IntPtr SetClassLongPtr(int nIndex, IntPtr dwNewLong)
        {
            if (!Environment.Is64BitProcess)
            {
                return (IntPtr)SetClassLong(nIndex, (int)dwNewLong);
            }

            return User32APIs.SetClassLongPtrW(Hwnd, nIndex, dwNewLong);
        }
    }
}
