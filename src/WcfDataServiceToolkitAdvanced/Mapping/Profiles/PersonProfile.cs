namespace WcfDataServiceToolkitAdvanced.Mapping.Profiles
{
    using AdventureWorks.Core;

    using AutoMapper;

    using WcfDataServiceToolkitAdvanced.Dto;

    public class PersonProfile : Profile
    {
        protected override void Configure()
        {
            this.CreateMap<Person, PersonDto>()
                .ForMember(d => d.ID, m => m.MapFrom(s => s.BusinessEntityID));
        }
    }
}