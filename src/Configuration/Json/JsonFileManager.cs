using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CAD.Core
{
    public class JsonFileManager : IConfigFileManager
    {
        public JsonFileManager()
        {

        }

        public IConfigFile Read(string configPath)
        {
            JObject _config = null;
            bool isNewFile = false;

            if (!String.IsNullOrEmpty(configPath))
            {
                if (System.IO.File.Exists(configPath))
                {
                        using (JsonTextReader reader = new JsonTextReader(System.IO.File.OpenText(configPath)))
                        {
                            try
                            {
                                _config = (JObject)JToken.ReadFrom(reader);
                            }
                            catch
                            {
                                //check if file is empty.. if empty, then we create new JObject..
                                //if not empty, return false 
                                long fileLength = new System.IO.FileInfo(configPath).Length;
                                if (fileLength == 0)
                                {
                                    isNewFile = true;
                                }
                            }
                        }
                    
                }
                else
                {
                    System.IO.File.Create(configPath);
                    isNewFile = true;
                }
            }

            if (isNewFile)
            {
                _config = new JObject();

            }

            return new JsonConfig(_config);
        }

        public void Write(string configPath, IConfigFile config)
        {
                using (JsonTextWriter writer = new JsonTextWriter(System.IO.File.CreateText(configPath)))
                {
                    (config as JsonConfig).JConfig.WriteTo(writer);
                }
          
        }


        public T Deserialize<T>(string configPath)
        {
            bool isNewFile = false;
            T _instance = default(T);

            if (!String.IsNullOrEmpty(configPath))
            {
                if (System.IO.File.Exists(configPath))
                {

                    using (System.IO.StreamReader file = System.IO.File.OpenText(configPath))
                    {
                        try
                        {
                            JsonSerializer serializer = new JsonSerializer();
                            _instance = (T)serializer.Deserialize(file, typeof(T));
                            if (_instance == null)
                            {
                                isNewFile = true;
                            }
                        }
                        catch
                        {
                            //check if file is empty.. if empty, then we create new JObject..
                            //if not empty, return false 
                            long fileLength = new System.IO.FileInfo(configPath).Length;
                            if (fileLength == 0)
                            {
                                isNewFile = true;
                            }
                        }

                    }
                }
                else
                {
                    System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(configPath));
                    System.IO.File.Create(configPath);
                    isNewFile = true;
                }
            }

            if (isNewFile)
            {
                _instance = Activator.CreateInstance<T>();
            }

            return _instance;

        }


        public void Serialize<T>(string configPath, T jsonObject)
        {
            if (jsonObject != null)
            {
                //string jsonString = JsonConvert.SerializeObject(jsonObject);

                using (System.IO.StreamWriter file = System.IO.File.CreateText(configPath))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, jsonObject);
                }

            }

        }


        public string Serialize<T>(T jsonClass)
        {
            string serializedString = String.Empty;

            try
            {
                serializedString = JsonConvert.SerializeObject(jsonClass);
            }
            catch
            {

            }

            return serializedString;
        }
    }
}
