using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RokoAwards.EFDataAccessLibrary.DataAccess;
using RokoAwards.EFDataAccessLibrary.Models;

namespace RokoAwards.BusinessLogicLibrary
{
    public class ImageLogic
    {
        private const string DefaultUserImageName = "DefaultUserImage";
        private const string DefaultAwardImageName = "DefaultAwardImage";
        private const string UserImagesFolder = "/Images/UserImages/";
        private const string AwardImagesFolder = "/Images/AwardImages/";
        private readonly ApplicationContext _db;
        private readonly UserLogic _userLogic;

        public ImageLogic(ApplicationContext context, UserLogic userLogic)
        {
            _db = context;
            _userLogic = userLogic;
        }

        public async Task<Image> GetDefaultUserImageAsync()
        {
            return await _db.Images.FirstOrDefaultAsync(i => i.ImageName.Contains(DefaultUserImageName));
        }

        public async Task<Image> GetDefaultAwardImageAsync()
        {
            return await _db.Images.FirstOrDefaultAsync(i => i.ImageName.Contains(DefaultAwardImageName));
        }

        public async Task UploadDefaultUserImageAsync(IFormFile newImage, string webRootPath)
        {
            Image image = await GetDefaultUserImageAsync();

            File.Delete(webRootPath + image.ImagePath);

            string newFileName = DefaultUserImageName + "_" + newImage.FileName;
            string path = UserImagesFolder + newFileName;

            using (var fileStream = new FileStream(webRootPath + path, FileMode.Create))
            {
                await newImage.CopyToAsync(fileStream);
            }

            image.ImageName = newFileName;
            image.ImagePath = path;
            await _db.SaveChangesAsync();
        }

        public async Task UploadDefaultAwardImageAsync(IFormFile newImage, string webRootPath)
        {
            Image image = await GetDefaultAwardImageAsync();

            File.Delete(webRootPath + image.ImagePath);

            string newFileName = DefaultAwardImageName + "_" + newImage.FileName;
            string path = AwardImagesFolder + newFileName;

            using (var fileStream = new FileStream(webRootPath + path, FileMode.Create))
            {
                await newImage.CopyToAsync(fileStream);
            }

            image.ImageName = newFileName;
            image.ImagePath = path;
            await _db.SaveChangesAsync();
        }

        public async Task AddImageToUserAsync(IFormFile newImage, string email, string webRootPath)
        {
            User user = await _userLogic.GetUserAsync(email);

            Image oldImage = user.Image;

            Image image = await SaveUserImageAsync(newImage, webRootPath);

            user.ImageId = image.ImageId;
            user.Image = image;
            await _db.SaveChangesAsync();

            if (!oldImage.ImageName.Contains(DefaultUserImageName))
            {
                await DeleteImageAsync(oldImage, webRootPath + oldImage.ImagePath);
            }
        }

        public async Task<Image> SaveAwardImageAsync(IFormFile newImage, string webRootPath)
        {
            string newFileName = Guid.NewGuid().ToString() + "_" + newImage.FileName;
            string path = AwardImagesFolder + newFileName;

            using (var fileStream = new FileStream(webRootPath + path, FileMode.Create))
            {
                await newImage.CopyToAsync(fileStream);
            }

            Image image = new Image { ImageName = newFileName, ImagePath = path };
            _db.Images.Add(image);
            await _db.SaveChangesAsync();

            return image;
        }

        private async Task<Image> SaveUserImageAsync(IFormFile newImage, string webRootPath)
        {
            string newFileName = Guid.NewGuid().ToString() + "_" + newImage.FileName;
            string path = UserImagesFolder + newFileName;

            using (var fileStream = new FileStream(webRootPath + path, FileMode.Create))
            {
                await newImage.CopyToAsync(fileStream);
            }

            Image image = new Image { ImageName = newFileName, ImagePath = path };
            _db.Images.Add(image);
            await _db.SaveChangesAsync();

            return image;
        }

        public async Task DeleteImageAsync(Image image, string path)
        {
            _db.Images.Remove(image);
            await _db.SaveChangesAsync();
            File.Delete(path);
        }
    }
}
