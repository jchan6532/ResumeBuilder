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

        public Resume()
        {
            Links = new Dictionary<string, string>();
            SectionsAndEntries = new Dictionary<string, List<SectionEntry>>();
        }

        public void LoadSectionsAndEntries(List<string> sections, List<SectionEntry> entries)
        {
            List<SectionEntry> currentSectionEntries = null;
            foreach (string section in sections)
            {
                currentSectionEntries.Clear();
                currentSectionEntries = null;
                currentSectionEntries = entries.Where(entry => entry.SectionType == section).ToList();
                SectionsAndEntries.Add(section, currentSectionEntries);
            }
        }

        public void CreateDoc()
        {

        }

        public void CreateHeader()
        {

        }

        public void CreateBulletPoint()
        {

        }

        public void CreateText()
        {

        }
    }
}
