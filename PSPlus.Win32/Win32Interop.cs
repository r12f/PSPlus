using System;
using System.Runtime.InteropServices;

namespace PSPlus.Win32
{
    public static class Win32Interop
    {
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hwnd, int show);

        // Attributes
        [DllImport("user32.dll")]
        public static extern BOOL DestroyWindow();

        [DllImport("user32.dll")]
        public static extern LONG GetWindowLong(_In_ int nIndex);

        [DllImport("user32.dll")]
        public static extern LONG_PTR GetWindowLongPtr(_In_ int nIndex);

        [DllImport("user32.dll")]
        public static extern LONG SetWindowLong(_In_ int nIndex, _In_ LONG dwNewLong);

        [DllImport("user32.dll")]
        public static extern LONG_PTR SetWindowLongPtr( _In_ int nIndex, _In_ LONG_PTR dwNewLong);

        [DllImport("user32.dll")]
        public static extern WORD GetWindowWord(_In_ int nIndex);

        [DllImport("user32.dll")]
        public static extern WORD SetWindowWord( _In_ int nIndex, _In_ WORD wNewWord);

        // Message Functions
        [DllImport("user32.dll")]
        public static extern LRESULT SendMessage( _In_ UINT message, _In_ WPARAM wParam = 0, _In_ LPARAM lParam = 0);

        [DllImport("user32.dll")]
        public static extern BOOL PostMessage( _In_ UINT message, _In_ WPARAM wParam = 0, _In_ LPARAM lParam = 0);

        [DllImport("user32.dll")]
        public static extern BOOL SendNotifyMessage( _In_ UINT message, _In_ WPARAM wParam = 0, _In_ LPARAM lParam = 0);

        // Window Text Functions
        [DllImport("user32.dll")]
        public static extern BOOL SetWindowText(_In_z_ LPCTSTR lpszString);

        [DllImport("user32.dll")]
        public static extern int GetWindowText( LPTSTR lpszStringBuf, _In_ int nMaxCount);
        [DllImport("user32.dll")]
        public static extern int GetWindowTextLength();

        // Font Functions
        [DllImport("user32.dll")]
        public static extern void SetFont(_In_ HFONT hFont, _In_ BOOL bRedraw = TRUE);

        [DllImport("user32.dll")]
        public static extern HFONT GetFont();

        // Menu Functions (non-child windows only)

        [DllImport("user32.dll")]
        public static extern HMENU GetMenu();

        [DllImport("user32.dll")]
        public static extern BOOL SetMenu(_In_ HMENU hMenu);

        [DllImport("user32.dll")]
        public static extern BOOL DrawMenuBar();

        [DllImport("user32.dll")]
        public static extern HMENU GetSystemMenu(_In_ BOOL bRevert);

        [DllImport("user32.dll")]
        public static extern BOOL HiliteMenuItem( _In_ HMENU hMenu, _In_ UINT uItemHilite, _In_ UINT uHilite);

        // Window Size and Position Functions
        [DllImport("user32.dll")]
        public static extern BOOL IsIconic();

        [DllImport("user32.dll")]
        public static extern BOOL IsZoomed();

        [DllImport("user32.dll")]
        public static extern BOOL MoveWindow(_In_ int x, _In_ int y, _In_ int nWidth, _In_ int nHeight, _In_ BOOL bRepaint = TRUE);

        [DllImport("user32.dll")]
        public static extern BOOL MoveWindow( _In_ LPCRECT lpRect, _In_ BOOL bRepaint = TRUE);

        [DllImport("user32.dll")]
        public static extern BOOL SetWindowPos( _In_opt_ HWND hWndInsertAfter, _In_ int x, _In_ int y, _In_ int cx, _In_ int cy, _In_ UINT nFlags);

        [DllImport("user32.dll")]
        public static extern BOOL SetWindowPos( _In_opt_ HWND hWndInsertAfter, _In_ LPCRECT lpRect, _In_ UINT nFlags);

        [DllImport("user32.dll")]
        public static extern UINT ArrangeIconicWindows();

