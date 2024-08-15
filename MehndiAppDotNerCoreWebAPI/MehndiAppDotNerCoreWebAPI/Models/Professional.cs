namespace MehndiAppDotNerCoreWebAPI.Models
{
    public class Professional
    {
        public decimal ProfessionalID { get; set; }  // Numeric(18, 0) - Identity column
        public string FirstName { get; set; } = "";      // Varchar(50)
        public string LastName { get; set; } = "";      // Varchar(50)
        public string FullName { get; set; } = "";      // Varchar(100)
        public string Email { get; set; } = "";          // Varchar(100)
        public string PhoneNumber { get; set; } = "";     // Varchar(15)
        public string Password { get; set; } = "";    // Varchar(50)
        public string PasswordHash { get; set; } = "";    // Varchar(255)
        public DateTime? CreatedAt { get; set; }     // Datetime
        public DateTime? UpdatedAt { get; set; }     // Datetime
        public int? ExperienceYears { get; set; }    // Int
        public string Specialization { get; set; } = "";  // Varchar(255)
        public string PortfolioURL { get; set; } = "";   // Varchar(255)
        public string Address { get; set; } = "";        // Varchar(255)
        public string City { get; set; } = "";           // Varchar(50)
        public string State { get; set; } = "";           // Varchar(50)
        public string ZipCode { get; set; } = "";        // Varchar(10)
        public decimal? Rating { get; set; }         // Decimal(3, 2)
        public bool? IsVerified { get; set; }        // Bit
    }

}
