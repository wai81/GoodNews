using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodNews.ServiceLemmatization
{
    public class ParsingModel
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("annotations")]
        public Annot annotations { get; set; }

    }
    public class Annot
    {
        [JsonProperty("lemma")]
        public List<Lemma> Lemma { get; set; }
    }
    public class Lemma
    {
        [JsonProperty("start")]
        public long Start { get; set; }

        [JsonProperty("end")]
        public long End { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}