        [DllImport("user32.dll")]
        public static extern BOOL BringWindowToTop();

        [DllImport("user32.dll")]
        public static extern BOOL GetWindowRect(_Out_ LPRECT lpRect);

        [DllImport("user32.dll")]
        public static extern BOOL GetClientRect(_Out_ LPRECT lpRect);

        [DllImport("user32.dll")]
        public static extern BOOL GetWindowPlacement(_Inout_ WINDOWPLACEMENT FAR* lpwndpl);

        [DllImport("user32.dll")]
        public static extern BOOL SetWindowPlacement(_In_ const WINDOWPLACEMENT FAR* lpwndpl);

        // Coordinate Mapping Functions
        [DllImport("user32.dll")]
        public static extern BOOL ClientToScreen(_Inout_ LPPOINT lpPoint);

        [DllImport("user32.dll")]
        public static extern BOOL ClientToScreen(_Inout_ LPRECT lpRect);

        [DllImport("user32.dll")]
        public static extern BOOL ScreenToClient(_Inout_ LPPOINT lpPoint);

        [DllImport("user32.dll")]
        public static extern BOOL ScreenToClient(_Inout_ LPRECT lpRect);

        [DllImport("user32.dll")]
        public static extern int MapWindowPoints( _In_ HWND hWndTo, LPPOINT lpPoint, _In_ UINT nCount);

        // Update and Painting Functions
        [DllImport("user32.dll")]
        public static extern HDC GetDC();

        [DllImport("user32.dll")]
        public static extern HDC GetWindowDC();

        [DllImport("user32.dll")]
        public static extern int ReleaseDC(_In_ HDC hDC);

        [DllImport("user32.dll")]
        public static extern void Print( _In_ HDC hDC, _In_ DWORD dwFlags);

        [DllImport("user32.dll")]
        public static extern void PrintClient( _In_ HDC hDC, _In_ DWORD dwFlags);

        [DllImport("user32.dll")]
        public static extern BOOL UpdateWindow();

        [DllImport("user32.dll")]
        public static extern void SetRedraw(_In_ BOOL bRedraw = TRUE);

        [DllImport("user32.dll")]
        public static extern BOOL GetUpdateRect( _In_opt_ LPRECT lpRect, _In_ BOOL bErase = FALSE);

        [DllImport("user32.dll")]
        public static extern int GetUpdateRgn( _In_ HRGN hRgn, _In_ BOOL bErase = FALSE);

        [DllImport("user32.dll")]
        public static extern BOOL Invalidate(_In_ BOOL bErase = TRUE);

        [DllImport("user32.dll")]
        public static extern BOOL InvalidateRect( _In_opt_ LPCRECT lpRect, _In_ BOOL bErase = TRUE);

        [DllImport("user32.dll")]
        public static extern BOOL ValidateRect(_In_opt_ LPCRECT lpRect);

        [DllImport("user32.dll")]
        public static extern void InvalidateRgn( _In_ HRGN hRgn, _In_ BOOL bErase = TRUE);

        [DllImport("user32.dll")]
        public static extern BOOL ValidateRgn(_In_opt_ HRGN hRgn);

        [DllImport("user32.dll")]
        public static extern BOOL ShowWindow(_In_ int nCmdShow);

        [DllImport("user32.dll")]
        public static extern BOOL IsWindowVisible();

        [DllImport("user32.dll")]
        public static extern BOOL ShowOwnedPopups(_In_ BOOL bShow = TRUE);

        [DllImport("user32.dll")]
        public static extern HDC GetDCEx( _In_ HRGN hRgnClip, _In_ DWORD flags);

        [DllImport("user32.dll")]
        public static extern BOOL LockWindowUpdate(_In_ BOOL bLock = TRUE);

        [DllImport("user32.dll")]
        public static extern BOOL RedrawWindow( _In_opt_ LPCRECT lpRectUpdate = NULL, _In_opt_ HRGN hRgnUpdate = NULL, _In_ UINT flags = RDW_INVALIDATE | RDW_UPDATENOW | RDW_ERASE);

