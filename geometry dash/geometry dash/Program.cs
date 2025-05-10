using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geometry_dash
{
    internal class Program
    {
        static void Main(string[] args)
        {
            level level = new level(1);

            display display = new display(level);
            display.ShowDialog();


            //ConsoleHelper.EnableANSI();
            //ConsoleHelper.SetConsoleFont("Consolas", 4);

            //Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);

            //ConsoleHelper.MoveConsoleToTopLeft();


            //using (FileStream fs = new FileStream(@"C:\Users\boyss\Documents\General\GitHub\geometry-dash-cmd\geometry dash\geometry dash\resources\bg.png", FileMode.Open))
            //{
            //    Bitmap background = new Bitmap(fs);
            //    render.renderBackground(background);

            //}

            //level level = new level(1);
            //render r = new render(level.objects);
            //Console.ReadKey();
        }
    }
}
