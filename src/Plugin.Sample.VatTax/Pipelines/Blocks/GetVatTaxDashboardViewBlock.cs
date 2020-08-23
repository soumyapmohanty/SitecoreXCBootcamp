using Plugin.Bootcamp.Exercises.VatTax.Entities;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.EntityViews;
using Sitecore.Framework.Conditions;
using Sitecore.Framework.Pipelines;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace Plugin.Bootcamp.Exercises.VatTax.EntityViews
{
    [PipelineDisplayName("GetVatTaxDashboardViewBlock")]
    public class GetVatTaxDashboardViewBlock : PipelineBlock<EntityView, EntityView, CommercePipelineExecutionContext>
    {
        private readonly CommerceCommander _commerceCommander;
        
        public GetVatTaxDashboardViewBlock(CommerceCommander commerceCommander)
        {
            this._commerceCommander = commerceCommander;
        }

        public override async Task<EntityView> Run(EntityView entityView, CommercePipelineExecutionContext context)
        {
            Contract.Requires(entityView != null);
            Contract.Requires(context != null);
            Condition.Requires(entityView).IsNotNull($"{this.Name}: The argument cannot be null");

            if (entityView != null && entityView.Name != "VatTaxDashboard")
            {
                return entityView;
            }
            var pluginPolicy = context!=null ? context.GetPolicy<PluginPolicy>():null;
            if (entityView != null)
            {
                var newEntityViewTable = entityView;
                entityView.UiHint = "Table";
                entityView.Icon = "cubes";
                entityView.ItemId = string.Empty;

                var sampleDashboardEntities = await _commerceCommander.Command<ListCommander>().GetListItems<VatTaxEntity>(context.CommerceContext, CommerceEntity.ListName<VatTaxEntity>(), 0, 99).ConfigureAwait(false);
                foreach (var sampleDashboardEntity in sampleDashboardEntities)
                {
                    newEntityViewTable.ChildViews.Add(
                        new EntityView
                        {
                            ItemId = sampleDashboardEntity.Id,
                            Icon = "cubes",
                            Properties = new List<ViewProperty>
                            {
                            new ViewProperty {Name = "TaxTag", RawValue = sampleDashboardEntity.TaxTag },
                            new ViewProperty {Name = "CountryCode", RawValue = sampleDashboardEntity.CountryCode },
                            new ViewProperty {Name = "TaxPct", RawValue = sampleDashboardEntity.TaxPct }
                            }
                        });
                }
            }
            return entityView;
        }
    }
}
