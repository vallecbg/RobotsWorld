using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RobotsWorld.ViewModels.OutputModels.Users;

namespace RobotsWorld.Services.Contracts
{
    public interface IAdminService
    {
        Task<IEnumerable<AdminUsersOutputModel>> GetAllUsers();

        Task DeleteUser(string userId);
    }
}
