using System;

using Sitecore.Commerce.Core;
using Sitecore.Framework.Rules;
using Sitecore.Commerce.Plugin.Carts;

namespace Plugin.Bootcamp.Exercises.Promotions
{
    [EntityIdentifier("MinTemperatureCondition")]
    public class MinTemperatureCondition : ICartsCondition
    {
        /* STUDENT: Add IRuleValue properties for the city, country, and
         * minimum temperature.
         */
        public IRuleValue<string> City { get; set; }
        public IRuleValue<string> Country { get; set; }
        public IRuleValue<decimal> MinimumTemperature { get; set; }
        public bool Evaluate(IRuleExecutionContext context)
        {
            /* STUDENT: Complete the Evaluate method to:
             * Retrieve the current temperature
             * Compare it to the temperature stored in the Policy
             * Return the result of that comparison
             */
            string applicationId = new Plugin.Bootcamp.Exercises.Promotions.WeatherServiceClientPolicy().ApiKey;
            var city = this.City.Yield(context);
            var country = this.Country.Yield(context);
            var currentTemperature = this.GetCurrentTemperature(city,country,applicationId);
            var policyTemperature = this.MinimumTemperature.Yield(context);
            bool isMinimumTemp = false;
             // compare if Current  Temperature is greater than Policy Temperature.
             if(currentTemperature > policyTemperature)
                {
                  isMinimumTemp = true;
                }
            
            // STUDENT: Replace this line. It is only provided so the stub 
            // project will build cleanly
            return isMinimumTemp;
        }

        public decimal GetCurrentTemperature(string city, string country, string applicationId)
        {
            WeatherService weatherService = new WeatherService(applicationId);
            var temperature = weatherService.GetCurrentTemperature(city, country).Result;

            return (decimal) temperature.Max;
        }
    }
}
