using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using Components.Entities;

namespace UI.Modals
{
    /// <summary>
    /// Interaction logic for AddSectionEntryModal.xaml
    /// </summary>
    public partial class AddSectionEntryModal : Window
    {
        public SectionEntry NewEntry { get; set; }

        public string SectionType { get; set; }
        public AddSectionEntryModal(string sectionType)
        {
            InitializeComponent();
        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            NewEntry = new SectionEntry()
            {
                Name = NameTxtBox.Text,
                Description = DescriptionTxtBox.Text,
                From = (DateTime)FromDate.SelectedDate,
                To = (DateTime)ToDate.SelectedDate,
                SectionType = "type"
            };

            this.Close();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            NewEntry = null;
            this.Close();
        }
    }
}
