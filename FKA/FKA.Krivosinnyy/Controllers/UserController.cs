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
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace FKA.Krivosinnyy.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IEmailSender _emailSender;
        public UserController(UserManager<User> userManager,
                SignInManager<User> signInManager,
                RoleManager<Role> roleManager,
                IMapper mapper,
                IUserService userService,
                IEmailSender emailSender
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _userService = userService;
            _emailSender = emailSender;
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
            if (ModelState.IsValid)
            {
                //Админа надо создавать при построении проекта
                var userRole = new Role() { Name = "Admin", Description = "Администратор" };

                if (!_roleManager.RoleExistsAsync(userRole.Name).Result)
                {
                    await _roleManager.CreateAsync(userRole);
                }
                var user = _mapper.Map<User>(model);

                if (_userManager.FindByEmailAsync(user.Email).Result != null)
                {
                    ModelState.AddModelError("", "Пользователь с таким Email уже зарегистрирован");
                    return View(model);
                }
                else
                {
                    var result = await _userManager.CreateAsync(user, model.PasswordReg);
                    if (result.Succeeded)
                    {
                        //// генерация токена пользователя
                        //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        //var callbackUrl = Url.Action(

                        //    "ConfirmEmail",
                        //    "User",
                        //    new { email = model.Email, code = code },
                        //    protocol: HttpContext.Request.Scheme
                        //    );
                        //// не получается отправить почту((
                        //_emailSender.Sent(model.Email, "Подтвердите ваш аккаунт",
                        //    $"Уважаемый {user.First_Name}! {Environment.NewLine} Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>link</a>");

                        //return Content("Для завершения регистрации проверьте электронную почту и перейдите по ссылке, указанной в письме");

                        //var rand = new Random(1000000);
                        //var code = rand.Next();
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        _emailSender.Sent(model.Email, "test",
                            $"{user.First_Name}! {Environment.NewLine} Код: <h2> {code} </h2>");

                        await _signInManager.SignInAsync(user, isPersistent: false);
                        //var currentUser = await _userManager.FindByIdAsync(user.Id);
                        var currentUser = await _userManager.FindByEmailAsync(user.Email);

                        await _userManager.AddToRoleAsync(currentUser, userRole.Name);
                        await _signInManager.RefreshSignInAsync(currentUser);

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        //если не создался корректно - удаляем
                        await _userManager.DeleteAsync(user);
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
        }
        [Route("ConfirmAccount")]
        [HttpGet]
        public IActionResult ConfirmAccount(UInt32 userId)
        {
            var user = _userService.GetUser(userId);
            var confAcc = new ConfirmAccountViewModel();
            if (user != null)
            {
                confAcc.Id = userId;
                confAcc.Email = user.Email;
            }
            return View("ConfirmAccount", confAcc);
        }
        [Route("ConfirmAccount")]
        [HttpPost]
        public async Task<IActionResult> ConfirmAccount(ConfirmAccountViewModel model)
        {
            if(ModelState.IsValid)
            {
                if (model.Email == null || model.Code == null)
                {
                    return View("Error");
                }
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return View("Error");
                }
                var result = await _userManager.ConfirmEmailAsync(user, model.Code);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View("Error");
                }
            }
            else
            {
                return View(model);
            }
        }
        [Route("ForgotPassword")]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [Route("ForgotPassword")]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null )
            {
                ModelState.AddModelError("", "Пользователь не найден!");
                return View(model); //?
            }
            if(!(await _userManager.IsEmailConfirmedAsync(user)))
            {
                ModelState.AddModelError("",$"Пользователь {model.Email} не активирован!");
                return View(model);
            }
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            _emailSender.Sent(model.Email, "test reset", $"{user.First_Name}! {Environment.NewLine} Код: <h2> {code} </h2>");

            return RedirectToAction("ResetPassword",new { email = model.Email });
        }
        [Route("ResetPassword")]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string email)
        {
            return View(new ResetPasswordViewModel() { Email = email });
        }
        [Route("ResetPassword")]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Не все поля заполнены!");
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if(user == null)
            {
                ModelState.AddModelError("", "Пользователь не найден!");
                return View(model);
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if(result.Succeeded)
            {
                return View("ResetPasswordConfirmation");
            }
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string email, string code)
        {
            if(email == null || code == null)
            {
                return View("Error");
            }
            var user = await _userManager.FindByEmailAsync(email);
            if(user  == null)
            {
                return View("Error");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if(result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Error");
            }
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
