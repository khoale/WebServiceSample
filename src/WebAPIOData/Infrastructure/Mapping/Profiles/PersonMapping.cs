namespace WebAPIOData.Infrastructure.Mapping.Profiles
{
    using AdventureWorks.Core;

    using AutoMapper;

    using WebAPIOData.Dtos;

    public class PersonMapping : Profile
    {
        protected override void Configure()
        {
            this.CreateMap<Person, PersonDto>()
                .ForMember(d => d.Id, m => m.MapFrom(s => s.BusinessEntityID));
        }
    }
}