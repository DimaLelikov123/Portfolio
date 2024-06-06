namespace Notebook.Models;

class PostalCode
{
    public string Code {get; private set;}

    public PostalCode(string code)
    {
        Code = code;
    }
}