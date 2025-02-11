// See https://aka.ms/new-console-template for more information

using BiblioEntityCore.Class;
using BiblioEntityCore.Migrations;
using Microsoft.EntityFrameworkCore;

namespace BiblioEntityCore;

public class Program
{
    public static void Main(string[] args)
    {
        using (AppDbContext context = new AppDbContext())
        {


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
                Console.WriteLine("10. Statistics");
                Console.WriteLine("11. Exit");
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
                    case "11":
                        return;
                    default:
                        Console.WriteLine("Choix invalide !");
                        break;
                }
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
            Console.WriteLine($"{author.Name}");
        }
    }

    private static void FindAuthor(AppDbContext context)
    {
        Console.WriteLine("Enter Author Name: ");
        string? name = Console.ReadLine();
        var author = context.Authors.FirstOrDefault(a => a.Name == name);

        if (author == null)
        {
            Console.WriteLine("Author not found");
            return;
        }
        Console.WriteLine($"Author'{name}' found");
    }

    private static void DeleteAuthor(AppDbContext context)
    {
        Console.WriteLine("Enter Author Name to delete: ");
        string? name = Console.ReadLine();
        var author = context.Authors.FirstOrDefault(a => a.Name == name);
        if (author == null)
        {
            Console.WriteLine("Author not found");
            return;
        }
        context.Authors.Remove(author);
        context.SaveChanges();
    }

    
    private static void AddBook(AppDbContext context)
    {
        Console.WriteLine("Enter Book Title: ");
        string title = Console.ReadLine()??"invalid";
        Console.WriteLine("Enter Book Author: ");
        string? author = Console.ReadLine();

        var existingAuthor = context.Authors.FirstOrDefault(a => author != null && a.Name.ToLower() == author.ToLower());
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

        foreach (var book in books )
        {
            Console.WriteLine($"{book.Title}");
        }
        
    }

    private static void FindBook(AppDbContext context)
    {
        Console.WriteLine("Enter Book Title you are looking for: ");
        string? title = Console.ReadLine();
        var book = context.Books.FirstOrDefault(b => b.Title.ToLower() == title.ToLower());
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
        string? name = Console.ReadLine();

        var author = context.Authors.FirstOrDefault(a => a.Name == name);

        if (author == null)
        {
            Console.WriteLine("Author not found.");
            return;
        }

        var books = context.Books.Where(b => b.AuthorId == author.Id).ToList();

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

}