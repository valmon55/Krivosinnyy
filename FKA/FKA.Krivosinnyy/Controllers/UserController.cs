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
        //private readonly RoleManager<Role> _roleManager;
        public UserController(UserManager<User> userManager,
                SignInManager<User> signInManager//,                RoleManager<Role> roleManager
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            //_roleManager = roleManager;
        }
        //public UserController() { }
        [Route("Register")]
        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }
        //[Route("Register")]
        //[HttpPost]
        //public async Task<IActionResult> Register(RegisterViewModel model)
        //{

        //}
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
