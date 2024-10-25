using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Product name can't be empty!")]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}