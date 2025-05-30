# geometry-dash-cmd
in progress project, basically attempts to render a geometry dash level in windows forms using the level data. was originally going to use the console which is why it has cmd in the name, but ansii escape sequences were being frustrating.

pretty useless in all honesty, but there is code in here to convert actual geometry dash level data into C# structures which you can do something with since it isn't just a string anymore:
```
public class Object
{
    public int ID { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
    public PictureBox pic { get; set; } // for rendering
}
```
of course this isn't every data type for an object, maybe ill update with more later. currently the level "stereo madness" is loaded in if you click run. sorry in advance for the lag.

oh also all the game textures are in here for some reason i don't know why i downloaded them all but oh well

