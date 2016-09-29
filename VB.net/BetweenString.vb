Public Shared Function BetweenString(StartString As String, text As String, EndString As String) As String
	Try
		Dim _Start As Integer = text.IndexOf(StartString)
		Dim _End As Integer = text.IndexOf(EndString)

		_Start += StartString.Length
		_End -= _Start

		If _Start > -1 And _End > -1 Then
			Return text.Substring(_Start, _End)
		End If
		Return ""
	Catch ex As Exception
		Return ""
	End Try
End Function