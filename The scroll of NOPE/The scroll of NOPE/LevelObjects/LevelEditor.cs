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
            StreamWriter writer;
            var store = IsolatedStorageFile.GetUserStoreForApplication();
            
            // IF file exist write over
            if (store.FileExists(levelName) && levelName != "")
            {
                var fileStorage = store.OpenFile(levelName, FileMode.Open);
                writer = new StreamWriter(fileStorage);

                // Write the entire levelObject to file in json format, this removes previous map of filename
                // Converts to json using NwetonsoftJson and writes to file
                string mapJSON = JsonConvert.SerializeObject(map);
                writer.Write(mapJSON);
                return true;
            }
            // If not create new file and write over
            else if(levelName != "")
            {
                // Create file
                var fileStorage = store.CreateFile(levelName);

                // Open file with stream
                writer = new StreamWriter(fileStorage);

                // Write to the file
                string mapJSON = JsonConvert.SerializeObject(map);
                writer.Write(mapJSON);

                return true;
            }

            // If something with the map or name or writing went wrong return false
            return false;
        }


        public LevelLayout LoadMap(string levelName)
        {
            StreamReader reader;

            var store = IsolatedStorageFile.GetUserStoreForApplication();

            if (store.FileExists(levelName))
            {
                // Open file
                var fileStorage = store.OpenFile(levelName, FileMode.Open);
                reader = new StreamReader(fileStorage);

                // Read file
                string unserialisedMap = reader.ReadToEnd();

                // Converts from json to LevelLayout
                LevelLayout map = JsonConvert.DeserializeObject<LevelLayout>(unserialisedMap);

                return map;
            }
            

            return null;
        }
    }
}
