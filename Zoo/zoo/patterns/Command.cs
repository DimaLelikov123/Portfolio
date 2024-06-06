using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zootopia
{
    public abstract class ICommand
    {
        public abstract void Do_Operation(Animal animal);
        public Worker worker { get; set; }
    }

    public class feedAnimal : ICommand
    {

        public override void Do_Operation(Animal animal)
        {
            worker.feedAnimal(animal);
        }
    }

    public class cleanTheEnclosure : ICommand
    {
        public override void Do_Operation(Animal animal)
        {
            worker.cleanTheEnclosure(animal);
        }
    }

    class treatAnimal : ICommand
    {

        public override void Do_Operation(Animal animal)
        {
            worker.TreatAnimal(animal);
        }
    }


    public class Worker
    {
        public int id;
        public string name;
        public bool doing_something;
        public string what_doing;
        public string with_which_animal;
        public string type;
       public Commands commands =  new Commands();


        public Worker(string name, int id, bool doing, string animal, string type)
        {
            this.name = name;
            this.doing_something = doing;
            this.id = id;
            with_which_animal = animal;
            this.type = type;

            var cleaner = new Cleaner_commands(commands);
            var medical = new Medical_commands(commands);
            var feeder = new Feeder_commands(commands);

            if (type.Trim() == "Medical")
            {
                feeder.setCommands(cleaner);
                medical.setCommands(feeder);
                medical.fillCommands();
                commands.commands = medical.commands;
            }
            else if (type.Trim() == "Cleaner")
            {
                cleaner.fillCommands();
                commands.commands = cleaner.commands;
            }
            else if (type.Trim() == "Feeder")
            {;
                feeder.setCommands(cleaner);
                feeder.fillCommands();
                commands.commands = feeder.commands;
            }
        }

        public void feedAnimal(Animal animal)
        {
            what_doing = $"Feed an animal whose species is {animal.species.Trim()}, whose name is {animal.name.Trim()}.";
            with_which_animal = animal.name;
            doing_something = true;
            editDB();
            System.Windows.MessageBox.Show("the notification was successfully sent");
        }

        public void cleanTheEnclosure(Animal animal)
        {
            if (!animal.cleanliness)
            {
                what_doing = $"Clean the {animal.typeOfEnclosure.Trim()} under the number {animal.numberOfEnclosure}.";
                with_which_animal = animal.name;
                doing_something = true;
                editDB();
                System.Windows.MessageBox.Show("the notification was successfully sent");
            }
            else System.Windows.MessageBox.Show("This animal does not require cleaning");
        }

        public void TreatAnimal(Animal animal)
        {
            if (animal.getHealthStatus().GetType().Name == "Sick")
            {
                what_doing = $"treat the animal whose name is {animal.name.Trim()}";
                with_which_animal = animal.name;
                doing_something = true;
                editDB();
                System.Windows.MessageBox.Show("the notification was successfully sent");
            }
            else System.Windows.MessageBox.Show("This animal does not require treatment");
        }

        public void editDB()
        {
            GlobalVariables.sqlQuery = $"UPDATE[dbo].[workers] SET[what_doing] = '{what_doing}', [with_which_animal]='{with_which_animal}', [doing_something] = {Convert.ToDouble(doing_something)} WHERE[id] = {id}";
            var connection = new SqlConnection(GlobalVariables.connectionString);
            var comm = new SqlCommand(GlobalVariables.sqlQuery, connection);
            connection.Open();
            comm.ExecuteNonQuery();
            connection.Close();
        }
    }



    public class Delegate
        {
            Worker worker;

            public void Set_receiver(Worker r)
            {
                this.worker = r;
            }

            public void Send_operation(ICommand command, Animal animal)
            {
                    command.worker = worker;
                   command.Do_Operation(animal);
             }
         }
    }
    



