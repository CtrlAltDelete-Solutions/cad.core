using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAD.Core
{
    /// <summary>
    /// Create your own config manager and inject IConfigFileManager 
    /// </summary>
    public class FileConfigManager
    {
        private static Lazy<FileConfigManager> _instance = new Lazy<FileConfigManager>(() => new FileConfigManager(new JsonFileManager()));
        private readonly IConfigFileManager _configManager;

        private FileConfigManager(IConfigFileManager configManager)
        {
            this._configManager = configManager;
        }

        public static FileConfigManager Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        public IConfigFile Read(string configPath)
        {
            return this._configManager.Read(configPath);
        }

        public void Write(string configPath, IConfigFile config)
        {
            this._configManager.Write(configPath, config);
        }

        public T Deserialize<T>(string configPath)
        {
            return this._configManager.Deserialize<T>(configPath);
        }

        public string Serialize<T>(T configClass)
        {
            return this._configManager.Serialize(configClass);
        }
        
        public void Serialize<T>(string configPath, T jsonObject)
        {
            this._configManager.Serialize(configPath, jsonObject);
        }


    }
}
