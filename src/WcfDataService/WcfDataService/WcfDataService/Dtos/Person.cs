namespace WcfDataService.Dtos
{
    using System.Data.Services.Common;

    [DataServiceKey("Id")]
    public class Person
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}