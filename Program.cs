using System;
using System.Collections.Generic;
using System.Linq;

class Book
{
    public string Author { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }
    public string Group { get; set; }

    public Book(string author, string title, int year, string group)
    {
        Author = author;
        Title = title;
        Year = year;
        Group = group;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        var otherBook = (Book)obj;
        return Author == otherBook.Author;
    }

    public override int GetHashCode()
    {
        return Author != null ? Author.GetHashCode() : 0;
    }
}

class CollectionType<T> : List<T> { }

class Program
{
    static void Main(string[] args)
    {
        // Створення словника об'єктів CollectionType<Book>
        var collections = new Dictionary<string, CollectionType<Book>>();

        // Заповнення словника випадковими даними
        var random = new Random();
        for (int i = 0; i < 5; i++)
        {
            var collection = new CollectionType<Book>();
            int numberOfBooks = random.Next(1, 6);
            for (int j = 0; j < numberOfBooks; j++)
            {
                collection.Add(new Book($"Автор {j + 1}", $"Назва {j + 1}", 1900 + random.Next(1, 130), $"Група {j + 1}"));
            }
            collections.Add($"Колекція {i + 1}", collection);
        }

        // Заданий елемент
        string targetElement = "Автор 1";

        // Знайти кількість колекцій, що містять вказаний елемент
        int countOfCollectionsContainingTarget = collections.Count(c => c.Value.Contains(new Book(targetElement, "", 0, "")));

        // Знайти максимальну колекцію, що містить вказаний елемент
        var largestCollectionContainingTarget = collections
            .Where(c => c.Value.Contains(new Book(targetElement, "", 0, "")))
            .OrderByDescending(c => c.Value.Count)
            .FirstOrDefault(); // використовуємо FirstOrDefault для уникнення винятку, якщо таких колекцій немає

        // Вивід результатів
        Console.WriteLine($"Кількість колекцій, що містять елемент {targetElement}: {countOfCollectionsContainingTarget}");

        if (largestCollectionContainingTarget.Value != null)
        {
            Console.WriteLine($"Максимальна колекція, що містить елемент {targetElement}, містить {largestCollectionContainingTarget.Value.Count} елементів");
        }
        else
        {
            Console.WriteLine($"Немає колекцій, що містять елемент {targetElement}");
        }

        Console.ReadLine();
    }
}

