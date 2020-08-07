﻿using Plugin.Bootcamp.Exercises.Order.Export.Pipelines.Arguments;
using Sitecore.Commerce.Core;
using Sitecore.Framework.Pipelines;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using XC = Sitecore.Commerce.Plugin.Orders;

namespace Plugin.Bootcamp.Exercises.Order.Export.Pipelines.Blocks
{
    [PipelineDisplayName("Orders.block.RetrieveOrderBlock")]
    public class RetrieveOrderBlock : PipelineBlock<ExportOrderArgument, XC.Order, CommercePipelineExecutionContext>
    {
        private readonly IFindEntityPipeline _findEntityPipeline;

        public RetrieveOrderBlock(IFindEntityPipeline findEntityPipeline)
        {
            this._findEntityPipeline = findEntityPipeline;
        }

        public override async Task<XC.Order> Run(ExportOrderArgument arg, CommercePipelineExecutionContext context)
        {
            Contract.Requires(arg != null);
            Contract.Requires(context != null);

            XC.Order order = await this._findEntityPipeline.Run(new FindEntityArgument(typeof(XC.Order), arg.OrderId, false), context).ConfigureAwait(false) as XC.Order;

            /* STUDENT: Add error handling to deal with the situation where no order was found. */

            return order;
        }
    }
}
