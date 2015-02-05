namespace WcfDataService.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.Data.Services.Toolkit.QueryModel;

    using WcfDataService.Dtos;
    using WcfDataService.Repositories;

    public class SampleDataContext : ODataContext
    {
        public IQueryable<Person> People
        {
            get
            {
                return this.CreateQuery<Person>();
            }
        }

        public IQueryable<Product> Products
        {
            get
            {
                return this.CreateQuery<Product>();
            }
        }

        public override object RepositoryFor(string fullTypeName)
        {
            switch (fullTypeName)
            {
                case "WcfDataService.Dtos.Person":
                    return new PeopleRepository();
                case "WcfDataService.Dtos.Product":
                    return new PeopleRepository();
            }

            throw new KeyNotFoundException(fullTypeName);
        }
    }
}