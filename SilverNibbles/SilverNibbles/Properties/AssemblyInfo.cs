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
[assembly: AssemblyCopyright("Copyright © Mark Heath 2007-2008")]
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
[assembly: AssemblyVersion("1.22.0.0")]
[assembly: AssemblyFileVersion("1.22.0.0")]

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
// v1.07 24 May 2007
// Scriptable Initial speed and Speed properties
// Score now tied to speed and level
// Combo box on form to choose a starting speed
// v1.08 24 May 2007
// Another bugfix to keyboard handling
// More fine-tuning of level speeds
// Speedup code implemented
// Current Speed displayed
// Improved game behavour past level 10 (faster, longer snakes, different background)
// v1.09 26 Jun 2007
// Changed to use Key rather than PlatformKeyCode
// Support for cursor keys on key-up only (not great, as it also will scroll the page up and down)
// v1.10 30 Jul 2007
// Updated to work with the new July Silverlight Alpha refresh
// v1.11 6 Mar 2008
// Updated to work with Silverlight 2.0 beta - major changes!
// v1.12 7 Mar 2008
// Improved the look of the instructions (slightly!)
// Added some buttons to the PauseControl to solve FireFox keyboard focus issue
// v1.13 8 Mar 2008
// Using the new DispatcherTimer instead of the old animation method
// Starting to use XAML to add custom controls
// Attempted to use Application.Resources for the background with limited success
// Cursor keys now work properly in 2.0 beta 1 !
// v1.14 11 Mar 2008
// Now makes use of a freeware font "DesignerBlock" from k-type.com
// Scores area has been moved out into a separate ScoreBoard control
// A new LivesControl shows lives left as a series of dots
// v1.15 14 Mar 2008
// Backed out of using DesignerBlock font due to licensing issues
// v1.16 15 Mar 2008
// Fixed an issue with PauseControl able to start new games while invisible
// v1.17 7 Jun 2008
// Updated to Silverlight 2.0 beta 2
// v1.18 7 Jun 2008
// Added a ding sound played when getting a number (turned off in this release)
// Found a new font with a more generous licensing terms - "teen bold" from larabiefonts.com
// New style using VisualStateManager for one and two player buttons
// Instructions made to look nicer
// v1.19 8 Jun 2008
// Font is now embedded in the XAP file although that means that visual studio doesn't show the right thing
// v1.20 9 Jun 2008
// Font is now a resource, as embedding in the XAP will not be supported after beta 2
// v1.21 8 Oct 2008
// Updating to work with Silverlight 2.0 RC0
// v1.22 14 Oct 2008
// Updating to work with Silverlight 2.0 RTW

// TODO:
// Logo
// experiment with gradients
// fair outcome of head-on collisions
// sound effects
// music
