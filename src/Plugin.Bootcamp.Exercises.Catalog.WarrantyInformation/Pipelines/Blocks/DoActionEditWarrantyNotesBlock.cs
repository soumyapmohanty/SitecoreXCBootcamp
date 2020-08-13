using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.EntityViews;
using Sitecore.Commerce.Plugin.Catalog;
using Sitecore.Framework.Conditions;
using Sitecore.Framework.Pipelines;
using Plugin.Bootcamp.Exercises.Catalog.WarrantyInformation.Components;


namespace Plugin.Bootcamp.Exercises.Catalog.WarrantyInformation.Pipelines.Blocks
{
    [PipelineDisplayName("DoActionEditWarrantyNotesBlock")]
    class DoActionEditWarrantyNotesBlock : PipelineBlock<EntityView, EntityView, CommercePipelineExecutionContext>
    {
        private readonly CommerceCommander _commerceCommander;

        public DoActionEditWarrantyNotesBlock(CommerceCommander commerceCommander)
        {
            this._commerceCommander = commerceCommander;
        }

        public override async Task<EntityView> Run(EntityView arg, CommercePipelineExecutionContext context)
        {
            Condition.Requires(arg).IsNotNull($"{Name}: The argument cannot be null.");

            /* STUDENT: Complete the Run method as specified in the requirements */
            /*  Process the action if the action is Warranty Edit */
            if(string.IsNullOrEmpty(arg.Action) || !arg.Action.Equals("WarrantyNotes-Edit",StringComparison.OrdinalIgnoreCase))
            {
               // return Task.FromResult(arg);
                return arg;

            }
            // Get SellableItem
            var entity = context.CommerceContext.GetObject<SellableItem>(x=> x.Id.Equals(arg.EntityId));
            // Validate if Entity is not null
            if(entity!= null)
            {
                //return Task.FromResult(arg);
                return arg;
            }
            // Get warranty components details  from the SelleableItem
            var component = entity.GetComponent<WarrantyNotesComponent>(arg.EntityId);
            // Map views properties to the  component 
            component.WarrantyInformation = arg.Properties.FirstOrDefault(x => x.Name.Equals(nameof(WarrantyNotesComponent.WarrantyInformation),StringComparison.OrdinalIgnoreCase)).Value.ToString();
            component.NoOfYears = Int32.Parse(arg.Properties.FirstOrDefault(x => x.Name.Equals(nameof(WarrantyNotesComponent.NoOfYears), StringComparison.OrdinalIgnoreCase)).Value);
           await this._commerceCommander.Pipeline<IEntityPersistedPipeline>().Run(new PersistEntityArgument(entity),context);
            // return Task.FromResult(arg);
            return arg;

        }
    }
}
