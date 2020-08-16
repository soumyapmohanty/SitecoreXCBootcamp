using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using Plugin.Bootcamp.Exercises.VatTax.Policies;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.EntityViews;
using Sitecore.Commerce.Plugin.Catalog;
using Sitecore.Framework.Conditions;
using Sitecore.Framework.Pipelines;

namespace Plugin.Bootcamp.Exercises.VatTax.EntityViews
{
    [PipelineDisplayName("GetVatTaxNavigationViewBlock")]
    public class GetVatTaxNavigationViewBlock : PipelineBlock<EntityView, EntityView, CommercePipelineExecutionContext>
    {
       

        public override Task<EntityView> Run(EntityView entityView, CommercePipelineExecutionContext context)
        {
            Contract.Requires(entityView != null);
            Contract.Requires(context != null);

            Condition.Requires(entityView).IsNotNull($"{this.Name}: The argument cannot be null");

            /* STUDENT: Add the necessary code to show your new Vat Tax dashboard in the Business Tools navigation */
           
            var vatTaxDashboard = context.GetPolicy<KnowVatTaxViewsPolicy>().VatTaxDashBoard;
            var vatTaxDashboardView = new EntityView()
            {
                Name = vatTaxDashboard,
                DisplayName= "Vat Tax",
                ItemId = vatTaxDashboard,
                //Icon ="add", //Views.Constants.Icons.MarketStand,
                DisplayRank = 9
            };

            // 3. Add the navigation view as a child view of the Business Tools Navigation view
            entityView.ChildViews.Add(vatTaxDashboardView);

            // 4. Return the updated Business Tools Navigation view
            return Task.FromResult(entityView);
        }
    }
}