        // Timer Functions

        [DllImport("user32.dll")]
        public static extern BOOL KillTimer(_In_ UINT_PTR nIDEvent);

        // Window State Functions
        [DllImport("user32.dll")]
        public static extern BOOL IsWindowEnabled();

        [DllImport("user32.dll")]
        public static extern BOOL EnableWindow(_In_ BOOL bEnable = TRUE);

        [DllImport("user32.dll")]
        public static extern HWND SetActiveWindow();

        [DllImport("user32.dll")]
        public static extern HWND SetCapture();

        [DllImport("user32.dll")]
        public static extern HWND SetFocus();

        // Dialog-Box Item Functions

        [DllImport("user32.dll")]
        public static extern BOOL CheckDlgButton( _In_ int nIDButton, _In_ UINT nCheck);

        [DllImport("user32.dll")]
        public static extern BOOL CheckRadioButton( _In_ int nIDFirstButton, _In_ int nIDLastButton, _In_ int nIDCheckButton);

        [DllImport("user32.dll")]
        public static extern int DlgDirList( _Inout_z_ LPTSTR lpPathSpec, _In_ int nIDListBox, _In_ int nIDStaticPath, _In_ UINT nFileType);

        [DllImport("user32.dll")]
        public static extern int DlgDirListComboBox( _Inout_z_ LPTSTR lpPathSpec, _In_ int nIDComboBox, _In_ int nIDStaticPath, _In_ UINT nFileType);

        [DllImport("user32.dll")]
        public static extern BOOL DlgDirSelect( LPTSTR lpString, _In_ int nCount, _In_ int nIDListBox);

        [DllImport("user32.dll")]
        public static extern BOOL DlgDirSelectComboBox( LPTSTR lpString, _In_ int nCount, _In_ int nIDComboBox);

        [DllImport("user32.dll")]
        public static extern UINT GetDlgItemInt( _In_ int nID, _Out_opt_ BOOL* lpTrans = NULL, _In_ BOOL bSigned = TRUE);

        [DllImport("user32.dll")]
        public static extern UINT GetDlgItemText( _In_ int nID, LPTSTR lpStr, _In_ int nMaxCount);

        [DllImport("user32.dll")]
        public static extern CWindow GetNextDlgGroupItem( _In_ HWND hWndCtl, _In_ BOOL bPrevious = FALSE);

        [DllImport("user32.dll")]
        public static extern CWindow GetNextDlgTabItem( _In_ HWND hWndCtl, _In_ BOOL bPrevious = FALSE);

        [DllImport("user32.dll")]
        public static extern UINT IsDlgButtonChecked(_In_ int nIDButton);

        [DllImport("user32.dll")]
        public static extern LRESULT SendDlgItemMessage( _In_ int nID, _In_ UINT message, _In_ WPARAM wParam = 0, _In_ LPARAM lParam = 0);

        [DllImport("user32.dll")]
        public static extern BOOL SetDlgItemInt( _In_ int nID, _In_ UINT nValue, _In_ BOOL bSigned = TRUE);

        [DllImport("user32.dll")]
        public static extern BOOL SetDlgItemText( _In_ int nID, _In_z_ LPCTSTR lpszString);


    // Scrolling Functions

        [DllImport("user32.dll")]
        public static extern int GetScrollPos(_In_ int nBar);

        [DllImport("user32.dll")]
        public static extern BOOL GetScrollRange( _In_ int nBar, _Out_ LPINT lpMinPos, _Out_ LPINT lpMaxPos);

