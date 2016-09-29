Public Shared Function AllIndexesOf(str As String, find As String) As List(Of Integer)
	If String.IsNullOrWhiteSpace(str) Or String.IsNullOrEmpty(str) Then Return New List(Of Integer)
		Dim indexes As New List(Of Integer)
		Dim startFrom As Integer = 0

		For i As Integer = 0 To str.Length
			Dim index As Integer = str.IndexOf(find, startFrom)
			If index = -1 Then
				Exit For
			End If
			startFrom = index + find.Length
			indexes.Add(index)
		Next
    Return indexes
End Function