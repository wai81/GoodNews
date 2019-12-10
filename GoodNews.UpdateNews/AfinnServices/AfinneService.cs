using Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GoodNews.NewsServices.AfinnServices
{
    public class AfinneService : IAfinneService
    {
        public Dictionary<string, string> LoadDictionary()
        {
            Dictionary<string, string> afinnDictionary = new Dictionary<string, string>();
            var afinnFile = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "\\.." + "\\.." + "\\.." + "\\.." + @"\GoodNews.UpdateNews\AfinnServices" + @"\AFINN-ru.json");
            afinnDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(afinnFile);
            return afinnDictionary;
        }
    }
}
