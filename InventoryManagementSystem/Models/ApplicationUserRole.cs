﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Models
{
    [Table("AspNetUserRoles")]
    public class ApplicationUserRole : IdentityUserRole<string>
    {        
        public int Id { get; set; }

    }
}
