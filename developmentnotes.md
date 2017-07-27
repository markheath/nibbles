# Stage 1 - Porting from WinForms to Silverlight

## Development Environment

The first challenge was simply to create a Silverlight application. This is most easily done by downloading the Visual Studio Orcas Beta 1 plus the Silverlight runtime, VS extensions and SDK. I found that the whole development environment ran nicely in Virtual PC.

Orcas creates a Page.Xaml file which hosts the main program. My first task was to simply put everything from the Windows Forms version of Nibbles into Page.Xaml.cs, and hack it until it would compile for Silverlight.

## Rectangles and TextBlocks

The WinForms Nibbles made use of FillRectangle for most of the drawing - the snake arena is divided up into a grid of squares. Whenever a snake moved, Invalidate was called, and the Paint routine would fill each square with the appropriate colour depending on whether it was a Snake, Wall or Blank space.

The simplest way to implement this in Silverlight was to add an array of Rectangle objects to the main Canvas.

```c#
// Create the array of rectangles
for (int col = 0; col < Columns; col++)
{
    for (int row = 0; row < Rows; row++)
    {
        Rectangle rect = new Rectangle();
        rect.SetValue<double>(Canvas.LeftProperty, col * DefaultBlockSize);
        rect.SetValue<double>(Canvas.TopProperty, row * DefaultBlockSize);
        rect.Width = DefaultBlockSize;
        rect.Height = DefaultBlockSize;
        arena[col, row](col,-row) = new Cell(CellType.Blank, rect);
        rootElement.Children.Add(rect);
    }
}
```

The number for the snake to eat, and the Labels that displayed high score information were implemented as TextBlock elements. Like .NET Label controls, all XAML elements handle their own invalidation - you don't need to call anything to get the display to update when you change a property on a visual element.

## Silverlight Limitations

Although Silverlight is very similar to WPF, the two are not exactly the same. WPF not only offers more classes, but the objects themselves have more capabilities. Here's a brief list of issues I ran into:

* Many WPF XAML files use Grid and StackPanel containers and set Margin, Padding, Horizontal and Vertical Alignment properties. You need to remove all these to use existing XAML code in Silverlight.
* You can't share a SolidColorBrush between two or more Rectangles. So even though I was drawing in just four colours, I needed to make one brush for every one of the 4000 rectangles in the grid. This is because Silverlight doesn't allow the sharing of resources.
* TextBlock controls do not have any support for text alignment. You have to perform any centering yourself.
* Polyline (see below) only supports an array of Points, not a PointCollection, making dynamically adding and removing points much more difficult.
* The Color class does not have any static members for known colours (e.g. White, Blue, Orange etc). You will need to consult a colour chart to get the RGB values yourself.

## Timer

SilverNibbles has a timer firing about once every 80ms. The way this is achieved in SilverLight is a little bit unusual. You create a Storyboard with a DoubleAnimation, and then handle its completed event. You can then begin the animation again if you need the timer to keep firing.

Here's the XAML in Page.xaml:

```xml
<Canvas.Resources>
    <Storyboard x:Name="timer">
        <DoubleAnimation Duration="00:00:0.08" />
    </Storyboard>
</Canvas.Resources>
```
Then in the Page.Xaml.cs constructor, we can add a handler and kick off the storyboard:

```c#
timer.Completed += new EventHandler(timer_Completed);
timer.Begin();
```

In SilverNibbles, we leave the timer running permanently, but it only has work to do if the game is actually running:

```c#
void timer_Completed(object sender, EventArgs e)
{
    if (arena.GameStatus == GameStatus.Running)
    {
        OnTimerTick(sender,e);
    }
    // restart the timer
    timer.Begin();
}
```

## Keyboard Handling

Nibbles works off the keyboard, which means two things. First, the Silverlight control must have keyboard focus. In the auto-generated HTML, there is some JavaScript to do this for us:

```html
<body onload="document.getElementById('SilverlightControl').focus()">
```

This works nicely in IE7, but unfortunately does not seem to be reliable in FireFox. Hopefully Microsoft can find a resolution to this.

