namespace Notebook.Models;

class UnregisteredPersonalData : PersonalData
{
    public override string Name => name;

    public override string LastName => lastName;

    public override string? SecondName => secondName;

    public override DateOnly DateOfBirth => dateOfBirth;

    public override OccupationPlace OccupationPlace => occupationPlace;

    public override string Qualities => throw new NotImplementedException();

    private string name, lastName, qualities;
    private string? secondName;
    private DateOnly dateOfBirth;
    private OccupationPlace occupationPlace;

    public UnregisteredPersonalData(string name, string lastName, string? secondName, DateOnly dateOfBirth, OccupationPlace occupationPlace, string qualities)
    {
        this.name = name;
        this.lastName = lastName;
        this.secondName = secondName;
        this.dateOfBirth = dateOfBirth;
        this.occupationPlace = occupationPlace;
        this.qualities = qualities;
    }
}