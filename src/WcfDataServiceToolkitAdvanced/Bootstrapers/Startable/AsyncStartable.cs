namespace WcfDataServiceToolkitAdvanced.Bootstrapers
{
    using System.Threading.Tasks;

    public abstract class AsyncStartable : IAsyncStartable
    {
        public Task Start()
        {
            return Task.Factory.StartNew(this.OnStart);
        }

        protected abstract void OnStart();
    }
}