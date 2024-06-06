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
    /// Interaction logic for GiveATask.xaml
    /// </summary>
    public partial class GiveATask : Window
    {
        public GiveATask()
        {
            InitializeComponent();
            FillPage();
        }

        static public Delegate del= new Delegate();
        static List<Worker> workers = GlobalVariables.LoadWorkers();
        static List<Animal> animals = GlobalVariables.LoadAnimals();
        static ICommand command;

        void FillPage()
        {
           
            foreach (Worker worker in workers)
            {
                if (worker.doing_something==false)
                workername.Items.Add(worker.name); 
            }

            foreach (Animal animal in animals)
            {
                    animalls.Items.Add(animal.name);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var worker = workers.Where(x => x.name == workername.SelectedItem.ToString()).FirstOrDefault();
            del.Set_receiver(worker);
            del.Send_operation(command, animals.Where(x => x.name == animalls.SelectedItem.ToString()).FirstOrDefault());
            Staff nwc = new Staff();
            Hide();
            nwc.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
			Staff nwc = new Staff();
			Hide();
			nwc.Show();
			this.Close();
		}

        private void workername_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var worker = workers.Where(x => x.name == workername.SelectedItem.ToString()).FirstOrDefault();
            type.Text =worker.type;
            commands.Items.Clear();
            foreach (var elem in worker.commands.commands)
            {
                commands.Items.Add(elem);
            }
        }

        private void animalls_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clean.Content = animals.Where(x => x.name == animalls.SelectedItem.ToString()).FirstOrDefault().cleanliness.ToString();
            health.Content = animals.Where(x => x.name == animalls.SelectedItem.ToString()).FirstOrDefault().health.GetType().Name.ToString();
        }

        private void commands_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (commands.SelectedItem.ToString() == "Feed animal")
                command = new feedAnimal();
            else if (commands.SelectedItem.ToString() == "Clean the enclosure")
                command = new cleanTheEnclosure();
            else if (commands.SelectedItem.ToString() == "Treat animal")
                command = new treatAnimal();
        }
    }
}
