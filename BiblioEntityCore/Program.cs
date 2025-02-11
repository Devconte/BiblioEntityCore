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
                Console.WriteLine("6. Find a Book by Title");
                Console.WriteLine("7. Delete Author");
                Console.WriteLine("8. Delete Book");
                Console.WriteLine("9. Statistics");
                Console.WriteLine("10. Exit");
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
                    case "10":
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

    private static void AddBook(AppDbContext context)
    {
        Console.WriteLine("Enter Book Title: ");
        string title = Console.ReadLine()??"invalid";
        Console.WriteLine("Enter Book Author: ");
        string? author = Console.ReadLine();

        var existingAuthor = context.Authors.FirstOrDefault(a => a.Name.ToLower() == author.ToLower());
        if (existingAuthor == null)
        {
            Console.WriteLine("Author not found");
            return;
        }
        var book = new Book { Title = title, Author = existingAuthor };
        context.Books.Add(book);
        context.SaveChanges();
       
    }
   
}