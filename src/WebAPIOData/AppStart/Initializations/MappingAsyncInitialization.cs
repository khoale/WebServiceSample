namespace WebAPIOData.Initializations
{
    using AutoMapper;

    using WebAPIOData.Infrastructure.Mapping.Profiles;

    public class MappingAsyncInitialization : AsyncInitialization
    {
        protected override void OnStart()
        {
            Mapper.Initialize(
                configuration =>
                    {
                        configuration.AddProfile<PersonMapping>();
                    });
        }
    }
}