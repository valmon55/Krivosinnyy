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
        private readonly IRelationshipService _relationshipService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PersonController(
                IMapper mapper,
                IPersonService personService,
                IRelationshipService relationshipService,
                IWebHostEnvironment webHostEnvironment
            )
        {
            _mapper = mapper;
            _personService = personService;
            _relationshipService = relationshipService;
            _webHostEnvironment = webHostEnvironment;
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
        [Route("ViewPerson")]
        [HttpGet]
        public IActionResult ViewPerson(int personId)
        {
            var rels = _relationshipService.PersonRelations(personId);
            return View("PersonWithRelations",rels);
        }
        [Route("SetAvatar")]
        [HttpPost]
        public async Task<IActionResult> SetAvatar(int personId, IFormFile uploadedFile)
        {
            var uniqFileName = "/Files/" + Guid.NewGuid().ToString() + ".jpeg";

            if(uploadedFile != null)
            {
                //string path = "/Files/" + uploadedFile.FileName;
                using(var fileStream = new FileStream(_webHostEnvironment.WebRootPath + uniqFileName, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                ///FileService --Add   
                _personService.SetAvatar(personId, uniqFileName);
            }
            var rels = _relationshipService.PersonRelations(personId);
            return View("PersonWithRelations", rels);
        }
        //[Authorize(Roles = "Admin")]
        [Route("EditPerson")]
        [HttpGet]
        public IActionResult EditPerson(int personId)
        {
            return View("EditPerson",_personService.ViewPerson(personId));
        }
        [Authorize(Roles = "Admin")]
        [Route("EditPerson")]
        [HttpPost]
        public IActionResult EditPerson(PersonViewModel model)
        {
            if (ModelState.IsValid)
            {
                _personService.UpdatePerson(model);
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
