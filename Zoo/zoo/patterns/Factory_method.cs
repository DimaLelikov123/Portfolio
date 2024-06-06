using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zootopia
{
    public abstract class Animal
    {
        
        public string kinfOfAnimal;
        public string whatEating;
        public string species;
        public string name;
        public double age;
        public double weight;
        public string description;
        public string typeOfEnclosure;
        public int numberOfEnclosure;
        public bool cleanliness = true;
        public IMedicalStrategy health = new Normal();
        private List<MedicalStaff> observers;


        public Animal(double age, double weight, string description, string name, string species, string eat)
        {
            this.age = age;
            this.weight = weight;
            this.description = description;
            this.name = name;
            this.species = species;
            this.whatEating = eat;
            observers = GlobalVariables.LoadMedicalstaff();
        }

        public void setHealhtStatus(IMedicalStrategy medicalstate)
        {
            health = medicalstate;
        }

        public void setHealhtStatus(IMedicalStrategy medicalstate, bool notify)
        {
            health = medicalstate;
            if (notify)
                NotifyObservers();
        }

        public string getHealthStatus()
        {
            return health.GetType().Name;
        }

        private void NotifyObservers()
        {
            foreach (var observer in observers)
            {
                if (observer.doing_something == false || observer.with_which_animal.Trim() == name)
                    observer.Update(this);
            }
        }

        public void setEnclosure(int number)
        {
            numberOfEnclosure = number;
        }

        public void setCleanliness(bool newStatus)
        {
            cleanliness = newStatus;
        }
    }



    class Mammal : Animal
    {
        public Mammal(double age, double weight, string description, string name, string species, string eat) : base(age, weight, description, name, species, eat)
        {
            kinfOfAnimal = "Mammal";
            typeOfEnclosure = "Big enclosure";
        }
    }

    class Bird : Animal
    {
        public Bird(double age, double weight, string description, string name, string species, string eat) : base(age, weight, description, name, species, eat)
        {
            kinfOfAnimal = "Bird";
            typeOfEnclosure = "Aviary";
        }

    }

    class Amphibian : Animal
    {
        public Amphibian(double age, double weight, string description, string name, string species, string eat) : base(age, weight, description, name, species, eat)
        {
            kinfOfAnimal = "Amphibian";
            typeOfEnclosure = "Terrarium";
        }
    }

    class Fish : Animal
    {
        public Fish(double age, double weight, string description, string name, string species, string eat) : base(age, weight, description, name, species, eat)
        {
            kinfOfAnimal = "Fish";
            typeOfEnclosure = "Aquarium";
        }
    }

    public class AnimalFactory
    {
        private readonly Dictionary<string, Func<double, double, string, string, string, string, Animal>> animals;

        public AnimalFactory()
        {
            animals = new Dictionary<string, Func<double, double, string, string, string, string, Animal>>();
        }

        public string[] RegisteredTypes => animals.Keys.ToArray();

        public void RegisterAnimals(string animalType, Func<double, double, string, string, string, string, Animal> factoryMethod)
        {
            if (string.IsNullOrEmpty(animalType)) return;
            if (factoryMethod is null) return;

            animals[animalType] = factoryMethod;
        }

        public Animal CreateAnimal(string animalType, double age, double weight, string description, string name, string species, string eat)
        {
            if (animals.TryGetValue(animalType, out var factoryMethod))
            {
                return factoryMethod(age, weight, description, name, species, eat);
            }
            else
            {
                return null;
            }
        }

    }
}

    
