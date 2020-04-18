Imports System.IO
Imports System.Text
Imports CSVSerializer

Public Class SerializeObject
    Public Shared Async Function Serialize() As Task

        ' create some data
        Dim users = New List(of User) From {
            New User With { .Username = "super user", .Email = "superuser@example.com", .Age = 54 },
            New User With { .Username = "user", .Email = "user@example.com", .Age = 20 }
        }

        ' without object mapper
        Using memoryStream As New MemoryStream()
            Dim serializer = New Serializer()
            Await serializer.SerializeObjects(users, memoryStream).ConfigureAwait(true)

            Console.WriteLine(Encoding.UTF8.GetString(memoryStream.ToArray()))
            Console.ReadLine()
        End Using

        ' with object mapper
        Using memoryStream As New MemoryStream()
            Dim mapper = New ObjectMapper(Of User)
            With mapper
                .AddMap(Function(u) u.Username, 1, "Name")
                .AddMap(Function(u) u.Age, 0)
                .AddMap(Function(u) u.Email)
            End With

            Dim serializer = New Serializer()
            Await serializer.SerializeObjects(users, memoryStream, mapper).ConfigureAwait(true)

            Console.WriteLine(Encoding.UTF8.GetString(memoryStream.ToArray()))
            Console.ReadLine()
        End Using
    End Function
End Class
