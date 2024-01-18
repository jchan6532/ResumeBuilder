using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Components.Entities;

namespace Components.Services
{
    public class SectionsManager
    {
        public List<string> SectionNames { get; set; }

        public List<string> SelectedSections { get; set; }

        public SectionsManager()
        {
            string currentDir = Environment.CurrentDirectory;
            if (!Directory.Exists("Resume Sections"))
            {
                Directory.CreateDirectory("Resume Sections");
            }

            SectionNames = new List<string>();
            SelectedSections = new List<string>();
            foreach (string sectionFileName in Directory.GetDirectories("Resume Sections"))
            {
                SectionNames.Add(Path.GetFileNameWithoutExtension($@"{currentDir}\Resume Sections\{sectionFileName}"));
            }
        }

        public static void CreateSectionDirectory(string sectionName)
        {
            string currentDir = Environment.CurrentDirectory;
            Directory.CreateDirectory($@"{currentDir}\Resume Sections\{sectionName}");
        }

        public static void CreateNewSectionEntry(string sectionName, SectionEntry entry)
        {
            string currentDir = Environment.CurrentDirectory;
            string serializedEntry = XmlService.Serialize<SectionEntry>(entry, false, false);
            File.WriteAllText($@"{currentDir}\Resume Sections\{sectionName}\{entry.Name}.txt", serializedEntry);
        }

        public static List<SectionEntry> GetAllSectionEntries(string sectionName)
        {
            string currentDir = Environment.CurrentDirectory;

            string[] files = Directory.GetFiles($@"{currentDir}\Resume Sections\{sectionName}");

            List<SectionEntry> deserializedResults = new List<SectionEntry>();
            foreach (string file in files)
            {
                string serialized = File.ReadAllText(file);
                SectionEntry entry = XmlService.Deserialize<SectionEntry>(serialized);
                deserializedResults.Add(entry);
            }

            return deserializedResults;
        }
    }
}
