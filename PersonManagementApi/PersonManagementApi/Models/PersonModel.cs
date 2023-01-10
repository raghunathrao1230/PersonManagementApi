using System.ComponentModel.DataAnnotations;

namespace PersonManagementApi.Models
{
    public class PersonModel:IValidatableObject
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [StringLength(150)]
        public string StreetName { get; set; }
        [Required]
        [StringLength(50)]
        public string HouseNumber { get; set; }
        [StringLength(50)]
        public string ApartmentNumber { get; set; }
        [Required]
        [StringLength (50)]
        public string PostalCode { get; set; }
        [Required]
        [StringLength(150)]
        public string Town { get; set; }
        [Required]
        [StringLength(12)]
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime DateofBirth { get; set; }
        [Range(0,100)]
        public int Age { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            DateTime now = DateTime.Today;
            int age = now.Year - DateofBirth.Year;
            if (DateofBirth > now.AddYears(-age)) age--;

            if(age!=Age)
            {
                results.Add(new ValidationResult("Age doesnt match with the Date Of Birth"));
            }
            if(DateofBirth>DateTime.Now)
            {
                results.Add(new ValidationResult("Date Of Birth Cannot be a future date"));
            }
            return results;
        }
    }
}
