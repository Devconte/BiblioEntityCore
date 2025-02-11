using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiblioEntityCore.Class;

public class Book

{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public int AuthorId { get; set; }
    // navigation property, allow to create the relationship with the books. https://learn.microsoft.com/en-us/ef/core/modeling/relationships
    public Author Author { get; set; } 
}