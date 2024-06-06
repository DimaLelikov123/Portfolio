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
using System.Data;
using System.Data.SqlClient;

namespace zootopia
{
    /// <summary>
    /// Interaction logic for EditAnimal.xaml
    /// </summary>
    public partial class EditAnimal : Window
    {
        public EditAnimal()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection(GlobalVariables.connectionString);
            SqlCommand command;

            if (input.Text != "" && ((bool)name.IsChecked || (bool)id.IsChecked))
            {
                string sqlQuery = "";
                if ((bool)name.IsChecked)
                    sqlQuery = $"SELECT a.*, ae.id_enclosure FROM animals a JOIN animals_enclosures ae ON a.id_animal = ae.id_animal WHERE a.[name_animal] = '{input.Text}'";
                else if ((bool)id.IsChecked)
                    sqlQuery = $"SELECT a.*, ae.id_enclosure FROM animals a JOIN animals_enclosures ae ON a.id_animal = ae.id_animal WHERE a.[id_animal] = {Convert.ToInt32(input.Text)}";
                command = new SqlCommand(sqlQuery, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                { 
                    while (reader.Read())
                    {

                        GlobalVariables.fillfactory();

                        var animal = GlobalVariables.animalfactory.CreateAnimal(reader["kind_of_animal"].ToString().Trim(), Convert.ToDouble(reader["age"].ToString()),
                           Convert.ToDouble(reader["weight"].ToString()), reader["description"].ToString().Trim(), reader["name_animal"].ToString().Trim(), reader["species"].ToString().Trim(), reader["what_eat"].ToString().Trim());
                        if (reader["health_status"].ToString().Trim() == "Normal")
                            animal.health = new Normal();
                        else if (reader["health_status"].ToString().Trim() == "Sick")
                            animal.health = new Sick();
                        else
                        {
                            animal.health = new Examine();
                        }
                       

                        GlobalVariables.animal = animal;

                        GlobalVariables.animal.setCleanliness(reader["cleanliness"].ToString().Equals("True"));
                        GlobalVariables.animal.numberOfEnclosure = Convert.ToInt32(reader["id_enclosure"].ToString());
                        }
                reader.Close();
                connection.Close();

                Edit nwc = new Edit();
                Hide();
                nwc.Show();
                this.Close();
               }
                else MessageBox.Show("No such animal was found");
            }
            else MessageBox.Show("Please fill out the form completely");
        }
            

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Animals nwc = new Animals();
            Hide();
            nwc.Show();
            this.Close();
        }
    }
}

