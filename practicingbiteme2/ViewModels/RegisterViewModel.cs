using System.ComponentModel.DataAnnotations;

namespace practicingbiteme2.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "First Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Only numbers allowed")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "User Role")]
        public string UserRole { get; set; }    // Πρόσθεσα αυτό

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public string City { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public string Street { get; set; }

        public string Number { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public string PostCode { get; set; }
    }
}