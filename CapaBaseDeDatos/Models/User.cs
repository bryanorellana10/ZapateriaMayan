using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaBaseDeDatos.Models
{
    public class User : IdentityUser
    {
        public static object Identity { get; internal set; }


        [Required]
        public string Name { get; set; }
        public bool IsInactive { get; set; }
    }
}
