using System;
using System.Collections.Generic;
using System.Text;

namespace RobotsWorld.ViewModels.Constants
{
    public class ViewModelsConstants
    {
        public const int UserModelNameMaxLength = 100;
        public const int UserModelNameMinLength = 3;
        public const string RegexValidateName = "[A-Za-z ]+";
        public const string ErrorMessageNameRegisterModel =
            "Your name should contains only latin alphabet symbols and spaces!";

        public const int UserModelUsernameMaxLength = 50;
        public const int UserModelUsernameMinLength = 3;
        public const string RegexValidateUsername = @"[A-Za-z0-9 ]+";
        public const string ErrorMessageUsernameRegisterModel =
            "Your username should contains only latin alphabet symbols, spaces and numbers!";

        public const string DisplayConfirmPassword = "Confirm Password";
    }
}
