using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using Plugin.Bootcamp.Exercises.VatTax.Entities;
using Plugin.Bootcamp.Exercises.VatTax.Policies;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.EntityViews;
using Sitecore.Framework.Conditions;
using Sitecore.Framework.Pipelines;
using System.Linq;


namespace Plugin.Bootcamp.Exercises.VatTax.EntityViews
{
    [PipelineDisplayName("EnsureActions")]
    public class PopulateVatTaxDashboardActionsBlock : PipelineBlock<EntityView, EntityView, CommercePipelineExecutionContext>
    {
      
        public override Task<EntityView> Run(EntityView entityView, CommercePipelineExecutionContext context)
        {
            Contract.Requires(entityView != null);
            Contract.Requires(context != null);
            Condition.Requires(entityView).IsNotNull($"{this.Name}: The argument cannot be null");
            // Validate if the View is the correct view and not Null
            if (entityView!= null && entityView.Name != "VatTaxDashboard")
            {
                return Task.FromResult(entityView);
            }

            var tableViewActionsPolicy =  entityView.GetPolicy<ActionsPolicy>();
            tableViewActionsPolicy.Actions.Add(new EntityActionView
            {
                Name = "VatTaxDashboard-AddDashboardEntity",
                DisplayName = "Adds a new Vat Tax Entry",
                Description = "Adds a new Vat Tax Entry",
                IsEnabled = true,
                RequiresConfirmation = false,
                EntityView = "VatTaxDashboard-FormAddDashboardEntity",
                Icon = "add"
            });

            tableViewActionsPolicy.Actions.Add(new EntityActionView
            {
                Name = "VatTaxDashboard-RemoveDashboardEntity",
                DisplayName = "Remove Vat Tax Entry",
                Description = "Removes a Vat Tax Entry",
                IsEnabled = true,
                RequiresConfirmation = true,
                EntityView = string.Empty,
                Icon = "delete"
            });

            return Task.FromResult(entityView);
        }
    }
}
