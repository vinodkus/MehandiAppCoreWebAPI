namespace MehndiAppDotNetCoreWebAPI.Models
{
    public class MehndiDesignRequest
    {
        public int DesignID { get; set; } = 0;
        public int ProfessionalID { get; set; }
        public string DesignName { get; set; } = "";
        public string DesignDescription { get; set; } = "";
        public int Mode { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string? DesignImageName { get; set; } = "";
      //  public string ImagePath { get; set; } = "";

    }

}
