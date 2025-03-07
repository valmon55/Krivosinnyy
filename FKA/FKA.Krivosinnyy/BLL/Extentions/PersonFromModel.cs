using FKA.Krivosinnyy.BLL.ViewModels.Person;
using FKA.Krivosinnyy.DAL.Entities;
using FKA.Krivosinnyy.DAL.Repositories;

namespace FKA.Krivosinnyy.BLL.Extentions
{
    public static class PersonFromModel
    {
        public static Person Convert(this Person person, PersonViewModel personViewModel)
        {
            person.Id = personViewModel.Id;
            person.FirstName = personViewModel.FirstName;
            person.MiddleName = personViewModel.MiddleName;
            person.LastName = personViewModel.LastName;
            person.BirthDate = new DateTime((int)personViewModel.Year, (int)personViewModel.Month, (int)personViewModel.Day);
            person.gender = personViewModel.gender;
            //person.Avatar = new DAL.Entities.File() { Path = personViewModel.Photo };

            return person;
        }
    }
}
