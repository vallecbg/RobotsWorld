using System;
using System.Collections.Generic;
using System.Text;
using RobotsWorld.Models;

namespace RobotsWorld.Services.Contracts
{
    public interface IVendorService
    {
        ICollection<string> GetAllVendorNames();

        Vendor GetVendorByName(string vendorName);

        bool CheckVendorIsValid(string vendorName);
    }
}
