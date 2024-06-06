using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace zootopia
{
 
    public interface IObserver
    {
        void Update(Animal animal);

    }

    public class MedicalStaff : IObserver
    {
        private int id;
        public string name;
        public bool doing_something;
        public string what_doing;
        public string with_which_animal;


        public MedicalStaff(string name, int id, bool doing, string animal)
        {
            this.name = name;
            this.doing_something = doing;
            this.id = id;
            with_which_animal = animal;
        }

        public void Update(Animal animal)
        {
            
           what_doing = animal.health.PerformMedicalProcedures(animal);
            if (what_doing == null)
            {
                doing_something = false;
                with_which_animal = null;
                GlobalVariables.sqlQuery = $"UPDATE[dbo].[workers] SET[what_doing] = '{what_doing}', [with_which_animal]=NULL, [doing_something] = {Convert.ToDouble(doing_something)} WHERE[id] = {id}";
            }
            else
            {
                doing_something = true;
                with_which_animal = animal.name;
                GlobalVariables.sqlQuery = $"UPDATE[dbo].[workers] SET[what_doing] = '{what_doing}', [with_which_animal]='{with_which_animal}', [doing_something] = {Convert.ToDouble(doing_something)} WHERE[id] = {id}";
            }
            
                var connection = new SqlConnection(GlobalVariables.connectionString);
                var command = new SqlCommand(GlobalVariables.sqlQuery, connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
        }
    }
}
