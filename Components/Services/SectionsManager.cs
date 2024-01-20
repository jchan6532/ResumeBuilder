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
        /// <summary>
        /// All resume sections existing within the resume sections directory
        /// </summary>
        public List<string> SectionNames { get; set; }

        /// <summary>
        /// Currently selected sections in the UI
        /// </summary>
        public List<string> SelectedSections { get; set; }

        /// <summary>
        /// Currently accumulated selected entries in the UI
        /// </summary>
        public List<SectionEntry> Entries { get; set; }

        public SectionsManager()
        {
            string currentDir = Environment.CurrentDirectory;
            if (!Directory.Exists("Resume Sections"))
            {
                Directory.CreateDirectory("Resume Sections");
            }

            SectionNames = new List<string>();
            SelectedSections = new List<string>();
            Entries = new List<SectionEntry>();
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
            string serializedEntry = JsonService.Stringify(entry);
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
                SectionEntry entry = JsonService.ToJson(serialized);
                deserializedResults.Add(entry);
            }

            return deserializedResults;
        }

        public static void DeleteSection(string sectionName)
        {
            string currentDir = Environment.CurrentDirectory;

            Directory.Delete($@"{currentDir}\Resume Sections\{sectionName}", true);
        }
    }
}
