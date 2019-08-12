using System;
using System.Collections.Generic;
using System.Text;

namespace RobotsWorld.Services.Constants
{
    public class GlobalConstants
    {
        //Errors
        public const string ModelError = "LoginError";
        public const string Error = "Error";

        //Routes
        public class RouteConstants
        {
            public const string CreateSubAssemblyRoute = "/SubAssembly/Create/{assemblyId}";
        }

        //Robots
        public const string RobotId = "robotId";
        public const string AssemblyId = "assemblyId";

        //Users
        public const string RegisterError = "There's already an registered user with this username or name!";
        public const string LoginError = "Wrong username or password!";

        public const string DefaultRole = "user";
        public const string AdminRole = "admin";

        //Cloudinary
        public const string CloudinaryCloudName = "vallec";
        public const string CloudinaryApiKey = "148382891263925";
        public const string CloudinaryApiSecret = "GDijvH1mRWflHJa0J6oerHATqqI";

        //Image
        public const string NoImageAvailableUrl =
            "https://res.cloudinary.com/vallec/image/upload/v1561301682/No_image_available_zvvugj.png";
        public static readonly string[] ImageExtensions = { "png", "jpg", "jpeg" };
        public static string WrongFileType = $"The image type should be: {string.Join(", ", ImageExtensions)}";

        //Rights
        public const string UserHasNoRights = "You don't have rights to do this!";
    }
}
