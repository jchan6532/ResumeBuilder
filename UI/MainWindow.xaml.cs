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
using System.Windows.Navigation;
using System.Windows.Shapes;
using UI.Modals;
using Components.Services;
using Components.Entities;
using System.IO;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public SectionsManager Manager { get; set; }

        public SectionEntry NewEntry { get; set; }

        public int ListItemHeight { get; set; } = 30;

        public string CurrentSelectedSection { get; set; } = null;
        public MainWindow()
        {
            InitializeComponent();

            Manager = new SectionsManager();
        }

        private void AddListBoxItem(string itemName)
        {
            ListBoxItem listItem = new ListBoxItem();
            listItem.Content = itemName;
            listItem.MinHeight = ListItemHeight;
            listItem.FontSize = 15;
            listItem.PreviewMouseLeftButtonDown += (ev, a) =>
            {
                if (CurrentSelectedSection != null)
                {
                    return;
                }

                SectionOverview.Content = $"{(ev as ListBoxItem).Content} Overview";
                CurrentSelectedSection = (ev as ListBoxItem).Content.ToString();
                List<SectionEntry> allEntries = SectionsManager.GetAllSectionEntries(CurrentSelectedSection);
                foreach (SectionEntry entry in allEntries)
                {
                    AddSectionContentTextBlock(entry);
                }
                SectionArea.Visibility = Visibility.Visible;
                SectionHeader.Visibility = Visibility.Visible;
            };
            listItem.MouseDoubleClick += (ev, a) =>
            {
                SectionContentModal sectionContentModal = new SectionContentModal();
                sectionContentModal.ShowDialog();
            };
            SectionsListsBox.Items.Add(listItem);
        }

        private void AddCheckBox(string sectionName)
        {
            CheckBox checkBox = new CheckBox();
            checkBox.Content = sectionName;
            checkBox.FontSize = 15;
            checkBox.Margin = new Thickness(10);
            checkBox.Click += (eventSender, args) =>
            {
                if ((bool)(eventSender as CheckBox).IsChecked)
                {
                    Manager.SelectedSections.Add((eventSender as CheckBox).Content.ToString());
                    AddListBoxItem(sectionName);
                }
                else
                {
                    Manager.SelectedSections.Remove((eventSender as CheckBox).Content.ToString());
                    var sectionsList = SectionsListsBox.Items.Cast<ListBoxItem>().ToList();
                    var sectionToRemove = sectionsList.FirstOrDefault(
                        listItem => listItem.Content as string == (eventSender as CheckBox).Content.ToString()
                        );
                    SectionsListsBox.Items.Remove(sectionToRemove);
                }

                if (Manager.SelectedSections.Count > 0)
                {
                    SectionsListsBox.Visibility = Visibility.Visible;
                }
                else
                {
                    SectionsListsBox.Visibility = Visibility.Collapsed;
                    SectionArea.Visibility = Visibility.Collapsed;
                }
            };
            SectionsCheckBoxes.Children.Add(checkBox);
        }

        private void AddSectionContentTextBlock(SectionEntry newEntry)
        {

            Border border = new Border();
            border.BorderThickness = new Thickness(1);
            border.BorderBrush = Brushes.Transparent;
            border.Background = Brushes.Transparent;
            border.Padding = new Thickness(5);
            border.CornerRadius = new CornerRadius(5);
            border.MouseEnter += TextBlockBorder_MouseEnter;
            border.MouseLeave += TextBlockBorder_MouseLeave;

            TextBlock newContentEntry = new TextBlock();
            newContentEntry.TextWrapping = TextWrapping.Wrap;
            newContentEntry.Margin = new Thickness()
            {
                Left = 0,
                Right = 0,
                Top = 5,
                Bottom = 5
            };
            newContentEntry.FontSize = 15;
            newContentEntry.Text = newEntry.Name;
            border.Child = newContentEntry;

            SectionArea.Children.Add(border);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SectionsListsBox.MinHeight = ListItemHeight * 3;

            foreach (var sectionName in Manager.SectionNames)
            {
                AddCheckBox(sectionName);
            }
        }

        private void AddSectionBtn_Click(object sender, RoutedEventArgs e)
        {
            AddNewSection.Visibility = Visibility.Visible;
        }

        private void TextBlockBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Border).Background = Brushes.LightGray;
            (sender as Border).BorderBrush = Brushes.Black;
        }

        private void TextBlockBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Border).Background = Brushes.Transparent;
            (sender as Border).BorderBrush = Brushes.Transparent;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            AddNewSection.Visibility = Visibility.Collapsed;
            SectionNameTxtBox.Text = string.Empty;
        }

        private void ConfirmSectionBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(SectionNameTxtBox.Text))
            {
                Manager.SectionNames.Add(SectionNameTxtBox.Text);
                SectionsManager.CreateSectionDirectory(SectionNameTxtBox.Text);
                AddCheckBox(SectionNameTxtBox.Text);
                AddNewSection.Visibility = Visibility.Collapsed;
            }
            else
            {
                ErrorMsg.Text = "Something must be entered";
            }
            SectionNameTxtBox.Text = string.Empty;
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("deleted");
        }

        private void NewEntryBtn_Click(object sender, RoutedEventArgs e)
        {
            AddSectionEntryModal addSectionEntryModal = new AddSectionEntryModal(CurrentSelectedSection);
            addSectionEntryModal.ShowDialog();
            string serializedEntry = XmlService.Serialize<SectionEntry>(addSectionEntryModal.NewEntry, false, false);

            string currentDir = Environment.CurrentDirectory;
            File.WriteAllText($@"{currentDir}\Resume Sections\{CurrentSelectedSection}\{addSectionEntryModal.NewEntry.Name}.txt", serializedEntry);

            AddSectionContentTextBlock(addSectionEntryModal.NewEntry);
        }
    }
}
