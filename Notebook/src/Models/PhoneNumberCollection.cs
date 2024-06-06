using Notebook.Enums;

namespace Notebook.Models;

class PhoneNumberCollection : AbstractCollection<PhoneNumberType, string>
{
    public PhoneNumberCollection(IEnumerable<CollectionEntry<PhoneNumberType, string>> entries) : base(entries)
    {
    }

    public void AddImportantNumber(string phoneNumber)
    {
        AddEntry(PhoneNumberType.Important, phoneNumber);
    }

    public void AddPersonalNumber(string phoneNumber)
    {
        AddEntry(PhoneNumberType.Personal, phoneNumber);
    }


    public void AddWorkNumber(string phoneNumber)
    {
        AddEntry(PhoneNumberType.Work, phoneNumber);
    }

    public override string ToString()
    {
        return string.Join(", ", entries.Select(number => number.Value));
    }
}