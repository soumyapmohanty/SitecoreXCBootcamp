namespace Plugin.Bootcamp.Exercises.VatTax.Entities
{
    using System;
    using System.Collections.Generic;

    using Sitecore.Commerce.Core;
    
    public class VatTaxEntity : CommerceEntity
    {
        public VatTaxEntity()
        {
            /* STUDENT: Write the body of the default constructor */
            this.TaxTag = string.Empty;
            this.TaxPct = 1;
            this.CountryCode = string.Empty;
        }

        public VatTaxEntity(string id): this()
        {
            /* STUDENT: Write the body of the constructor that is called with the id */
        }

        /* STUDENT: Add read/write properties to the class to meet the requirements */
        public string TaxTag { get; set; }
        public int TaxPct { get; set; }
        public string CountryCode { get; set; }

    }
}