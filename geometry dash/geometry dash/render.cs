using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;

namespace geometry_dash
{
    public class render
    {
        // add dlls to make console better

        private Object[] objects;
        private static readonly string basePath = @"C:\Users\boyss\Documents\General\GitHub\geometry-dash-cmd\geometry dash\geometry dash\resources\textures\";
        private static Dictionary<int, string> textureMap = new Dictionary<int, string>
        {
            {1, @"square_01_001.png" }, // block
            {8, @"spike_01_001.png" }, // spike
            {39, @"spike_02_001.png" } // half spike
        };
        public render(Object[] objects)
        {
            this.objects = objects;
            int startX = 0;
            int startY = -100;
            for (startX = 0; startX < 100000; startX+=10)
            {
                renderObjects(startX, startY, Console.WindowWidth, Console.WindowHeight);
                Thread.Sleep(500);
                Console.SetCursorPosition(0, 0);
                renderBackground(new Bitmap(@"C:\Users\boyss\Documents\General\GitHub\geometry-dash-cmd\geometry dash\geometry dash\resources\bg.png"));
            }
        }

        // not all objects are on the screen at once
        // so we only need to render the objects that are on the screen
        public void renderObjects(float screenX, float screenY, float screenWidth, float screenHeight)
        {
            foreach (Object obj in objects)
            {
                if (obj == null) { continue; }

                // convert object coordinates to screen coordinates
                float screenObjX = obj.X - screenX;
                float screenObjY = obj.Y - screenY;

                if (Math.Abs(screenObjX * 2) < Console.WindowWidth && Math.Abs(screenObjY) < Console.WindowHeight)
                {
                    // load the texture
                    if (textureMap.ContainsKey(obj.ID))
                    {
                        string texturePath = basePath + textureMap[obj.ID];
                        Bitmap texture = new Bitmap(texturePath);

                        // create new bitmap with dimensions 30x30
                        Bitmap scaledTexture = new Bitmap(texture, new Size(30, 30));


                        int consoleX = (int)(screenObjX * 2); // scale to console size
                        int consoleY = Console.WindowHeight - (int) screenObjY; // invert y axis





                        // render the texture to the console

                        for (int y = 0; y < scaledTexture.Height; y++)
                        {
                            Console.SetCursorPosition(consoleX, consoleY + y);
                            StringBuilder builder = new StringBuilder();
                            for (int x = 0; x < scaledTexture.Width; x++)
                            {
                                Color color = scaledTexture.GetPixel(x, y);
                                builder.Append($"\x1b[48;2;{color.R};{color.G};{color.B}m  ");
                            }
                            builder.Append("\x1b[0m\n"); // Reset color and newline
                            
                            Console.Write(builder);
                        }
                        
                    }
   
                    //Console.ReadKey();
                }
            }
        }
        public static void renderBackground(Bitmap background)
        {
            // render background
            // create a new bitmap with the same size as the screen
            Bitmap screenBitmap = new Bitmap(254 / 9 * 16, 254);

            // draw the background on the screen
            using (Graphics g = Graphics.FromImage(screenBitmap))
            {
                g.DrawImage(background, 0, 0, screenBitmap.Width, screenBitmap.Height);
            }

            // render screen bitmap to the console
            StringBuilder builder = new StringBuilder();
            for (int y = 0; y < screenBitmap.Height; y++)
            {
                for (int x = 0; x < screenBitmap.Width; x++)
                {
                    Color color = screenBitmap.GetPixel(x, y);
                    builder.Append($"\x1b[48;2;{color.R};{color.G};{color.B}m  ");
                }
                builder.Append("\x1b[0m\n"); // Reset color and newline
            }
            // write to console
            Console.Out.Write(builder);

        }
    }
    class ConsoleHelper
    {
        const int STD_OUTPUT_HANDLE = -11;
        const uint ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004;

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll")]
        static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

        [DllImport("kernel32.dll")]
        static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

        public static void EnableANSI()
        {
            IntPtr handle = GetStdHandle(STD_OUTPUT_HANDLE);
            if (!GetConsoleMode(handle, out uint mode))
            {
                Console.WriteLine("Failed to get console mode.");
                return;
            }

            mode |= ENABLE_VIRTUAL_TERMINAL_PROCESSING;
            if (!SetConsoleMode(handle, mode))
            {
                Console.WriteLine("Failed to set console mode.");
            }
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct CONSOLE_FONT_INFO_EX
        {
            public int cbSize;
            public int nFont;
            public COORD dwFontSize;
            public int FontFamily;
            public int FontWeight;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string FaceName;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct COORD
        {
            public short X;
            public short Y;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetCurrentConsoleFontEx(IntPtr hConsoleOutput, bool bMaximumWindow, ref CONSOLE_FONT_INFO_EX lpConsoleCurrentFontEx);

        public static void SetConsoleFont(string fontName, short fontSize)
        {
            IntPtr hnd = GetStdHandle(STD_OUTPUT_HANDLE);
            CONSOLE_FONT_INFO_EX info = new CONSOLE_FONT_INFO_EX
            {
                cbSize = Marshal.SizeOf<CONSOLE_FONT_INFO_EX>(),
                FaceName = fontName,
                dwFontSize = new COORD { X = 0, Y = fontSize },
                FontFamily = 0,  // FF_DONTCARE
                FontWeight = 400
            };

            bool success = SetCurrentConsoleFontEx(hnd, false, ref info);
            if (!success)
            {
                Console.WriteLine("Failed to set console font.");
            }
        }
        // Constants for SetWindowPos
        private const int SWP_NOMOVE = 0x0002;
        private const int SWP_NOZORDER = 0x0004;
        private const int HWND_TOP = 0;

        // Import kernel32.dll to call GetConsoleWindow
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetConsoleWindow();

        // Import user32.dll to call SetWindowPos
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int width, int height, int flags);

        // Method to move the console window to the top-left corner
        public static void MoveConsoleToTopLeft()
        {
            IntPtr consoleWindow = GetConsoleWindow();
            if (consoleWindow == IntPtr.Zero)
            {
                Console.WriteLine("Failed to get console window handle.");
                return;
            }

            // Get the current width and height of the console window
            int consoleWidth = Console.LargestWindowWidth; // Approximate width in pixels (8px per character)
            int consoleHeight = Console.LargestWindowHeight; // Approximate height in pixels (16px per row)

            // Move the console window to the top-left corner, adjusting for window decorations
            SetWindowPos(consoleWindow, HWND_TOP, 0, 0, consoleWidth, consoleHeight, SWP_NOMOVE | SWP_NOZORDER);

            // Set the console window size (this is a necessary step to make sure it's visible)
            Console.SetWindowSize(254 / 9 * 16 * 2, 254); // Adjust window size as needed
            Console.SetBufferSize(1000, 1000); // Adjust buffer size

            
        }
    }
}
