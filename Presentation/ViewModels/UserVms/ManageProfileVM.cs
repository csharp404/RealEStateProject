using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels.UserVms
{
    public class ManageProfileVM
    {
        [Display(Name ="First name")]
        public string? FirstName { get; set; }
        

        [Display(Name ="Last name")]

        public string? LastName { get; set; }
        
        public string? City { get; set; }
        
        public string? Country { get; set; }
        
        public string? Password { get; set; }
        [DataType(DataType.EmailAddress)]
        
        public string? Email { get; set; }
        [Display(Name = "Phone number")]

        public string? PhoneNumber { get; set; }
        
        
        public string? ValidationMessage { get; set; }
        
        public string? ImagePath { get; set; }
        
        public IFormFile? ImageFile { get; set; }

    }
}
