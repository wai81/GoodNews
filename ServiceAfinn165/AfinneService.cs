using Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ServiceAfinn165
{
    public class AfinneService : IAfinneService
    {
        public Dictionary<string, string> LoadDictionary()
        {
            Dictionary<string, string> afinnDictionary = new Dictionary<string, string>();
            var afinnFile = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "\\.." + "\\.." + "\\.." + "\\.." + @"\ServiceAfinn165" + @"\AFINN-ru.json");
            afinnDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(afinnFile);
            return afinnDictionary;
        }
    }
}
