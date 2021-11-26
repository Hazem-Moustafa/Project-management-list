﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagement.Models
{
    public class List
    {
        public int ListId { get; set; }

        [Required]
        [StringLength(30)] // Think about that
        public string Title { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public Board Board { get; set; }
        public List<Work> Works { get; set; } = new List<Work>();
    }

}
