using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using RobotsWorld.Data;
using RobotsWorld.Models;
using RobotsWorld.Services.Constants;
using RobotsWorld.Services.Contracts;
using RobotsWorld.ViewModels.InputModels.Robots;

namespace RobotsWorld.Services
{
    public class RobotService : BaseService, IRobotService
    {
        public RobotService(UserManager<User> userManager, RobotsWorldContext context, IMapper mapper) : base(userManager, context, mapper)
        {
        }

        public async Task<string> CreateRobot(RobotInputModel model)
        {
            var cloudinary = SetCloudinary();

            var url = await UploadImage(cloudinary, model.Image, model.Name);

            var robot = Mapper.Map<Robot>(model);

            robot.User = await this.UserManager.FindByNameAsync(model.User);
            robot.ImageUrl = url ?? GlobalConstants.NoImageAvailableUrl;

            this.Context.Robots.Add(robot);
            await this.Context.SaveChangesAsync();

            return robot.Id;
        }

        private async Task<string> UploadImage(Cloudinary cloudinary, IFormFile fileform, string name)
        {
            if (fileform == null)
            {
                return null;
            }

            byte[] storyImage;

            using (var memoryStream = new MemoryStream())
            {
                await fileform.CopyToAsync(memoryStream);
                storyImage = memoryStream.ToArray();
            }

            var ms = new MemoryStream(storyImage);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(name, ms),
                Transformation = new Transformation().Width(200).Height(250).Crop("fit").SetHtmlWidth(250).SetHtmlHeight(100)
            };

            var uploadResult = cloudinary.Upload(uploadParams);

            ms.Dispose();
            return uploadResult.SecureUri.AbsoluteUri;
        }

        private Cloudinary SetCloudinary()
        {
            Account account = new Account(
                GlobalConstants.CloudinaryCloudName, GlobalConstants.CloudinaryApiKey,
                GlobalConstants.CloudinaryApiSecret);

            Cloudinary cloudinary = new Cloudinary(account);

            return cloudinary;
        }
    }
}
