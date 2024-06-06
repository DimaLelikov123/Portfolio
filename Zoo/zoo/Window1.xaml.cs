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

   
    public partial class Window1 : Window
    {
        static string connectionString = "Data Source=DESKTOP-OAB67H0;Initial Catalog=ZooDb1;Integrated Security=True";
        SqlConnection connection = new SqlConnection(connectionString);
        SqlCommand command;
        public DataTable dT;
        static  EnclosureCollection collenction = new EnclosureCollection(GlobalVariables.enclosures);
        AviaryIterator iterator = (AviaryIterator)collenction.GetEnumerator();
        

        public Window1()
        {
            InitializeComponent();
            FillWindow1();
            
        }
        
        public void FillWindow1()
        {
            iterator.SetType(GlobalVariables.animal.typeOfEnclosure);
            if (iterator.MoveNext())
            {
                Enclosure enclosure = (Enclosure)iterator.Current;
                encimage.Source = new BitmapImage((new Uri(enclosure.imageSourse)));
                descr.Text = enclosure.description;
                nameencl.Content = enclosure.type;
                number.Content = enclosure.number;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (iterator.MoveNext())
            { 
                
                    Enclosure enclosure = (Enclosure)iterator.Current;
                    encimage.Source = new BitmapImage((new Uri(enclosure.imageSourse)));
                    descr.Text = enclosure.description;
                    number.Content = enclosure.number;
                    nameencl.Content = enclosure.type;        
            }
            else MessageBox.Show("There is only one enclosure of this type");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (iterator.MovePrevious())
            {   
                    Enclosure enclosure = (Enclosure)iterator.Current;
                    encimage.Source = new BitmapImage((new Uri(enclosure.imageSourse)));
                    descr.Text = enclosure.description;
                    nameencl.Content = enclosure.type;
                    number.Content = enclosure.number;
            }
            else MessageBox.Show("There is only one enclosure of this type");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            GlobalVariables.animal.numberOfEnclosure = Convert.ToInt32(number.Content);
            connection.Open();
            string sqlq = $"INSERT INTO [dbo].[animals] ([name_animal], [kind_of_animal], [what_eat], [cleanliness], [species], [age], [weight], [description], [type_of_enclosure], [health_status]) VALUES" +
              $"('{GlobalVariables.animal.name}', '{GlobalVariables.animal.kinfOfAnimal}', '{GlobalVariables.animal.whatEating}'," +
              $" 1, '{GlobalVariables.animal.species}', {GlobalVariables.animal.age}," +
              $" {GlobalVariables.animal.weight}, '{GlobalVariables.animal.description}', '{GlobalVariables.animal.typeOfEnclosure}', '{GlobalVariables.animal.health.GetType().Name}');" +
              $" INSERT INTO [dbo].[animals_enclosures] ([id_animal], [id_enclosure]) SELECT[id_animal], {GlobalVariables.animal.numberOfEnclosure} FROM[dbo].[animals] WHERE[name_animal] = '{GlobalVariables.animal.name}'; ";

            command = new SqlCommand(sqlq, connection);
            command.ExecuteNonQuery();
            connection.Close();


            MessageBox.Show("The animal was successfully added");
            Animals nwc = new Animals();
            Hide();
            nwc.Show();
            this.Close();
        }
    }
}
