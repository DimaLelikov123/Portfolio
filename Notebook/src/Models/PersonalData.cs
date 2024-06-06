namespace Notebook.Models;

abstract class PersonalData
{
    public abstract string Name {get;}
    public abstract string LastName {get;}
    public abstract string? SecondName {get;}
    public abstract DateOnly DateOfBirth {get;}

    public abstract OccupationPlace OccupationPlace {get;}

    public abstract string Qualities {get;}

    public virtual string GetFullName()
    {
        return string.Concat(Name, LastName, SecondName);
    }
}