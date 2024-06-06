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

namespace zootopia
{
    /// <summary>
    /// Interaction logic for ChangeStatus.xaml
    /// </summary>
    public partial class ChangeStatus : Window
    {
        public ChangeStatus()
        {
            InitializeComponent();
            health.SelectedItem = health.Items.Cast<ComboBoxItem>().FirstOrDefault(x => x.Name == GlobalVariables.animal.getHealthStatus());
            clean.SelectedItem = clean.Items[Convert.ToInt32(GlobalVariables.animal.cleanliness)];
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Edit nwc = new Edit();
            Hide();
            nwc.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (health.SelectedIndex==2)
            GlobalVariables.animal.setHealhtStatus(new Sick(), (bool)notify.IsChecked);
            else if (health.SelectedIndex == 1)
                GlobalVariables.animal.setHealhtStatus(new Examine(), (bool)notify.IsChecked);
            else
                GlobalVariables.animal.setHealhtStatus(new Normal(), (bool)notify.IsChecked);
            GlobalVariables.animal.cleanliness = Convert.ToBoolean(clean.SelectedIndex);
            Edit nwc = new Edit(); 
            Hide();
            nwc.Show();
            this.Close();
        }
    }
}
