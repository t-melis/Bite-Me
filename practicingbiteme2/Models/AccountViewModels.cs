using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace practicingbiteme2.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        //[Display(Name = "First Name")]
        //[Required]
        //[StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
        //[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        //public string FirstName { get; set; }

        //[Display(Name = "Last Name")]
        //[Required]
        //[StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
        //[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        //public string LastName { get; set; }

        //[Required]
        //[Display(Name = "City")]
        //[StringLength(100, MinimumLength = 2, ErrorMessage = "City name must be between 2 and 100 characters")]
        //[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        //public string City { get; set; }

        //[Required]
        //[Display(Name = "Street")]
        //[StringLength(100, MinimumLength = 2, ErrorMessage = "Street name must be between 2 and 100 characters")]
        //public string Street { get; set; }

        //public string Number { get; set; }

        //[Required]
        //[Display(Name = "Post Code")]
        //[StringLength(10, MinimumLength = 3, ErrorMessage = "Post code must be between 3 and 100 characters")]
        //public string PostCode { get; set; }



    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }




    public class ResetPasswordViewModel
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

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
