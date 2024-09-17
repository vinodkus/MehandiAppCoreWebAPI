namespace MehndiAppDotNetCoreWebAPI.Models
{
    public class MehndiDesign
    {
        public int DesignID { get; set; }           // Unique identifier for the Mehndi design
        public int ProfessionalID { get; set; }     // Identifier for the professional who created the design
        public string DesignName { get; set; } = "";      // Name of the design
        public string DesignImageName { get; set; } = ""; // Name of the Design Design
        public string DesignDescription { get; set; } = ""; // Description of the design
        //public string ImagePath { get; set; } = "";       // Path to the image file for the design
        public DateTime CreatedAt { get; set; }     // Date and time when the design was created
        public DateTime UpdatedAt { get; set; }     // Date and time when the design was last updated
    }

}
