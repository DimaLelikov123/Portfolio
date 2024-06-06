namespace Notebook.Models;

class OccupationPlace : Adress
{
    private const string FORMAT = "{0} - {1}";
    public string Ocupation {get; private set;}
    public OccupationPlace(string code, string street, string home, string ocupation) : base(code, street, home)
    {
        Ocupation = ocupation;
    }

    public override string ToString()
    {
        return string.Format(FORMAT, base.ToString(), Ocupation);
    }
}