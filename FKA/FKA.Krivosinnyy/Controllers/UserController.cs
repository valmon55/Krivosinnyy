using Microsoft.AspNetCore.Mvc;

namespace FKA.Krivosinnyy.Controllers
{
    public class UserController : Controller
    {
        [Route("Register")]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
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
