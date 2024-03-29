﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RobotsWorld.Services.Constants
{
    public class GlobalConstants
    {
        //Errors
        public const string ModelError = "LoginError";
        public const string Error = "Error";
        public const string ErrorOnDeleteUser = "An error happened during the deletion of this user!";
        public const string NullName = "The name cannot be null or empty!";
        public const string VendorNameDuplicate = "This vendor name already exists in the database!";
        public const string TransportTypeNameDuplicate = "This transport name already exists in the database!";
        public const string RecordDoesntExist = "This record doesn't exist in the database!";

        //Routes
        public class RouteConstants
        {
            public const string CreateSubAssemblyRoute = "/SubAssembly/Create/{assemblyId}";
            public const string CreatePartRoute = "/Part/Create/{subAssemblyId}";
            public const string CreateDeliveryRoute = "/Delivery/Create/{robotId}";
            public const string Administration = "Administration";
        }

        //Ids
        public const string RobotId = "robotId";
        public const string AssemblyId = "assemblyId";
        public const string SubAssemblyId = "subAssemblyId";

        //Users
        public const string RegisterError = "There's already an registered user with this username or name!";
        public const string LoginError = "Wrong username or password!";

        //Roles
        public const string DefaultRole = "user";
        public const string AdminRole = "admin";
        public const string Admin = "admin";

        //Keys and apis
        public const string CloudinaryCloudName = "vallec";
        public const string CloudinaryApiKey = "148382891263925";
        public const string CloudinaryApiSecret = "GDijvH1mRWflHJa0J6oerHATqqI";
        public const string GoogleMapsApiKey = "AIzaSyDDVS1cK3oavlECL5gTbvkdOAVYIupLd-A";

        //Image
        public const string NoImageAvailableUrl =
            "https://res.cloudinary.com/vallec/image/upload/v1561301682/No_image_available_zvvugj.png";
        public static readonly string[] ImageExtensions = { "png", "jpg", "jpeg" };
        public static string WrongFileType = $"The image type should be: {string.Join(", ", ImageExtensions)}";

        //Rights
        public const string UserHasNoRights = "You don't have rights to do this!";
        

        //Acts
        public const string Success = "Success";
        public const string Failed = "Failed the task";
    }
}
