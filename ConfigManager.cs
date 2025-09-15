using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2 {
    internal class ConfigManager {
        public ConfigManager() { }

        public static void CreateConfig() {
            if (!File.Exists("config.json"))
            {
                ConfigJson configJson = new ConfigJson();
                configJson.DiscordToken = "";

                var jsonWriteData = JsonConvert.SerializeObject(configJson);
                using (var sw = new StreamWriter(@"config.json", false, System.Text.Encoding.UTF8))
                {
                    sw.Write(jsonWriteData);
                    Console.WriteLine(sw.ToString());
                }
            }
        }

        public static ConfigJson GetConfig()
        {
            ConfigJson configJson;
            using (var sr = new StreamReader(@"config.json", System.Text.Encoding.UTF8))
            {
                var jsonReadData = sr.ReadToEnd();
                configJson = JsonConvert.DeserializeObject<ConfigJson>(jsonReadData);
            }
            return configJson;
        }
    }

    [JsonObject("ConfigJson")]
    internal class ConfigJson {
        [JsonProperty("DiscordToken")]
        public string DiscordToken { get; set; } 
    }
}
