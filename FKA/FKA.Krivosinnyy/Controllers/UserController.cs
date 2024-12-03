using AutoMapper;
using FKA.Krivosinnyy.BLL.ViewModels.User;
using FKA.Krivosinnyy.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FKA.Krivosinnyy.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        public UserController(UserManager<User> userManager,
                SignInManager<User> signInManager,                
                RoleManager<Role> roleManager,
                IMapper mapper
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
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
                    var currentUser = await _userManager.FindByIdAsync(user.Id);

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
        //[Route("Login")]
        //[HttpPost]
        //public async Task<IActionResult> Login(LoginViewModel model)
        //{

        //}
        [Route("Logout")]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
