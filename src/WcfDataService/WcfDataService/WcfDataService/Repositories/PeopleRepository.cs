namespace WcfDataService.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using WcfDataService.Dtos;

    public class PeopleRepository
    {
        private static readonly List<Person> Peoples;

        static PeopleRepository()
        {
            Peoples = new List<Person>();
            Peoples.Add(new Person() { FirstName = "Khoa", LastName = "Le" });
            Peoples.Add(new Person() { FirstName = "Khoa", LastName = "Le" });
        }

        public IQueryable<Person> GetAll()
        {
            return Peoples.AsQueryable();
        }
    }
}