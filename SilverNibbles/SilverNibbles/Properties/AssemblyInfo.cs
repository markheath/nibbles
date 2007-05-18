using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("SilverNibbles")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Mark Heath")]
[assembly: AssemblyProduct("SilverNibbles")]
[assembly: AssemblyCopyright("Copyright © Mark Heath 2007")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("3d5900ae-111a-45be-96b3-d9e4606ca793")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:
[assembly: AssemblyVersion("1.3.0.0")]
[assembly: AssemblyFileVersion("1.3.0.0")]

// v1.02 17 May 2007
// made snake arena 48 * 80 rather than 50 * 80 with two blank rows
// made snake slightly faster
// use CellType to govern target number collision
// blank cells are now invisible, allowing potential gradient backgrounds
// v1.03 18 May 2007
// snake now uses a polyline - much nicer appearance, with rounded ends and corners
// snakes drawn onto a snake canvas

// TODO:
// make level control which draws levels separately
// get rid of the rectangle array
// experiment with gradients
// fair outcome of head-on collisions
// animate the pause control in and out

// Tasks:
// Improve on colours
// Create a separate SnakeArena
// Level number label
// integrate with the DOM to allow tweaking the features

// ideas for enhanced drawing performance
// Don't need rectangles for background, just collapse cells without snake or wall
// Snake as a path
// backgrounds drawn separately - get rid altogether of grid of rectangles