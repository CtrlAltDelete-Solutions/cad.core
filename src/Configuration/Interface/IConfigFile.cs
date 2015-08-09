using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAD.Core
{
    public interface IConfigFile
    {
        T GetValue<T>(string propertyName);
        string GetValue(string propertyName);
        void SetValue<T>(string propertyName, T propertyValue);
        void SetValue(string propertyName, string propertyValue);
    }
}