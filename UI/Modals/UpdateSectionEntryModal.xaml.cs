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
    }
}
