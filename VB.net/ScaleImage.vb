Public Shared Function ScaleImage(ByVal img As Bitmap, ByVal MaxSize As Integer) As Bitmap
    Dim W, H As Integer

    W = img.Width
    H = img.Height

    Dim newimg As Bitmap

    Dim Ratio As Double

    Dim NewW As Integer
    Dim NewH As Integer

    Ratio = CDbl(W) / CDbl(H)

    If Ratio > 1.0 Then
        NewW = MaxSize
        NewH = (MaxSize / Ratio)
    Else
        NewW = MaxSize * Ratio
        NewH = MaxSize
    End If

    newimg = New Bitmap(NewW, NewH)
    Dim g As Graphics = Graphics.FromImage(newimg)

    g.CompositingMode = CompositingMode.SourceOver
    g.CompositingQuality = CompositingQuality.HighQuality
    g.InterpolationMode = InterpolationMode.HighQualityBicubic
    g.SmoothingMode = SmoothingMode.HighQuality
    g.PixelOffsetMode = PixelOffsetMode.HighQuality

    g.DrawImage(img, New RectangleF(0, 0, NewW, NewH))
    g.Save()

    img.Dispose()
    img = Nothing


    Return newimg

End Function