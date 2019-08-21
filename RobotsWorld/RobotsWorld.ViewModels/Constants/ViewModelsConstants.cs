using System;
using System.Collections.Generic;
using System.Text;

namespace RobotsWorld.ViewModels.Constants
{
    public class ViewModelsConstants
    {
        //Users
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

        //Properties names
        public const string Name = "Name";
        public const string Quantity = "Quantity";
        public const string Weight = "Weight";

        //Robots
        public const int RobotNameMaxLength = 100;
        public const int RobotNameMinLength = 3;
        public const int SerialNumberMaxLength = 200;
        public const int SerialNumberMinLength = 3;
        public const string ImageDisplay = "Image";

        //Assemblies
        public const int AssemblyNameMaxLength = 100;
        public const int AssemblyNameMinLength = 3;

        //SubAssemblies
        public const int SubAssemblyNameMaxLength = 100;
        public const int SubAssemblyNameMinLength = 3;
        public const int SubAssemblyMaxQuantity = 999;
        public const int SubAssemblyMinQuantity = 1;
        public const double SubAssemblyMaxWeight = 999.99;
        public const double SubAssemblyMinWeight = 0.01;

        //Parts
        public const int PartNameMaxLength = 200;
        public const int PartNameMinLength = 1;
        public const double PartMinPrice = 0.01;
        public const double PartMaxPrice = double.MaxValue;
        public const int PartMaxQuantity = 999;
        public const int PartMinQuantity = 1;

        //Deliveries
        public const int DeliveryStartPointMinLength = 1;
        public const int DeliveryStartPointMaxLength = 200;
        public const int DeliveryDestinationPointMinLength = 1;
        public const int DeliveryDestinationPointMaxLength = 200;
        public const double DeliveryMinPrice = 0.01;
        public const double DeliveryMaxPrice = double.MaxValue;
        public const int DeliveryReceiverUsernameMinLength = 3;
        public const int DeliveryReceiverUsernameMaxLength = 200;
    }
}
