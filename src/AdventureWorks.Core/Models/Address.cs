namespace AdventureWorks.Core.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Services;
    using System.Spatial;

    using AdventureWorks.Core.Wrappers;

    [IgnoreProperties("SpatialLocation")]
    public partial class Address
    {
        private GeographyWrapper locationWrapper;

        public System.Data.Entity.Spatial.DbGeography SpatialLocation
        {
            get
            {
                return this.locationWrapper;
            }

            set
            {
                this.locationWrapper = value;
            }
        }

        [NotMapped]
        public GeographyPoint Location
        {
            get
            {
                return this.locationWrapper;
            }

            set
            {
                this.locationWrapper = value;
            }
        }
    }
}