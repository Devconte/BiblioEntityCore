using BiblioEntityCore.Data;
using BiblioEntityCore.Models;
using Microsoft.EntityFrameworkCore;

namespace BiblioEntityCore;

public class Program
{
	public static void Main(string[] args)
	{
		AppDbContext context = new AppDbContext();
		while (true)
		{
			Console.WriteLine("1. Add Author");
			Console.WriteLine("2. Add Book");
			Console.WriteLine("3. List Authors");
			Console.WriteLine("4. Find an Author by Name");
			Console.WriteLine("5. List Books");
			Console.WriteLine("6. Find books for a specific Author");
			Console.WriteLine("7. Find a Book by Title");
			Console.WriteLine("8. Delete Author");
			Console.WriteLine("9. Delete Book");
			Console.WriteLine("10. Count books");
			Console.WriteLine("11. Count books for a specific Author");
			Console.WriteLine("12. List authors with more than one book");
			Console.WriteLine("13. Verify if an author exists");
			Console.WriteLine("14. Exit");
			var choice = Console.ReadLine();

			switch (choice)
			{
				case "1":
					AddAuthor(context);
					break;
				case "2":
					AddBook(context);
					break;
				case "3":
					ListAuthors(context);
					break;
				case "4":
					FindAuthor(context);
					break;
				case "5":
					ListBooks(context);
					break;
				case "6":
					ListAllBooksByAuthor(context);
					break;
				case "7":
					FindBook(context);
					break;
				case "8":
					DeleteAuthor(context);
					break;
				case "9":
					DeleteBook(context);
					break;
				case "10":
					CountBooks(context);
					break;
				case "11":
					CountBooksByAuthor(context);
					return;
				case "12":
					ListAuthorsWithMoreThanOneBooks(context);
					return;
				case "13":
					VerifyIfAuthorExists(context);
					return;
				case "14":
					break;
				default:
					Console.WriteLine("Choix invalide !");
					break;
			}
		}
	}

	private static void AddAuthor(AppDbContext context)
	{
		Console.WriteLine("Enter Author Name: ");
		string? name = Console.ReadLine();
		if (string.IsNullOrWhiteSpace(name))
		{
			Console.WriteLine("Name cannot be empty");
			return;
		}

		var author = new Author { Name = name };
		context.Authors.Add(author);
		context.SaveChanges();
		Console.WriteLine($"Author '{name}' added");
	}

	private static void ListAuthors(AppDbContext context)
	{
		var authors = context.Authors.ToList();
		if (authors.Count == 0)
		{
			Console.WriteLine("There are no authors");
			return;
		}

		foreach (var author in authors)
		{
			Console.WriteLine(author.Name);
		}
	}

	private static void FindAuthor(AppDbContext context)
	{
		Console.WriteLine("Enter Author Name: ");
		string name = Console.ReadLine() ?? "";
		// == Verifie les 2 objets, .Equals() vérifie le contenu de la String
		var author = context.Authors.FirstOrDefault(a => a.Name.Equals(name));

		Console.WriteLine(
			author == null
				? "Author not found"
				: $"Author'{name}' found"
		);
	}

	private static void DeleteAuthor(AppDbContext context)
	{
		Console.WriteLine("Enter Author Name to delete: ");
		string name = Console.ReadLine() ?? "";
		var author = context.Authors.FirstOrDefault(a => a.Name == name);
		if (author == null)
		{
			Console.WriteLine("Author not found");
			return;
		}

		context.Authors.Remove(author);
		context.SaveChanges();
		Console.WriteLine($"Author '{name}' deleted");
	}


	private static void AddBook(AppDbContext context)
	{
		Console.WriteLine("Enter Book Title: ");
		string title = Console.ReadLine() ?? "invalid";
		Console.WriteLine("Enter Book Author: ");
		string? author = Console.ReadLine() ?? "";

		var existingAuthor =
			context.Authors.FirstOrDefault(a => a.Name.ToLower().Equals(author.ToLower()));
		if (existingAuthor == null)
		{
			Console.WriteLine("Author not found");
			return;
		}

		var book = new Book { Title = title, Author = existingAuthor };
		context.Books.Add(book);
		context.SaveChanges();
	}

