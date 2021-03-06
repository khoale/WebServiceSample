﻿namespace WcfDataServiceToolkitAdvanced.Services
{
    using System.Linq;

    using WcfDataServiceToolkitAdvanced.Dto;
    using WcfDataServiceToolkitAdvanced.Repositories;

    public class PersonODataContext : BaseODataContext
    {
        public PersonODataContext(RepositoryFactory repositoryFactory)
            : base(repositoryFactory)
        {
        }

        public IQueryable<PersonDto> People
        {
            get
            {
                return this.CreateQuery<PersonDto>();
            }
        }
    }
}