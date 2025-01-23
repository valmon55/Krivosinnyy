using AutoMapper;
using FKA.Krivosinnyy.BLL.ViewModels.Person;
using FKA.Krivosinnyy.BLL.ViewModels.Relationship;
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
    public class RelationshipController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPersonService _personService;
        private readonly IRelationshipService _relationshipService;
        public RelationshipController(
                IMapper mapper,
                IPersonService personService,
                IRelationshipService relationshipService
            )
        {
            _mapper = mapper;
            _personService = personService;
            _relationshipService = relationshipService;
        }
        [Route("AddPersonRelation")]
        [HttpGet]
        /// для человека с personId
        /// подготавливаем список возможных людей 
        /// исключаем себя и  уже добавленных людей
        public IActionResult AddPersonRelation(int personId)
        {
            var rels = _relationshipService.EditPersonRelations(personId);
            return View(rels);
        }
        [Route("AddPersonRelation")]
        [HttpPost]
        public async Task<IActionResult> AddPersonRelation(AddPersonRelationsViewModel model)
        {
            if(ModelState.IsValid)
            {
                _relationshipService.SavePersonRelations(model);
            }
            return RedirectToAction("AllPersons", "Person");
        }
        //[Authorize(Roles = "Admin")]
        [Route("RemovePersonRelation")]
        [HttpPost] ///почему не работает [HttpDelete]  ???
        /// для человека с personId
        /// убираем человека "person" из связей
        public IActionResult RemovePersonRelation(int personId, Person person)
        {
            
            return RedirectToAction("AllPersons","Person");
        }
    }
}
