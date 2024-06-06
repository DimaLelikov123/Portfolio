namespace Notebook.Models;

class Adress : PostalCode
{
    private const string FORMAT = "|{0}|, {1}, {2}";

    public string Street {get; set;}
    public string Home {get; set;}

    public Adress(string code, string street, string home) : base(code)
    {
        Street = street;
        Home = home;
    }

    public override string ToString()
    {
        return string.Format(FORMAT, Code, Street, Home);
    }
}