using System;
using System.Collections.Generic;

class Book {
	public string Title { get; set; }
	public string Author { get; set; }
	public bool IsLent { get; set; }

	public Book(string title, string author) {
		Title = title;
		Author = author;
		IsLent = false;
	}
}

class Program {
	static void Main() {
		List<Book> library = new List<Book>();
		bool running = true;

		while (running) {
			Console.WriteLine("\n=== LIBRARY MENU ===");
			Console.WriteLine("1. Add Book");
			Console.WriteLine("2. Remove Book");
			Console.WriteLine("3. Lend Book");
			Console.WriteLine("4. Return Book");
			Console.WriteLine("5. Show Books");
			Console.WriteLine("6. Exit");

			Console.Write("Choose an option: ");
			string choice = Console.ReadLine();

			switch (choice) {
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
					running = false;
					Console.WriteLine("Goodbye!");
					break;

				default:
					Console.WriteLine("Invalid option.");
					break;
			}
		}
	}

	static void AddBook(List<Book> library) {
		Console.Write("Enter title: ");
		string title = Console.ReadLine();

		Console.Write("Enter author: ");
		string author = Console.ReadLine();

		library.Add(new Book(title, author));

		Console.WriteLine("Book added successfully.");
	}

	static void RemoveBook(List<Book> library) {
		Console.Write("Enter title of the book to remove: ");
		string title = Console.ReadLine();

		Book book = library.Find(b =>
			b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

		if (book != null) {
			library.Remove(book);
			Console.WriteLine("Book removed successfully.");
		}
		else {
			Console.WriteLine("Book not found.");
		}
	}

	static void LendBook(List<Book> library) {
		Console.Write("Enter title of the book to lend: ");
		string title = Console.ReadLine();

		Book book = library.Find(b =>
			b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

		if (book == null) {
			Console.WriteLine("Book not found.");
		}
		else if (book.IsLent) {
			Console.WriteLine("This book is already lent.");
		}
		else {
			book.IsLent = true;
			Console.WriteLine("Book lent successfully.");
		}
	}

	static void ReturnBook(List<Book> library) {
		Console.Write("Enter title of the book to return: ");
		string title = Console.ReadLine();

		Book book = library.Find(b =>
			b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

		if (book == null) {
			Console.WriteLine("Book not found.");
		}
		else if (!book.IsLent) {
			Console.WriteLine("This book was not lent.");
		}
		else {
			book.IsLent = false;
			Console.WriteLine("Book returned successfully.");
		}
	}

	static void ShowBooks(List<Book> library) {
		if (library.Count == 0) {
			Console.WriteLine("The library is empty.");
			return;
		}

		Console.WriteLine("\n=== BOOK LIST ===");

		foreach (Book book in library) {
			string status = book.IsLent ? "Lent" : "Available";

			Console.WriteLine($"Title: {book.Title}");
			Console.WriteLine($"Author: {book.Author}");
			Console.WriteLine($"Status: {status}");
			Console.WriteLine("-------------------");
		}
	}
}
