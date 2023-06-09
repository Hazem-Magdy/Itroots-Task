﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Itroots_Task.Models
{
    public class User
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }

        [ForeignKey("Role")]

        public int RoleId { get; set; }

        public virtual Role? Role { get; set; } 
    }
}
