using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace CAD.Core
{
    public class JsonConfig : IConfigFile
    {
        protected string _configFile;
        public JObject JConfig;

        public JsonConfig(JObject jsonConfig)
        {
            this.JConfig = jsonConfig;
        }

        public string GetValue(string propertyName)
        {
            return this.GetValue<string>(propertyName);
        }

        public T GetValue<T>(string propertyName)
        {
            if (this.JConfig != null && this.JConfig[propertyName] != null)
            {
                var _val = this.JConfig.GetValue(propertyName).ToObject<T>();
                return _val;
            }
            return default(T);

        }

        public void SetValue(string propertyName, string propertyValue)
        {
            this.SetValue<string>(propertyName, propertyValue);
        }

        public void SetValue<T>(string propertyName, T propertyValue)
        {
            if (this.JConfig != null)
            {
                this.JConfig[propertyName] = JToken.FromObject(propertyValue);
            }
        }
    }
}
