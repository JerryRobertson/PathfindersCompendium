﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PathfindersCompendium.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(28)]
        public string FirebaseUserId { get; set; }
        [Required]
        [MaxLength(50)]
        public string DisplayName { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [MaxLength(255)]
        [DataType(DataType.ImageUrl)]
        public List<Sheet> Sheets { get; set; }
    }
}