	private static void ListBooks(AppDbContext context)
	{
		var books = context.Books.ToList();
		if (books.Count == 0)
		{
			Console.WriteLine("There are no books");
			return;
		}

		// Suppression des {} car qu'une seule instruction suivant le foreach (marche aussi pour if-else, while, ...)
		foreach (var book in books)
			Console.WriteLine($"{book.Title}");
	}

	private static void FindBook(AppDbContext context)
	{
		Console.WriteLine("Enter Book Title you are looking for: ");
		string title = Console.ReadLine() ?? "";
		var book = context.Books.FirstOrDefault(b => b.Title.ToLower().Equals(title.ToLower()));
		if (book == null)
		{
			Console.WriteLine("Book not found");
			return;
		}

		Console.WriteLine($"Book '{title}' was found");
	}

	private static void ListAllBooksByAuthor(AppDbContext context)
	{
		Console.Write("Enter Author Name: ");
		string name = Console.ReadLine() ?? "";

		// Utilisation du Include pour charger les livres appartenant à Author
		var author = context.Authors
			.Include(a => a.Books)
			.FirstOrDefault(a => a.Name.ToLower().Equals(name.ToLower()));

		if (author == null)
		{
			Console.WriteLine("Author not found.");
			return;
		}

		// Utilisation de l'ORM et non du SQL
		var books = author.Books.ToList();

		if (books.Count == 0)
		{
			Console.WriteLine($"No books found for {name}.");
			return;
		}

		Console.WriteLine($"Books by {name}:");
		foreach (var book in books)
		{
			Console.WriteLine($"- {book.Title}");
		}
	}

	private static void DeleteBook(AppDbContext context)
	{
		Console.WriteLine("Enter Book name to delete: ");
		string name = Console.ReadLine() ?? "";

		var findBookToDelete = context.Books.FirstOrDefault(b => b.Title.ToLower().Equals(name.ToLower()));

		if (findBookToDelete == null) return;

		context.Books.Remove(findBookToDelete);
		context.SaveChanges();
		Console.WriteLine($"Book '{name}' deleted");
	}

	private static void CountBooks(AppDbContext context)
	{
		var booksCount = context.Books.Count();
		Console.WriteLine($"There are {booksCount} books in the library");
	}

	private static void CountBooksByAuthor(AppDbContext context)
	{
		Console.WriteLine("Enter author's name to count their books:");
		string name = Console.ReadLine() ?? "";
		// On charge les données de l'auteur et la référence des livres grâce au Include
		Author? author = context.Authors
			.Include(a => a.Books)
			.FirstOrDefault(a => a.Name.ToLower().Equals(name.ToLower()));
		if (author == null)
		{
			Console.WriteLine("Author not found");
			return;
		}

		// On récupère les livres grâce a la liste stocké dans author
		List<Book> books = author.Books.ToList();
		var count = books.Count;
		Console.WriteLine($"There are {count} books written by {name} in our library");
	}

	private static void ListAuthorsWithMoreThanOneBooks(AppDbContext context)
	{
		var authors = context.Books
			.GroupBy(b => b.Author)
			.Where(c => c.Count() > 1)
			.Select(c => new { Author = c.Key, Count = c.Count() })
			.ToList();

		if (authors.Count == 0) return;
		Console.WriteLine("Authors with more than one book written:");
		foreach (var author in authors)
		{
			Console.WriteLine($"- {author.Author.Name} ({author.Count} books)");
		}
	}

	private static void VerifyIfAuthorExists(AppDbContext context)
	{
		Console.WriteLine("Enter Author Name to verify: ");
		string name = Console.ReadLine() ?? "";
		bool authorExists = context.Authors.Any(a => a.Name.ToLower().Equals(name.ToLower()));
		Console.WriteLine(authorExists ? $"Author '{name}' found" : $"Author '{name}' does not exist");
	}
}