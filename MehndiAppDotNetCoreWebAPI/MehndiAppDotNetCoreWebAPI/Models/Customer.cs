namespace MehndiAppDotNerCoreWebAPI.Models
{
    public class Customer
    {
        public decimal? CustomerID { get; set; }
        public string FullName { get; set; } = "";  
        public string Email { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string Password { get; set; } = ""; 
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Address { get; set; } = "";
        public string City { get; set; } = "";
        public string State { get; set; } = "";
        public string ZipCode { get; set; } = "";
    }
}