To subscribe to keyboard events, we simply add an event handler to the KeyDown event. We also have a LostFocus event handler on the main canvas, which allows us to pause the game if the Silverlight control loses focus.

```c#
this.LostFocus += new EventHandler(Page_LostFocus);
this.KeyDown += new System.Windows.Input.KeyboardEventHandler(rootElement_KeyDown);
```

The keyboard handler itself simply receives an integer representing the key code of the key that is pressed. Unfortunately, Silverlight does not provide an enumeration of possible values, so we have created our own Keys enum with keycodes of interest to us:

```c#
enum Keys
{
    Return = 13,
    Escape = 27,
    Space = ' ',
    N1 = '1',
    N2 = '2',
    A = 'A',
    D = 'D',
    ...
```

As can be seen, the platform key code is usually the ASCII value of the character in question. Unfortunately, it appears that the cursor keys do not cause events to be raised at all in the Silverlight control. This meant that Nibbles had to be modified to work with different keys. The keyboard event handler itself can simply cast the PlatformKeyCode to our custom Keys enumeration and take the appropriate action:

```c#
void  rootElement_KeyDown(object sender, KeyboardEventArgs args)
{
     Keys key = (Keys)args.PlatformKeyCode;
     ...
```

## The Pause Control

The pause control offered us the first chance to improve the look and feel of the old Nibbles application. First of all, it must display the "Paused" message while the game is in pause. But it was also useful to replace the messageboxes that appeared when snakes died or the game ended.

The pause control was implemented as a custom XAML control. This works slightly differently from the Page.Xaml class as it is constructed differently. Page.Xaml is loaded directly by the Silverlight control, and therefore must contain information about the DLL and class that contain its .NET code:

```xaml
<Canvas x:Name="parentCanvas"
    ...        
    x:Class="SilverNibbles.Page;assembly=ClientBin/SilverNibbles.dll"
    ...
        >
```

The Pause class on the other hand has its XAML embedded into the SilverNibbles.dll assembly. Therefore, the x:Class attribute is not required, and the relevant XAML is extracted in the constructor of the PauseControl object. If we want to manipulate any of the objects, we must get references to them by calling FindName on the root element of the loaded XAML.

```c#
TextBlock textBlockMessage;
Rectangle rectBorder;

public PauseControl()
{
    System.IO.Stream s = 
        this.GetType().Assembly.GetManifestResourceStream("SilverNibbles.PauseControl.xaml");
    FrameworkElement rootElement = 
        this.InitializeFromXaml(new System.IO.StreamReader(s).ReadToEnd());
    textBlockMessage = (TextBlock) rootElement.FindName("textBlockMessage");
    rectBorder = (Rectangle)rootElement.FindName("rectBorder");
}
```

The FindName function can be used because we have set the x:Name attributes on the TextBlock and Rectangle in our XAML file:

```xaml
<Rectangle x:Name="rectBorder" Width="320" Height="140" 
    Stroke="Black" StrokeThickness="4" 
    RadiusX="5" RadiusY="5" Fill="#FFC1C1C1" />
<TextBlock x:Name="textBlockMessage" Width="304" Height="124" 
    Canvas.Left="8" Canvas.Top="8" 
    Text="Welcome To SilverNibbles" TextWrapping="Wrap"/>
```

Using XAML to construct the look and feel of the Pause control allows us to easily style it. Here we have simply given it a border and rounded corners.

We can also expose a Text property on our Pause control, allowing the pause message to be modified from code that creates the PauseControl.

```c#
public string Text
{
    get { return textBlockMessage.Text; }
    set { textBlockMessage.Text = value; }
}
```

There is however one remaining problem. When the PauseControl is resized, its constituent parts (the rectangle and textBlock) also need to be resized. As the Canvas does not offer us any auto-resizing of child elements, we will have to do it ourselves. Unfortunately though, there is no Canvas Resized event, and the Width and Height properties are not virtual, so cannot be overridden. The solution is found in the example controls supplied with the Silverlight SDK. We will create new Width and Height properties, and pass the new size down to the original properties when they are called. Then we can call our own resizing code for the Canvas' children.

