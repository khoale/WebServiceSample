namespace WebAPIOData.Initializations
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AsyncStarter
    {
        private readonly List<IAsyncInitialization> initializations;

        public AsyncStarter(
            MappingAsyncInitialization mappingAsyncInitialization, 
            ODataAsyncInitialization odataAsyncInitialization)
        {
            this.initializations = new List<IAsyncInitialization>
            {
                mappingAsyncInitialization,
                odataAsyncInitialization
            };
        }

        public void Start()
        {
            Task.WhenAll(this.initializations.Select(x => x.Initialize())).Wait();
        }
    }
}