using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RobotsWorld.ViewModels.InputModels;
using RobotsWorld.ViewModels.InputModels.Users;
using RobotsWorld.ViewModels.OutputModels.Users;

namespace RobotsWorld.Services.Contracts
{
    public interface IUserService
    {
        Task<SignInResult> Register(RegisterInputModel registerModel);

        SignInResult Login(LoginInputModel loginModel);

        UserOutputModel GetUserDetails(string id);

        void Logout();
    }
}