```c#
// Sets/gets the Width of the actual control
public new double Width
{
    get { return base.Width; }
    set
    {
        base.Width = value;
        UpdateLayout();
    }
}

// Sets/gets the Height of the actual control
public virtual new double Height
{
    get { return base.Height; }
    set
    {
        base.Height = value;
        UpdateLayout();
    }
}

protected virtual void UpdateLayout()        
{
    rectBorder.Width = Width;
    rectBorder.Height = Height;
    textBlockMessage.Width = Width - 8 * 2;
    textBlockMessage.Height = Height - 8 * 2;
}
```

Now we have done this, all that remains is for the host canvas to create a PauseControl object, size and position it, and set its text.

```c#
pauseControl = new PauseControl();
pauseControl.Width = 380;
pauseControl.Height = 140;
pauseControl.SetValue<double>(Canvas.LeftProperty, (this.Width - pauseControl.Width) / 2);
pauseControl.SetValue<double>(Canvas.TopProperty, (this.Height - pauseControl.Height) / 2);
rootElement.Children.Add(pauseControl);
            
pauseControl.Text =
    String.Format("SilverNibbles 1.03 by Mark Heath\r\n{0}",
    Instructions);
```

Finally, we are able to show or hide the pause control by setting its visibility. As long as it was the last item to be added to the Canvas, it will appear over the top of anything else drawn on the screen. We set its visibility to Collapsed when we want it to disappear:

```c#
pauseControl.Visibility = Visibility.Collapsed;
```

## High Scores

So now we have updated our graphics to display in Siverlight we have a working game, as all the snake detection and positioning logic still works from the original Windows Forms version. The only thing remaining is to find a way to store the high score record.

Silverlight provides a mechanism for persisting files to the local hard disk, called IsolatedStorage. This allows each Silverlight application to save up to 1Mb of data (it is unique to the URL and instance of Silverlight). We will use this to create an XML file containing the high score and the date. Unlike the client application, we will not prompt for a user name, partly because Silverlight doesn't currently have a built-in TextBox, and partly because most users have their own login and hence their own IsolatedStorage.

Saving the record is relatively simple. We get hold of the storage area for our application, and then we create a new stream, overwriting any existing one as we only plan to store a maximum of one high score at the moment. Then we use an XmlWriter to enable us to easily create well-formed XML.

```c#
private void SaveRecord()
{
    IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication();
    IsolatedStorageFileStream stream = new IsolatedStorageFileStream(
                                           "record.xml",
                                           System.IO.FileMode.Create,
                                           file);
    using (XmlWriter writer = XmlWriter.Create(stream))
    {
        writer.WriteStartElement("HighScores");
        writer.WriteStartElement("HighScore");
        writer.WriteAttributeString("Score", recordScore.ToString());
        writer.WriteAttributeString("Date", recordDate.ToLongDateString());
        writer.WriteEndElement();
        writer.WriteEndElement();
    }
}
```

Loading the record is almost as easy, except we must handle a file not found exception. I have also noticed some other exceptions are thrown in this function on the first run under Windows Vista. I don't yet know what exactly is the cause of this. Reading the XML in is nice and easy thanks to the XmlReader class.

```c#
private void LoadRecord()
{
    IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication();    
    IsolatedStorageFileStream stream = null;
    try
    {
        stream = new IsolatedStorageFileStream(
                     "record.xml",
                     System.IO.FileMode.Open,
                     System.IO.FileAccess.Read,
                     file);
        if (stream != null)
        {
            using (XmlReader reader = XmlReader.Create(stream))
            {
                if (reader.ReadToFollowing("HighScore"))
                {
                    recordScore = Int32.Parse(reader.GetAttribute("Score"));
                    recordDate = DateTime.Parse(reader.GetAttribute("Date"));
                }
            }
        }
    }
    catch (System.IO.FileNotFoundException)
    {
        // this is OK - will be not found first time in
    }
    catch (Exception)
    {
        // first run on Vista seems to have a problem,
        // that doesn't result in a FileNotFoundException
        // - need to work out what this is
    }
}
```

