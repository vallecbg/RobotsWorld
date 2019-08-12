using System.ComponentModel.DataAnnotations;
using RobotsWorld.ViewModels.Constants;

namespace RobotsWorld.ViewModels.InputModels.Users
{
    public class RegisterInputModel
    {
        [Required]
        [StringLength(ViewModelsConstants.UserModelNameMaxLength, MinimumLength = ViewModelsConstants.UserModelNameMinLength)]
        [RegularExpression(ViewModelsConstants.RegexValidateName, ErrorMessage = ViewModelsConstants.ErrorMessageNameRegisterModel)]
        public string Name { get; set; }

        [Required]
        [StringLength(ViewModelsConstants.UserModelUsernameMaxLength, MinimumLength = ViewModelsConstants.UserModelUsernameMinLength)]
        [RegularExpression(ViewModelsConstants.RegexValidateUsername, ErrorMessage = ViewModelsConstants.ErrorMessageUsernameRegisterModel)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        [Display(Name = ViewModelsConstants.DisplayConfirmPassword)]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
