using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RokoAwards.EFDataAccessLibrary.DataAccess;
using RokoAwards.EFDataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RokoAwards.BusinessLogicLibrary
{
    public class AwardLogic
    {
        private readonly ApplicationContext _db;
        private readonly UserLogic _userLogic;
        private readonly ImageLogic _imageLogic;
        public AwardLogic(ApplicationContext context, UserLogic userLogic, ImageLogic imageLogic)
        {
            _db = context;
            _userLogic = userLogic;
            _imageLogic = imageLogic;
        }

        public async Task<Award> GetAwardByNameAsync(string awardTitle)
        {
            return await _db.Awards
                .Include(aw => aw.Creater)
                .Include(aw => aw.Image)
                .Include(aw => aw.UserAwardsReceived)
                .Include(aw => aw.UserAwardsSent)
                .FirstOrDefaultAsync(aw => aw.AwardTitle == awardTitle);
        }

        public async Task<Award> GetAwardByIdAsync(int awardId)
        {
            return await _db.Awards
                .Include(aw => aw.Creater)
                .Include(aw => aw.Image)
                .Include(aw => aw.UserAwardsReceived)
                .Include(aw => aw.UserAwardsSent)
                .FirstOrDefaultAsync(aw => aw.AwardId == awardId);
        }

        public async Task CreateAwardAsync(IFormFile newImage, Award award, string webRootPath)
        {
            Image image;
            if (newImage == null)
            {
                image = await _imageLogic.GetDefaultAwardImageAsync();
            }
            else
            {
                image = await _imageLogic.SaveAwardImageAsync(newImage, webRootPath);
            }

            User user = await _userLogic.GetUserAsync(award.Creater.Email);

            award.CreaterId = user.UserId;
            award.Creater = user;
            award.CreatingDate = DateTime.Now;
            award.ImageId = image.ImageId;
            award.Image = image;

            _db.Awards.Add(award);
            await _db.SaveChangesAsync();
        }

        public async Task<List<Award>> GetAwardsAsync()
        {
            return await _db.Awards
                .Include(a => a.Creater)
                .Include(a => a.Image)
                .Include(a => a.UserAwardsReceived)
                .Include(a => a.UserAwardsSent)
                .ToListAsync();
        }

        public async Task EditAwardAsync(IFormFile newImage, Award award, string webRootPath)
        {
            Image image;
            Image oldImage = award.Image;

            if (newImage != null)
            {
                image = await _imageLogic.SaveAwardImageAsync(newImage, webRootPath);
                award.ImageId = image.ImageId;
                award.Image = image;
            }

            await _db.SaveChangesAsync();

            if (!oldImage.ImageName.Contains("DefaultAwardImage"))
            {
                await _imageLogic.DeleteImageAsync(oldImage, webRootPath + oldImage.ImagePath);
            }
        }
    }
}
