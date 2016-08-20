Public Function FileSize(s As Long) As String
    If s < 1024 ^ 2 Then
        Return Math.Round((s / 1024), 1) & " KB"
    ElseIf s < 1024 ^ 3 Then
        Return Math.Round((s / 1024 ^ 2), 1) & " MB"
    ElseIf s < 1024 ^ 4 Then
        Return Math.Round((s / 1024 ^ 3), 1) & " GB"
    ElseIf s < 1024 ^ 5 Then
        Return Math.Round((s / 1024 ^ 4), 1) & " TB"
    End If
End Function