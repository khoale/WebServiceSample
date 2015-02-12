namespace WebAPIOData
{
    using System.Threading.Tasks;

    public abstract class AsyncInitialization : IAsyncInitialization
    {
        public Task Initialize()
        {
            return Task.Factory.StartNew(this.OnStart);
        }

        protected abstract void OnStart();
    }
}