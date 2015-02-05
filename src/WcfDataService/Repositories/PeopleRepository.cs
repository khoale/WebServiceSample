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
            Peoples.Add(new Person() { ID = 1, FirstName = "Teo", LastName = "Nguyen" });
            Peoples.Add(new Person() { ID = 2, FirstName = "Ti", LastName = "Nguyen" });
        }

        public IQueryable<Person> GetAll()
        {
            return Peoples.AsQueryable();
        }
    }
}