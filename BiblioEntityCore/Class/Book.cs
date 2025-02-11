using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiblioEntityCore.Class;

public class Book

{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public int AuthorId { get; set; }
    // navigation property, allow to create the relationship with the books. https://learn.microsoft.com/en-us/ef/core/modeling/relationships
    [ForeignKey("AuthorId")]
    public Author Author { get; set; } 
}