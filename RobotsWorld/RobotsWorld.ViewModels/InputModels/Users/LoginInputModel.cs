using System.ComponentModel.DataAnnotations;
using RobotsWorld.ViewModels.Constants;

namespace RobotsWorld.ViewModels.InputModels.Users
{
    public class LoginInputModel
    {
        [Required]
        [StringLength(ViewModelsConstants.UserModelNameMaxLength, MinimumLength = ViewModelsConstants.UserModelNameMinLength)]
        [RegularExpression(ViewModelsConstants.RegexValidateUsername, ErrorMessage = ViewModelsConstants.ErrorMessageUsernameRegisterModel)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
