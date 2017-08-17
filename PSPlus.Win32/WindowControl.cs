using PSPlus.Win32.Interop;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSPlus.Win32
{
    public class WindowControl
    {
        public IntPtr Hwnd { get; set; }

        public static WindowControl GetForegroundWindow()
        {
            IntPtr hwnd = Win32APIs.GetForegroundWindow();
            return new WindowControl(hwnd);
        }

        public static WindowControl GetDesktopWindow()
        {
            IntPtr hwnd = Win32APIs.GetDesktopWindow();
            return new WindowControl(hwnd);
        }

        public static WindowControl WindowFromPoint(Win32Point point)
        {
            IntPtr hwnd = Win32APIs.WindowFromPoint(point);
            return new WindowControl(hwnd);
        }

        public WindowControl(IntPtr hwnd)
        {
            Hwnd = hwnd;
        }

        public bool IsWindow()
        {
            return Hwnd != null && Win32APIs.IsWindow(Hwnd);
        }

        public bool DestroyWindow()
        {
            return Win32APIs.DestroyWindow(Hwnd);
        }

        public int GetWindowLong(int nIndex)
        {
            return Win32APIs.GetWindowLong(Hwnd, nIndex);
        }

        public IntPtr GetWindowLongPtr(int nIndex)
        {
            if (!Environment.Is64BitProcess)
            {
                return (IntPtr)GetWindowLong(nIndex);
            }

            return Win32APIs.GetWindowLongPtr(Hwnd, nIndex);
        }

        public int SetWindowLong(int nIndex, int dwNewLong)
        {
            return Win32APIs.SetWindowLong(Hwnd, nIndex, dwNewLong);
        }

        public IntPtr SetWindowLongPtr(int nIndex, IntPtr dwNewLong)
        {
            if (!Environment.Is64BitProcess)
            {
                return (IntPtr)SetWindowLong(nIndex, (int)dwNewLong);
            }

            return Win32APIs.SetWindowLongPtr(Hwnd, nIndex, dwNewLong);
        }

        public bool IsWindowEnabled()
        {
            return Win32APIs.IsWindowEnabled(Hwnd);
        }

        public bool EnableWindow(bool bEnable)
        {
            return Win32APIs.EnableWindow(Hwnd, bEnable);
        }

        public WindowControl GetActiveWindow()
        {
            using (AttachThreadInputScope scope = new AttachThreadInputScope(GetWindowThreadID()))
            {
                return new WindowControl(Win32APIs.GetActiveWindow());
            }
        }

        public IntPtr SetActiveWindow()
        {
            using (AttachThreadInputScope scope = new AttachThreadInputScope(GetWindowThreadID()))
            {
                return Win32APIs.SetActiveWindow(Hwnd);
            }
        }

        public WindowControl GetCapture()
        {
            using (AttachThreadInputScope scope = new AttachThreadInputScope(GetWindowThreadID()))
            {
                return new WindowControl(Win32APIs.GetCapture());
            }
        }

        public IntPtr SetCapture()
        {
            using (AttachThreadInputScope scope = new AttachThreadInputScope(GetWindowThreadID()))
            {
                return Win32APIs.SetCapture(Hwnd);
            }
        }

        public bool ReleaseCapture()
        {
            using (AttachThreadInputScope scope = new AttachThreadInputScope(GetWindowThreadID()))
            {
                return Win32APIs.ReleaseCapture();
            }
        }

        public WindowControl GetFocus()
        {
            using (AttachThreadInputScope scope = new AttachThreadInputScope(GetWindowThreadID()))
            {
                return new WindowControl(Win32APIs.GetFocus());
            }
        }

        public IntPtr SetFocus()
        {
            using (AttachThreadInputScope scope = new AttachThreadInputScope(GetWindowThreadID()))
            {
                return Win32APIs.SetFocus(Hwnd);
            }
        }

        public WindowControl ChildWindowFromPoint(Win32Point point)
        {
            return new WindowControl(Win32APIs.ChildWindowFromPoint(Hwnd, point));
        }

        public WindowControl ChildWindowFromPointEx(Win32Point point, uint uFlags)
        {
            return new WindowControl(Win32APIs.ChildWindowFromPointEx(Hwnd, point, uFlags));
        }

        public WindowControl GetTopWindow()
        {
            return new WindowControl(Win32APIs.GetTopWindow(Hwnd));
        }

        public WindowControl GetWindow(uint nCmd)
        {
            return new WindowControl(Win32APIs.GetWindow(Hwnd, nCmd));
        }

        public IEnumerable<WindowControl> GetChildren()
        {
            WindowControl childWindow = GetWindow(Win32Consts.GW_CHILD);
            while (childWindow.IsWindow())
            {
                yield return childWindow;
                childWindow = childWindow.GetWindow(Win32Consts.GW_HWNDNEXT);
            }
        }

        public WindowControl GetLastActivePopup()
        {
            return new WindowControl(Win32APIs.GetLastActivePopup(Hwnd));
        }

        public bool IsChild(IntPtr hWnd)
        {
            return Win32APIs.IsChild(Hwnd, hWnd);
        }

        public WindowControl GetParent()
        {
            return new WindowControl(Win32APIs.GetParent(Hwnd));
        }

        public IntPtr SetParent(IntPtr hWndNewParent)
        {
            return Win32APIs.SetParent(Hwnd, hWndNewParent);
        }

        public IntPtr SendMessageA(uint message, IntPtr wParam, IntPtr lParam)
        {
            return Win32APIs.SendMessageA(Hwnd, message, wParam, lParam);
        }

        public IntPtr SendMessageW(uint message, IntPtr wParam, IntPtr lParam)
        {
            return Win32APIs.SendMessageW(Hwnd, message, wParam, lParam);
        }

        public IntPtr SendMessageTimeoutA(uint message, IntPtr wParam, IntPtr lParam, uint flags, uint timeout, ref IntPtr result)
        {
            unsafe
            {
                fixed (IntPtr* resultPtr = &result)
                {
                    return Win32APIs.SendMessageTimeoutA(Hwnd, message, wParam, lParam, flags, timeout, resultPtr);
                }
            }
        }

        public IntPtr SendMessageTimeoutW(uint message, IntPtr wParam, IntPtr lParam, uint flags, uint timeout, ref IntPtr result)
        {
            unsafe
            {
                fixed (IntPtr* resultPtr = &result)
                {
                    return Win32APIs.SendMessageTimeoutW(Hwnd, message, wParam, lParam, flags, timeout, resultPtr);
                }
            }
        }

        public bool PostMessageA(uint message, IntPtr wParam, IntPtr lParam)
        {
            return Win32APIs.PostMessageA(Hwnd, message, wParam, lParam);
        }

        public bool PostMessageW(uint message, IntPtr wParam, IntPtr lParam)
        {
            return Win32APIs.PostMessageW(Hwnd, message, wParam, lParam);
        }

        public bool SendNotifyMessageA(uint message, IntPtr wParam, IntPtr lParam)
        {
            return Win32APIs.SendNotifyMessageA(Hwnd, message, wParam, lParam);
        }

        public bool SendNotifyMessageW(uint message, IntPtr wParam, IntPtr lParam)
        {
            return Win32APIs.SendNotifyMessageW(Hwnd, message, wParam, lParam);
        }

        public bool SetWindowText(string text)
        {
            byte[] textBuffer = Encoding.Unicode.GetBytes(text);

            unsafe
            {
                fixed (byte* textBufferPtr = textBuffer)
                {
                    return Win32APIs.SetWindowTextW(Hwnd, new IntPtr(textBufferPtr));
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
                    Win32APIs.GetWindowTextW(Hwnd, new IntPtr(textBufferPtr), textLength + 1);
                }
                return Encoding.Unicode.GetString(textBuffer);
            }
        }

        public int GetWindowTextLength()
        {
            return Win32APIs.GetWindowTextLength(Hwnd);
        }

        public IntPtr GetMenu()
        {
            return Win32APIs.GetMenu(Hwnd);
        }

        public bool SetMenu(IntPtr hMenu)
        {
            return Win32APIs.SetMenu(Hwnd, hMenu);
        }

        public bool DrawMenuBar()
        {
            return Win32APIs.DrawMenuBar(Hwnd);
        }

        public IntPtr GetSystemMenu(bool bRevert)
        {
            return Win32APIs.GetSystemMenu(Hwnd, bRevert);
        }

        public bool HiliteMenuItem(IntPtr hMenu, uint uItemHilite, uint uHilite)
        {
            return Win32APIs.HiliteMenuItem(Hwnd, hMenu, uItemHilite, uHilite);
        }

        public bool IsIconic()
        {
            return Win32APIs.IsIconic(Hwnd);
        }

        public bool IsZoomed()
        {
            return Win32APIs.IsZoomed(Hwnd);
        }

        public bool MoveWindow(int x, int y, int nWidth, int nHeight, bool bRepaint)
        {
            return Win32APIs.MoveWindow(Hwnd, x, y, nWidth, nHeight, bRepaint);
        }

        public bool MoveWindow(Win32Rect rect, bool bRepaint)
        {
            unsafe
            {
                return Win32APIs.MoveWindow(Hwnd, &rect, bRepaint);
            }
        }

        public bool SetWindowPos(IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint nFlags)
        {
            return Win32APIs.SetWindowPos(Hwnd, hWndInsertAfter, x, y, cx, cy, nFlags);
        }

        public bool SetWindowPos(IntPtr hWndInsertAfter, Win32Rect rect, uint nFlags)
        {
            unsafe
            {
                return Win32APIs.SetWindowPos(Hwnd, hWndInsertAfter, &rect, nFlags);
            }
        }

        public uint ArrangeIconicWindows()
        {
            return Win32APIs.ArrangeIconicWindows(Hwnd);
        }

        public bool BringWindowToTop()
        {
            return Win32APIs.BringWindowToTop(Hwnd);
        }

        public Win32Rect GetWindowRect()
        {
            Win32Rect rect;

            unsafe
            {
                Win32APIs.GetWindowRect(Hwnd, &rect);
            }

            return rect;
        }

        public Win32Rect GetClientRect()
        {
            Win32Rect rect;

            unsafe
            {
                Win32APIs.GetClientRect(Hwnd, &rect);
            }

            return rect;
        }

        public Win32WindowPlacement GetWindowPlacement()
        {
            Win32WindowPlacement wndpl;

            unsafe
            {
                Win32APIs.GetWindowPlacement(Hwnd, &wndpl);
            }

            return wndpl;
        }

        public bool SetWindowPlacement(Win32WindowPlacement wndpl)
        {
            unsafe
            {
                return Win32APIs.SetWindowPlacement(Hwnd, &wndpl);
            }
        }

        public Win32Point ClientToScreen(Win32Point point)
        {
            Win32Point screenPoint = point;
            ClientToScreenInPlace(ref screenPoint);
            return screenPoint;
        }

        public bool ClientToScreenInPlace(ref Win32Point point)
        {
            unsafe
            {
                fixed (Win32Point* lpPoint = &point)
                {
                    return Win32APIs.ClientToScreen(Hwnd, lpPoint);
                }
            }
        }

        public Win32Rect ClientToScreen(Win32Rect rect)
        {
            Win32Rect screenRect = rect;
            ClientToScreenInPlace(ref screenRect);
            return screenRect;
        }

        public bool ClientToScreenInPlace(ref Win32Rect rect)
        {
            unsafe
            {
                fixed (Win32Rect* lpRect = &rect)
                {
                    Win32Point* lpPoint = (Win32Point*)lpRect;
                    if (!Win32APIs.ClientToScreen(Hwnd, lpPoint))
                    {
                        return false;
                    }
                    return Win32APIs.ClientToScreen(Hwnd, lpPoint + 1);
                }
            }
        }

        public Win32Point ScreenToClient(Win32Point rect)
        {
            Win32Point clientPoint = rect;
            ScreenToClientInPlace(ref clientPoint);
            return clientPoint;
        }

        public bool ScreenToClientInPlace(ref Win32Point point)
        {
            unsafe
            {
                fixed (Win32Point* lpPoint = &point)
                {
                    return Win32APIs.ScreenToClient(Hwnd, lpPoint);
                }
            }
        }

        public Win32Rect ScreenToClient(Win32Rect rect)
        {
            Win32Rect clientRect = rect;
            ScreenToClientInPlace(ref clientRect);
            return clientRect;
        }

        public bool ScreenToClientInPlace(ref Win32Rect rect)
        {
            unsafe
            {
                fixed (Win32Rect* lpRect = &rect)
                {
                    Win32Point* lpPoint = (Win32Point*)lpRect;
                    if (!Win32APIs.ScreenToClient(Hwnd, lpPoint))
                    {
                        return false;
                    }
                    return Win32APIs.ScreenToClient(Hwnd, lpPoint + 1);
                }
            }
        }

        public int MapWindowPoints(IntPtr hWndTo, Win32Point[] point)
        {
            unsafe
            {
                fixed (Win32Point* pointPtr = point)
                {
                    return Win32APIs.MapWindowPoints(Hwnd, hWndTo, pointPtr, (uint)point.Length);
                }
            }
        }

        public IntPtr GetDC()
        {
            return Win32APIs.GetDC(Hwnd);
        }

        public IntPtr GetWindowDC()
        {
            return Win32APIs.GetWindowDC(Hwnd);
        }

        public int ReleaseDC(IntPtr hDC)
        {
            return Win32APIs.ReleaseDC(Hwnd, hDC);
        }

        public void Print(IntPtr hDC, uint dwFlags)
        {
            Win32APIs.Print(Hwnd, hDC, dwFlags);
        }

        public void PrintClient(IntPtr hDC, uint dwFlags)
        {
            Win32APIs.PrintClient(Hwnd, hDC, dwFlags);
        }

        public bool UpdateWindow()
        {
            return Win32APIs.UpdateWindow(Hwnd);
        }

        public void SetRedraw(bool bRedraw)
        {
            Win32APIs.SetRedraw(Hwnd, bRedraw);
        }

        public Win32Rect GetUpdateRect(bool bErase)
        {
            Win32Rect rect;

            unsafe
            {
                Win32APIs.GetUpdateRect(Hwnd, &rect, bErase);
            }

            return rect;
        }

        public int GetUpdateRgn(IntPtr hRgn, bool bErase)
        {
            return Win32APIs.GetUpdateRgn(Hwnd, hRgn, bErase);
        }

        public bool Invalidate(bool bErase)
        {
            return Win32APIs.Invalidate(Hwnd, bErase);
        }

        public bool InvalidateRect(Win32Rect rect, bool bErase)
        {
            unsafe
            {
                return Win32APIs.InvalidateRect(Hwnd, &rect, bErase);
            }
        }

        public bool ValidateRect(Win32Rect rect)
        {
            unsafe
            {
                return Win32APIs.ValidateRect(Hwnd, &rect);
            }
        }

        public void InvalidateRgn(IntPtr hRgn, bool bErase)
        {
            Win32APIs.InvalidateRgn(Hwnd, hRgn, bErase);
        }

        public bool ValidateRgn(IntPtr hRgn)
        {
            return Win32APIs.ValidateRgn(Hwnd, hRgn);
        }

        public bool ShowWindow(int nCmdShow)
        {
            return Win32APIs.ShowWindow(Hwnd, nCmdShow);
        }

        public bool ShowWindowAsync(int nCmdShow)
        {
            return Win32APIs.ShowWindowAsync(Hwnd, nCmdShow);
        }

        public bool IsWindowVisible()
        {
            return Win32APIs.IsWindowVisible(Hwnd);
        }

        public bool ShowOwnedPopups(bool bShow)
        {
            return Win32APIs.ShowOwnedPopups(Hwnd, bShow);
        }

        public IntPtr GetDCEx(IntPtr hRgnClip, uint flags)
        {
            return Win32APIs.GetDCEx(Hwnd, hRgnClip, flags);
        }

        public bool LockWindowUpdate(bool bLock)
        {
            return Win32APIs.LockWindowUpdate(Hwnd, bLock);
        }

        public bool RedrawWindow(Win32Rect rectUpdate, IntPtr hRgnUpdate, uint flags)
        {
            unsafe
            {
                return Win32APIs.RedrawWindow(Hwnd, &rectUpdate, hRgnUpdate, flags);
            }
        }

        public IntPtr SetTimer(UIntPtr nIDEvent, uint uElapse, IntPtr lpTimerProc)
        {
            return Win32APIs.SetTimer(Hwnd, nIDEvent, uElapse, lpTimerProc);
        }

        public bool KillTimer(UIntPtr nIDEvent)
        {
            return Win32APIs.KillTimer(Hwnd, nIDEvent);
        }

        public bool CheckDlgButton(int nIDButton, uint nCheck)
        {
            return Win32APIs.CheckDlgButton(Hwnd, nIDButton, nCheck);
        }

        public bool CheckRadioButton(int nIDFirstButton, int nIDLastButton, int nIDCheckButton)
        {
            return Win32APIs.CheckRadioButton(Hwnd, nIDFirstButton, nIDLastButton, nIDCheckButton);
        }

        public uint GetDlgItemInt(int nID, ref bool? translated, bool bSigned)
        {
            unsafe
            {
                bool tempTranslated = false;

                uint result = Win32APIs.GetDlgItemInt(Hwnd, nID, &tempTranslated, bSigned);
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
                int textLength = (int)Win32APIs.GetDlgItemTextW(Hwnd, nID, new IntPtr(0), 0);
                byte[] textBuffer = new byte[(textLength + 1) * 2];
                fixed (byte* textBufferPtr = textBuffer)
                {
                    Win32APIs.GetDlgItemTextW(Hwnd, nID, new IntPtr(textBufferPtr), textLength + 1);
                }

                return Encoding.Unicode.GetString(textBuffer);
            }
        }

        public IntPtr GetNextDlgGroupItem(IntPtr hWndCtl, bool bPrevious)
        {
            return Win32APIs.GetNextDlgGroupItem(Hwnd, hWndCtl, bPrevious);
        }

        public IntPtr GetNextDlgTabItem(IntPtr hWndCtl, bool bPrevious)
        {
            return Win32APIs.GetNextDlgTabItem(Hwnd, hWndCtl, bPrevious);
        }

        public uint IsDlgButtonChecked(int nIDButton)
        {
            return Win32APIs.IsDlgButtonChecked(Hwnd, nIDButton);
        }

        public IntPtr SendDlgItemMessage(int nID, uint message, IntPtr wParam, IntPtr lParam)
        {
            return Win32APIs.SendDlgItemMessage(Hwnd, nID, message, wParam, lParam);
        }

        public bool SetDlgItemInt(int nID, uint nValue, bool bSigned)
        {
            return Win32APIs.SetDlgItemInt(Hwnd, nID, nValue, bSigned);
        }

        public bool SetDlgItemText(int nID, string text)
        {
            byte[] textBuffer = Encoding.Unicode.GetBytes(text);

            unsafe
            {
                fixed (byte* textBufferPtr = textBuffer)
                {
                    return Win32APIs.SetDlgItemTextW(Hwnd, nID, new IntPtr(textBufferPtr));
                }
            }
        }

        public int GetScrollPos(int nBar)
        {
            return Win32APIs.GetScrollPos(Hwnd, nBar);
        }

        public bool GetScrollRange(int nBar, ref int minPos, ref int maxPos)
        {
            unsafe
            {
                fixed (int* minPosPtr = &minPos)
                {
                    fixed (int* maxPosPtr = &maxPos)
                    {
                        return Win32APIs.GetScrollRange(Hwnd, nBar, minPosPtr, maxPosPtr);
                    }
                }
            }
        }

        public bool ScrollWindow(int xAmount, int yAmount, Win32Rect rect, Win32Rect clipRect)
        {
            unsafe
            {
                return Win32APIs.ScrollWindow(Hwnd, xAmount, yAmount, &rect, &clipRect);
            }
        }

        public int ScrollWindowEx(int dx, int dy, Win32Rect rectScroll, Win32Rect rectClip, IntPtr hRgnUpdate, ref Win32Rect rectUpdate, uint uFlags)
        {
            unsafe
            {
                fixed (Win32Rect* rectUpdatePtr = &rectUpdate)
                {
                    return Win32APIs.ScrollWindowEx(Hwnd, dx, dy, &rectScroll, &rectClip, hRgnUpdate, rectUpdatePtr, uFlags);
                }
            }
        }

        public int SetScrollPos(int nBar, int nPos, bool bRedraw)
        {
            return Win32APIs.SetScrollPos(Hwnd, nBar, nPos, bRedraw);
        }

        public bool SetScrollRange(int nBar, int nMinPos, int nMaxPos, bool bRedraw)
        {
            return Win32APIs.SetScrollRange(Hwnd, nBar, nMinPos, nMaxPos, bRedraw);
        }

        public bool ShowScrollBar(uint nBar, bool bShow)
        {
            return Win32APIs.ShowScrollBar(Hwnd, nBar, bShow);
        }

        public bool EnableScrollBar(uint uSBFlags, uint uArrowFlags)
        {
            return Win32APIs.EnableScrollBar(Hwnd, uSBFlags, uArrowFlags);
        }

        public int GetDlgCtrlID()
        {
            return Win32APIs.GetDlgCtrlID(Hwnd);
        }

        public int SetDlgCtrlID(int nID)
        {
            return Win32APIs.SetDlgCtrlID(Hwnd, nID);
        }

        public IntPtr GetDlgItem(int nID)
        {
            return Win32APIs.GetDlgItem(Hwnd, nID);
        }

        public bool FlashWindow(bool bInvert)
        {
            return Win32APIs.FlashWindow(Hwnd, bInvert);
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
                        return Win32APIs.MessageBoxW(Hwnd, new IntPtr(textBufferPtr), new IntPtr(captionBufferPtr), uType);
                    }
                }
            }
        }

        public bool ChangeClipboardChain(IntPtr hWndNewNext)
        {
            return Win32APIs.ChangeClipboardChain(Hwnd, hWndNewNext);
        }

        public IntPtr SetClipboardViewer()
        {
            return Win32APIs.SetClipboardViewer(Hwnd);
        }

        public bool OpenClipboard()
        {
            return Win32APIs.OpenClipboard(Hwnd);
        }

        public bool CreateCaret(IntPtr hBitmap)
        {
            return Win32APIs.CreateCaret(Hwnd, hBitmap);
        }

        public bool CreateSolidCaret(int nWidth, int nHeight)
        {
            return Win32APIs.CreateSolidCaret(Hwnd, nWidth, nHeight);
        }

        public bool CreateGrayCaret(int nWidth, int nHeight)
        {
            return Win32APIs.CreateGrayCaret(Hwnd, nWidth, nHeight);
        }

        public bool HideCaret()
        {
            return Win32APIs.HideCaret(Hwnd);
        }

        public bool ShowCaret()
        {
            return Win32APIs.ShowCaret(Hwnd);
        }

        public void DragAcceptFiles(bool bAccept)
        {
            Win32APIs.DragAcceptFiles(Hwnd, bAccept);
        }

        public IntPtr SetIcon(IntPtr hIcon, bool bBigIcon)
        {
            return Win32APIs.SetIcon(Hwnd, hIcon, bBigIcon);
        }

        public IntPtr GetIcon(bool bBigIcon)
        {
            return Win32APIs.GetIcon(Hwnd, bBigIcon);
        }

        public bool WinHelp(string help, uint nCmd, uint dwData)
        {
            byte[] helpBuffer = Encoding.Unicode.GetBytes(help);

            unsafe
            {
                fixed (byte* helpBufferPtr = helpBuffer)
                {
                    return Win32APIs.WinHelpW(Hwnd, new IntPtr(helpBufferPtr), nCmd, dwData);
                }
            }
        }

        public bool SetWindowContextHelpId(uint dwContextHelpId)
        {
            return Win32APIs.SetWindowContextHelpId(Hwnd, dwContextHelpId);
        }

        public uint GetWindowContextHelpId()
        {
            return Win32APIs.GetWindowContextHelpId(Hwnd);
        }

        public Win32ScrollInfo GetScrollInfo(int nBar)
        {
            Win32ScrollInfo scrollInfo;

            unsafe
            {
                Win32APIs.GetScrollInfo(Hwnd, nBar, &scrollInfo);
            }

            return scrollInfo;
        }

        public int SetScrollInfo(int nBar, Win32ScrollInfo scrollInfo, bool bRedraw)
        {
            unsafe
            {
                return Win32APIs.SetScrollInfo(Hwnd, nBar, &scrollInfo, bRedraw);
            }
        }

        public bool IsDialogMessage(Win32Msg msg)
        {
            unsafe
            {
                return Win32APIs.IsDialogMessage(Hwnd, &msg);
            }
        }

        public int GetWindowRgn(IntPtr hRgn)
        {
            return Win32APIs.GetWindowRgn(Hwnd, hRgn);
        }

        public int SetWindowRgn(IntPtr hRgn, bool bRedraw)
        {
            return Win32APIs.SetWindowRgn(Hwnd, hRgn, bRedraw);
        }

        public IntPtr DeferWindowPos(IntPtr hWinPosInfo, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags)
        {
            return Win32APIs.DeferWindowPos(Hwnd, hWinPosInfo, hWndInsertAfter, x, y, cx, cy, uFlags);
        }

        public uint GetWindowThreadID()
        {
            unsafe
            {
                return Win32APIs.GetWindowThreadProcessId(Hwnd, null);
            }
        }

        public uint GetWindowProcessID()
        {
            uint processId = 0;

            unsafe
            {
                Win32APIs.GetWindowThreadProcessId(Hwnd, &processId);
            }

            return processId;
        }

        public bool IsWindowUnicode()
        {
            return Win32APIs.IsWindowUnicode(Hwnd);
        }
    }
}
