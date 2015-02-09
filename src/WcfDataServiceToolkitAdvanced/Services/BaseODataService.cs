namespace WcfDataServiceToolkitAdvanced.Services
{
    using System.Data.Services;
    using System.Data.Services.Common;

    using Autofac;

    using Microsoft.Data.Services.Toolkit;

    public abstract class BaseODataService<TODataContext> : ODataService<TODataContext>
    {
        private readonly ILifetimeScope lifetimeScope;

        public BaseODataService(ILifetimeScope lifetimeScope)
        {
            this.lifetimeScope = lifetimeScope;
            this.ProcessingPipeline.ProcessingRequest += this.OnProcessingRequest;
            this.ProcessingPipeline.ProcessedRequest += this.OnProcessedRequest;
        }

        protected virtual void OnProcessingRequest(object sender, DataServiceProcessingPipelineEventArgs dataServiceProcessingPipelineEventArgs)
        {
            // Begin UnitOfWork
        }

        protected override void HandleException(HandleExceptionArgs args)
        {
            // This rollback UnifOfWork
            // Wrap exception with data service exception
            base.HandleException(args);
        }

        protected virtual void OnProcessedRequest(object sender, DataServiceProcessingPipelineEventArgs e)
        {
            // Commit UnitOfWork
        }

        // ReSharper disable once UnusedMember.Global
        public static void InitializeService(DataServiceConfiguration config)
        {
            // This is required
            config.SetEntitySetAccessRule("*", EntitySetRights.All);
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;
            config.UseVerboseErrors = true;
        }

        protected override TODataContext CreateDataSource()
        {
            return this.lifetimeScope.Resolve<TODataContext>();
        }
    }
}