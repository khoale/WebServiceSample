namespace WebAPIOData.Infrastructure.Mapping.Profiles
{
    using AdventureWorks.Core;

    using AutoMapper;

    using WebAPIOData.Dtos;

    public class CountryRegionMapping : Profile
    {
        protected override void Configure()
        {
            this.CreateMap<CountryRegion, CountryRegionDto>();
        }
    }
}