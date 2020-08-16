using Sitecore.Commerce.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Bootcamp.Exercises.VatTax.Policies
{
    public class KnowVatTaxViewsPolicy : Policy
    {
        string  vatTaxDashboardName = "Vat Tax Dashboard";
        
        public KnowVatTaxViewsPolicy() 
        {
            this.VatTaxDashBoard= vatTaxDashboardName;
        }

        public string VatTaxDashBoard { get; set; }
    }
}
