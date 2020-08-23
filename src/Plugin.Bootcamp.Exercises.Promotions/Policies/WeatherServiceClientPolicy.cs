using Sitecore.Commerce.Core;
using Sitecore.Framework.Rules;

namespace Plugin.Bootcamp.Exercises.Promotions
{
   public class WeatherServiceClientPolicy : Policy
    {
        /* Student: Create a property to store the API key.
         * Create a constructor to initialize it to an empty
         * string. */
       
        public WeatherServiceClientPolicy()
        {
            this.ApiKey = "962890ac55b7a01a4a67f1ebd73e6148";    
        }
        public string ApiKey  { get; set; }

    }
}
