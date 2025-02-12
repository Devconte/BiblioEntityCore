using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace BiblioEntityCore.Models;

public class Author
{
	[Key] public int Id { get; set; }
	[MaxLength(255)] public string Name { get; set; } = "";
	public List<Book> Books { get; set; } = new();
}