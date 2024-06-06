using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for Animals.xaml
    /// </summary>
    public partial class Animals : Window
    {

        static string connectionString = "Data Source=DESKTOP-OAB67H0;Initial Catalog=ZooDb1;Integrated Security=True";
        SqlConnection connection = new SqlConnection(connectionString);
        SqlCommand command;
        SqlDataAdapter adapter;
        public DataTable dT;
        DataTable Table;

        public Animals()
        {
            InitializeComponent();
            Refresh();
        }

        public void Refresh ()
        {
            connection.Open();
            string sqlq  = "SELECT [name_animal], [kind_of_animal] ,[what_eat] ,[cleanliness]  ,[species] ,[age] ,[weight] ,[description], [health_status] FROM[dbo].[animals]";
            command = new SqlCommand(sqlq, connection);
            adapter = new SqlDataAdapter(command);
            Table = new DataTable("");
            adapter.Fill(Table);
            animals.ItemsSource = Table.DefaultView;
            connection.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddAnimal nwc = new AddAnimal();
            Hide();
            nwc.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            EditAnimal nwc = new EditAnimal();
            Hide();
            nwc.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ChooseAnAction nwc = new ChooseAnAction();
            Hide();
            nwc.Show();
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            RemoveAnimal nwc = new RemoveAnimal();
            Hide();
            nwc.Show();
            this.Close();
        }
    }
}
