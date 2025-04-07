using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FKA.Krivosinnyy.BLL.ViewModels.User;
using FKA.Krivosinnyy.DAL.Entities;
using FKA.Krivosinnyy.Services.IServices;
using FKA.Krivosinnyy.Services;
using System.Security.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
            if(ModelState.IsValid)
            {
                var result = await _userService.Login(model);
                if(result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный лигин или пароль");
                }
                return View(model);
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
                /// Нужно так делать, чтобы заполнился PasswordHash
                var user = await _userManager.FindByEmailAsync(userViewModel.Email);
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
