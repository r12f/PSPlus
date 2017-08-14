using System;
using System.Runtime.InteropServices;

namespace PSPlus.Win32
{
    public static class Win32APIs
    {
        // Attributes
        [DllImport("user32.dll")]
        public static extern bool DestroyWindow(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hwnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowLongPtr(IntPtr hwnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hwnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowLongPtr(IntPtr hwnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll")]
        public static extern ushort GetWindowWord(IntPtr hwnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern ushort SetWindowWord(IntPtr hwnd, int nIndex, ushort wNewWord);

        // Message Functions
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hwnd, uint message, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern bool PostMessage(IntPtr hwnd, uint message, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern bool SendNotifyMessage(IntPtr hwnd, uint message, IntPtr wParam, IntPtr lParam);

        // Window Text Functions
        [DllImport("user32.dll")]
        public static extern bool SetWindowText(IntPtr hwnd, [MarshalAs(UnmanagedType.LPStr)] string lpszString);

        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hwnd, LPTSTR lpszStringBuf, int nMaxCount);

        [DllImport("user32.dll")]
        public static extern int GetWindowTextLength(IntPtr hwnd);

        // Font Functions
        [DllImport("user32.dll")]
        public static extern void SetFont(IntPtr hwnd, HFONT hFont, bool bRedraw);

        [DllImport("user32.dll")]
        public static extern HFONT GetFont(IntPtr hwnd);

        // Menu Functions (IntPtr hwnd, non-child windows only);

        [DllImport("user32.dll")]
        public static extern HMENU GetMenu(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern bool SetMenu(IntPtr hwnd, HMENU hMenu);

        [DllImport("user32.dll")]
        public static extern bool DrawMenuBar(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern HMENU GetSystemMenu(IntPtr hwnd, bool bRevert);

        [DllImport("user32.dll")]
        public static extern bool HiliteMenuItem(IntPtr hwnd, HMENU hMenu, uint uItemHilite, uint uHilite);

        // Window Size and Position Functions
        [DllImport("user32.dll")]
        public static extern bool IsIconic(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern bool IsZoomed(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern bool MoveWindow(IntPtr hwnd, int x, int y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll")]
        public static extern bool MoveWindow(IntPtr hwnd, LPCRECT lpRect, bool bRepaint);

        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hwnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint nFlags);

        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hwnd, IntPtr hWndInsertAfter, LPCRECT lpRect, uint nFlags);

        [DllImport("user32.dll")]
        public static extern uint ArrangeIconicWindows(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern bool BringWindowToTop(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hwnd, ref LPRECT lpRect);

        [DllImport("user32.dll")]
        public static extern bool GetClientRect(IntPtr hwnd, ref LPRECT lpRect);

        [DllImport("user32.dll")]
        public static extern bool GetWindowPlacement(IntPtr hwnd, ref WINDOWPLACEMENT* lpwndpl);

        [DllImport("user32.dll")]
        public static extern bool SetWindowPlacement(IntPtr hwnd, const WINDOWPLACEMENT* lpwndpl);

        // Coordinate Mapping Functions
        [DllImport("user32.dll")]
        public static extern bool ClientToScreen(IntPtr hwnd, ref LPPOINT lpPoint);

        [DllImport("user32.dll")]
        public static extern bool ClientToScreen(IntPtr hwnd, ref LPRECT lpRect);

        [DllImport("user32.dll")]
        public static extern bool ScreenToClient(IntPtr hwnd, ref LPPOINT lpPoint);

        [DllImport("user32.dll")]
        public static extern bool ScreenToClient(IntPtr hwnd, ref LPRECT lpRect);

        [DllImport("user32.dll")]
        public static extern int MapWindowPoints(IntPtr hwnd, IntPtr hWndTo, LPPOINT lpPoint, uint nCount);

        // Update and Painting Functions
        [DllImport("user32.dll")]
        public static extern HDC GetDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern HDC GetWindowDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr hwnd, HDC hDC);

        [DllImport("user32.dll")]
        public static extern void Print(IntPtr hwnd, HDC hDC, uint dwFlags);

        [DllImport("user32.dll")]
        public static extern void PrintClient(IntPtr hwnd, HDC hDC, uint dwFlags);

        [DllImport("user32.dll")]
        public static extern bool UpdateWindow(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern void SetRedraw(IntPtr hwnd, bool bRedraw);

        [DllImport("user32.dll")]
        public static extern bool GetUpdateRect(IntPtr hwnd, LPRECT lpRect, bool bErase);

        [DllImport("user32.dll")]
        public static extern int GetUpdateRgn(IntPtr hwnd, HRGN hRgn, bool bErase);

        [DllImport("user32.dll")]
        public static extern bool Invalidate(IntPtr hwnd, bool bErase);

        [DllImport("user32.dll")]
        public static extern bool InvalidateRect(IntPtr hwnd, LPCRECT lpRect, bool bErase);

        [DllImport("user32.dll")]
        public static extern bool ValidateRect(IntPtr hwnd, LPCRECT lpRect);

        [DllImport("user32.dll")]
        public static extern void InvalidateRgn(IntPtr hwnd, HRGN hRgn, bool bErase);

        [DllImport("user32.dll")]
        public static extern bool ValidateRgn(IntPtr hwnd, HRGN hRgn);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern bool IsWindowVisible(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern bool ShowOwnedPopups(IntPtr hwnd, bool bShow);

        [DllImport("user32.dll")]
        public static extern HDC GetDCEx(IntPtr hwnd, HRGN hRgnClip, uint flags);

        [DllImport("user32.dll")]
        public static extern bool LockWindowUpdate(IntPtr hwnd, bool bLock);

        [DllImport("user32.dll")]
        public static extern bool RedrawWindow(IntPtr hwnd, LPCRECT lpRectUpdate, HRGN hRgnUpdate, uint flags);

        // Timer Functions
        [DllImport("user32.dll")]
        public static extern bool KillTimer(IntPtr hwnd, UIntPtr nIDEvent);

        // Window State Functions
        [DllImport("user32.dll")]
        public static extern bool IsWindowEnabled(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern bool EnableWindow(IntPtr hwnd, bool bEnable);

        [DllImport("user32.dll")]
        public static extern IntPtr SetActiveWindow(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern IntPtr SetCapture(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern IntPtr SetFocus(IntPtr hwnd);

        // Dialog-Box Item Functions
        [DllImport("user32.dll")]
        public static extern bool CheckDlgButton(IntPtr hwnd, int nIDButton, uint nCheck);

        [DllImport("user32.dll")]
        public static extern bool CheckRadioButton(IntPtr hwnd, int nIDFirstButton, int nIDLastButton, int nIDCheckButton);

        [DllImport("user32.dll")]
        public static extern int DlgDirList(IntPtr hwnd, ref LPTSTR lpPathSpec, int nIDListBox, int nIDStaticPath, uint nFileType);

        [DllImport("user32.dll")]
        public static extern int DlgDirListComboBox(IntPtr hwnd, ref LPTSTR lpPathSpec, int nIDComboBox, int nIDStaticPath, uint nFileType);

        [DllImport("user32.dll")]
        public static extern bool DlgDirSelect(IntPtr hwnd, LPTSTR lpString, int nCount, int nIDListBox);

        [DllImport("user32.dll")]
        public static extern bool DlgDirSelectComboBox(IntPtr hwnd, LPTSTR lpString, int nCount, int nIDComboBox);

        [DllImport("user32.dll")]
        public static extern uint GetDlgItemInt(IntPtr hwnd, int nID, ref bool* lpTrans, bool bSigned);

        [DllImport("user32.dll")]
        public static extern uint GetDlgItemText(IntPtr hwnd, int nID, LPTSTR lpStr, int nMaxCount);

        [DllImport("user32.dll")]
        public static extern IntPtr GetNextDlgGroupItem(IntPtr hwnd, IntPtr hWndCtl, bool bPrevious);

        [DllImport("user32.dll")]
        public static extern IntPtr GetNextDlgTabItem(IntPtr hwnd, IntPtr hWndCtl, bool bPrevious);

        [DllImport("user32.dll")]
        public static extern uint IsDlgButtonChecked(IntPtr hwnd, int nIDButton);

        [DllImport("user32.dll")]
        public static extern IntPtr SendDlgItemMessage(IntPtr hwnd, int nID, uint message, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern bool SetDlgItemInt(IntPtr hwnd, int nID, uint nValue, bool bSigned);

        [DllImport("user32.dll")]
        public static extern bool SetDlgItemText(IntPtr hwnd, int nID, [MarshalAs(UnmanagedType.LPStr)] string lpszString);


        // Scrolling Functions
        [DllImport("user32.dll")]
        public static extern int GetScrollPos(IntPtr hwnd, int nBar);

        [DllImport("user32.dll")]
        public static extern bool GetScrollRange(IntPtr hwnd, int nBar, ref LPINT lpMinPos, ref LPINT lpMaxPos);

        [DllImport("user32.dll")]
        public static extern bool ScrollWindow(IntPtr hwnd, int xAmount, int yAmount, LPCRECT lpRect, LPCRECT lpClipRect);

        [DllImport("user32.dll")]
        public static extern int ScrollWindowEx(IntPtr hwnd, int dx, int dy, LPCRECT lpRectScroll, LPCRECT lpRectClip, HRGN hRgnUpdate, LPRECT lpRectUpdate, uint uFlags);

        [DllImport("user32.dll")]
        public static extern int ScrollWindowEx(IntPtr hwnd, int dx, int dy, uint uFlags, LPCRECT lpRectScroll, LPCRECT lpRectClip, HRGN hRgnUpdate, LPRECT lpRectUpdate);

        [DllImport("user32.dll")]
        public static extern int SetScrollPos(IntPtr hwnd, int nBar, int nPos, bool bRedraw);

        [DllImport("user32.dll")]
        public static extern bool SetScrollRange(IntPtr hwnd, int nBar, int nMinPos, int nMaxPos, bool bRedraw);

        [DllImport("user32.dll")]
        public static extern bool ShowScrollBar(IntPtr hwnd, uint nBar, bool bShow);

        [DllImport("user32.dll")]
        public static extern bool EnableScrollBar(IntPtr hwnd, uint uSBFlags, uint uArrowFlags);

        // Window Access Functions
        [DllImport("user32.dll")]
        public static extern IntPtr ChildWindowFromPoint(IntPtr hwnd, POINT point);

        [DllImport("user32.dll")]
        public static extern IntPtr ChildWindowFromPointEx(IntPtr hwnd, POINT point, uint uFlags);

        [DllImport("user32.dll")]
        public static extern IntPtr GetTopWindow(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindow(IntPtr hwnd, uint nCmd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetLastActivePopup(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern bool IsChild(IntPtr hwnd, IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetParent(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr hwnd, IntPtr hWndNewParent);

        // Window Tree Access
        [DllImport("user32.dll")]
        public static extern int GetDlgCtrlID(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern int SetDlgCtrlID(IntPtr hwnd, int nID);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDlgItem(IntPtr hwnd, int nID);

        // Alert Functions
        [DllImport("user32.dll")]
        public static extern bool FlashWindow(IntPtr hwnd, bool bInvert);

        [DllImport("user32.dll")]
        public static extern int MessageBox(IntPtr hwnd, [MarshalAs(UnmanagedType.LPStr)] string lpszText, [MarshalAs(UnmanagedType.LPStr)] string lpszCaption, uint uType);

        // Clipboard Functions
        [DllImport("user32.dll")]
        public static extern bool ChangeClipboardChain(IntPtr hwnd, IntPtr hWndNewNext);

        [DllImport("user32.dll")]
        public static extern IntPtr SetClipboardViewer(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern bool OpenClipboard(IntPtr hwnd);

        //// Caret Functions
        [DllImport("user32.dll")]
        public static extern bool CreateCaret(IntPtr hwnd, HBITMAP hBitmap);

        [DllImport("user32.dll")]
        public static extern bool CreateSolidCaret(IntPtr hwnd, int nWidth, int nHeight);

        [DllImport("user32.dll")]
        public static extern bool CreateGrayCaret(IntPtr hwnd, int nWidth, int nHeight);

        [DllImport("user32.dll")]
        public static extern bool HideCaret(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern bool ShowCaret(IntPtr hwnd);

        // Drag-Drop Functions
        [DllImport("user32.dll")]
        public static extern void DragAcceptFiles(IntPtr hwnd, bool bAccept);

        // Icon Functions
        [DllImport("user32.dll")]
        public static extern HICON SetIcon(IntPtr hwnd, HICON hIcon, bool bBigIcon);

        [DllImport("user32.dll")]
        public static extern HICON GetIcon(IntPtr hwnd, bool bBigIcon);

        // Help Functions
        [DllImport("user32.dll")]
        public static extern bool WinHelp(IntPtr hwnd, [MarshalAs(UnmanagedType.LPStr)] string lpszHelp, uint nCmd, uint dwData);

        [DllImport("user32.dll")]
        public static extern bool SetWindowContextHelpId(IntPtr hwnd, uint dwContextHelpId);

        [DllImport("user32.dll")]
        public static extern uint GetWindowContextHelpId(IntPtr hwnd);


        // Misc. Operations
        [DllImport("user32.dll")]
        public static extern bool GetScrollInfo(IntPtr hwnd, int nBar, ref LPSCROLLINFO lpScrollInfo);

        [DllImport("user32.dll")]
        public static extern int SetScrollInfo(IntPtr hwnd, int nBar, LPSCROLLINFO lpScrollInfo, bool bRedraw);

        [DllImport("user32.dll")]
        public static extern bool IsDialogMessage(IntPtr hwnd, LPMSG lpMsg);

        [DllImport("user32.dll")]
        public static extern int GetWindowRgn(IntPtr hwnd, ref HRGN hRgn);

        [DllImport("user32.dll")]
        public static extern int SetWindowRgn(IntPtr hwnd, HRGN hRgn, bool bRedraw);

        [DllImport("user32.dll")]
        public static extern HDWP DeferWindowPos(IntPtr hwnd, HDWP hWinPosInfo, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadID(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern uint GetWindowProcessID(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern bool IsWindow(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern bool IsWindowUnicode(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern bool ShowWindowAsync(IntPtr hwnd, int nCmdShow);
    }
}
