using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public class Rental
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        //[ValidateNever]

    }
}
