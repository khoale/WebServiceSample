namespace WcfDataServiceToolkitAdvanced.Bootstrapers
{
    using System.Threading.Tasks;

    public interface IAsyncStartable
    {
        Task Start();
    }
}