using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Models
{
    [Table("AspNetRoles")]
    public class ApplicationRole : IdentityRole
    {
    }
}
