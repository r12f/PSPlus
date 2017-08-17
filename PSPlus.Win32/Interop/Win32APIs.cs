using System;
using System.Runtime.InteropServices;

namespace PSPlus.Win32.Interop
{
    public unsafe static class Win32APIs
    {
        // Thread input
        [DllImport("user32.dll")]
        public static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

        // Destory
        [DllImport("user32.dll")]
        public static extern bool DestroyWindow(IntPtr hwnd);

        // Attributes
        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hwnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowLongPtr(IntPtr hwnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hwnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowLongPtr(IntPtr hwnd, int nIndex, IntPtr dwNewLong);

        // Window State Functions
        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern bool IsWindowEnabled(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern bool EnableWindow(IntPtr hwnd, bool bEnable);

        [DllImport("user32.dll")]
        public static extern IntPtr GetActiveWindow();

        [DllImport("user32.dll")]
        public static extern IntPtr SetActiveWindow(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetCapture();

        [DllImport("user32.dll")]
        public static extern IntPtr SetCapture(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern IntPtr GetFocus();

        [DllImport("user32.dll")]
        public static extern IntPtr SetFocus(IntPtr hwnd);

        // Window Access Functions
        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(Win32Point Point);

        [DllImport("user32.dll")]
        public static extern IntPtr ChildWindowFromPoint(IntPtr hwnd, Win32Point point);

        [DllImport("user32.dll")]
        public static extern IntPtr ChildWindowFromPointEx(IntPtr hwnd, Win32Point point, uint uFlags);

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

        [DllImport("user32.dll")]
        public static extern IntPtr GetAncestor(IntPtr hwnd, uint gaFlags);

        // Message Functions
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessageA(IntPtr hwnd, uint message, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessageW(IntPtr hwnd, uint message, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessageTimeoutA(IntPtr hwnd, uint message, IntPtr wParam, IntPtr lParam, uint flags, uint timeout, IntPtr* result);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessageTimeoutW(IntPtr hwnd, uint message, IntPtr wParam, IntPtr lParam, uint flags, uint timeout, IntPtr* result);

        [DllImport("user32.dll")]
        public static extern bool PostMessageA(IntPtr hwnd, uint message, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern bool PostMessageW(IntPtr hwnd, uint message, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern bool SendNotifyMessageA(IntPtr hwnd, uint message, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern bool SendNotifyMessageW(IntPtr hwnd, uint message, IntPtr wParam, IntPtr lParam);

        // Window Text Functions
        [DllImport("user32.dll")]
        public static extern bool SetWindowTextA(IntPtr hwnd, IntPtr lpszString);

        [DllImport("user32.dll")]
        public static extern bool SetWindowTextW(IntPtr hwnd, IntPtr lpszString);

        [DllImport("user32.dll")]
        public static extern int GetWindowTextA(IntPtr hwnd, IntPtr lpszStringBuf, int nMaxCount);

        [DllImport("user32.dll")]
        public static extern int GetWindowTextW(IntPtr hwnd, IntPtr lpszStringBuf, int nMaxCount);

        [DllImport("user32.dll")]
        public static extern int GetWindowTextLength(IntPtr hwnd);

        // Menu Functions (non-child windows only)
        [DllImport("user32.dll")]
        public static extern IntPtr GetMenu(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern bool SetMenu(IntPtr hwnd, IntPtr hMenu);

        [DllImport("user32.dll")]
        public static extern bool DrawMenuBar(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetSystemMenu(IntPtr hwnd, bool bRevert);

        [DllImport("user32.dll")]
        public static extern bool HiliteMenuItem(IntPtr hwnd, IntPtr hMenu, uint uItemHilite, uint uHilite);

        // Window Size and Position Functions
        [DllImport("user32.dll")]
        public static extern bool IsIconic(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern bool IsZoomed(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern bool MoveWindow(IntPtr hwnd, int x, int y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hwnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint nFlags);

        [DllImport("user32.dll")]
        public static extern uint ArrangeIconicWindows(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern bool BringWindowToTop(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hwnd, Win32Rect* lpRect);

        [DllImport("user32.dll")]
        public static extern bool GetClientRect(IntPtr hwnd, Win32Rect* lpRect);

        [DllImport("user32.dll")]
        public static extern bool GetWindowPlacement(IntPtr hwnd, Win32WindowPlacement* lpwndpl);

        [DllImport("user32.dll")]
        public static extern bool SetWindowPlacement(IntPtr hwnd, Win32WindowPlacement* lpwndpl);

        // Coordinate Mapping Functions
        [DllImport("user32.dll")]
        public static extern bool ClientToScreen(IntPtr hwnd, Win32Point* lpPoint);

        [DllImport("user32.dll")]
        public static extern bool ScreenToClient(IntPtr hwnd, Win32Point* lpPoint);

        [DllImport("user32.dll")]
        public static extern int MapWindowPoints(IntPtr hwnd, IntPtr hWndTo, Win32Point* lpPoint, uint nCount);

        // Update and Painting Functions
        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr hwnd, IntPtr hDC);

        [DllImport("user32.dll")]
        public static extern void Print(IntPtr hwnd, IntPtr hDC, uint dwFlags);

        [DllImport("user32.dll")]
        public static extern void PrintClient(IntPtr hwnd, IntPtr hDC, uint dwFlags);

        [DllImport("user32.dll")]
        public static extern bool UpdateWindow(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern void SetRedraw(IntPtr hwnd, bool bRedraw);

        [DllImport("user32.dll")]
        public static extern bool GetUpdateRect(IntPtr hwnd, Win32Rect* lpRect, bool bErase);

        [DllImport("user32.dll")]
        public static extern int GetUpdateRgn(IntPtr hwnd, IntPtr hRgn, bool bErase);

        [DllImport("user32.dll")]
        public static extern bool Invalidate(IntPtr hwnd, bool bErase);

        [DllImport("user32.dll")]
        public static extern bool InvalidateRect(IntPtr hwnd, Win32Rect* lpRect, bool bErase);

        [DllImport("user32.dll")]
        public static extern bool ValidateRect(IntPtr hwnd, Win32Rect* lpRect);

        [DllImport("user32.dll")]
        public static extern void InvalidateRgn(IntPtr hwnd, IntPtr hRgn, bool bErase);

        [DllImport("user32.dll")]
        public static extern bool ValidateRgn(IntPtr hwnd, IntPtr hRgn);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern bool IsWindowVisible(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern bool ShowOwnedPopups(IntPtr hwnd, bool bShow);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDCEx(IntPtr hwnd, IntPtr hRgnClip, uint flags);

        [DllImport("user32.dll")]
        public static extern bool LockWindowUpdate(IntPtr hwnd, bool bLock);

        [DllImport("user32.dll")]
        public static extern bool RedrawWindow(IntPtr hwnd, Win32Rect* lpRectUpdate, IntPtr hRgnUpdate, uint flags);

        // Timer Functions
        [DllImport("user32.dll")]
        public static extern IntPtr SetTimer(IntPtr hwnd, UIntPtr nIDEvent, uint uElapse, IntPtr lpTimerProc);

        [DllImport("user32.dll")]
        public static extern bool KillTimer(IntPtr hwnd, UIntPtr nIDEvent);

        // Dialog-Box Item Functions
        [DllImport("user32.dll")]
        public static extern bool CheckDlgButton(IntPtr hwnd, int nIDButton, uint nCheck);

        [DllImport("user32.dll")]
        public static extern bool CheckRadioButton(IntPtr hwnd, int nIDFirstButton, int nIDLastButton, int nIDCheckButton);

        [DllImport("user32.dll")]
        public static extern int DlgDirListA(IntPtr hwnd, IntPtr lpPathSpec, int nIDListBox, int nIDStaticPath, uint nFileType);

        [DllImport("user32.dll")]
        public static extern int DlgDirListW(IntPtr hwnd, IntPtr lpPathSpec, int nIDListBox, int nIDStaticPath, uint nFileType);

        [DllImport("user32.dll")]
        public static extern int DlgDirListComboBoxA(IntPtr hwnd, IntPtr lpPathSpec, int nIDComboBox, int nIDStaticPath, uint nFileType);

        [DllImport("user32.dll")]
        public static extern bool DlgDirSelectExA(IntPtr hwnd, IntPtr lpString, int nCount, int nIDListBox);

        [DllImport("user32.dll")]
        public static extern bool DlgDirSelectExW(IntPtr hwnd, IntPtr lpString, int nCount, int nIDListBox);

        [DllImport("user32.dll")]
        public static extern bool DlgDirSelectComboBoxA(IntPtr hwnd, IntPtr lpString, int nCount, int nIDComboBox);

        [DllImport("user32.dll")]
        public static extern bool DlgDirSelectComboBoxW(IntPtr hwnd, IntPtr lpString, int nCount, int nIDComboBox);

        [DllImport("user32.dll")]
        public static extern uint GetDlgItemInt(IntPtr hwnd, int nID, bool* lpTrans, bool bSigned);

        [DllImport("user32.dll")]
        public static extern uint GetDlgItemTextA(IntPtr hwnd, int nID, IntPtr lpStr, int nMaxCount);

        [DllImport("user32.dll")]
        public static extern uint GetDlgItemTextW(IntPtr hwnd, int nID, IntPtr lpStr, int nMaxCount);

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
        public static extern bool SetDlgItemTextA(IntPtr hwnd, int nID, IntPtr lpszString);

        [DllImport("user32.dll")]
        public static extern bool SetDlgItemTextW(IntPtr hwnd, int nID, IntPtr lpszString);

        // Scrolling Functions
        [DllImport("user32.dll")]
        public static extern int GetScrollPos(IntPtr hwnd, int nBar);

        [DllImport("user32.dll")]
        public static extern bool GetScrollRange(IntPtr hwnd, int nBar, int* lpMinPos, int* lpMaxPos);

        [DllImport("user32.dll")]
        public static extern bool ScrollWindow(IntPtr hwnd, int xAmount, int yAmount, Win32Rect* lpRect, Win32Rect* lpClipRect);

        [DllImport("user32.dll")]
        public static extern int ScrollWindowEx(IntPtr hwnd, int dx, int dy, Win32Rect* lpRectScroll, Win32Rect* lpRectClip, IntPtr hRgnUpdate, Win32Rect* lpRectUpdate, uint uFlags);

        [DllImport("user32.dll")]
        public static extern int SetScrollPos(IntPtr hwnd, int nBar, int nPos, bool bRedraw);

        [DllImport("user32.dll")]
        public static extern bool SetScrollRange(IntPtr hwnd, int nBar, int nMinPos, int nMaxPos, bool bRedraw);

        [DllImport("user32.dll")]
        public static extern bool ShowScrollBar(IntPtr hwnd, uint nBar, bool bShow);

        [DllImport("user32.dll")]
        public static extern bool EnableScrollBar(IntPtr hwnd, uint uSBFlags, uint uArrowFlags);

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
        public static extern int MessageBoxA(IntPtr hwnd, IntPtr lpszText, IntPtr lpszCaption, uint uType);

        [DllImport("user32.dll")]
        public static extern int MessageBoxW(IntPtr hwnd, IntPtr lpszText, IntPtr lpszCaption, uint uType);

        // Clipboard Functions
        [DllImport("user32.dll")]
        public static extern bool ChangeClipboardChain(IntPtr hwnd, IntPtr hWndNewNext);

        [DllImport("user32.dll")]
        public static extern IntPtr SetClipboardViewer(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern bool OpenClipboard(IntPtr hwnd);

        //// Caret Functions
        [DllImport("user32.dll")]
        public static extern bool CreateCaret(IntPtr hwnd, IntPtr hBitmap);

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
        public static extern IntPtr SetIcon(IntPtr hwnd, IntPtr hIcon, bool bBigIcon);

        [DllImport("user32.dll")]
        public static extern IntPtr GetIcon(IntPtr hwnd, bool bBigIcon);

        // Help Functions
        [DllImport("user32.dll")]
        public static extern bool WinHelpA(IntPtr hwnd, IntPtr lpszHelp, uint nCmd, uint dwData);

        [DllImport("user32.dll")]
        public static extern bool WinHelpW(IntPtr hwnd, IntPtr lpszHelp, uint nCmd, uint dwData);

        [DllImport("user32.dll")]
        public static extern bool SetWindowContextHelpId(IntPtr hwnd, uint dwContextHelpId);

        [DllImport("user32.dll")]
        public static extern uint GetWindowContextHelpId(IntPtr hwnd);

        // Misc. Operations
        [DllImport("user32.dll")]
        public static extern bool GetScrollInfo(IntPtr hwnd, int nBar, Win32ScrollInfo* lpScrollInfo);

        [DllImport("user32.dll")]
        public static extern int SetScrollInfo(IntPtr hwnd, int nBar, Win32ScrollInfo* lpScrollInfo, bool bRedraw);

        [DllImport("user32.dll")]
        public static extern bool IsDialogMessage(IntPtr hwnd, Win32Msg* lpMsg);

        [DllImport("user32.dll")]
        public static extern int GetWindowRgn(IntPtr hwnd, IntPtr hRgn);

        [DllImport("user32.dll")]
        public static extern int SetWindowRgn(IntPtr hwnd, IntPtr hRgn, bool bRedraw);

        [DllImport("user32.dll")]
        public static extern IntPtr DeferWindowPos(IntPtr hwnd, IntPtr hWinPosInfo, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(IntPtr hwnd, uint* processId);

        [DllImport("user32.dll")]
        public static extern bool IsWindow(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern bool IsWindowUnicode(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern bool ShowWindowAsync(IntPtr hwnd, int nCmdShow);
    }
}
