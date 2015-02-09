namespace WcfDataServiceToolkitAdvanced.Repositories
{
    using System;

    using WcfDataServiceToolkitAdvanced.Dto;

    public class ExceptionRepositroy : RepositoryBase<ExceptionDto, string>
    {
        public override ExceptionDto GetOne(string id)
        {
            switch (id.ToLower())
            {
                case "unknow":
                    throw new Exception(id);
                case "notsupport":
                    throw new NotSupportedException(id);
            }

            throw new Exception();
        }
    }
}