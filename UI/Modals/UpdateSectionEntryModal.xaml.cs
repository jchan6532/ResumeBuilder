using Components.Entities;
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

namespace UI.Modals
{
    /// <summary>
    /// Interaction logic for UpdateSectionEntryModal.xaml
    /// </summary>
    public partial class UpdateSectionEntryModal : Window
    {
        public SectionEntry Entry { get; set; }

        public bool CreateNewEntry { get; set; }
        public UpdateSectionEntryModal(SectionEntry entry)
        {
            InitializeComponent();
            Entry = entry;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            NameTxtBox.Text = Entry.Name;
            FromDate.SelectedDate = Entry.From;
            ToDate.SelectedDate = Entry.To;
            InstitutionTxtBox.Text = Entry.Institution;
            DescriptionTxtBox.Text = Entry.Description;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (NameTxtBox.Text != Entry.Name)
            {
                MessageBoxResult result = MessageBox.Show("Would you like to create a new entry instead?",
                    "Create new entry",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                CreateNewEntry = result == MessageBoxResult.Yes ? true : false;
            }

            Entry.Name = NameTxtBox.Text;
            Entry.From = FromDate.SelectedDate.Value;
            Entry.To = ToDate.SelectedDate.Value;
            Entry.Institution = InstitutionTxtBox.Text;
            Entry.Description = DescriptionTxtBox.Text;

            this.Close();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Entry = null;
            this.Close();
        }
    }
}
