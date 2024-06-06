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
    /// Interaction logic for AddAnimal.xaml
    /// </summary>
    public partial class AddAnimal : Window
    {
        public AddAnimal()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (kind.Text != "" && age.Text != "" && weight.Text != "" && description.Text != "" && name.Text != "")
            {

                GlobalVariables.fillfactory();
                var animal = GlobalVariables.animalfactory.CreateAnimal(kind.Text, Convert.ToDouble(age.Text), Convert.ToInt32(weight.Text), description.Text,
                    name.Text, species.Text, eat.Text);
                if (health.SelectedIndex == 1)
                    animal.setHealhtStatus(new Sick());
                else if (health.SelectedIndex == 2)
                    animal.setHealhtStatus(new Examine());
                else
                    animal.setHealhtStatus(new Normal());
                GlobalVariables.animal = animal;
                Window1 nwc = new Window1();
                Hide();
                nwc.Show();
                this.Close();
            }
            else
                MessageBox.Show("Please fill in all fields");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Animals nwc = new Animals();
            Hide();
            nwc.Show();
            this.Close();
        }
    }
}
