using CapaBaseDeDatos.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZapateriaMayan.ViewModels
{
    public class UsersViewModel
    {
        public User User { get; set; } = new User();
        public IList<string> UserRoles { get; set; }
        public SelectList Roles { get; set; }

        public List<Role> tempRolesList { get; set; }

        public string Ids { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Required]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "La contraseña de confirmación es inválida.")]
        public string ConfirmPassword { get; set; }


        public UsersViewModel()
        {

        }

        public UsersViewModel(string id)
        {
            Ids = id;
        }

        public virtual void Init()
        {
            Roles = new SelectList(tempRolesList, "Name", "Name");
        }

        public string Id
        {
            get { return User.Id; }
            set { User.Id = value; }
        }
    }
}