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
    /// Interaction logic for Staff.xaml
    /// </summary>
    public partial class Staff : Window
    {
        public Staff()
        {
            InitializeComponent();
            Refresh();
        }

        static string connectionString = "Data Source=DESKTOP-OAB67H0;Initial Catalog=ZooDb1;Integrated Security=True";
        SqlConnection connection = new SqlConnection(connectionString);
        SqlCommand command;
        SqlDataAdapter adapter;
        public DataTable dT;
        DataTable Table;

        public void Refresh()
        {
            connection.Open();
            string sqlq = "SELECT * FROM[dbo].[workers]";
            command = new SqlCommand(sqlq, connection);
            adapter = new SqlDataAdapter(command);
            Table = new DataTable("");
            adapter.Fill(Table);
            animals.ItemsSource = Table.DefaultView;
            connection.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GiveATask nwc = new GiveATask();
            Hide();
            nwc.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            removeWorker nwc = new removeWorker();
            Hide();
            nwc.Show();
            this.Close();
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var nwc = new ChooseAnAction();
            Hide();
            nwc.Show();
            this.Close();
        }
    }
}
