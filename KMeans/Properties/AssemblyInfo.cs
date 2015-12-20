using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("'Generic k-Means Implementations")]
[assembly: AssemblyDescription("A generic n-dimentional k-means implementation in parallel and sequential versions")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Andre Sayre")]
[assembly: AssemblyProduct("KMeans")]
[assembly: AssemblyCopyright("Copyright © Andre Sayre 2010")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("ea3bbbdc-2d8a-4468-8dcf-a1bc1028cec4")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

//Our internals are visible to KMeansExample for various demonstration and comparison purposes only.
[assembly: InternalsVisibleTo( "KMeansExample")]
