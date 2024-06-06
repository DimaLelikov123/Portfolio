using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zootopia
{
    public interface IMedicalStrategy
    {
        string PerformMedicalProcedures(Animal animal);
    }

    public class Normal : IMedicalStrategy
    {
        public string PerformMedicalProcedures(Animal animal)
        {
           return null;
        }
    }

    public class Sick : IMedicalStrategy
    {
        public string PerformMedicalProcedures(Animal animal)
        {
            return $"Must go and treat an animal named {animal.name}";
        }
    }

    public class Examine : IMedicalStrategy
    {
        public  string PerformMedicalProcedures(Animal animal)
        {
            return $"Must go and examine an animal named {animal.name}";
        }
    }
}