# Stage 2 - Taking advantage of the power of Silverlight

## Improvement 1: The SnakeArena control

Now we have created a fully working Silverlight port of the Windows Forms Nibbles.NET game, we can start working on making a few enhancements to allow us to make use of some of the more cool technology that Silverlight has to offer. The first improvement was to add another custom Silverlight control called SnakeArena. This would host the game playing area, but not include the labels used to display current score and high score. This gives the advantage that the SnakeArena can be more easily repositioned within the overall Silverlight control. The original QBASIC Nibbles game used a grid size of 50 rows by 80 columns, but the top two rows were not used. The SnakeArena control has a grid of 48 rows by 80 columns to avoid the unwanted two blank rows being rendered.

Positioning isn't the only benefit of moving the snake arena to its own control. Silverlight's powerful rendering capabilities mean that now we could easily do any of the following:

* Resize the snake arena to any scale using a simple transform
* Fade the snake arena in or out
* We could even rotate the arena to make the gameplay more challenging!

All of these would be extremely awkward to add on to the Windows Forms version, but are trivial in Silverlight (or WPF).

## Improvement 2: Collapsing background

One of the weaknesses of this application is the use of an array of rectangles to present the majority of the graphics. As well as creating a huge number (almost 4000) of rectangles, this also limits the possibilities for nice graphical touches. The first step is to simply make background cells visibility to Collapsed, rather than painting each cell with a solid background colour. While this is not a radical change, it opens the door for more visual enhancements:

* The background can now be a gradient instead of a solid colour
* We could be even more adventurous and use pictures, video footage, or animations to form the background of each level with minimal code required.

## Improvement 3: Snake Polyline

Now we come to terms with the fact that the snake itself does not look very impressive. Each snake simply has a Queue of points which represents the cells it occupies. Each timer tick, another point is added to the front of the queue, and one is taken off the end as well unless the snake is currently "growing" because it has just eaten a number.

Rather than colouring a rectangle for each point occupied by the snake, we will now allow the Snake class itself to render itself. We will do this by using a Polyline:

```c#
polyline = new Polyline();
polyline.Stroke = new SolidColorBrush(color);
polyline.StrokeThickness = 0.8;
polyline.StrokeLineJoin = PenLineJoin.Round;
polyline.StrokeEndLineCap = PenLineCap.Round;
polyline.StrokeStartLineCap = PenLineCap.Round;
```

We have given the polyline rounded ends and corners, which will make the snake look a bit nicer. We have also made the StrokeThickness 0.8 which will mean that the snake will be slightly thinner, again improving the visual appearance as you can now see where it has been more clearly.

For some reason, the Points property of a Polyline is a fixed size array of Point structures, which means that whenever we add or remove a point, we will need to completely replace the Points property with a new array. This is despite the Silverlight 1.1 alpha documentation claiming that the Points property is a PointCollection object.

When we add a new point to the snake, we translate its position by 0.5 so that it will be drawn in the centre of the grid square it operates on.

```c#
        public void Enqueue(Position position)
        {
            body.Enqueue(new Point(position.X + 0.5, position.Y + 0.5));
            polyline.Points = body.ToArray();
        }
```

The Snake can now give its own graphics object to the SnakeArena, meaning that the SnakeArena no longer has to render the snake itself - it simply adds the Snake's Graphics element to its Canvas.

```c#
public FrameworkElement Graphics
{
   get { return polyline; }
}
```

You may have noticed that the Polyline will draw the line connecting up points in a 48 x 80 area, even though the SnakeArena itself draws blocks 8 pixels by 8 pixels wide. The RenderTransform property of the Polyline makes it simple to expand the snake to be the right size for the snake arena.


```c#
ScaleTransform transform = new ScaleTransform();
transform.ScaleX = 8;
transform.ScaleY = 8;
polyline.RenderTransform = transform;
```

So now the snake can draw itself, and looks much better for it. The SnakeArena creates a new Canvas with a transparent background for the Snake to draw itself onto. This is simply to allow the PauseControl to remain the topmost control, even though we are adding and removing Snake Polylines every time a new game is started.

