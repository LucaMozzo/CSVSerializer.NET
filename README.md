# CSVSerializer.NET
A CSV serializer and deserializer library for .NET

CSVSerializer.NET is a class library to include full support for CSV in your own application.
This class library follows the [specifications](https://www.ietf.org/rfc/rfc4180.txt) of CSV.

<h2>Getting started</h2>

<h3>Object serialization example</h3>
Here's an example of how to serialize a list of objects in C# with custom header and ordering of columns

```
using (MemoryStream memoryStream = new MemoryStream())
{
    var mapper = new ObjectMapper<User>();
    mapper.AddMap(u => u.Username, 1, "Name") // index 1 with header "name"
        .AddMap(u => u.Age, 0) // index 0 with header "age"
        .AddMap(u => u.Email); // index 2 with header "email"
        .AddMap(u => u.DateOfBirth, header: "Date of birth", 
            transformFunction: dob => dob.ToString("yyyy-mm-dd")); // index 3 with header "Date of birth" and formatted as ISO 8601 date
    Serializer serializer = new Serializer();
    await serializer.SerializeObjects(users, memoryStream, mapper).ConfigureAwait(true);
    Console.WriteLine(Encoding.UTF8.GetString(memoryStream.ToArray()));
}
```