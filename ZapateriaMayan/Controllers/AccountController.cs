using CapaBaseDeDatos.Data;
using CapaBaseDeDatos.Models;
using CapaBaseDeDatos.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ZapateriaMayan.ViewModels;

namespace ZapateriaMayan.Controllers
{
    public class AccountController : Controller
    {

        private readonly Context context = new Context();
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationSignInManager _signInManager;
        private readonly IAuthenticationManager _authenticationManager;


        private readonly UsersRepository _usersRepository = null;

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IAuthenticationManager authenticationManager, UsersRepository usersRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationManager = authenticationManager;
            _usersRepository = usersRepository;
        }


        [AllowAnonymous]
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignIn(AccountSignInViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            //ignore blank spaces from the textbox.
            string newEmailString = (viewModel.Email).Replace(" ", string.Empty);


            //signin 
            var result = await _signInManager.PasswordSignInAsync(newEmailString, viewModel.Password, viewModel.RememberMe, shouldLockout: false);

            //check the result
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToAction("TodasGraficas", "Dashboard");
                case SignInStatus.Failure:
                    ModelState.AddModelError("", "Los datos que ha ingresado no coinciden.");
                    return View(viewModel);
                case SignInStatus.LockedOut:
                    ModelState.AddModelError("", "El usuario ingresado está bloqueado por el administrador.");
                    return View(viewModel);
                case SignInStatus.RequiresVerification:
                    throw new NotImplementedException("Característica no implementada...");
                default:
                    throw new Exception("Unexpected Microsoft.AspNet.Identity.Owin.SignInStatus enum value: " + result);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignOut()
        {
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("TodasGraficas", "Dashboard");
        }


        public ActionResult MantenimientoUsuarios()
        {
            var users = _usersRepository.GetUsers();
            return View(users);
        }

        public ActionResult NuevoUsuario()
        {
            var rolestore = new RoleStore<Role>(context);
            var rolemanager = new RoleManager<Role>(rolestore);
            var GetRolesList = rolemanager.Roles.Select(r => r.Name).ToList();

            var viewModel = new AccountRegisterViewModel()
            {
                RoleList = new SelectList(GetRolesList)
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NuevoUsuario(AccountRegisterViewModel viewModel)
        {

            if(ModelState.IsValid)
            {
                var user = new User()
                {
                    UserName = viewModel.Email,
                    Name = viewModel.Name
                };

                await _userManager.CreateAsync(user, viewModel.Password);
                var result = await _userManager.AddToRoleAsync(user.Id, viewModel.Role);

                if (result.Succeeded)
                {
                    TempData["Message"] = "EL USUARIO SE HA CREADO CON ÉXITO.";
                    return RedirectToAction("MantenimientoUsuarios");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }

            }

            var rolestore = new RoleStore<Role>(context);
            var rolemanager = new RoleManager<Role>(rolestore);
            var GetRolesList = rolemanager.Roles.Select(r => r.Name).ToList(); 

            var view = new AccountRegisterViewModel()
            {
                RoleList = new SelectList(GetRolesList)
            };


            return View(view);
        }

        public async Task<ActionResult> EditarUsuario (string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            var viewModel = new UsersViewModel()
            {
                User = await user
            };


            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> EditarEmailUsuario(UsersViewModel viewModel)
        {
            var user = await _userManager.FindByIdAsync(viewModel.Id);
            if (user == null)
            {
                return HttpNotFound();
            }
            user.Name = viewModel.User.Name;
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                TempData["Message"] = "EL NOMBRE SE HA CAMBIADO CON ÉXITO.";
                return RedirectToAction("EditarUsuario", "Account", new { id = viewModel.Id });
            }

            return View(viewModel);
        }

        public async Task<ActionResult> UpdateNewPasswordByAdmin(UsersViewModel viewModel)
        {
            if (viewModel.NewPassword == string.Empty || viewModel.ConfirmPassword == string.Empty)
            {
                TempData["Message"] = "NO SE ACEPTAN CAMPOS VACÍOS";
                //return RedirectToAction("AccountSettings");
                return RedirectToAction("EditarUsuario", "Account", new { id = viewModel.Id });
                //result_ajax = "NO SE PERMITEN CAMPOS VACIOS";
                //return Json(result_ajax, JsonRequestBehavior.AllowGet);
            }

            if (viewModel.NewPassword == viewModel.ConfirmPassword)
            {
                var user = await _userManager.FindByIdAsync(viewModel.Id);
                if (user == null)
                {
                    return HttpNotFound();
                }

                user.PasswordHash = _userManager.PasswordHasher.HashPassword(viewModel.NewPassword);
                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    //result_ajax = "SE HA CAMBIADO";
                    //return Json(result_ajax, JsonRequestBehavior.AllowGet);
                    TempData["Message"] = "LA CONTRASEÑA SE HA CAMBIADO CON ÉXITO.";
                    //_authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                    return RedirectToAction("EditarUsuario", "Account", new { id = viewModel.Id });

                }
            }

            TempData["Message"] = "LA CONTRASEÑA NO COINCIDE. POR FAVOR INTÉNTELO DE NUEVO.";
            return RedirectToAction("EditarUsuario", "Account", new { id = viewModel.Id });


        }

        public ActionResult RoleMaintenance(string id)
        {
            var user = _userManager.FindById(id);
            var role = _userManager.GetRoles(user.Id);

            var rolestore = new RoleStore<Role>(context);
            var rolemanager = new RoleManager<Role>(rolestore);
            var GetRolesList = rolemanager.Roles.ToList(); 

            var viewModel = new UsersViewModel()
            {
                User = user,
                UserRoles = role,
                tempRolesList = GetRolesList
            };

            viewModel.Init();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateMaintenance(string Id, string NewRoleName, string oldRoleName)
        {
            var user = await _userManager.FindByIdAsync(Id);
            await _userManager.RemoveFromRoleAsync(user.Id, oldRoleName);

            var result = await _userManager.AddToRoleAsync(user.Id, NewRoleName);

            if (result.Succeeded)
            {
                TempData["Message"] = "EL ROL SE HA CAMBIADO CON ÉXITO.";
                //_authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return RedirectToAction("EditarUsuario", "Account", new { Id });
            }

            return View();

        }

        public ActionResult AccountSettings()
        {
            if (Request.IsAuthenticated)
            {
                var UserId = User.Identity.GetUserId();
                var user = _userManager.FindById(UserId);

                var role = _userManager.GetRoles(user.Id);

                var rolestore = new RoleStore<Role>(context);
                var rolemanager = new RoleManager<Role>(rolestore);
                var GetRolesList = rolemanager.Roles.ToList();


                var viewModel = new UsersViewModel()
                {
                    User = user,
                    UserRoles = role,
                    tempRolesList = GetRolesList
                };

                return View(viewModel);
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ChangePassword(string newPassword, string confirmPassword)
        {
            //string result_ajax = "SE HA PRODUCIDO UN ERROR";

            if (newPassword == string.Empty || confirmPassword == string.Empty)
            {
                TempData["Message"] = "NO SE ACEPTAN CAMPOS VACÍOS";
                return RedirectToAction("AccountSettings");
                //result_ajax = "NO SE PERMITEN CAMPOS VACIOS";
                //return Json(result_ajax, JsonRequestBehavior.AllowGet);
            }

            if (newPassword == confirmPassword)
            {
                if (Request.IsAuthenticated)
                {
                    var userId = User.Identity.GetUserId();
                    var user = await _userManager.FindByIdAsync(userId);

                    if (user == null)
                    {
                        return HttpNotFound();
                    }

                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(newPassword);
                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        //result_ajax = "SE HA CAMBIADO";
                        //return Json(result_ajax, JsonRequestBehavior.AllowGet);
                        TempData["Message"] = "LA CONTRASEÑA SE HA CAMBIADO CON ÉXITO. POR FAVOR INICIE SESIÓN.";
                        _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                        return RedirectToAction("TodasGraficas", "Dashboard");
                    }
                    else
                    {
                        TempData["Message"] = "HA OCURRIDO UN ERROR INTERNO.";
                        return RedirectToAction("AccountSettings");
                    }

                }

            }

            TempData["Message"] = "LA CONTRASEÑA NO COINCIDE. POR FAVOR INTÉNTELO DE NUEVO.";
            return RedirectToAction("AccountSettings", "Account");
        }


        [HttpPost]
        public async Task<ActionResult> ChangeUserNameEmail(string newEmail, string confirmEmail)
        {
            if (newEmail == string.Empty || confirmEmail == string.Empty)
            {
                TempData["Message"] = "NO SE ACEPTAN CAMPOS VACÍOS";
                return RedirectToAction("AccountSettings", "Account");
 
            }


            if (newEmail == confirmEmail)
            {
                if (Request.IsAuthenticated)
                {
                    var userId = User.Identity.GetUserId();
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user == null)
                    {
                        return HttpNotFound();
                    }
                    user.UserName = newEmail;
                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        TempData["Message"] = "EL CORREO SE HA CAMBIADO CON ÉXITO. POR FAVOR INICIE SESIÓN.";
                        _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                        return RedirectToAction("TodasGraficas", "Dashboard");
                    }
                    else
                    {
                        TempData["Message"] = "HA OCURRIDO UN ERROR INTERNO.";
                        return RedirectToAction("AccountSettings", "Account");
                    }
                }

            }

            TempData["Message"] = "EL CORREO INGRESADO NO COINCIDE. POR FAVOR INTÉNTELO DE NUEVO.";
            return RedirectToAction("AccountSettings", "Account");

        }


        public async Task<ActionResult> BlockUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            user.IsInactive = true;
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                TempData["Message"] = "EL USUARIO SE HA BLOQUEADO CON ÉXITO";
                return RedirectToAction("EditarUsuario", "Account", new { id });
            }

            return View(id);
        }

        public async Task<ActionResult> UnBlockUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            user.IsInactive = false;
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                TempData["Message"] = "EL USUARIO SE HA DESBLOQUEADO CON ÉXITO";
                return RedirectToAction("EditarUsuario", "Account", new {  id });
            }

            return View(id);
        }


    }
}