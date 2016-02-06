Imports CSVSerializer
Imports System.IO
Module MainModule

    Sub Main()
        Const fileName As String = "users.csv"

        ' 1. Make sure the file exists, the exception Is Not handled
        If (Not File.Exists(fileName)) Then
            Console.WriteLine("File not found!")
            GoTo exitProgram
        End If

        ' 2. Create a deserializer object with the file name as argument for the constructor
        Dim deserializer As New Deserializer(fileName)

        ' 3. Use Document type to organize the extracted values
        Dim doc As Document = deserializer.Deserialize()

        ' 4. Loop through the headers
        For Each s In doc.Headers
            Console.Write(s + vbTab)
        Next
        Console.WriteLine("")

        ' 5. Loop through the rows
        For Each r In doc.Rows

            ' 6. Loop through the values
            For Each v In r.Values
                Console.Write(v.ToString() + vbTab)
            Next
            Console.WriteLine("")
        Next

        'Let's change some stuff...
        doc.Rows(0).UpdateValue(0, New Value(Of Object)("Michael"))
        doc.Rows(0).UpdateValue(2, New Value(Of Object)(64))

        ' 7. Serializer part - Create a serializer
        Dim Serializer As New Serializer(doc, "users_.csv")
        Serialize(Serializer) 'TODO: doesn't work
exitProgram: Console.ReadLine()
    End Sub
    'we need to create another method because Main can't be async
    Private Async Sub Serialize(ByVal s As Serializer)
        If Await s.Serialize() Then
            Console.WriteLine("Successfully serialized")
        Else
            Console.WriteLine("An error has occurred")
        End If
    End Sub
End Module
