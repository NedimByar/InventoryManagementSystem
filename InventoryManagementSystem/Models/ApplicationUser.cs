﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public int userNo { get; set; }

        [Required]
        public string Department { get; set; }
    }
}