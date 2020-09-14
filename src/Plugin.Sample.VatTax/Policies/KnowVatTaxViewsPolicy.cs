using Plugin.Bootcamp.Exercises.VatTax.Entities;
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
        public string Master { get; set; } = nameof(Master);
        public string VatTaxDashboard { get; internal set; } = nameof(VatTaxDashboard);
       
    }
}
