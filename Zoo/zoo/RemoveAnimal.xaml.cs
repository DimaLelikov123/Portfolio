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
    /// Interaction logic for RemoveAnimal.xaml
    /// </summary>
    public partial class RemoveAnimal : Window
    {
        public RemoveAnimal()
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
                {
                    sqlQuery = $"DELETE FROM [dbo].[animals] WHERE [name_animal] = '{input.Text}'";
                }
                else if ((bool)id.IsChecked)
                {
                    sqlQuery = $"DELETE FROM [dbo].[animals] WHERE [id_animal] = {Convert.ToInt32(input.Text)}";
                }
                command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected>0)
                {
                    MessageBox.Show("Deletion query executed successfully.");
                    Animals nwc = new Animals();
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
            Animals  nwc = new Animals();
            Hide();
            nwc.Show();
            this.Close();
        }
    }
    
}
