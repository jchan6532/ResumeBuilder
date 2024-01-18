using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.Entities
{
    public class Resume
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Number { get; set; }

        public string Address { get; set; }

        public Dictionary<string, string> Links { get; set; }

        public Dictionary<string, List<SectionEntry>> SectionsAndEntries { get; set; }

        public void LoadSectionsAndEntries()
        {

        }
    }
}
