using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CatalogAPI.Domain;

/*
 * Many redundants data annotations ahead - doing for the sake of practice.
 */

[Table("Products")]
public class Product
{
    [Key]
    public int ProductId { get; set; }
    
    [Required]
    [StringLength(80)] // Bytes 
    public string? Name { get; set; }
    
    [Required]
    [StringLength(300)]
    public string? Description { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(10, 2)")] // 10 digits, 2 decimal places
    public decimal Price { get; set; }
    
    [Required]
    [StringLength(300)]
    public string? ImageUrl { get; set; }
    
    public float Stock { get; set; }
    
    public DateTime RegistrationDate { get; set; }
    
    public int CategoryId { get; set; }
    
    [JsonIgnore]
    public Category? Category { get; set; }
}