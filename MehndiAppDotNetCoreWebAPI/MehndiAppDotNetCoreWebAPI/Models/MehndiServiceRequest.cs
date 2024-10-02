namespace MehndiAppDotNetCoreWebAPI.Models
{
    public class MehndiServiceRequest
    {
        public MehndiServiceRequest() { }
        public int ServiceId { get; set; }
        public int ProfessionalID { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; } = string.Empty;
        public decimal Price    { get; set; }
        public int Mode { get; set; }


    }
}
