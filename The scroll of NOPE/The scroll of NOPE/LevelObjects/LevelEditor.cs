using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace The_scroll_of_NOPE.LevelObjects
{
    class LevelEditor
    {
        
        

        public LevelEditor()
            {
                
            }

        // Using streamWriter writes level object to a file
        public bool SaveMap(LevelLayout map, string levelName)
        {

            var appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            appDataDir = Path.Combine(appDataDir, "TheScrollOfNope");
            Directory.CreateDirectory(appDataDir); // to-do: handle exceptions
            var fileName = Path.Combine(appDataDir, levelName);
           

            StreamWriter writer;
            
            
            // IF file exist write over
            if (File.Exists(fileName))
            {
                

                writer = new StreamWriter(fileName);

                // Write the entire levelObject to file in json format, this removes previous map of filename
                // Converts to json using NwetonsoftJson and writes to file
                string mapJSON = JsonConvert.SerializeObject(map);
                writer.Write(mapJSON);
                writer.Close();
                return true;
            }
            // If not create new file and write over
            else if(levelName != "")
            {
                // Create file
                

                // Open file with stream
                writer = new StreamWriter(fileName);

                // Write to the file
                string mapJSON = JsonConvert.SerializeObject(map);
                writer.Write(mapJSON);
                writer.Close();
                return true;
            }

            // If something with the map or name or writing went wrong return false
            return false;
        }


        public LevelLayout LoadMap(string levelName)
        {

            StreamReader reader;

            var appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            appDataDir = Path.Combine(appDataDir, "TheScrollOfNope");
            Directory.CreateDirectory(appDataDir); // to-do: handle exceptions
            var fileName = Path.Combine(appDataDir, levelName);      

            if (File.Exists(fileName))
            {
                // Open file
                
                reader = new StreamReader(fileName);

                // Read file
                string unserialisedMap = reader.ReadToEnd();

                // Converts from json to LevelLayout
                
                LevelLayout map = JsonConvert.DeserializeObject<LevelLayout>(unserialisedMap);
                reader.Close();
                return map;
            }
            

            return null;
        }
    }
}
