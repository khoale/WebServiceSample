namespace WebAPIOData
{
    using System.Threading.Tasks;

    public interface IAsyncInitialization
    {
        Task Initialize();
    }
}