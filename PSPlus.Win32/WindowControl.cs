using PSPlus.Win32.Interop;
using System;
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

        public static WindowControl GetActiveWindow()
        {
            IntPtr hwnd = Win32APIs.GetActiveWindow();
            return new WindowControl(hwnd);
        }

        public static WindowControl GetCapture()
        {
            IntPtr hwnd = Win32APIs.GetCapture();
            return new WindowControl(hwnd);
        }

        public static WindowControl GetFocus()
        {
            IntPtr hwnd = Win32APIs.GetFocus();
            return new WindowControl(hwnd);
        }

        public static WindowControl WindowFromPoint(Win32Point point)
        {
            IntPtr hwnd = Win32APIs.WindowFromPoint(point);
            return new WindowControl(hwnd);
        }

        public WindowControl(ulong hwnd)
        {
            Hwnd = (IntPtr)hwnd;
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
            return Win32APIs.GetWindowLongPtr(Hwnd, nIndex);
        }

        public int SetWindowLong(int nIndex, int dwNewLong)
        {
            return Win32APIs.SetWindowLong(Hwnd, nIndex, dwNewLong);
        }

        public IntPtr SetWindowLongPtr(int nIndex, IntPtr dwNewLong)
        {
            return Win32APIs.SetWindowLongPtr(Hwnd, nIndex, dwNewLong);
        }

        public ushort GetWindowWord(int nIndex)
        {
            return Win32APIs.GetWindowWord(Hwnd, nIndex);
        }

        public ushort SetWindowWord(int nIndex, ushort wNewWord)
        {
            return Win32APIs.SetWindowWord(Hwnd, nIndex, wNewWord);
        }

        public bool IsWindowEnabled()
        {
            return Win32APIs.IsWindowEnabled(Hwnd);
        }

        public bool EnableWindow(bool bEnable)
        {
            return Win32APIs.EnableWindow(Hwnd, bEnable);
        }

        public IntPtr SetActiveWindow()
        {
            return Win32APIs.SetActiveWindow(Hwnd);
        }

        public IntPtr SetCapture()
        {
            return Win32APIs.SetCapture(Hwnd);
        }

        public IntPtr SetFocus()
        {
            return Win32APIs.SetFocus(Hwnd);
        }

        public IntPtr ChildWindowFromPoint(Win32Point point)
        {
            return Win32APIs.ChildWindowFromPoint(Hwnd, point);
        }

        public IntPtr ChildWindowFromPointEx(Win32Point point, uint uFlags)
        {
            return Win32APIs.ChildWindowFromPointEx(Hwnd, point, uFlags);
        }

        public IntPtr GetTopWindow()
        {
            return Win32APIs.GetTopWindow(Hwnd);
        }

        public IntPtr GetWindow(uint nCmd)
        {
            return Win32APIs.GetWindow(Hwnd, nCmd);
        }

        public IntPtr GetLastActivePopup()
        {
            return Win32APIs.GetLastActivePopup(Hwnd);
        }

        public bool IsChild(IntPtr hWnd)
        {
            return Win32APIs.IsChild(Hwnd, hWnd);
        }

        public IntPtr GetParent()
        {
            return Win32APIs.GetParent(Hwnd);
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
            byte[] textBuffer = new byte[textLength];

            unsafe
            {
                fixed (byte* textBufferPtr = textBuffer)
                {
                    Win32APIs.GetWindowTextW(Hwnd, new IntPtr(textBufferPtr), textLength);
                }
            }

            return Encoding.Unicode.GetString(textBuffer);
        }

        public int GetWindowTextLength()
        {
            return Win32APIs.GetWindowTextLength(Hwnd);
        }

        public void SetFont(IntPtr hFont, bool bRedraw)
        {
            Win32APIs.SetFont(Hwnd, hFont, bRedraw);
        }

        public IntPtr GetFont()
        {
            return Win32APIs.GetFont(Hwnd);
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

        public bool ClientToScreen(Win32Rect* lpRect)
        {
            return Win32APIs.ClientToScreen(Hwnd, lpRect);
        }

        public bool ScreenToClient(Win32Point* lpPoint)
        {
            return Win32APIs.ScreenToClient(Hwnd, lpPoint);
        }

        public bool ScreenToClient(Win32Rect* lpRect)
        {
            return Win32APIs.ScreenToClient(Hwnd, lpRect);
        }

        public int MapWindowPoints(IntPtr hWndTo, Win32Point* lpPoint, uint nCount)
        {
            return Win32APIs.MapWindowPoints(Hwnd, hWndTo, lpPoint, nCount);
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

        public bool GetUpdateRect(Win32Rect* lpRect, bool bErase)
        {
            return Win32APIs.GetUpdateRect(Hwnd, lpRect, bErase);
        }

        public int GetUpdateRgn(IntPtr hRgn, bool bErase)
        {
            return Win32APIs.GetUpdateRgn(Hwnd, hRgn, bErase);
        }

        public bool Invalidate(bool bErase)
        {
            return Win32APIs.Invalidate(Hwnd, bErase);
        }

        public bool InvalidateRect(Win32Rect* lpRect, bool bErase)
        {
            return Win32APIs.InvalidateRect(Hwnd, lpRect, bErase);
        }

        public bool ValidateRect(Win32Rect* lpRect)
        {
            return Win32APIs.ValidateRect(Hwnd, lpRect);
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

        public bool RedrawWindow(Win32Rect* lpRectUpdate, IntPtr hRgnUpdate, uint flags)
        {
            return Win32APIs.RedrawWindow(Hwnd, lpRectUpdate, hRgnUpdate, flags);
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

        public int DlgDirList(string pathSpec, int nIDListBox, int nIDStaticPath, uint nFileType)
        {
            return Win32APIs.DlgDirListW(Hwnd, lpPathSpec, nIDListBox, nIDStaticPath, nFileType);
        }

        public int DlgDirListComboBoxA(IntPtr lpPathSpec, int nIDComboBox, int nIDStaticPath, uint nFileType)
        {
            return Win32APIs.DlgDirListComboBoxA(Hwnd, lpPathSpec, nIDComboBox, nIDStaticPath, nFileType);
        }

        public bool DlgDirSelectEx(string s, int nCount, int nIDListBox)
        {
            return Win32APIs.DlgDirSelectExW(Hwnd, lpString, nCount, nIDListBox);
        }

        public bool DlgDirSelectComboBox(string s, int nCount, int nIDComboBox)
        {
            return Win32APIs.DlgDirSelectComboBoxW(Hwnd, lpString, nCount, nIDComboBox);
        }

        public uint GetDlgItemInt(int nID, bool* lpTrans, bool bSigned)
        {
            return Win32APIs.GetDlgItemInt(Hwnd, nID, lpTrans, bSigned);
        }

        public uint GetDlgItemText(int nID, string s, int nMaxCount)
        {
            return Win32APIs.GetDlgItemTextW(Hwnd, nID, lpStr, nMaxCount);
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

        public bool SetDlgItemText(int nID, string s)
        {
            return Win32APIs.SetDlgItemTextW(Hwnd, nID, lpszString);
        }

        public int GetScrollPos(int nBar)
        {
            return Win32APIs.GetScrollPos(Hwnd, nBar);
        }

        public bool GetScrollRange(int nBar, int* lpMinPos, int* lpMaxPos)
        {
            return Win32APIs.GetScrollRange(Hwnd, lpMinPos, lpMaxPos);
        }

        public bool ScrollWindow(int xAmount, int yAmount, Win32Rect* lpRect, Win32Rect* lpClipRect)
        {
            return Win32APIs.ScrollWindow(Hwnd, xAmount, yAmount, lpRect, lpClipRect);
        }

        public int ScrollWindowEx(int dx, int dy, Win32Rect* lpRectScroll, Win32Rect* lpRectClip, IntPtr hRgnUpdate, Win32Rect* lpRectUpdate, uint uFlags)
        {
            return Win32APIs.ScrollWindowEx(Hwnd, dx, dy, lpRectScroll, lpRectClip, hRgnUpdate, lpRectUpdate, uFlags);
        }

        public int ScrollWindowEx(int dx, int dy, uint uFlags, Win32Rect* lpRectScroll, Win32Rect* lpRectClip, IntPtr hRgnUpdate, Win32Rect* lpRectUpdate)
        {
            return Win32APIs.ScrollWindowEx(Hwnd, dx, dy, uFlags, lpRectScroll, lpRectClip, hRgnUpdate, lpRectUpdate);
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
            return Win32APIs.MessageBoxW(Hwnd, lpszText, lpszCaption, uType);
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
            return Win32APIs.WinHelpW(lpszHelp, nCmd, dwData);
        }

        public bool SetWindowContextHelpId(uint dwContextHelpId)
        {
            return Win32APIs.SetWindowContextHelpId(Hwnd, dwContextHelpId);
        }

        public uint GetWindowContextHelpId()
        {
            return Win32APIs.GetWindowContextHelpId(Hwnd);
        }

        public bool GetScrollInfo(int nBar, Win32ScrollInfo* lpScrollInfo)
        {
            return Win32APIs.GetScrollInfo(Hwnd, nBar, lpScrollInfo);
        }

        public int SetScrollInfo(int nBar, Win32ScrollInfo* lpScrollInfo, bool bRedraw)
        {
            return Win32APIs.SetScrollInfo(Hwnd, nBar, lpScrollInfo, bRedraw);
        }

        public bool IsDialogMessage(Win32Msg* lpMsg)
        {
            return Win32APIs.IsDialogMessage(Hwnd, lpMsg);
        }

        public int GetWindowRgn(ref IntPtr hRgn)
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
            return Win32APIs.GetWindowThreadID(Hwnd);
        }

        public uint GetWindowProcessID()
        {
            return Win32APIs.GetWindowProcessID(Hwnd);
        }

        public bool IsWindowUnicode()
        {
            return Win32APIs.IsWindowUnicode(Hwnd);
        }

        public bool ShowWindowAsync(int nCmdShow)
        {
            return Win32APIs.ShowWindowAsync(Hwnd, nCmdShow);
        }
    }
}
