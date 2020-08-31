using Microsoft.Extensions.DependencyInjection;
using Plugin.Bootcamp.Exercises.Order.Export.Pipelines;
using Plugin.Bootcamp.Exercises.Order.Export.Pipelines.Arguments;
using Sitecore.Commerce.Core;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Sitecore.Commerce.Plugin.Orders;
using XC = Sitecore.Commerce.Plugin.Orders;
using System.Diagnostics.Contracts;
using Sitecore.Framework.Conditions;

namespace Plugin.Bootcamp.Exercises.Order.Export.Minions
{
    public class ExportOrdersMinion : Sitecore.Commerce.Core.Minion
    {
        protected IExportOrderMinionPipeline ExportOrderPipeline { get; set; }

        public override void Initialize(IServiceProvider serviceProvider,
            MinionPolicy policy,
            CommerceContext globalContext)
        {
            base.Initialize(serviceProvider, policy, globalContext);
            ExportOrderPipeline = serviceProvider.GetService<IExportOrderMinionPipeline>();
        }

        protected override async Task<MinionRunResultsModel> Execute()
        {
           
            MinionRunResultsModel runResults = new MinionRunResultsModel();
            /* STUDENT: Complete the body of this method. You need to pull from an appropriate list
             * and then execute an appropriate pipeline. */
             long listCount = await GetListCount(Policy.ListToWatch).ConfigureAwait(false);
             this.Logger.LogInformation(string.Format("{0}-Review List {1}: Count:{2}", (object)this.Name, (object)this.Policy.ListToWatch, (object)listCount));
            // var  entitleMentList = await this.GetListIds<XC.Order>(this.Policy.ListToWatch, Convert.ToInt32(listCount)).ConfigureAwait(false);

            foreach (var commerceEnty in (await this.GetListItems<XC.Order>(Policy.ListToWatch, Convert.ToInt32(listCount)).ConfigureAwait(false)))
            {

                //this.Logger.LogDebug(string.Format("{0}-Reviewing Pending Order: {1}", commerceEnty.Name, commerceEnty.Id), Array.Empty<object>());

                var minionPipeline = ExportOrderPipeline;
                var ordersMinionArgument = new ExportOrderArgument(commerceEnty.Id);
                using (var commerceContext = new CommerceContext(this.Logger, this.MinionContext.TelemetryClient, (IGetLocalizableMessagePipeline)null))
                {
                    commerceContext.Environment = this.Environment;
                    CommercePipelineExecutionContextOptions executionContextOptions = new CommercePipelineExecutionContextOptions(commerceContext, null, null, null, null, null);
                    var order = await minionPipeline.Run(ordersMinionArgument, executionContextOptions).ConfigureAwait(false);
                }
            }

            return runResults;
        }


    }
}
