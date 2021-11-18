# CSVSerializer.NET
A CSV serializer and deserializer library for .NET

Currently under development.

CSVSerializer.NET is a .NET class library to include full support for CSV in your own application.
This class library follows the [specifications](https://www.ietf.org/rfc/rfc4180.txt) of CSV.

<h2>Getting started</h2>

<h3>Installation</h3>
<b>DLL:</b> In the release section of this Github project you can download the DLL file <br/>
<b>Nuget (recommended):</b> Run <code>Install-Package CSVSerializer.Net -Version 2.0.0</code> in the Package Management Console

<h3>Object serialization example</h3>
Here's an example of how to serialize a list of objects in C# with custom header and ordering of columns

```
using (MemoryStream memoryStream = new MemoryStream())
{
    var mapper = new ObjectMapper<User>();
    mapper.AddMap(u => u.Username, 1, "Name") // index 1 with header "name"
        .AddMap(u => u.Age, 0) // index 0 with header "age"
        .AddMap(u => u.Email); // index 2 with header "email"
    Serializer serializer = new Serializer();
    await serializer.SerializeObjects(users, memoryStream, mapper).ConfigureAwait(true);
    Console.WriteLine(Encoding.UTF8.GetString(memoryStream.ToArray()));
}
```

<h2>Minimum requirements</h2>
<i>For contributing</i>
<ul>
<li>Visual Studio 2015</li>
<li>.NET Standard 2.0 SDK</li>
</ul>
<i>For using it</i>
<ul>
<li>.NET Standard 2.0+</li>
<li>.NET Core 2.0+</li>
<li>.NET Framework 4.6.1+</li>
</ul>