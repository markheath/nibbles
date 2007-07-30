using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Nibbles")]
[assembly: AssemblyDescription(".NET version of Nibbles")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Nibbles")]
[assembly: AssemblyCopyright("Copyright © Mark Heath 2006")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("588a433d-f65d-4651-8543-4ceedf43a59c")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
[assembly: AssemblyVersion("0.5.0.13")]
[assembly: AssemblyFileVersion("0.5.0.13")]

// 15 May 2007 v0.5 build 13
// new release to coincide with SilverNibbles release 
// fixed a redraw issue
// 31 Jan 2007 v0.4 build 11
// some minor cleanup ready for first release
// 29 Nov 2006 v0.3 build 10
// Conversion to Visual Studio 2005
// UserControl for snake arena
// Double-buffering & rectangle invalidation
// Cursor keys can only be used if there are no enabled controls on the form that can accept focus -


// allow form resizing
// about form
// finish off proper separation of SnakeArenaControl from main form

// high score table
// configurable options 
//   - colours
//   - snake names
//   - keys
//   - speed
//   - speedup etc
//   - snake length not decreasing / increasing option
// use .NET settings for options
// cooler drawing (rounded edges)
// fairer winner for head-on collision
// installer
// custom level design
// get rid of the surplus area at the top of snake arena

// WPF conversion!