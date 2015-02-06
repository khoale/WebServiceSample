namespace WcfDataServiceToolkitAdvanced.Repositories
{
    using System.Linq;

    using AdventureWorks.Core;

    public class PeopleRepository : RepositoryBase<Person, int>
    {
        private readonly PersonContext personContext;

        public PeopleRepository(PersonContext personContext)
        {
            this.personContext = personContext;
        }

        public override IQueryable<Person> GetAll()
        {
            return this.personContext.People;
        }
    }
}