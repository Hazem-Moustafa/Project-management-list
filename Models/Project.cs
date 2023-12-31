﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagement.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
      
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        public int ManagerId { get; set; }
        public Member Manager { get; set; } // for just Project Manager

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool IsFinished { get; set; }

        public bool IsCancelled { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<Board> Boards { get; set; }// every project can have multiple Boards, new list must be created

        public ICollection<ProjectMember> ProjectMembers { get; set; }
    }
}
