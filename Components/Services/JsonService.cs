using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Components.Entities;
using Newtonsoft.Json;

namespace Components.Services
{
    public class JsonService
    {
        public static string Stringify(SectionEntry entry)
        {
            return JsonConvert.SerializeObject(entry, Formatting.Indented);
        }

        public static SectionEntry ToJson(string jsonStr)
        {
            return JsonConvert.DeserializeObject<SectionEntry>(jsonStr);
        }
    }
}
