namespace Notebook.Models;

class QuestionnaireData
{
    private const string FORAMT = "Name: {0}, Last name: {1}, Second name: {2}, DoB: {3}";
    public QuestionnaireData(string name, string lastName, string? secondName, DateOnly dateOfBirth)
    {
        Name = name;
        LastName = lastName;
        SecondName = secondName;
        DateOfBirth = dateOfBirth;
    }

    public string Name {get; set;}
    public string LastName {get; set;}
    public string? SecondName {get; set;}
    public DateOnly DateOfBirth {get; set;}
    public int Age {
        get
        {
            DateTime n = DateTime.Now;
            int age = n.Year - DateOfBirth.Year;

            if (n.Month < DateOfBirth.Month || (n.Month == DateOfBirth.Month && n.Day < DateOfBirth.Day))
                age--;

            return age;
        }
    }

    public override string ToString()
    {
        return "{" + string.Format(FORAMT, Name, LastName, SecondName, DateOfBirth.ToString("dd.MM.yyyy")) + "}";
    }
}