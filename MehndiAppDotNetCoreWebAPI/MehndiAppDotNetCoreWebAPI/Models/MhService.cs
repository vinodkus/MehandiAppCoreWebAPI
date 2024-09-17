namespace MehndiAppDotNetCoreWebAPI.Models
{
    public class MhService
    {
        public int ServiceID { get; set; }
        public int ProfessionalID { get; set; }
        public string ServiceName { get; set; } = "";
        public string ServiceDescription { get; set; } = "";
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }



    }
}
