using Notebook.Models;

abstract class StorageService
{
    public abstract IEnumerable<NotebookEntry> GetAll();
    public abstract void Add(NotebookEntry entry);
    public abstract void Remove(NotebookEntry entry);
}