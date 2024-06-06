using Notebook.Models;

class NotebookService : StorageService
{
    List<NotebookEntry> entries = new List<NotebookEntry>();

    public override void Add(NotebookEntry entry)
    {
        entries.Add(entry);
    }

    public override IEnumerable<NotebookEntry> GetAll()
    {
        return entries;
    }

    public override void Remove(NotebookEntry entry)
    {
        entries.Remove(entry);
    }
}