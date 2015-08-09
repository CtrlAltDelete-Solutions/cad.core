using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAD.Core
{
    public interface IConfigFileManager
    {
        IConfigFile Read(string configPath);
        void Write(string configPath, IConfigFile config);
        T Deserialize<T>(string configPath);
        string Serialize<T>(T configClass);
        void Serialize<T>(string configPath, T jsonObject);
    }
}
