// See https://aka.ms/new-console-template for more information

using BiblioEntityCore.Class;

namespace BiblioEntityCore;

public class Program
{
    public static void Main(string[] args)
    {
        Constants.ConnectionPassword = args[1];


        Console.WriteLine("Hello World!");
    }
}