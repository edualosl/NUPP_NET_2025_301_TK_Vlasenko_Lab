namespace Library.Common.Services;

public interface ICrudService<T>
{
    void Create(T element);
    T Read(Guid id);
    IEnumerable<T> ReadAll();
    void Update(T element);
    void Remove(T element);
    void Save(string filePath);
    void Load(string filePath);
    event ItemAddedEventHandler<T> ItemAdded;
}

public class CrudService<T> : ICrudService<T> where T : class
{
    private List<T> _items = new();
    public event ItemAddedEventHandler<T>? ItemAdded;

    public void Create(T element)
    {
        _items.Add(element);
        ItemAdded?.Invoke(element);
    }

    public T Read(Guid id)
    {
        return _items.FirstOrDefault(x => (Guid)x?.GetType().GetProperty("Id")?.GetValue(x)! == id)!;
    }

    public IEnumerable<T> ReadAll() => _items;

    public void Update(T element)
    {
        var id = (Guid)element.GetType().GetProperty("Id")?.GetValue(element)!;
        Remove(Read(id));
        Create(element);
    }

    public void Remove(T element) => _items.Remove(element);

    public void Save(string filePath)
    {
        var json = System.Text.Json.JsonSerializer.Serialize(_items);
        File.WriteAllText(filePath, json);
    }

    public void Load(string filePath)
    {
        if (File.Exists(filePath))
        {
            var json = File.ReadAllText(filePath);
            var data = System.Text.Json.JsonSerializer.Deserialize<List<T>>(json);
            if (data != null)
                _items = data;
        }
    }
}
