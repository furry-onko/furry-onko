using System;
using System.Collections.Generic;

class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Category { get; set; }
    public bool IsLent { get; set; }

    public Book(int id, string title, string author, string category)
    {
        Id = id;
        Title = title;
        Author = author;
        Category = category;
        IsLent = false;
    }
}

class Program
{
    static int nextId = 1;

    static void Main()
    {
        List<Book> library = new List<Book>();
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n=== LIBRARY MENU ===");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. Remove Book by ID");
            Console.WriteLine("3. Lend Book by ID");
            Console.WriteLine("4. Return Book by ID");
            Console.WriteLine("5. Show Books");
            Console.WriteLine("6. Sort Books");
            Console.WriteLine("7. Exit");

            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddBook(library);
                    break;

                case "2":
                    RemoveBook(library);
                    break;

                case "3":
                    LendBook(library);
                    break;

                case "4":
                    ReturnBook(library);
                    break;

                case "5":
                    ShowBooks(library);
                    break;

                case "6":
                    SortBooks(library);
                    break;

                case "7":
                    running = false;
                    Console.WriteLine("Goodbye!");
                    break;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }

    static void AddBook(List<Book> library)
    {
        Console.Write("Enter title: ");
        string title = Console.ReadLine();

        Console.Write("Enter author: ");
        string author = Console.ReadLine();

        Console.Write("Enter category: ");
        string category = Console.ReadLine();

        Book newBook = new Book(nextId, title, author, category);
        library.Add(newBook);

        Console.WriteLine("\nBook added successfully.");
        Console.WriteLine($"Assigned ID: {newBook.Id}");
        Console.WriteLine($"Title: {newBook.Title}");
        Console.WriteLine($"Author: {newBook.Author}");
        Console.WriteLine($"Category: {newBook.Category}");
        Console.WriteLine("Status: Available");

        nextId++;
    }

    static void RemoveBook(List<Book> library)
    {
        Console.Write("Enter ID of the book to remove: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        Book book = library.Find(b => b.Id == id);

        if (book != null)
        {
            library.Remove(book);
            Console.WriteLine("Book removed successfully.");
        }
        else
        {
            Console.WriteLine("Book not found.");
        }
    }

    static void LendBook(List<Book> library)
    {
        Console.Write("Enter ID of the book to lend: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        Book book = library.Find(b => b.Id == id);

        if (book == null)
        {
            Console.WriteLine("Book not found.");
        }
        else if (book.IsLent)
        {
            Console.WriteLine("This book is already lent.");
        }
        else
        {
            book.IsLent = true;
            Console.WriteLine("Book lent successfully.");
        }
    }

    static void ReturnBook(List<Book> library)
    {
        Console.Write("Enter ID of the book to return: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        Book book = library.Find(b => b.Id == id);

        if (book == null)
        {
            Console.WriteLine("Book not found.");
        }
        else if (!book.IsLent)
        {
            Console.WriteLine("This book was not lent.");
        }
        else
        {
            book.IsLent = false;
            Console.WriteLine("Book returned successfully.");
        }
    }

    static void ShowBooks(List<Book> library)
    {
        if (library.Count == 0)
        {
            Console.WriteLine("The library is empty.");
            return;
        }

        Console.WriteLine("\n=== BOOK LIST ===");

        foreach (Book book in library)
        {
            string status = book.IsLent ? "Lent" : "Available";

            Console.WriteLine($"ID: {book.Id}");
            Console.WriteLine($"Title: {book.Title}");
            Console.WriteLine($"Author: {book.Author}");
            Console.WriteLine($"Category: {book.Category}");
            Console.WriteLine($"Status: {status}");
            Console.WriteLine("-------------------");
        }
    }

    static void SortBooks(List<Book> library)
    {
        if (library.Count == 0)
        {
            Console.WriteLine("The library is empty.");
            return;
        }

        Console.WriteLine("\n=== SORT BOOKS ===");
        Console.WriteLine("1. Sort by ID");
        Console.WriteLine("2. Sort by title");
        Console.WriteLine("3. Sort by author");
        Console.WriteLine("4. Sort by category");
        Console.WriteLine("5. Sort by status");

        Console.Write("Choose sorting option: ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                library.Sort((a, b) => a.Id.CompareTo(b.Id));
                Console.WriteLine("Books sorted by ID.");
                break;

            case "2":
                library.Sort((a, b) => a.Title.CompareTo(b.Title));
                Console.WriteLine("Books sorted by title.");
                break;

            case "3":
                library.Sort((a, b) => a.Author.CompareTo(b.Author));
                Console.WriteLine("Books sorted by author.");
                break;

            case "4":
                library.Sort((a, b) => a.Category.CompareTo(b.Category));
                Console.WriteLine("Books sorted by category.");
                break;

            case "5":
                library.Sort((a, b) => a.IsLent.CompareTo(b.IsLent));
                Console.WriteLine("Books sorted by status.");
                break;

            default:
                Console.WriteLine("Invalid sorting option.");
                break;
        }

        ShowBooks(library);
    }
}
