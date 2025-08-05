using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogAPI.Domain;

/*
 * Many redundants data annotations ahead - doing for the sake of practice.
 */

[Table("Categories")]
public class Category
{
    public Category()
    {
        Products = new Collection<Product>();
    }
    [Key]
    public int CategoryId { get; set; }
    
    [Required]
    [StringLength(80)] // Bytes
    public string? Name { get; set; }
    
    [StringLength(300)]
    public string? ImageUrl { get; set; }
    
    public ICollection<Product>? Products { get; set; }
}