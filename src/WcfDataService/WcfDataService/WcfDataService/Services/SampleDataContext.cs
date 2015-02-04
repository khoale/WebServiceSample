namespace WcfDataService.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.Data.Services.Toolkit.QueryModel;

    using WcfDataService.Dtos;
    using WcfDataService.Repositories;

    public class SampleDataContext : ODataContext
    {
        public IQueryable<Person> Peoples
        {
            get
            {
                return this.CreateQuery<Person>();
            }
        }

        public override object RepositoryFor(string fullTypeName)
        {
            switch (fullTypeName)
            {
                case "WcfDataService.Dtos.Person":
                    return new PeopleRepository();
            }

            throw new KeyNotFoundException(fullTypeName);
        }
    }
}