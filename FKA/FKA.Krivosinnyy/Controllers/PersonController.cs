using AutoMapper;
using FKA.Krivosinnyy.BLL.ViewModels.Person;
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
    public class PersonController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPersonService _personService;
        public PersonController(
                IMapper mapper,
                IPersonService personService
            )
        {
            _mapper = mapper;
            _personService = personService;
        }
        [Route("AddPerson")]
        [HttpGet]
        public IActionResult AddPerson()
        {
            return View(new PersonViewModel());
        }
        [Route("AddPerson")]
        [HttpPost]
        public async Task<IActionResult> AddPerson(PersonViewModel model)
        {
            if(ModelState.IsValid)
            {
                _personService.AddPerson(model);
            }
            return RedirectToAction("AllPersons", "Person");
        }
        //[Authorize(Roles = "Admin")]
        [Route("AllPersons")]
        [HttpGet]
        public IActionResult AllPersons()
        {
            return View(_personService.AllPersons());
        }
        //[Authorize(Roles = "Admin")]
        [Route("EditPerson")]
        [HttpGet]
        public IActionResult EditPerson(int personId)
        {
            return View("EditUser");///
        }
        [Authorize(Roles = "Admin")]
        [Route("Edit")]
        [HttpPost]
        public IActionResult EditPerson(PersonViewModel model)
        {
            if (ModelState.IsValid)
            {

            }
            return RedirectToAction("AllPersons");
        }
        //[Authorize(Roles = "Admin")]
        [Route("RemovePerson")]
        [HttpPost] ///почему не работает [HttpDelete]  ???
        public IActionResult RemovePerson(int personId)
        {
            _personService.DeletePerson(personId);
            return RedirectToAction("AllPersons");
        }
    }
}
