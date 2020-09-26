using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesBoard.Models
{
    public class ApplicationUser :IdentityUser
    {
        public string Name { get; set; }  
        public int Age { get; set; } 
        public byte[] Avtar { get; set; }
    }
}
