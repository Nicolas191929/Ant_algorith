Option Strict Off

Imports System.Runtime.CompilerServices

Module FilterLoadHelper

    Public Function SupportedPictureFilters() As String
        ' Load file filters
        Dim supportedExtensions As Object() = { _
            New With {.Key = "*.bmp", .Value = "Windows Bitmap"}, _
            New With {.Key = "*.gif", .Value = "Graphics Interchange Format"}, _
            New With {.Key = "*.png", .Value = "Portable Network Graphic"}, _
            New With {.Key = "*.jpg;*.jpeg", .Value = "JPEG file"}, _
            New With {.Key = "*.tif;*.tiff", .Value = "TIFF file"} _
        }

        Dim extensions = From x In supportedExtensions Select DirectCast(x.Key, String)
        Dim descriptions = From x In supportedExtensions Select DirectCast(x.Value, String)
        Dim pairs = From x In supportedExtensions _
                    Select DirectCast(x.Value, String) & " (" & DirectCast(x.Key, String) & ")|" & DirectCast(x.Key, String)

        Dim ext As New System.Text.StringBuilder()

        ext.Append("All supported files|")
        ext.Append(String.Join(";", extensions.ToArray()))
        ext.Append("|"c)
        ext.Append(String.Join("|", pairs.ToArray()))
        Return ext.ToString()
    End Function

End Module
