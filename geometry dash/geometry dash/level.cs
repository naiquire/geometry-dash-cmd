using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Xml.Linq;

namespace geometry_dash
{
    public class Object
    {
        public int ID { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
    }
    public class level
    {
        private int levelID;
        private Object[] objects;

        private static Dictionary<int, Action<Object, string>> propertyMap = new Dictionary<int, Action<Object, string>>
        {
            { 1, (obj, val) => obj.ID = int.Parse(val) },
            { 2, (obj, val) => obj.X = float.Parse(val) },
            { 3, (obj, val) => obj.Y = float.Parse(val) },
            // Add more mappings (like rotation, scale, etc.)
        };

        public level(int levelID)
        {
            this.levelID = levelID;
            objects = loadLevel(levelID);
        }
        private string loadLevelString(int levelID)
        {
            // change working directory
            string scriptPath = @"C:\Users\boyss\Documents\General\GitHub\geometry-dash-cmd\geometry dash\geometry dash";

            // load level string from file
            string levelData = string.Empty;
            try
            {
                levelData = System.IO.File.ReadAllText($@"{scriptPath}\levels\{levelID}.txt");
            }
            catch (System.IO.FileNotFoundException e)
            {
                Console.WriteLine($"File not found: {e.Message}");
                return null;
            }

            #region decode
            // decode level data
            //var psi = new ProcessStartInfo
            //{
            //    FileName = "python",
            //    Arguments = $"decode.py \"{levelData}\"",
            //    RedirectStandardOutput = true,
            //    UseShellExecute = false,
            //    CreateNoWindow = true
            //};

            //using (var process = Process.Start(psi))
            //{
            //    using (var reader = process.StandardOutput)
            //    {
            //        string result = reader.ReadToEnd();
            //        process.WaitForExit();
            //        return result;
            //    }
            //}
            #endregion

            return levelData;
        }
        private Object[] loadLevel(int levelID)
        {
            string levelData = loadLevelString(levelID);

            string[] objectData = levelData.Split(';');

            // parse object data
            objects = new Object[objectData.Length];
            for (int i = 1; i < objectData.Length; i++)
            {
                string[] data = objectData[i].Split(',');
                Object obj = new Object();

                if (data.Length == 1 && data[0] == "") continue;
                for (int j = 0; j < data.Length; j+=2)
                {
                    int key = int.Parse(data[j]);
                    string value = data[j + 1];
                    if (propertyMap.ContainsKey(key))
                    {
                        propertyMap[key](obj, value);
                    }
                }
                objects[i - 1] = obj;
            }

            return objects;

        }
    }
    
}
