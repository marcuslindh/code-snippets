Public Class EmailValidator
	'http://blog.onyxbits.de/validating-email-addresses-with-a-regex-do-yourself-a-favor-and-dont-391/
    Public Shared Function isValid(Input As String) As Boolean
        If String.IsNullOrEmpty(Input) Then Return False

        Dim state As Integer = 0
        Dim ch As String = ""
        Dim index As Integer = 0
        Dim mark As Integer = 0
        Dim local As String = ""
        Dim domain As New ArrayList

        While index <= Input.Length And Not state = -1
            'Dealing with a char instead of char[] makes life a lot easier!
            If index = Input.Length Then
                ch = "\0"
            Else
                ch = Input.Substring(index, 1)
                If ch = "\0" Then
                    'but the terminator may not be part of the input!
                    Return False
                End If
            End If

            Select Case state
                Case 0
                    If (ch >= "a" And ch <= "z") Or
                        (ch >= "A" And ch <= "Z") Or
                        (ch >= "0" And ch <= "9") Or
                        ch = "_" Or ch = "-" Or ch = "+" Then
                        state = 1
                        Exit Select
                    End If
                    'Unexpected Character -> Error state
                    state = -1
                    Exit Select
                Case 1
                    ' Consume {atext}
                    If (ch >= "a" And ch <= "z") Or
                        (ch >= "A" And ch <= "Z") Or
                        (ch >= "0" And ch <= "9") Or
                        ch = "_" Or ch = "-" Or ch = "+" Then
                        Exit Select
                    End If
                    If ch = "." Then
                        state = 2
                        Exit Select
                    End If
                    If (ch = "@") Then ' Endof local part
                        local = New String(Input, 0, index - mark)
                        mark = index + 1
                        state = 3
                        Exit Select
                    End If
                    'Unexpected Character -> Error state
                    state = -1
                    Exit Select
                Case 2
                    ' Transition on {atext}
                    If (ch >= "a" And ch <= "z") Or
                        (ch >= "A" And ch <= "Z") Or
                        (ch >= "0" And ch <= "9") Or
                        ch = "_" Or ch = "-" Or ch = "+" Then
                        state = 1
                        Exit Select
                    End If
                    ' Unexpected Character -> Error state
                    state = -1
                    Exit Select
                Case 3
                    ' Transition on {alnum}
                    If (ch >= "a" And ch <= "z") Or
                        (ch >= "0" And ch <= "9") Or
                        (ch >= "A" And ch <= "Z") Then
                        state = 4
                        Exit Select
                    End If
                    ' Unexpected Character -> Error state
                    state = -1
                    Exit Select


                Case 4
                    ' Consume {alnum}
                    If (ch >= "a" And ch <= "z") Or (ch >= "0" And ch <= "9") Or (ch >= "A" And ch <= "Z") Then
                        Exit Select
                    End If
                    If ch = "-" Then
                        state = 5
                        Exit Select
                    End If
                    If ch = "." Then
                        domain.Add(New String(Input, mark, index - mark))
                        mark = index + 1
                        state = 5
                        Exit Select
                    End If
                    ' Match EOL
                    If ch = "\0" Then
                        domain.Add(New String(Input, mark, index - mark))
                        state = 6
                        Exit Select ' EOL -> Finish
                    End If
                    ' Unexpected Character -> Error state
                    state = -1
                    Exit Select


                Case 5
                    If (ch >= "a" And ch <= "z") Or
                        (ch >= "0" And ch <= "9") Or
                        (ch >= "A" And ch <= "Z") Then
                        state = 4
                        Exit Select
                    End If
                    If ch = "-" Then
                        Exit Select
                    End If
                    ' Unexpected Character -> Error state
                    state = -1
                    Exit Select
                Case 6
                    ' Success! (we don't really get here, though)
                    Exit Select
            End Select
            index += 1
        End While

        ' Sanity checks

        ' Input not accepted
        If Not state = 6 Then
            Return False
        End If

        ' Require at least a second level domain
        If (domain.Count < 2) Then
            Return False
        End If

        ' RFC 5321 limits the length of the local part
        If (local.Length() > 64) Then
            Return False
        End If
        ' RFC 5321 limits the total length of an address
        If (Input.Length > 254) Then
            Return False
        End If
        ' TLD must only consist of letters and be at least two characters long.
        index = Input.Length - 1
        While (index > 0)


            ch = Input(index)
            If (ch = "." And Input.Length - index > 2) Then
                Return True
            End If
            If Not ((ch >= "a" And ch <= "z") Or (ch >= "A" And ch <= "Z")) Then
                Return False
            End If
            index -= 1
        End While

        Return True
    End Function
End Class