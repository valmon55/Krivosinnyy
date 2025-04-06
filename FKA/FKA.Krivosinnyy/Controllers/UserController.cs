using AutoMapper;
using FKA.Krivosinnyy.BLL.ViewModels.User;
using FKA.Krivosinnyy.DAL.Entities;
using FKA.Krivosinnyy.Services;
using FKA.Krivosinnyy.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FKA.Krivosinnyy.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public UserController(UserManager<User> userManager,
                SignInManager<User> signInManager,                
                RoleManager<Role> roleManager,
                IMapper mapper,
                IUserService userService
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _userService = userService;
        }
        //public UserController() { }
        [Route("Register")]
        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }
        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                //Админа надо создавать при построении проекта
                var userRole = new Role() { Name = "Admin", Description = "Администратор" };

                if (!_roleManager.RoleExistsAsync(userRole.Name).Result)
                {
                    await _roleManager.CreateAsync(userRole);
                }
                var user = _mapper.Map<User>(model);
                var result = await _userManager.CreateAsync(user, model.PasswordReg);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    //var currentUser = await _userManager.FindByIdAsync(user.Id);
                    var currentUser = await _userManager.FindByEmailAsync(user.Email);

                    await _userManager.AddToRoleAsync(currentUser, userRole.Name);
                    await _signInManager.RefreshSignInAsync(currentUser);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [Route("Login")]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<User>(model);
                User signedUser = _userManager.Users.FirstOrDefault(u => u.Email == model.Email);
                if (signedUser == null)
                {
                    ModelState.AddModelError("Login", "Неверный логин");
                }
                var role = _userManager.GetRolesAsync(user).Result.FirstOrDefault();
                if (role is null)
                {
                    if (signedUser.UserName == "Admin")
                    {
                        await _userManager.AddToRoleAsync(signedUser, "Admin");
                    }
                    else
                    {
                        var defaultRole = _roleManager.Roles.Where(r => r.Name != "Admin").FirstOrDefault();
                        await _userManager.AddToRoleAsync(signedUser, "Admin");
                    }
                }
                if (signedUser != null)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                        new Claim(ClaimsIdentity.DefaultRoleClaimType, _userManager.GetRolesAsync(signedUser).Result.FirstOrDefault())
                    };
                    await _signInManager.SignInWithClaimsAsync(signedUser, isPersistent: false, claims);
                }
                else
                {

                }
            }
            return RedirectToAction("Index", "Home");
        }
        [Route("Logout")]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [Authorize(Roles = "Admin")]
        [Route("AllUsers")]
        [HttpGet]
        public IActionResult AllUsers()
        {
            return View(_userService.AllUsers());
        }
        [Route("ChangePassword")]
        [HttpGet]
        public IActionResult ChangePassword(UInt32 Id)
        {
            return View(new ChangePasswordViewModel() { Id = Id, Password = "", NewPassword = "", NewPasswordConfirm = ""});
        }
        [Route("ChangePassword")]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if(ModelState.IsValid)
            {
                var userViewModel = _userService.GetUser(model.Id);
                var user = _mapper.Map<User>(userViewModel);
                if(user != null)
                {
                    ///Обновляем пароль
                    var result = await _userManager.ChangePasswordAsync(user, model.Password, model.NewPassword);
                    if(result.Succeeded)
                    {
                        return RedirectToAction("AllUsers");
                    }
                    else
                    {
                        foreach(var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");
                }
            }
            return View(model);            
        }
        [Authorize(Roles = "Admin")]
        [Route("Edit")]
        [HttpGet]
        public IActionResult Edit(UInt32 userId)
        {
            return View("EditUser",_userService.GetUser(userId));///
        }
        [Authorize(Roles = "Admin")]
        [Route("Edit")]
        [HttpPost]
        public IActionResult Edit(UserViewModel model)
        {
            //if (ModelState.IsValid)
            //{
            //    _userService.UpdateUser(model);
            //}
            _userService.UpdateUser(model);
            return RedirectToAction("AllUsers");
        }
        [Authorize(Roles = "Admin")]
        [Route("Delete")]
        [HttpDelete]
        public IActionResult Delete(UInt32 userId)
        {
            return RedirectToAction("AllUsers");
        }
    }
}
