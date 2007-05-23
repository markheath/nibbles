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
[assembly: AssemblyVersion("1.7.0.0")]
[assembly: AssemblyFileVersion("1.7.0.0")]

// v1.02 17 May 2007
// made snake arena 48 * 80 rather than 50 * 80 with two blank rows
// made snake slightly faster
// use CellType to govern target number collision
// blank cells are now invisible, allowing potential gradient backgrounds
// v1.03 18 May 2007
// snake now uses a polyline - much nicer appearance, with rounded ends and corners
// snakes drawn onto a snake canvas
// v1.04 20 May 2007
// finally all walls drawn individually using LevelControl - no more rectangle array
// level number label
// v1.05 22 May 2007
// Made NewGame Scriptable (remember to also do the class), 
// allowing HTML buttons to start new game
// Also added some animation to fade and zoom the pause control in and out
// Added a scale property to LevelControl
// v1.06 23 May 2007
// Fixes to keyboard handling for people who hold down the keys and fill the buffer
// v1.07 
// Beginnings of customizable speed support
// Score now tied to speed and level


// TODO:
// experiment with gradients
// fair outcome of head-on collisions
// animate the pause control in and out

// Tasks:
// Improve on colours
// Create a separate SnakeArena
// integrate with the DOM to allow tweaking the features
