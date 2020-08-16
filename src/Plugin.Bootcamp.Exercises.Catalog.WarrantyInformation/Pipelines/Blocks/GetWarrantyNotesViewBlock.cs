using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.EntityViews;
using Sitecore.Commerce.Plugin.Catalog;
using Sitecore.Framework.Conditions;
using Sitecore.Framework.Pipelines;
using Plugin.Bootcamp.Exercises.Catalog.WarrantyInformation.Components;

namespace Plugin.Bootcamp.Exercises.Catalog.WarrantyInformation.Pipelines.Blocks
{
    [PipelineDisplayName("GetWarrantyNotesViewBlock")]
    public class GetWarrantyNotesViewBlock : PipelineBlock<EntityView, EntityView, CommercePipelineExecutionContext>
    {
        public override Task<EntityView> Run(EntityView arg, CommercePipelineExecutionContext context)
        {
            Condition.Requires(arg).IsNotNull($"{Name}: The argument cannot be null.");
            var catalogViewsPolicy = context.GetPolicy<KnownCatalogViewsPolicy>();

            /* STUDENT: Complete the Run method as specified in the requirements */
            /* Declare variables to initilize the state of the loaded Views */
            var isVariantView = arg.Name.Equals(catalogViewsPolicy.Variant, StringComparison.OrdinalIgnoreCase);
            var isDetailsView = arg.Name.Equals(catalogViewsPolicy.Master, StringComparison.OrdinalIgnoreCase);
            var isWarantyNotesView = arg.Name.Equals("WarrantyNotes", StringComparison.OrdinalIgnoreCase);
            var isConnectSellableItemView = arg.Name.Equals(catalogViewsPolicy.ConnectSellableItem, StringComparison.OrdinalIgnoreCase);
            var request = context.CommerceContext.GetObject<EntityViewArgument>();


            /*  valdation  to check if the views is the correct targeted view else return back*/
            if (string.IsNullOrEmpty(arg.Name) || !isVariantView && !isDetailsView && !isWarantyNotesView
                && !isConnectSellableItemView)
            {

                return Task.FromResult(arg);
            }
            /*  Validate if the  Entity loaded is a SellabaleItem Entity*/
            if (!(request.Entity is SellableItem))
            {
                return Task.FromResult(arg);
            }
            var sellableItem = (SellableItem)request.Entity;

            // Check id the if the current item is a base sellableitem or a variant of it.
            var variantId = string.Empty;
            if (isVariantView && !string.IsNullOrEmpty(arg.ItemId))
            {
                variantId = arg.ItemId;

            }
            /* Check if Edit view is requested action */
            var isEditView = !string.IsNullOrEmpty(arg.Action) && arg.Action.Equals("WarrantyNotes-Edit", StringComparison.OrdinalIgnoreCase);
            var componentView = arg;
            /* If the incomming argument is Edit request then EntityView will be the Entity of the Component*/

            if (!isEditView)
            {
                /* If the argument is not Edit View then the  argument is EntityView for the SellableItem Details View */
                /*  New View will be created and added to the Current Entity View */
                componentView = new EntityView
                {
                    Name = "WarrantyNotes",
                    DisplayName = "Warranty Information",
                    EntityId = arg.EntityId,
                    EntityVersion = request.EntityVersion == null ? 1 : (int)request.EntityVersion,
                    ItemId = variantId

                };
                arg.ChildViews.Add(componentView);
                System.Diagnostics.Debug.Write($"Get Entity view in Master EntityView, Version of the View is",arg.VersionedItemId);
            }
            else
            {
                System.Diagnostics.Debug.Write($"Get the current Master EntityView in Edit Mode, Version of the View is", arg.VersionedItemId);
            }
            /* Validate if the Item is SellableItem &  HasWarrantyComponent & isConnectSellableItemView*/
            /* Get the Item Details */
            if (sellableItem != null && sellableItem.HasComponent<WarrantyNotesComponent>(variantId)
                || isConnectSellableItemView || isEditView )
            {
                var component = sellableItem.GetComponent<WarrantyNotesComponent>(variantId);
                componentView.Properties.Add(
                    new ViewProperty {
                        DisplayName="Description",
                        Name = nameof(WarrantyNotesComponent.WarrantyInformation),
                        RawValue =component.WarrantyInformation,
                        IsReadOnly = !isEditView,
                        IsRequired=false
                    });
                componentView.Properties.Add(
                  new ViewProperty
                  {
                      DisplayName = "Warranty Terms  In(Years)",
                      Name = nameof(WarrantyNotesComponent.NoOfYears),
                      RawValue = component.NoOfYears,
                      IsReadOnly = !isEditView,
                      IsRequired = false
                  });
                System.Diagnostics.Debug.Write($"Warranty Description : {component.WarrantyInformation} , Terms in Years:{component.NoOfYears}" );
            }
            return Task.FromResult(arg);
        }
    }
}
