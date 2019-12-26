Public Partial Class Map
    
    Private Structure Pair(Of T1, T2)
        Public ReadOnly First As T1
        Public ReadOnly Second As T2

        Public Sub New(ByVal first As T1, ByVal second As T2)
            Me.First = first
            Me.Second = second
        End Sub
    End Structure

End Class
