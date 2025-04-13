namespace Library.Common.Entities;

public abstract class LibraryItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; }
    public int Year { get; set; }

    protected LibraryItem(string title, int year)
    {
        Title = title;
        Year = year;
    }
}

public class Book : LibraryItem
{
    public string Author { get; set; }
    public string ISBN { get; set; }

    public static int BookCount;

    static Book() => BookCount = 0;

    public Book(string title, int year, string author, string isbn) : base(title, year)
    {
        Author = author;
        ISBN = isbn;
        BookCount++;
    }

    public string GetDescription() => $"{Title} by {Author}, {Year}";
}

public class Magazine : LibraryItem
{
    public int IssueNumber { get; set; }

    public Magazine(string title, int year, int issueNumber) : base(title, year)
    {
        IssueNumber = issueNumber;
    }
}

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }

    public User(string name) => Name = name;
}

public delegate void ItemAddedEventHandler<T>(T item);

public static class LibraryExtensions
{
    public static void PrintAll<T>(this IEnumerable<T> items)
    {
        foreach (var item in items)
            Console.WriteLine(item);
    }
}
