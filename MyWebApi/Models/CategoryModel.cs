﻿using System.ComponentModel.DataAnnotations;

namespace MyWebApi.Models
{
    public class CategoryModel
    {
        public int? Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Name { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
