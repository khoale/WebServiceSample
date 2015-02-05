namespace WcfDataService.Repositories
{
    using System;
    using System.Linq;

    public abstract class RepositoryBase<TDto, TKey>
        where TDto : new()
    {
        public virtual TDto CreateResource()
        {
            return new TDto();
        }

        public virtual IQueryable<TDto> GetAll()
        {
            throw new NotSupportedException();
        }

        public virtual void Save(TDto dto)
        {
            throw new NotSupportedException();
        }

        public virtual void Update(TDto dto)
        {
            throw new NotSupportedException();
        }

        public virtual TDto GetOne(TKey id)
        {
            throw new NotSupportedException();
        }

        public virtual void Remove(TDto dto)
        {
            throw new NotSupportedException();
        }
    }
}