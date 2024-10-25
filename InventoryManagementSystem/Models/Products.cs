﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Models
{
    public class Products //hardware
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public int SerialNumber { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; }

        [Required]
        public string WarrantyPeriod { get; set; }

        [Required]
        [Range(1, 100000)]
        public int Price { get; set; }

        [ValidateNever]
        public int InventoryId { get; set; }
        [ForeignKey("InventoryId")]

        [ValidateNever]
        public Inventory Inventory { get; set; }

        [ValidateNever]
        public string ImageURL { get; set; }
    }
}