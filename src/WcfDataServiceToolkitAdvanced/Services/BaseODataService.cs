namespace WcfDataServiceToolkitAdvanced.Services
{
    using System;
    using System.Data.Entity;
    using System.Data.Services;
    using System.Data.Services.Common;
    using System.Diagnostics;

    using AdventureWorks.Core;

    using Autofac;

    using Microsoft.Data.Services.Toolkit;

    public abstract class BaseODataService<TODataContext> : ODataService<TODataContext>, IDisposable
    {
        private readonly ILifetimeScope lifetimeScope;

        private readonly PersonContext personContext;

        private DbContextTransaction transaction;

        public BaseODataService(ILifetimeScope lifetimeScope)
        {
            // ODataService using transient lifetime scope
            Debug.WriteLine("{0} object id {1}", this.GetType().FullName, this.GetHashCode());
            this.lifetimeScope = lifetimeScope;
            this.personContext = this.lifetimeScope.Resolve<PersonContext>();
            this.ProcessingPipeline.ProcessingRequest += this.OnProcessingRequest;
            this.ProcessingPipeline.ProcessedRequest += this.OnProcessedRequest;
        }

        // ReSharper disable once UnusedMember.Global
        public static void InitializeService(DataServiceConfiguration config)
        {
            // This is required
            config.SetEntitySetAccessRule("*", EntitySetRights.All);
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;
            config.UseVerboseErrors = true;
        }

        public void Dispose()
        {
            this.ReleaseTransaction();
        }

        protected virtual void OnProcessingRequest(object sender, DataServiceProcessingPipelineEventArgs dataServiceProcessingPipelineEventArgs)
        {
            // Begin UnitOfWork
            this.transaction = this.personContext.Database.BeginTransaction();
        }

        protected override void HandleException(HandleExceptionArgs args)
        {
            base.HandleException(args);

            // Rollback UnifOfWork
            if (this.transaction != null)
            {
                this.transaction.Rollback();
            }
            
            // Wrap exception with DataServiceException to show mean full message
        }

        protected virtual void OnProcessedRequest(object sender, DataServiceProcessingPipelineEventArgs e)
        {
            // Commit UnitOfWork, this should not be call when have exception
            if (this.transaction != null)
            {
                this.transaction.Commit();
            }
        }

        protected override TODataContext CreateDataSource()
        {
            return this.lifetimeScope.Resolve<TODataContext>();
        }

        private void ReleaseTransaction()
        {
            if (this.transaction != null)
            {
                this.transaction.Dispose();
                this.transaction = null;
            }
        }
    }
}