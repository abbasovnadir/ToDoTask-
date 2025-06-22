using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Mobile.App.Models;
public class RegisterPageModel
{
    [Required(ErrorMessage = "Full name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Please enter a valid email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$",
        ErrorMessage = "Password must contain uppercase, lowercase and number")]
    public string Password { get; set; }

    [Range(typeof(bool), "true", "true", ErrorMessage = "You must agree to the terms")]
    public bool AgreeToTerms { get; set; }
}
