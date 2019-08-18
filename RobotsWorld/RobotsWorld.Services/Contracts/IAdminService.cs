using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RobotsWorld.ViewModels.OutputModels.Users;
using RobotsWorld.ViewModels.OutputModels.Vendors;

namespace RobotsWorld.Services.Contracts
{
    public interface IAdminService
    {
        Task<IEnumerable<AdminUsersOutputModel>> GetAllUsers();

        Task<IdentityResult> ChangeRole(ChangingRoleModel model);

        ChangingRoleModel AdminModifyRole(string Id);

        Task DeleteUser(string userId);

        IEnumerable<VendorAdminOutputModel> GetAllVendors();

        Task<string> AddVendor(string vendorName);
    }
}