This change again opens up the door for future graphical enhancements:

* The snake could have a gradient brush, or even an animated gradient to create a slithering appearance
* The snake could animate its end-point to slide smoothly rather than jump one block at a time.
* The snake could change from being a Polyline to a complicated Canvas made up of all kinds of visual elements, and no change would be needed to the SnakeArena control.

## Improvement 4: Level drawing

The final improvement is for each level to draw itself onto a Canvas, constructing Rectangles as needed, rather than simply reporting the coordinates of each square to be drawn. This means we can finally get rid of the array of 4000 Rectangles, and also that the obstacles in each level can have rounded edges, along with any other graphical niceties we can think of. This was implemented by creating a new XAML control called LevelControl.

## Improvement 5: Making it Scriptable

What if we want to have some buttons on our web page that start a new game, or checkboxes that turn various gameplay options on. This requires us to make our Page class scriptable. We will make the NewGame function accessible to JavaScript.

First we must mark both the Page class and the NewGame function with the **Scriptable** attribute, found in the System.Windows.Browser namespace.

```c#
using System.Windows.Browser;
...
namespace SilverNibbles
{
    [Scriptable](Scriptable)
    public partial class Page : Canvas
    {
        ...
        [Scriptable](Scriptable)
        public void NewGame(int players)
        {
           ...
```

The next step is to register a variable that JavaScript can use to access the instance of the Page class. We do this in the Page's Loaded event handler. We have chosen to call our instance, "SilverNibbles".

```c#
public void Page_Loaded(object o, EventArgs e)
{
    ...
    WebApplication.Current.RegisterScriptableObject("SilverNibbles", this);
```

Now we will create some buttons in our main HTML page that will start a new game, along with some JavaScript that handles them. Notice I have set the focus to the Silverlight control which will ensure we have keyboard focus for the new game. Also notice that our registered scriptable object is referenced as a member of the Content property of the Silverlight control.

```html
<head>
    <title>SilverNibbles</title>
    <script type="text/javascript" src="Silverlight.js"></script>
    <script type="text/javascript" src="SilverNibbles.html.js"></script>    
    <script type="text/javascript">
    function onNewGameClick(players) {
       var silverlight = document.getElementById('SilverlightControl');
       silverlight.focus();
       silverlight.Content.SilverNibbles.NewGame(players);
    }
    </script>    
</head>
<body>
   ...
   <p>New game: 
      <input type="button" onclick="onNewGameClick(1)" value="One Player" />
      <input type="button" onclick="onNewGameClick(2)" value="Two Players" />
   </p>
</body>
```

## Animation

Coming soon...

## Future Possibilities 

So we have finally ended up with not just a Silverlight port of a Windows Forms application, but one that begins to explore the new possibilites that the Silverlight rendering engine offers us. As well as the graphical enhancements described earlier, there are a couple more Silverlight technologies we could utilise to enhance this application further.

The first is to integrate more fully with the host web page.

* A new game button could be added, which could set the focus to the control and launch a new game, possibly avoiding the user wondering why the keys are unresponsive
* Text boxes and check boxes could allow the user to customise the gameplay, setting colours or enabling various ways of enhancing the gameplay:
	* Snakes that get faster
	* Snakes that only grow, requiring you to take care not to box yourself into a corner
	* Snakes and walls that go invisible, requiring you to remember what you were doing
	* Psychedelic background colours and rotating display
	* Walls that can move

Another avenue for improvement would be to make use of Siverlight's audio playback to create background music and sound-effects for various events.

## Conclusion

Silverlight offers a number of great benefits over Windows Forms for creating casual games

* You can host the game on your website, without the need for any server side hosting, or the need for the user to install your app on their PC (they will of course need Silverlight)
* The power of the Silverlight Rendering engine puts advanced graphical techniques within the reach of developers without the need to learn any complicated GDI+ APIs.
* The XAML based nature of Silverlight means that you could collaborate with a graphic designer and incorporate their work very easily into your code.
