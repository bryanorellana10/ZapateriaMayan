using CapaBaseDeDatos.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testing
{
    public partial class login : Form
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationSignInManager _signInManager;

        public login()
        {
            InitializeComponent();
        }

        private void login_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        //public async Task<bool> VerifyUserNamePassword(string userName, string password)
        //{
        //    var usermanager = new _userManager<IdentityUser>(new UserStore<IdentityUser>(new IdentityDbContext()));
        //    return await usermanager.FindAsync(userName, password) != null;

        //}
    }
}
