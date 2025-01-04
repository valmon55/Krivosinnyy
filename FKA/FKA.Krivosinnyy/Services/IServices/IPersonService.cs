using FKA.Krivosinnyy.BLL.ViewModels.Person;

namespace FKA.Krivosinnyy.Services.IServices
{
    public interface IPersonService
    {
        public List<PersonViewModel> AllPersons();
        public void AddPerson(PersonViewModel person);
        public PersonViewModel UpdatePerson(int personId);
        public void UpdatePerson(PersonViewModel person);
        public void DeletePerson(int personId);
        PersonViewModel ViewPerson(int personId);
    }
}