        [DllImport("user32.dll")]
        public static extern BOOL ScrollWindow( _In_ int xAmount, _In_ int yAmount, _In_opt_ LPCRECT lpRect = NULL, _In_opt_ LPCRECT lpClipRect = NULL);

//	int ScrollWindowEx( _In_ int dx, _In_ int dy, _In_opt_ LPCRECT lpRectScroll, _In_opt_ LPCRECT lpRectClip, _In_opt_ HRGN hRgnUpdate, _In_opt_ LPRECT lpRectUpdate, _In_ UINT uFlags) throw()
//	{
//		ATLASSERT(::IsWindow(m_hWnd));
//		return ::ScrollWindowEx(m_hWnd, dx, dy, lpRectScroll, lpRectClip, hRgnUpdate, lpRectUpdate, uFlags);
//	}

//	int ScrollWindowEx( _In_ int dx, _In_ int dy, _In_ UINT uFlags, _In_opt_ LPCRECT lpRectScroll = NULL, _In_opt_ LPCRECT lpRectClip = NULL, _In_opt_ HRGN hRgnUpdate = NULL, _In_opt_ LPRECT lpRectUpdate = NULL) throw()
//	{
//		ATLASSERT(::IsWindow(m_hWnd));
//		return ::ScrollWindowEx(m_hWnd, dx, dy, lpRectScroll, lpRectClip, hRgnUpdate, lpRectUpdate, uFlags);
//	}

//	int SetScrollPos( _In_ int nBar, _In_ int nPos, _In_ BOOL bRedraw = TRUE) throw()
//	{
//		ATLASSERT(::IsWindow(m_hWnd));
//		return ::SetScrollPos(m_hWnd, nBar, nPos, bRedraw);
//	}

//	BOOL SetScrollRange( _In_ int nBar, _In_ int nMinPos, _In_ int nMaxPos, _In_ BOOL bRedraw = TRUE) throw()
//	{
//		ATLASSERT(::IsWindow(m_hWnd));
//		return ::SetScrollRange(m_hWnd, nBar, nMinPos, nMaxPos, bRedraw);
//	}

//	BOOL ShowScrollBar( _In_ UINT nBar, _In_ BOOL bShow = TRUE) throw()
//	{
//		ATLASSERT(::IsWindow(m_hWnd));
//		return ::ShowScrollBar(m_hWnd, nBar, bShow);
//	}

//	BOOL EnableScrollBar( _In_ UINT uSBFlags, _In_ UINT uArrowFlags = ESB_ENABLE_BOTH) throw()
//	{
//		ATLASSERT(::IsWindow(m_hWnd));
//		return ::EnableScrollBar(m_hWnd, uSBFlags, uArrowFlags);
//	}

    // Window Access Functions

//	CWindow ChildWindowFromPoint(_In_ POINT point) const throw()
//	{
//		ATLASSERT(::IsWindow(m_hWnd));
//		return CWindow(::ChildWindowFromPoint(m_hWnd, point));
//	}

//	CWindow ChildWindowFromPointEx( _In_ POINT point, _In_ UINT uFlags) const throw()
//	{
//		ATLASSERT(::IsWindow(m_hWnd));
//		return CWindow(::ChildWindowFromPointEx(m_hWnd, point, uFlags));
//	}

//	CWindow GetTopWindow() const throw()
//	{
//		ATLASSERT(::IsWindow(m_hWnd));
//		return CWindow(::GetTopWindow(m_hWnd));
//	}

//	CWindow GetWindow(_In_ UINT nCmd) const throw()
//	{
//		ATLASSERT(::IsWindow(m_hWnd));
//		return CWindow(::GetWindow(m_hWnd, nCmd));
//	}

//	CWindow GetLastActivePopup() const throw()
//	{
//		ATLASSERT(::IsWindow(m_hWnd));
//		return CWindow(::GetLastActivePopup(m_hWnd));
//	}

//	BOOL IsChild(_In_ HWND hWnd) const throw()
//	{
//		ATLASSERT(::IsWindow(m_hWnd));
//		return ::IsChild(m_hWnd, hWnd);
//	}

//	CWindow GetParent() const throw()
//	{
//		ATLASSERT(::IsWindow(m_hWnd));
//		return CWindow(::GetParent(m_hWnd));
//	}

//	CWindow SetParent(_In_ HWND hWndNewParent) throw()
//	{
//		ATLASSERT(::IsWindow(m_hWnd));
//		return CWindow(::SetParent(m_hWnd, hWndNewParent));
//	}

//// Window Tree Access

//	int GetDlgCtrlID() const throw()
//	{
//		ATLASSERT(::IsWindow(m_hWnd));
//		return ::GetDlgCtrlID(m_hWnd);
//	}

//	int SetDlgCtrlID(_In_ int nID) throw()
//	{
//		ATLASSERT(::IsWindow(m_hWnd));
//		return (int)::SetWindowLong(m_hWnd, GWL_ID, nID);
//	}

//	CWindow GetDlgItem(_In_ int nID) const throw()
//	{
//		ATLASSERT(::IsWindow(m_hWnd));
//		return CWindow(::GetDlgItem(m_hWnd, nID));
//	}

