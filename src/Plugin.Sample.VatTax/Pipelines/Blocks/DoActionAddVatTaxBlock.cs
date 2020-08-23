using Microsoft.Extensions.Logging;
using Plugin.Bootcamp.Exercises.VatTax.Entities;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.EntityViews;
using Sitecore.Commerce.Plugin.ManagedLists;
using Sitecore.Framework.Conditions;
using Sitecore.Framework.Pipelines;
using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace Plugin.Bootcamp.Exercises.VatTax.EntityViews
{
    [PipelineDisplayName("DoActionAddDashboardEntity")]
    public class DoActionAddVatTaxBlock : PipelineBlock<EntityView, EntityView, CommercePipelineExecutionContext>
    {
        private readonly CommerceCommander _commerceCommander;

        public DoActionAddVatTaxBlock(CommerceCommander commerceCommander)
        {
            this._commerceCommander = commerceCommander;
        }

        public override async Task<EntityView> Run(EntityView entityView, CommercePipelineExecutionContext context)
        {

            Contract.Requires(entityView != null);
            Contract.Requires(context != null);
            Condition.Requires(entityView).IsNotNull($"{this.Name}: The argument cannot be null");

            if (entityView == null || !entityView.Action.Equals("VatTaxDashboard-AddDashboardEntity", StringComparison.OrdinalIgnoreCase))
            {
                return entityView;
            }
            
            if (entityView!=null && entityView.Action.Equals("VatTaxDashboard-AddDashboardEntity", StringComparison.OrdinalIgnoreCase))
            {
                var taxTag = entityView.Properties.First(p => p.Name == "TaxTag").Value ?? "";
                var countryCode = entityView.Properties.First(p => p.Name == "CountryCode").Value ?? "";
                var taxPct = Convert.ToDecimal(entityView.Properties.First(p => p.Name == "TaxPct").Value ?? "0");

                using (var sampleDashboardEntity = new VatTaxEntity())
                {
                    try
                    {
                        sampleDashboardEntity.Id = CommerceEntity.IdPrefix<VatTaxEntity>() + Guid.NewGuid().ToString("N");
                        sampleDashboardEntity.Name = string.Empty;
                        sampleDashboardEntity.TaxTag = taxTag;
                        sampleDashboardEntity.CountryCode = countryCode;
                        sampleDashboardEntity.TaxPct = taxPct;
                        sampleDashboardEntity.GetComponent<ListMembershipsComponent>().Memberships.Add(CommerceEntity.ListName<VatTaxEntity>());
                        await _commerceCommander.Pipeline<IPersistEntityPipeline>().Run(new PersistEntityArgument(sampleDashboardEntity), context).ConfigureAwait(false);
                    }
                    catch (ArgumentNullException ex)
                    {
                        context.Logger.LogError($"Catalog.DoActionAddDashboardEntity.Exception: Message={ex.Message}");
                    }
                }
            }
            else
            {
                System.Diagnostics.Debug.Write($"Error creating the View is", entityView.Name);
            }

            return entityView;
        }
    }
}
