using Sitecore.Commerce.Core;

namespace Plugin.Bootcamp.Exercises.Catalog.WarrantyInformation.Components
{
    public class WarrantyNotesComponent : Component
    {
        /* STUDENT: Add properties as specified in the requirements */
        /* Extending the  Components with Additional WarrantyNotes Component */
        public WarrantyNotesComponent():base()
        {
            this.WarrantyInformation = string.Empty;
            this.NoOfYears = 1;
        }
        public string WarrantyInformation { get; set; } = string.Empty;
        public int NoOfYears { get; set; } = 1;
    }
}
