using Library.Common.Entities;
using Library.Common.Services;

class Program
{
    static void Main()
    {
        var bookService = new CrudService<Book>();
        bookService.ItemAdded += item => Console.WriteLine($"[EVENT] Added: {item.Title}");

        var book1 = new Book("1984", 1949, "George Orwell", "123-456");
        var book2 = new Book("Brave New World", 1932, "Aldous Huxley", "789-012");

        bookService.Create(book1);
        bookService.Create(book2);

        Console.WriteLine("All Books:");
        foreach (var b in bookService.ReadAll())
            Console.WriteLine(b.GetDescription());

        var savePath = "books.json";
        bookService.Save(savePath);
        Console.WriteLine($"Saved to {savePath}");

        var newService = new CrudService<Book>();
        newService.Load(savePath);
        Console.WriteLine("Loaded books:");
        foreach (var b in newService.ReadAll())
            Console.WriteLine(b.GetDescription());
    }
}
