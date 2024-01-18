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
using System.IO;
using Components.Services;
using Components.Entities;

namespace UI.Modals
{
    /// <summary>
    /// Interaction logic for AddSectionModal.xaml
    /// </summary>
    public partial class AddSectionModal : Window
    {
        public string SectionName { get; set; }
        public AddSectionModal()
        {
            InitializeComponent();
        }

        private void AddSectionEntryBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(SectionNameTxtBox.Text))
            {
                return;
            }
            SectionName = SectionNameTxtBox.Text;

            AddSectionEntryModal addSectionEntryModal = new AddSectionEntryModal(SectionName);
            addSectionEntryModal.ShowDialog();

            if (addSectionEntryModal.NewEntry != null)
            {
                SectionsManager.CreateNewSectionEntry(SectionName, addSectionEntryModal.NewEntry);
            }
        }

        private void AddSectionBtn_Click(object sender, RoutedEventArgs e)
        {
            SectionName = SectionNameTxtBox.Text;
            this.Close();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            SectionName = null;
            this.Close();
        }
    }
}
