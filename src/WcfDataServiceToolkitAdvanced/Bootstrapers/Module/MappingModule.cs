namespace WcfDataServiceToolkitAdvanced.Bootstrapers
{
    using System;
    using System.Diagnostics;

    using Autofac;

    using AutoMapper;

    using WcfDataServiceToolkitAdvanced.Mapping.Profiles;

    public class MappingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MappingStartable>().As<IAsyncStartable>();
        }
    }

    public class MappingStartable : AsyncStartable
    {
        protected override void OnStart()
        {
            Mapper.Initialize(
                configuration =>
                    {
                        configuration.AddProfile<PersonProfile>();
                    });

            this.VerifyWhenDebug();
        }

        [Conditional("DEBUG")]
        private void VerifyWhenDebug()
        {
            try
            {
                Mapper.AssertConfigurationIsValid();
            }
            catch (Exception)
            {
                if (Debugger.IsAttached)
                {
                    Debugger.Break();
                }

                throw;
            }
        }
    }
}