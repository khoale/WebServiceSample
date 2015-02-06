namespace WcfDataServiceToolkitAdvanced.Repositories
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    public abstract class RepositoryBase<TResource, TKey> : IRepository<TResource>
        where TResource : class, new()
    {
        public virtual TResource CreateResource()
        {
            return new TResource();
        }

        public virtual IQueryable<TResource> GetAll()
        {
            throw new NotSupportedException();
        }

        public virtual void Save(TResource dto)
        {
            throw new NotSupportedException();
        }

        public virtual void Update(TResource dto)
        {
            throw new NotSupportedException();
        }

        public virtual TResource GetOne(TKey id)
        {
            throw new NotSupportedException();
        }

        public virtual void Remove(TResource dto)
        {
            throw new NotSupportedException();
        }
    }

    [SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1201:ElementsMustAppearInTheCorrectOrder", Justification = "Reviewed. Suppression is OK here.")]
    public interface IRepository<TResource> where TResource : class, new()
    {
    }
}