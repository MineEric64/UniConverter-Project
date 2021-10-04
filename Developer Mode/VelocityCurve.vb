Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D

Public Class VelocityCurve
    Public Function SetImageOpacity(ByVal image As Image, ByVal opacity As Single) As Image
        Try
            Dim bmp As Bitmap = New Bitmap(image.Width, image.Height)

            Using gfx As Graphics = Graphics.FromImage(bmp)
                Dim matrix As ColorMatrix = New ColorMatrix()
                matrix.Matrix33 = opacity
                Dim attributes As ImageAttributes = New ImageAttributes()
                attributes.SetColorMatrix(matrix, ColorMatrixFlag.[Default], ColorAdjustType.Bitmap)
                gfx.DrawImage(image, New Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes)
            End Using

            Return bmp
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return Nothing
        End Try
    End Function

    Public Sub MakeToOne()
        Dim logo As New Bitmap(PictureBox1.Image)
        logo.MakeTransparent()

        ' Display the result.
        PictureBox1.Image = logo

        ' Copy the label onto the main picture.
        Dim template As New Bitmap(PictureBox2.Image)
        Dim gr As Graphics = Graphics.FromImage(template)
        gr.DrawImage(logo, template.Width - 2007, (template.Height - 1800) \ 6, 500, 450)

        ' Display the result.
        PictureBox2.Image = template
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        MakeToOne()
    End Sub

    Private Points As List(Of Point) = New List(Of Point)()
    Private ErrVal As Double = 0.5

    Private Sub VelocityCurve_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick
        If e.Button = MouseButtons.Right Then
            Points.Clear()
            Refresh()

            Return
        End If

        Points.Add(e.Location)
        Refresh()
    End Sub

    Private Sub VelocityCurve_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias

        For Each point As Point In Points
            e.Graphics.FillEllipse(Brushes.Black, point.X - 3, point.Y - 3, 5, 5)
        Next
        If Points.Count < 2 Then Return

        e.Graphics.DrawCurve(Pens.Red, Points.ToArray(), 1.0)
    End Sub

    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TrackBar1.Scroll
        ErrVal = TrackBar1.Value / 100.0R
        Label1.Text = ErrVal.ToString()

        Dim picture As New Bitmap(128, 128)
        Dim points As Point()
        points = DeveloperMode_Project.DrawVelocityGraph(0, 127, Double.Parse(Button3.Text), 0, 127, "Clip", ErrVal)

        Using gfx As Graphics = Graphics.FromImage(picture)
            Using brush As New SolidBrush(Color.White)
                gfx.FillRectangle(brush, 0, 0, picture.Width, picture.Height)
            End Using
        End Using

        For Each point In points
            picture.SetPixel(Math.Min(Math.Max(point.X, 0), 127), 127 - Math.Min(point.Y, 127), Color.OrangeRed)
        Next

        PictureBox3.Image = picture
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Button3.Text = "1.0" Then
            Button3.Text = "-1.0"
        Else
            Button3.Text = "1.0"
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Clipboard.SetImage(PictureBox3.Image)
    End Sub
End Class