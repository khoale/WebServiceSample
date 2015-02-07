namespace WcfDataServiceToolkitAdvanced.Bootstrapers
{
    using AdventureWorks.Core;

    using Autofac;

    public class DataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Use func can increase speed when create new instance by 10x
            builder.Register(
                context => new PersonContext());
        }
    }
}