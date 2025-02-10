// See https://aka.ms/new-console-template for more information

using BiblioEntityCore.Class;

namespace BiblioEntityCore;

public class Program
{
    public static void Main(string[] args)
    {
        using (AppDbContext context = new AppDbContext())
        {
        
            Console.WriteLine("Hello World!");
            
        }

    }
}