using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace geometry_dash
{
    public partial class display : Form
    {
        private Timer timer = new Timer();
        private const int bgSpeed = 0;
        private const int levelSpeed = 5; // Speed of the level objects
        private const int bgLoopWidth = 1020; // Width of the background image

        private int cameraX = 0; // Camera X position

        private List<Object> objects;
        //private static readonly string basePath = @"C:\Users\boyss\Documents\General\GitHub\geometry-dash-cmd\geometry dash\geometry dash\resources\textures\";
        private static readonly string basePath = @"H:\Subjects\Computer Science\git\geometry-dash-cmd\geometry dash\geometry dash\resources\textures\";

        public struct ObjectData
        {
            public string filename;
            public int x;
            public int y;
            public int xoffset;
            public int yoffset;
        }

        private static Dictionary<int, ObjectData> textureMap = new Dictionary<int, ObjectData>
        {
            { // block
                1, new ObjectData()
                {
                    filename = "square_01_001.png",
                    x = 30,
                    y = 30,
                    xoffset = 0,
                    yoffset = 0,
                }
            },
            { // half block
                0, new ObjectData()
                 {
                    filename = "square_01_001.png",
                    x = 30,
                    y = 15,
                    xoffset = 0,
                    yoffset = 15,
                 }
            },
            { // spike
                8, new ObjectData()
                {
                    filename = "spike_01_001.png",
                    x = 30,
                    y = 30,
                    xoffset = 0,
                    yoffset = 0,
                }
            },
            { // half spike
                39, new ObjectData()
                {
                    filename = "spike_02_001.png",
                    x = 30,
                    y = 15,
                    xoffset = 0,
                    yoffset = -15,
                }
            },

            // grid tiles
            { // top
                2, new ObjectData()
                {
                    filename = "square_02_001.png",
                    x = 30,
                    y = 30,
                    xoffset = 0,
                    yoffset = 0,
                }
            },
            { // corner
                3, new ObjectData()
                {
                    filename = "square_02_001.png",
                    x = 30,
                    y = 30,
                    xoffset = 0,
                    yoffset = 0,
                }
            },
            { // dot
                4, new ObjectData()
                {
                    filename = "square_04_001.png",
                    x = 30,
                    y = 30,
                    xoffset = 0,
                    yoffset = 0,
                }
            },
            { // none
                5, new ObjectData()
                {
                    filename = "square_05_001.png",
                    x = 30,
                    y = 30,
                    xoffset = 0,
                    yoffset = 0,
                }
            },
            { // much top
                6, new ObjectData()
                {
                    filename = "square_06_001.png",
                    x = 30,
                    y = 30,
                    xoffset = 0,
                    yoffset = 0,
                }
            },
            { // pillar
                7, new ObjectData()
                {
                    filename = "square_07_001.png",
                    x = 30,
                    y = 30,
                    xoffset = 0,
                    yoffset = 0,
                }
            },


      
            

        };
        public display(level level)
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            objects = level.objects.ToList();

            // to reduce lag:
            // create all objects just off the screen on the right
            // track the camera x position
            // start moving objects when they should be on screen
            // once off screen stop moving

            // create object images
            foreach (var obj in objects)
            {
                if (obj == null) { continue; }
                if (textureMap.TryGetValue(obj.ID, out var texture))
                {
                    // convert object coordinates to screen coordinates
                    int screenX;
                    if (obj.X < ClientSize.Width) // only create objects that are on screen
                    {
                        screenX = (int)obj.X;
                    }
                    else
                    {
                        screenX = ClientSize.Width;
                    }
                    int screenY = ClientSize.Height - 200 - (int)obj.Y;


                    PictureBox pic = new PictureBox()
                    {
                        BackgroundImage = Bitmap.FromFile(basePath + texture.filename),
                        BackgroundImageLayout = ImageLayout.Stretch,
                        Size = new Size(texture.x, texture.y),
                        Location = new Point(screenX - texture.xoffset, screenY - texture.yoffset), // just off the right side of the screen
                    };
                    obj.pic = pic; // assign the PictureBox to the object
                    pic.BringToFront();
                    this.Controls.Add(pic);
                }
            }

            // align the three images
            pic_bg_2.Left = bgLoopWidth;
            pic_bg_3.Left = bgLoopWidth * 2;

            timer.Interval = 20; // 20 milliseconds ~ 50 FPS
            timer.Tick += bgScroller();
            timer.Tick += levelScroller();
            timer.Start();
        }
        public EventHandler bgScroller()
        {
            return new EventHandler((sender, e) =>
            {
                // Move the background image to the left
                pic_bg.Left -= bgSpeed;
                pic_bg_2.Left -= bgSpeed;
                pic_bg_3.Left -= bgSpeed;

                pic_bg.SendToBack();
                pic_bg_2.SendToBack();
                pic_bg_3.SendToBack();

                // If the image has moved off the screen, reset its position
                if (pic_bg.Right < 0)
                {
                    pic_bg.Left = pic_bg_3.Left + bgLoopWidth;
                }
                if (pic_bg_2.Right < 0)
                {
                    pic_bg_2.Left = pic_bg.Left + bgLoopWidth;
                }
                if (pic_bg_3.Right < 0)
                {
                    pic_bg_3.Left = pic_bg_2.Left + bgLoopWidth;
                }
            });
        }
        public EventHandler levelScroller()
        {
            return new EventHandler((sender, e) =>
            {
                // Move the level objects to the left
                cameraX += levelSpeed;
                foreach (var obj in objects)
                {
                    // temporary fix for null objects which i haven't coded in yet
                    if (obj == null) { continue; }
                    if (obj.pic == null) { continue; }

                    // check if object is on the screen
                    if (obj.X < cameraX + ClientSize.Width)
                    {
                        obj.pic.Left -= levelSpeed;
                    }
                    if (obj.pic.Right < 0)
                    {
                        // remove object from the form
                        this.Controls.Remove(obj.pic);
                        obj.pic.Dispose();
                        obj.pic = null; // set to null to avoid memory leaks
                    }



                }
            });
        }
    }
}
