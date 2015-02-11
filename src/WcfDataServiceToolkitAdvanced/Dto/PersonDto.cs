namespace WcfDataServiceToolkitAdvanced.Dto
{
    // [DataServiceKey("ID")] // Default key is ID, you only need to declare when your key isn't ID
    public class PersonDto
    {
        public int ID { get; set; }

        public string PersonType { get; set; }

        public bool NameStyle { get; set; }

        public string Title { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Suffix { get; set; }

        public int EmailPromotion { get; set; }

        public string AdditionalContactInfo { get; set; }

        public string Demographics { get; set; }

        public System.DateTime ModifiedDate { get; set; }
    }
}