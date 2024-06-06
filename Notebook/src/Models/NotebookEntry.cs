using Notebook.Enums;

namespace Notebook.Models;

class NotebookEntry : PersonalData
{
    public override string Name => QuestionnaireData.Name;

    public override string LastName => QuestionnaireData.LastName;

    public override string? SecondName => QuestionnaireData.SecondName;

    public override DateOnly DateOfBirth => QuestionnaireData.DateOfBirth;

    public OccupationPlace _ocupationPlace;
    public override OccupationPlace OccupationPlace {get => _ocupationPlace;}

    private string _qualities;
    public override string Qualities => _qualities;

    public QuestionnaireData QuestionnaireData {get; private set;}
    public List<Adress> Adresses {get; private set;}
    public PhoneNumberCollection PhoneNumberCollection {get; private set;}
    public Dictionary<RelationshipType, List<PersonalData>> Relationships {get; private set;}

    public DateTime CreationDate {get; private set;}

    public NotebookEntry(string qualities,
                        QuestionnaireData questionnaireData,
                        List<Adress> adresses,
                        PhoneNumberCollection phoneNumberCollection,
                        Dictionary<RelationshipType, List<PersonalData>> relationships,
                        OccupationPlace ocupationPlace)
    {
        _qualities = qualities;
        QuestionnaireData = questionnaireData;
        Adresses = adresses;
        PhoneNumberCollection = phoneNumberCollection;
        Relationships = relationships;
        _ocupationPlace = ocupationPlace;

        CreationDate = DateTime.Now;
    }

    public override string ToString()
    {
        System.Text.StringBuilder builder = new ();
        builder.Append("Персональні дані: " + QuestionnaireData);
        builder.Append("\n");
        builder.Append("Адреси: " + string.Join(", ", Adresses));
        builder.Append("\n");
        builder.Append("Телефони: " + PhoneNumberCollection);
        builder.Append("\n");
        builder.Append("Кліькість відносин: " + Relationships.Select(e => e.Value.Count).Sum());
        builder.Append("\n");
        builder.Append("Якості: " + Qualities);
        builder.Append("\n");
        builder.Append("Місце роботи: " + OccupationPlace);
        return builder.ToString();
    }
}