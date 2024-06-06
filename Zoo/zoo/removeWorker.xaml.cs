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
    /// Interaction logic for removeWorker.xaml
    /// </summary>
    public partial class removeWorker : Window
    {
        public removeWorker()
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
                    sqlQuery = $"DELETE FROM [dbo].[workers] WHERE [full_name] = '{input.Text}'";
                }
                else if ((bool)id.IsChecked)
                {
                    sqlQuery = $"DELETE FROM [dbo].[workers] WHERE [id] = {Convert.ToInt32(input.Text)}";
                }
                command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Deletion query executed successfully.");
                    Staff nwc = new Staff();
                    Hide();
                    nwc.Show();
                    this.Close();
                }
                else MessageBox.Show("No such worker was found");
            }
            else MessageBox.Show("Please fill out the form completely");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Staff nwc = new Staff();
            Hide();
            nwc.Show();
            this.Close();
        }
    }
}
