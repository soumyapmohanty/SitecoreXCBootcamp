using System;
using System.Threading.Tasks;
using Sitecore.Commerce.Core;
using Sitecore.Framework.Conditions;
using Sitecore.Framework.Pipelines;
using Plugin.Bootcamp.Exercises.Order.ConfirmationNumber.Policies;
using Sitecore.Commerce.Plugin.Orders;
using System.Diagnostics.Contracts;


namespace Plugin.Bootcamp.Exercises.Order.ConfirmationNumber.Blocks
{
    [PipelineDisplayName("OrderConfirmation.OrderConfirmationIdBlock")]
    public class OrderPlacedAssignCustomConfirmationIdBlock : PipelineBlock<Sitecore.Commerce.Plugin.Orders.Order, Sitecore.Commerce.Plugin.Orders.Order, CommercePipelineExecutionContext>
    {
        public override Task<Sitecore.Commerce.Plugin.Orders.Order> Run(Sitecore.Commerce.Plugin.Orders.Order arg, CommercePipelineExecutionContext context)
        {

            Contract.Requires(arg != null);
            Contract.Requires(context != null);
            Condition.Requires(arg).IsNotNull($"{this.Name} : the name cannot be null");
            OrderPlacedAssignCustomConfirmationIdBlock ConfirmationIdBlock = this;
            string uniquecode = string.Empty;
            /* STUDENT: Complete this method to set the order number as specified in the requirements */
            // Order.ConfirmationNumber
            try
            {
                uniquecode = this.GetCustomerConfirmationNumber(context);
            }
            catch (Exception ex)
            {
                context.CommerceContext.LogException((ConfirmationIdBlock.Name) + "- UniqueCodeGenerationException", ex);
                throw;
            }
            arg.OrderConfirmationId = uniquecode;
            return Task.FromResult<Sitecore.Commerce.Plugin.Orders.Order>(arg);
        }
        // Get unique Confirmation Number based on date
        private string GetCustomerConfirmationNumber(CommercePipelineExecutionContext context )
        {
            var policy = context.GetPolicy<OrderNumberPolicy>();
            return policy.IncludeDate == true ? $"{policy.Prefix},{DateTime.Today.ToString("d", System.Globalization.CultureInfo.InvariantCulture)},{policy.Suffix},{Guid.NewGuid().ToString()}" :
                                              $"{policy.Prefix},{string.Empty},{policy.Suffix},{Guid.NewGuid().ToString()}";
        }
    }

}
