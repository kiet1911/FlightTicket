# EVO HTML to PDF Library for .NET

[![EVO PDF Logo Image](https://raw.githubusercontent.com/EvoPdf/evopdf-files/main/readme/evopdf-logo-banner.jpg)](http://www.evopdf.com)

[HTML to PDF for .NET](http://www.evopdf.com/html-to-pdf-converter.aspx) | [PDF Library for .NET and C#](http://www.evopdf.com) | [Free Trial](http://www.evopdf.com/download.aspx) | [Licensing](http://www.evopdf.com/buy.aspx) | [Support](http://www.evopdf.com/contact.aspx)

**EVO HTML to PDF Library for .NET** can be easily integrated in your applications targeting the .NET Framework to create PDF documents from HTML pages and strings.

The library can also be used to convert HTML to images, convert HTML to SVG, create, edit and merge PDF documents.

This version of the library is compatible with .NET Framework on Windows 32-bit (x86) and 64-bit (x64) platforms.

For .NET Core and .NET Standard applications on Windows you can use the library from  [EvoPdf.NetCore](https://www.nuget.org/packages/EvoPdf.NetCore/) package. The same library for .NET Core and .NET Standard is also available in [EvoPdf.HtmlToPdf.NetCore](https://www.nuget.org/packages/EvoPdf.HtmlToPdf.NetCore/) package.

In any .NET application for Linux, macOS, Windows, Azure App Service, Xamarin, UWP and other platforms you can use the cross-platform library from [EvoPdf.Client](https://www.nuget.org/packages/EvoPdf.Client/) package.

## Main Features

* Create PDF documents from HTML with advanced support for CSS3, SVG, Web Fonts and JavaScript
* Automatically create PDF links, forms, bookmarks and table of contents from HTML tags
* Place the content from multiple HTML documents at any position in PDF pages, headers or footers
* Create JPEG, PNG and Bitmap raster images from HTML documents
* Create high quality SVG vector images from HTML documents
* Create PDF documents with text, graphics, images, headers and footers
* Create PDF documents with security features and digital signatures
* Create interactive PDF documents with forms, internal links, text notes and JavaScript actions
* Edit, stamp and merge PDF documents

## Compatibility

EVO HTML to PDF Library for .NET is compatible with Windows platforms which support .NET Framework 2.0, 4.0 and above, including:

* .NET Framework 4.8.1, 4.7.2, 4.6.1, 4.0, 3.5, 2.0 (and above)
* Windows 32-bit (x86) and 64-bit (x64)
* Azure Cloud Services and Azure Virtual Machines
* Web, Console and Desktop applications

## Getting Started

After the reference to library was added to your project you are now ready to start writing code to convert HTML to PDF in your .NET application.
You can copy the C# code lines from the section below to create a PDF document from a web page or from a HTML string and save the resulted PDF to a memory buffer for further processing, to a PDF file or to send it to browser for download in ASP.NET applications.

### C# Code Samples

At the top of your C# source file add the ```using EvoPdf;``` statement to make available the EVO HTML to PDF API for your .NET application.

```csharp
// add this using statement at the top of your C# file
using EvoPdf;
```

To convert a HTML string or an URL to a PDF file you can use the C# code below.

```csharp
// create the converter object in your code where you want to run conversion
HtmlToPdfConverter converter = new HtmlToPdfConverter();

// convert the HTML string to a PDF file
converter.ConvertHtmlToFile("<b>Hello World</b> from EVO PDF !", null, "HtmlToFile.pdf");

// convert HTML page from URL to a PDF file
string htmlPageURL = "http://www.evopdf.com";
converter.ConvertUrlToFile(htmlPageURL, "UrlToFile.pdf");
```

To convert a HTML string or an URL to a PDF document in a memory buffer and then save it to a file you can use the C# code below.

```csharp
// create the converter object in your code where you want to run conversion
HtmlToPdfConverter converter = new HtmlToPdfConverter();

// convert a HTML string to a memory buffer
byte[] htmlToPdfBuffer = converter.ConvertHtml("<b>Hello World</b> from EVO PDF !", null);

// write the memory buffer to a PDF file
System.IO.File.WriteAllBytes("HtmlToMemory.pdf", htmlToPdfBuffer);

// convert an URL to a memory buffer
string htmlPageURL = "http://www.evopdf.com";
byte[] urlToPdfBuffer = converter.ConvertUrl(htmlPageURL);

// write the memory buffer to a PDF file
System.IO.File.WriteAllBytes("UrlToMemory.pdf", urlToPdfBuffer);
```

To convert in your ASP.NET MVC applications a HTML string or an URL to a PDF document in a memory buffer and then send it for download to browser you can use the C# code below.

```csharp
// create the converter object in your code where you want to run conversion
HtmlToPdfConverter converter = new HtmlToPdfConverter();

// convert a HTML string to a memory buffer
byte[] htmlToPdfBuffer = converter.ConvertHtml("<b>Hello World</b> from EVO PDF !", null);

FileResult fileResult = new FileContentResult(htmlToPdfBuffer, "application/pdf");
fileResult.FileDownloadName = "HtmlToPdf.pdf";
return fileResult;
```

To convert in your ASP.NET Web Forms application a HTML string to a PDF document in a memory buffer and then send it for download to browser you can use the C# code below.

```csharp
// create the converter object in your code where you want to run conversion
HtmlToPdfConverter converter = new HtmlToPdfConverter();

// convert a HTML string to a memory buffer
byte[] htmlToPdfBuffer = converter.ConvertHtml("<b>Hello World</b> from EVO PDF !", null);

HttpResponse httpResponse = HttpContext.Current.Response;
httpResponse.AddHeader("Content-Type", "application/pdf");
httpResponse.AddHeader("Content-Disposition",
    String.Format("attachment; filename=HtmlToPdf.pdf; size={0}",
    htmlToPdfBuffer.Length.ToString()));
httpResponse.BinaryWrite(htmlToPdfBuffer);
httpResponse.End();
```

## Free Trial

You can download the full EVO HTML to PDF Converter for .NET Framework package from [EVO PDF Downloads](http://www.evopdf.com/download.aspx) page of the website.

The package for .NET Framework contains the product binaries, demo Visual Studio projects with full C# code for ASP.NET Web Forms and ASP.NET MVC targeting .NET Framework 4 and later versions, demo projects for Windows Forms Desktop applications, a demo project for an Azure Cloud Service application, the library documentation in CHM format.

You can evaluate the library for free as long as it is needed to ensure that the solution fits your application needs.

## Licensing

The EVO PDF Software licenses are perpetual which means they never expire for a version of the product and include free maintenance for the first year. You can find [more details about licensing](http://www.evopdf.com/buy.aspx) on website.

## Support

For technical and sales questions or for general inquiries about our software and company you can contact us using the email addresses from the [contact page](http://www.evopdf.com/contact.aspx) of the website. 