    // Alert Functions

//	BOOL FlashWindow(_In_ BOOL bInvert) throw()
//	{
//		ATLASSERT(::IsWindow(m_hWnd));
//		return ::FlashWindow(m_hWnd, bInvert);
//	}

//	int MessageBox( _In_z_ LPCTSTR lpszText, _In_opt_z_ LPCTSTR lpszCaption = _T(""), _In_ UINT nType = MB_OK) throw()
//	{
//		ATLASSERT(::IsWindow(m_hWnd));
//		return ::MessageBox(m_hWnd, lpszText, lpszCaption, nType);
//	}

    // Clipboard Functions

//	BOOL ChangeClipboardChain(_In_ HWND hWndNewNext) throw()
//	{
//		ATLASSERT(::IsWindow(m_hWnd));
//		return ::ChangeClipboardChain(m_hWnd, hWndNewNext);
//	}

//	HWND SetClipboardViewer() throw()
//	{
//		ATLASSERT(::IsWindow(m_hWnd));
//		return ::SetClipboardViewer(m_hWnd);
//	}

//	BOOL OpenClipboard() throw()
//	{
//		ATLASSERT(::IsWindow(m_hWnd));
//		return ::OpenClipboard(m_hWnd);
//	}

//// Caret Functions

//	BOOL CreateCaret(_In_ HBITMAP hBitmap) throw()
//	{
//		ATLASSERT(::IsWindow(m_hWnd));
//		return ::CreateCaret(m_hWnd, hBitmap, 0, 0);
//	}

//	BOOL CreateSolidCaret(_In_ int nWidth, _In_ int nHeight) throw()
//	{
//		ATLASSERT(::IsWindow(m_hWnd));
//		return ::CreateCaret(m_hWnd, (HBITMAP)0, nWidth, nHeight);
//	}

//	BOOL CreateGrayCaret(_In_ int nWidth, _In_ int nHeight) throw()
//	{
//		ATLASSERT(::IsWindow(m_hWnd));
//		return ::CreateCaret(m_hWnd, (HBITMAP)1, nWidth, nHeight);
//	}

//	BOOL HideCaret() throw()
//	{
//		ATLASSERT(::IsWindow(m_hWnd));
//		return ::HideCaret(m_hWnd);
//	}

//	BOOL ShowCaret() throw()
//	{
//		ATLASSERT(::IsWindow(m_hWnd));
//		return ::ShowCaret(m_hWnd);
//	}

//#ifdef _INC_SHELLAPI
//// Drag-Drop Functions
//	void DragAcceptFiles(_In_ BOOL bAccept = TRUE) throw()
//	{
//		ATLASSERT(::IsWindow(m_hWnd)); ::DragAcceptFiles(m_hWnd, bAccept);
//	}
//#endif

    // Icon Functions

//	HICON SetIcon( _In_ HICON hIcon, _In_ BOOL bBigIcon = TRUE) throw()
//	{
//		ATLASSERT(::IsWindow(m_hWnd));
//		return (HICON)::SendMessage(m_hWnd, WM_SETICON, bBigIcon, (LPARAM)hIcon);
//	}

//	HICON GetIcon(_In_ BOOL bBigIcon = TRUE) const throw()
//	{
//		ATLASSERT(::IsWindow(m_hWnd));
//		return (HICON)::SendMessage(m_hWnd, WM_GETICON, bBigIcon, 0);
//	}

