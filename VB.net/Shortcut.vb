Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Runtime.InteropServices.ComTypes
Imports System.Text

Public Class Shortcut

    Public Shared Sub Create(Name As String, Description As String, Path As String, ShortcutPath As String)
        Dim link As IShellLink = DirectCast(New ShellLink(), IShellLink)
        link.SetDescription(Description)
        link.SetPath(Path)
        Dim file As IPersistFile = DirectCast(link, IPersistFile)
        file.Save(IO.Path.Combine(ShortcutPath, Name & ".lnk"), False)
    End Sub

End Class




<ComImport>
<Guid("00021401-0000-0000-C000-000000000046")>
Friend Class ShellLink
End Class

<ComImport>
<InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>
<Guid("000214F9-0000-0000-C000-000000000046")>
Friend Interface IShellLink
    Sub GetPath(<Out, MarshalAs(UnmanagedType.LPWStr)> pszFile As StringBuilder, cchMaxPath As Integer, ByRef pfd As IntPtr, fFlags As Integer)
    Sub GetIDList(ByRef ppidl As IntPtr)
    Sub SetIDList(pidl As IntPtr)
    Sub GetDescription(<Out, MarshalAs(UnmanagedType.LPWStr)> pszName As StringBuilder, cchMaxName As Integer)
    Sub SetDescription(<MarshalAs(UnmanagedType.LPWStr)> pszName As String)
    Sub GetWorkingDirectory(<Out, MarshalAs(UnmanagedType.LPWStr)> pszDir As StringBuilder, cchMaxPath As Integer)
    Sub SetWorkingDirectory(<MarshalAs(UnmanagedType.LPWStr)> pszDir As String)
    Sub GetArguments(<Out, MarshalAs(UnmanagedType.LPWStr)> pszArgs As StringBuilder, cchMaxPath As Integer)
    Sub SetArguments(<MarshalAs(UnmanagedType.LPWStr)> pszArgs As String)
    Sub GetHotkey(ByRef pwHotkey As Short)
    Sub SetHotkey(wHotkey As Short)
    Sub GetShowCmd(ByRef piShowCmd As Integer)
    Sub SetShowCmd(iShowCmd As Integer)
    Sub GetIconLocation(<Out, MarshalAs(UnmanagedType.LPWStr)> pszIconPath As StringBuilder, cchIconPath As Integer, ByRef piIcon As Integer)
    Sub SetIconLocation(<MarshalAs(UnmanagedType.LPWStr)> pszIconPath As String, iIcon As Integer)
    Sub SetRelativePath(<MarshalAs(UnmanagedType.LPWStr)> pszPathRel As String, dwReserved As Integer)
    Sub Resolve(hwnd As IntPtr, fFlags As Integer)
    Sub SetPath(<MarshalAs(UnmanagedType.LPWStr)> pszFile As String)
End Interface
