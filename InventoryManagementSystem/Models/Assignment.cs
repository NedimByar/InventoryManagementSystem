using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace InventoryManagementSystem.Models
{
    public class Assignment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        [ValidateNever]
        public ApplicationUser User { get; set; }

        [ValidateNever]
        public int ProductId {  get; set; }

        [ForeignKey("ProductId")]
        [ValidateNever]
        public Products Product { get; set; }
    }
}
