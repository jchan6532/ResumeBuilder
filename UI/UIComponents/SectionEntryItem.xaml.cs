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

namespace UI.UIComponents
{
    /// <summary>
    /// Interaction logic for SectionEntryItem.xaml
    /// </summary>
    public partial class SectionEntryItem : UserControl
    {
        public SectionEntryItem()
        {
            InitializeComponent();
        }

        public bool OrderComplete { get; set; } = false;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            if (!OrderComplete)
            {
                OrderComplete = !OrderComplete;
                FoodPanel.Background = new SolidColorBrush(Colors.DarkGreen);
                button.Content = "Deselect";
            }
            else
            {
                OrderComplete = !OrderComplete;
                FoodPanel.Background = new SolidColorBrush(Colors.Wheat);
                button.Content = "Select";
            }

        }
    }
}
