using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Interaction logic for Edit.xaml
    /// </summary>
    public partial class Edit : Window
    {
        public Edit()
        {
            InitializeComponent();
            FillPage();
        }

        public void FillPage()
        {
            name.Text = GlobalVariables.animal.name;
            kind.Text = GlobalVariables.animal.kinfOfAnimal;
            species.Text = GlobalVariables.animal.species;
            age.Text = GlobalVariables.animal.age.ToString();
            weight.Text = GlobalVariables.animal.weight.ToString();
            description.Text = GlobalVariables.animal.description;
            name.Text = GlobalVariables.animal.name;
            eat.Text = GlobalVariables.animal.whatEating;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChangeStatus nwc = new ChangeStatus();
            Hide();
            nwc.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window2 nwc = new Window2();
            Hide();
            nwc.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            EditAnimal nwc = new EditAnimal();
            Hide();
            nwc.Show();
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

            GlobalVariables.sqlQuery =  $"UPDATE[dbo].[animals]  SET [kind_of_animal] = '{kind.Text}',  " +
                $"[what_eat] = '{GlobalVariables.animal.whatEating}',  [cleanliness] = {Convert.ToDouble(GlobalVariables.animal.cleanliness)},  [species] = '{species.Text}'," +
                $" [age] = {Convert.ToDouble(age.Text)},  [weight] = {Convert.ToDouble(weight.Text)},  [description] = '{description.Text}', [type_of_enclosure] = '{GlobalVariables.animal.typeOfEnclosure}', "
              +$"[health_status] = '{GlobalVariables.animal.health.GetType().Name}' WHERE[name_animal] = '{name.Text}' ";
            var connection = new SqlConnection(GlobalVariables.connectionString);
            var comm = new SqlCommand(GlobalVariables.sqlQuery, connection);
            connection.Open();
            comm.ExecuteNonQuery();
            connection.Close();

            MessageBox.Show("Changes have been made successfully");
            Animals nwc = new Animals();
            
            Hide();
            nwc.Show();
            this.Close();
        }

    
    }
}
