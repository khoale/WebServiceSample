namespace WebAPIOData
{
    using SimpleInjector;

    public interface IRegistration
    {
        void Register(Container container);
    }
}