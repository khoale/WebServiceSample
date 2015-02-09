namespace WcfDataServiceToolkitAdvanced.Bootstrapers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Autofac;

    public class AsyncBooter : IStartable
    {
        private readonly IEnumerable<IAsyncStartable> startables;

        public AsyncBooter(IEnumerable<IAsyncStartable> startables)
        {
            this.startables = startables;
        }

        public void Start()
        {
            Task.WhenAll(this.startables.Select(x => x.Start()));
        }
    }
}