    // Help Functions

//	BOOL WinHelp( _In_z_ LPCTSTR lpszHelp, _In_ UINT nCmd = HELP_CONTEXT, _In_ DWORD dwData = 0) throw()
//	{
//		ATLASSERT(::IsWindow(m_hWnd));
//		return ::WinHelp(m_hWnd, lpszHelp, nCmd, dwData);
//	}

//	BOOL SetWindowContextHelpId(_In_ DWORD dwContextHelpId) throw()
//	{
//		ATLASSERT(::IsWindow(m_hWnd));
//		return ::SetWindowContextHelpId(m_hWnd, dwContextHelpId);
//	}

//	DWORD GetWindowContextHelpId() const throw()
//	{
//		ATLASSERT(::IsWindow(m_hWnd));
//		return ::GetWindowContextHelpId(m_hWnd);
//	}


    // Misc. Operations

//	BOOL GetScrollInfo( _In_ int nBar, _Inout_ LPSCROLLINFO lpScrollInfo) throw()
//	{
//		ATLASSERT(::IsWindow(m_hWnd));
//		return ::GetScrollInfo(m_hWnd, nBar, lpScrollInfo);
//	}
//	int SetScrollInfo( _In_ int nBar, _In_ LPSCROLLINFO lpScrollInfo, _In_ BOOL bRedraw = TRUE) throw()
//	{
//		ATLASSERT(::IsWindow(m_hWnd));
//		return ::SetScrollInfo(m_hWnd, nBar, lpScrollInfo, bRedraw);
//	}
//	BOOL IsDialogMessage(_In_ LPMSG lpMsg) throw()
//	{
//		ATLASSERT(::IsWindow(m_hWnd));
//		return ::IsDialogMessage(m_hWnd, lpMsg);
//	}

//	int GetWindowRgn(_Inout_ HRGN hRgn) throw()
//	{
//		ATLASSERT(::IsWindow(m_hWnd));
//		return ::GetWindowRgn(m_hWnd, hRgn);
//	}
//	int SetWindowRgn( _In_opt_ HRGN hRgn, _In_ BOOL bRedraw = FALSE) throw()
//	{
//		ATLASSERT(::IsWindow(m_hWnd));
//		return ::SetWindowRgn(m_hWnd, hRgn, bRedraw);
//	}
//	HDWP DeferWindowPos( _In_ HDWP hWinPosInfo, _In_ HWND hWndInsertAfter, _In_ int x, _In_ int y, _In_ int cx, _In_ int cy, _In_ UINT uFlags) throw()
//	{
//		ATLASSERT(::IsWindow(m_hWnd));
//		return ::DeferWindowPos(hWinPosInfo, m_hWnd, hWndInsertAfter, x, y, cx, cy, uFlags);
//	}
//	DWORD GetWindowThreadID() throw()
//	{
//		ATLASSERT(::IsWindow(m_hWnd));
//		return ::GetWindowThreadProcessId(m_hWnd, NULL);
//	}
//	DWORD GetWindowProcessID() throw()
//	{
//		ATLASSERT(::IsWindow(m_hWnd));
//		DWORD dwProcessID;
//		::GetWindowThreadProcessId(m_hWnd, &dwProcessID);
//		return dwProcessID;
//	}
//	BOOL IsWindow() const throw()
//	{
//		return ::IsWindow(m_hWnd);
//	}
//	BOOL IsWindowUnicode() const throw()
//	{
//		ATLASSERT(::IsWindow(m_hWnd));
//		return ::IsWindowUnicode(m_hWnd);
//	}
//	BOOL ShowWindowAsync(_In_ int nCmdShow) throw()
//	{
//		ATLASSERT(::IsWindow(m_hWnd));
//		return ::ShowWindowAsync(m_hWnd, nCmdShow);
//	}
    }
}
