// See https://aka.ms/new-console-template for more information

using BiblioEntityCore.Class;
using BiblioEntityCore.Migrations;

namespace BiblioEntityCore;

public class Program
{
    public static void Main(string[] args)
    {
        using (AppDbContext context = new AppDbContext())
        {


            while (true)
            {
                Console.WriteLine("\n1. Add Author\n2. Add Book\n3. List Authors\n4. List Books\n5. Delete Author\n6. Delete Book\n7. Statistics\n8. Exit"
            );
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddAuthor(context);
                    break;
                case "8":
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
        string name = Console.ReadLine();
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
}