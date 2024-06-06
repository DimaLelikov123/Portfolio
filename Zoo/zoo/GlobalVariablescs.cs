using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace zootopia
{
    public class GlobalVariables
    {
        public static UserAuthorizationProxy proxy = new UserAuthorizationProxy();

        //public static string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ZooDb;Integrated Security=True";
        public static string connectionString = "Data Source=DESKTOP-OAB67H0;Initial Catalog=ZooDb1;Integrated Security=True";
        public static string sqlQuery = "SELECT * FROM [dbo].[enclosures]";
        static public List<Enclosure> enclosures = LoadEnclosures();
        static public AnimalFactory animalfactory = new AnimalFactory();
        

       

        static public Animal animal;
        public List<MedicalStaff> medicalstaff = new List<MedicalStaff>();
        public List<Worker> workers;


        public static void fillfactory()
        {
            animalfactory.RegisterAnimals("Mammal", (age, weight, description, name, species, eat) => new Mammal(age, weight, description, name, species, eat));
            animalfactory.RegisterAnimals("Bird", (age, weight, description, name, species, eat) => new Bird(age, weight, description, name, species, eat));
            animalfactory.RegisterAnimals("Fish", (age, weight, description, name, species, eat) => new Fish(age, weight, description, name, species, eat));
            animalfactory.RegisterAnimals("Amphibian", (age, weight, description, name, species, eat) => new Amphibian(age, weight, description, name, species, eat));
        }

        public static List<Enclosure> LoadEnclosures()
        {
            List<Enclosure> enclosures = new List<Enclosure>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Enclosure enclosure = new Enclosure(reader["type_of_enclosure"].ToString().Trim(), Convert.ToInt32(reader["id_enclosure"]), reader["image_link"].ToString().Trim(), reader["description"].ToString().Trim());
                    enclosures.Add(enclosure);
                }

                reader.Close();
            }
            return enclosures;
        }

        public static List<MedicalStaff> LoadMedicalstaff()
        {
            List<MedicalStaff> medicals = new List<MedicalStaff>();
            sqlQuery = "SELECT [full_name] ,[id]  ,[doing_something] ,[what_doing]  ,[type_of_worker], [with_which_animal] FROM[dbo].[workers] WHERE[type_of_worker] = 'Medical'; ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    MedicalStaff medical = new MedicalStaff(reader["full_name"].ToString().Trim(), Convert.ToInt32(reader["id"]), reader["doing_something"].ToString().Equals("True"), reader["with_which_animal"].ToString());
                    medicals.Add(medical);
                }

                reader.Close();
            }
            return medicals;
        }

        public static List<Worker> LoadWorkers()
        {
            List<Worker> workers = new List<Worker>();
            sqlQuery = "SELECT [full_name] ,[id]  ,[doing_something] ,[what_doing]  ,[type_of_worker], [with_which_animal] FROM[dbo].[workers] ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Worker medical = new Worker(reader["full_name"].ToString().Trim(), Convert.ToInt32(reader["id"]), reader["doing_something"].ToString().Equals("True"), reader["with_which_animal"].ToString(), reader["type_of_worker"].ToString());
                    workers.Add(medical);
                }

                reader.Close();
            }
            return workers;
        }

        public static List<Animal> LoadAnimals()
        {
            fillfactory();
            List<Animal> animals = new List<Animal>();
            sqlQuery = "SELECT a.*, ae.id_enclosure FROM animals a JOIN animals_enclosures ae ON a.id_animal = ae.id_animal";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var animal = animalfactory.CreateAnimal(reader["kind_of_animal"].ToString().Trim(), Convert.ToDouble(reader["age"].ToString()), Convert.ToDouble(reader["weight"].ToString()),
                        reader["description"].ToString(), reader["name_animal"].ToString(), reader["species"].ToString(), reader["what_eat"].ToString());
                    animal.setCleanliness(reader["cleanliness"].ToString().Equals("True"));
                    animal.numberOfEnclosure = Convert.ToInt32(reader["id_enclosure"].ToString());
                    animals.Add(animal);
                    
                }

                reader.Close();
            }
            return animals;
        }
    }
}

