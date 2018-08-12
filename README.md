# EmbeddedResourceHelper
.Net helper to pull embedded resources.  Ideal for unit testing or web applications where you want to pull embedded resources as string, streams, or byte arrays.

This package is available from nuget.org

https://www.nuget.org/packages/EmbeddedResourceHelper/

## Basic Usage
```csharp
using EmbeddedResourceHelper;
  ...
  //You can pull a resouce by just the file name.  If multiple it will be first or default
  var resultBytes = EmbeddedResource.GetAsByteArrayFromCallingAssembly("ExampleEmbedded.pdf");
  
  //You can pull a resouce by a more specific by using a dot '.' or a slash '/'.
  //it just depends on how your developers want to think about the embeded resource.
  var resultBytes1 = EmbeddedResource.GetAsByteArrayFromCallingAssembly("Resources.ExampleEmbedded.pdf");
  var resultBytes2 = EmbeddedResource.GetAsByteArrayFromCallingAssembly("Resources/ExampleEmbedded.pdf");
  
  //You can specify the assembly to be searched
  var file = "ExampleEmbedded.txt";
  var asm = Assembly.Load("EmbeddedResourceHelper");
  var strm = EmbeddedResource.GetAsStream(asm, file);
  
  //Get the file content as string
  var strm = EmbeddedResource.GetAsString(asm, file);
  
  //Save an embedded resource to a folder.
  EmbeddedResource.SaveToDisk(asm, file, "c:\temp\");
```

Version History:

1.0.0.11
* Added method for GetAsByteArray
