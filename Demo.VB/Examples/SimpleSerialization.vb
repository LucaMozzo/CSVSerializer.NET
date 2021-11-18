Imports System.Text
Imports CsvSerializer

Public Class SimpleSerialization

    Public Shared Async Function Serialize() As Task
        Dim fileContent As String = My.Resources.Resource.users

        ' 1. Create a deserializer object with the file name as argument for the constructor
        Dim deserializer As New Deserializer(Encoding.UTF8.GetBytes(fileContent))

        ' 2. Use Document type to organize the extracted values
        Dim doc As Document = deserializer.Deserialize()

        ' 3. Loop through the headers
        For Each s In doc.Headers
            Console.Write(s + vbTab)
        Next
        Console.WriteLine("")

        ' 4. Loop through the rows
        For Each r In doc.Rows

            ' 5. Loop through the values
            For Each v In r.Values
                Console.Write(v.ToString() + vbTab)
            Next
            Console.WriteLine("")
        Next

        'Let's change some stuff...
        doc.Rows(0).UpdateValue(0, New Value(Of Object)("Michael"))
        doc.Rows(0).UpdateValue(2, New Value(Of Object)(64))

        ' 6. Serializer part - Create a serializer
        Dim serializer As New Serializer(doc, "output.csv")
        Await serializer.Serialize()
        Console.ReadLine()
    End Function
End Class
