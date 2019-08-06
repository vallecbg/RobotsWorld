using System;
using System.Collections.Generic;
using System.Text;

namespace RobotsWorld.Services.Constants
{
    public class GlobalConstants
    {
        public const string ModelError = "LoginError";
        public const string RegisterError = "There's already an registered user with this username or name!";
        public const string LoginError = "Wrong username or password!";

        public const string DefaultRole = "user";
        public const string AdminRole = "admin";
    }
}
