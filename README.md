# CSVSerializer.NET
A CSV serializer and deserializer library for .NET

Currently under development.

CSVSerializer.NET is a .NET class library to include full support for CSV in your own application.
This class library follows the [specifications](https://www.ietf.org/rfc/rfc4180.txt) of CSV.

<h2>Getting started</h2>

<h3>Installation</h3>
<b>DLL:</b> In the release section of this Github project you can download the DLL file <br/>
<b>Nuget (recommended):</b> Run <code>Install-Package CSVSerializer.Net -Version 1.1.0.2</code> in the Package Management Console

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

<h3>Legacy functions usage</h3>
I created a new type, for an enhanced way to visualize things: <strong>Document</strong>. This will allow an easier organization of the document, a better encapsulation and reusability. The structure is explained in the image below.
<img src="https://raw.githubusercontent.com/LucaMozzo/CSVSerializer.NET/master/ReadmeImages/Document-format.png" />
Please bear in mind that a Header is of type Row! I kept it in a different variable inside Document to provide more customization.
<h3>Complete beginners - Preliminary operations</h3>
Please the following procedures before using the code from the demos I provided. The following examples are made in VB .NET but it's the same for C#.
<h4>Create a new project</h4>
<img src="https://raw.githubusercontent.com/LucaMozzo/CSVSerializer.NET/master/ReadmeImages/usage-step1.gif" />
<h4>Add a reference to the library</h4>
<img src="https://raw.githubusercontent.com/LucaMozzo/CSVSerializer.NET/master/ReadmeImages/usage-step2.gif" />

<h2>Minimum requirements</h2>
<i>For contributing</i>
<ul>
<li>Visual Studio 2015</li>
<li>.NET Framework 4.5 SDK</li>
</ul>
<i>For using it</i>
<ul>
<li>.NET Framework 4.5 Application</li>
</ul>

<h2>Building the NuGet package</h2>
Rebuild the project using the Release configutation, then in the <code>CSVSerializer.NET</code> folder run <code>nuget pack .\CSVSerializer.nuspec</code>
