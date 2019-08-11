# CSVSerializer.NET
A CSV serializer and deserializer library for .NET

Currently under development.

CSVSerializer.NET is a .NET class library to include full support for CSV in your own application.
This class library follows the [specifications](https://www.ietf.org/rfc/rfc4180.txt) of CSV.

<h2>Getting started</h2>

<h3>Installation</h3>
<b>DLL:</b> In the release section of this Github project you can download the DLL file <br/>
<b>Nuget (recommended):</b> Run <code>Install-Package CSVSerializer.Net -Version 1.0.1.1</code> in the Package Management Console

<h3>Usage</h3>
I created a new type, for an enhanced way to visualize things: <strong>Document</strong>. This will allow an easier organization of the document, a better encapsulation and reusability. The structure is explained in the image below.
<img src="https://raw.githubusercontent.com/LucaMozzo/CSVSerializer.NET/master/Demo.VB/Media/Document-format.png" />
Please bear in mind that a Header is of type Row! I kept it in a different variable inside Document to provide more customization.
<h3>Complete beginners - Preliminary operations</h3>
Please the following procedures before using the code from the demos I provided. The following examples are made in VB .NET but it's the same for C#.
<h4>Create a new project</h4>
<img src="https://raw.githubusercontent.com/LucaMozzo/CSVSerializer.NET/master/Demo.VB/Media/usage-step1.gif" />
<h4>Add a reference to the library</h4>
<img src="https://raw.githubusercontent.com/LucaMozzo/CSVSerializer.NET/master/Demo.VB/Media/usage-step2.gif" />

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
