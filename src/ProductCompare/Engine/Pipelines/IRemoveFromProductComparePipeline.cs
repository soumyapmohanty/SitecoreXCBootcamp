﻿using Sitecore.Commerce.Core;
using Sitecore.Framework.Pipelines;
using Plugin.Bootcamp.Exercises.ProductCompare.Entities;
using Plugin.Bootcamp.Exercises.ProductCompare.Pipelines.Arguments;

namespace Plugin.Bootcamp.Exercises.ProductCompare.Pipelines
{
    public interface IRemoveFromProductComparePipeline : IPipeline<RemoveFromProductCompareArgument, ProductComparison, CommercePipelineExecutionContext>
    {
    }
}
