Public Class Map

    Private WithEvents m_Display As PictureBox
    Private m_Bitmap As Bitmap
    Private m_BackgroundPicture As Image
    Private ReadOnly m_Cities As New List(Of City)()
    Private ReadOnly m_Roads As New List(Of Pair(Of City, City))()
    Private m_CityMap As New Dictionary(Of AntColonyTSP.City, City)
    Private m_RoadMap As Dictionary(Of Road, Pair(Of City, City))
    Private m_ShowLabels As Boolean

    Public Sub New(ByVal display As PictureBox)
        m_Display = display
        m_Bitmap = New Bitmap(display.Width, display.Height)
    End Sub

    Public Property ShowLabels() As Boolean
        Get
            Return m_ShowLabels
        End Get
        Set(ByVal value As Boolean)
            m_ShowLabels = value
            Redraw()
        End Set
    End Property

    Public Property BackgroundPicture() As Image
        Get
            Return m_BackgroundPicture
        End Get
        Set(ByVal value As Image)
            m_BackgroundPicture = value
            Redraw()
        End Set
    End Property

    Public ReadOnly Property CityCount() As Integer
        Get
            Return m_Cities.Count
        End Get
    End Property

    Public Sub AddCity(ByVal location As Point)
        Dim city As New City(location, NameFromLocation(location), Nothing)

        For Each c In m_Cities
            m_Roads.Add(New Pair(Of City, City)(city, c))
        Next

        m_Cities.Add(city)
        Redraw()
    End Sub

    Public Function FindCity(ByVal location As Point) As City
        Return m_Cities.Find(Function(c) City.Distance(location, c.Location) <= City.Radius * 2)
    End Function

    Public Sub RemoveCity(ByVal location As Point)
        Dim city = FindCity(location)
        m_Cities.Remove(city)

        m_Roads.RemoveAll(Function(road) road.First Is city OrElse road.Second Is city)
        Redraw()
    End Sub

    Public Sub Clear()
        m_Cities.Clear()
        m_Roads.Clear()
        Redraw()
    End Sub

    Public Function ConstructTsp() As World
        Dim wb As New WorldBuilder()

        m_CityMap.Clear()
        m_CityMap = New Dictionary(Of AntColonyTSP.City, City)(m_Cities.Count)

        For Each c In m_Cities
            c.TspCity = wb.AddCity(c.Name)
            m_CityMap.Add(c.TspCity, c)
        Next

        m_RoadMap = New Dictionary(Of Road, Pair(Of City, City))(CInt(m_Cities.Count ^ 2))

        For Each road In m_Roads
            m_RoadMap.Add( _
                wb.AddRoad( _
                    City.Distance(road.First.Location, road.Second.Location), _
                    road.First.TspCity, road.Second.TspCity _
                ), road _
            )
        Next

        Return New World(wb)
    End Function

    Private Shared Function NameFromLocation(ByVal location As Point) As String
        Return location.ToString()
    End Function

    Public Sub DrawBestTour(ByVal tour As IEnumerable(Of AntColonyTSP.City))
        If tour Is Nothing Then
            Return
        End If

        Using g = Graphics.FromImage(m_Bitmap)
            DrawTour(tour, g, Color.Red)
        End Using
        m_Display.Invalidate()
    End Sub

    Public Sub Redraw(Optional ByVal world As World = Nothing, Optional ByVal e As UpdateEventArgs = Nothing)
        Using g = Graphics.FromImage(m_Bitmap)
            g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
            g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAliasGridFit

            '
            ' Draw the background picture.
            '

            If m_BackgroundPicture Is Nothing Then
                g.Clear(Color.White)
            Else
                g.DrawImage(m_BackgroundPicture, 0, 0, m_Bitmap.Width, m_Bitmap.Height)
            End If

            '
            ' Draw the roads.
            '

            If world Is Nothing Then
                ' We're not currently running the heuristic: Draw standard roads.
                Using p As New Pen(Color.FromArgb(26, Color.Blue), 2)
                    For Each road In m_Roads
                        g.DrawLine(p, road.First.Location, road.Second.Location)
                    Next
                End Using
            Else
                ' While running the heuristic, we don't draw the usual roads.
                ' Instead, we draw the roads according to their pheromone level.

                Dim sum_of_pheromones = Aggregate road In world.Roads Into Sum(road.PheromoneLevel)
                Dim factor = 255 * world.Roads.Count

                For Each road In world.Roads
                    Dim line = m_RoadMap(road)
                    Dim alpha = Math.Min(Math.Max(CInt(road.PheromoneLevel / sum_of_pheromones * factor), 0), 255)
                    Using p As New Pen(Color.FromArgb(alpha, Color.Blue), 2)
                        g.DrawLine(p, line.First.Location, line.Second.Location)
                    End Using
                Next
            End If

            '
            ' Draw the cities.
            '

            Using p As New Pen(Color.Black, City.Radius / 3)
                For Each c In m_Cities
                    g.DrawEllipse(p, c.Location.X - City.Radius, c.Location.Y - City.Radius, City.Radius * 2, City.Radius * 2)
                Next
            End Using

            '
            ' Draw the labels.
            '

            If ShowLabels Then
                Using b As New SolidBrush(Color.FromArgb(128, Color.Blue))
                    For Each road In m_Roads
                        Dim text_pos As _
                            New PointF( _
                                (road.First.Location.X + road.Second.Location.X) / 2.0F, _
                                (road.First.Location.Y + road.Second.Location.Y) / 2.0F _
                            )
                        g.DrawString( _
                            City.Distance(road.First.Location, road.Second.Location).ToString("0"), _
                            m_Display.Font, b, text_pos _
                        )
                    Next
                End Using

                Using b As New SolidBrush(Color.Black)
                    For Each c In m_Cities
                        Dim text_pos = c.Location
                        text_pos.Offset(City.Radius + 5, 0)
                        g.DrawString(c.Name, m_Display.Font, b, text_pos)
                    Next
                End Using
            End If

            '
            ' If we're running the heuristic, draw the currently best tour.
            '

            If e IsNot Nothing Then
                If e.BestTour IsNot Nothing Then
                    DrawTour(e.BestTour, g, Color.FromArgb(192, Color.DarkGreen))
                End If
            End If
        End Using

        m_Display.Invalidate()
    End Sub

    Private Sub DrawTour(ByVal tour As IEnumerable(Of AntColonyTSP.City), ByVal g As Graphics, ByVal color As Color)
        Using p As New Pen(color, 2)
            Dim i = tour.GetEnumerator()
            i.MoveNext()
            Dim first = m_CityMap(i.Current)
            Dim c1 = first

            Do While i.MoveNext()
                Dim c2 = m_CityMap(i.Current)
                g.DrawLine(p, c1.Location, c2.Location)
                c1 = c2
            Loop

            g.DrawLine(p, c1.Location, first.Location)
        End Using
    End Sub

    Private Sub Display_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles m_Display.Resize
        m_Bitmap = New Bitmap(m_Display.Width, m_Display.Height)
        Redraw()
    End Sub

    Private Sub Display_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles m_Display.Paint
        e.Graphics.DrawImageUnscaled(m_Bitmap, 0, 0)
    End Sub

End Class
