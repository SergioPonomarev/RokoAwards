using Microsoft.EntityFrameworkCore;
using RokoAwards.EFDataAccessLibrary.DataAccess;
using RokoAwards.EFDataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RokoAwards.BusinessLogicLibrary
{
    public class UserAwardLogic
    {
        private readonly ApplicationContext _db;
        private readonly UserLogic _userLogic;
        private readonly AwardLogic _awardLogic;

        public UserAwardLogic(ApplicationContext context,
                                UserLogic userLogic,
                                AwardLogic awardLogic)
        {
            _db = context;
            _userLogic = userLogic;
            _awardLogic = awardLogic;
        }

        public async Task AddAwardAsync(UserAward userAward)
        {
            Award award = await _awardLogic.GetAwardByNameAsync(userAward.AwardReceived.AwardTitle);
            User fromUser = await _userLogic.GetUserAsync(userAward.FromUser.Email);
            User recipient = await _userLogic.GetUserAsync(userAward.User.Email);

            userAward.AwardIdReceived = award.AwardId;
            userAward.AwardReceived = award;
            userAward.AwardIdSent = award.AwardId;
            userAward.AwardSent = award;
            userAward.FromUserId = fromUser.UserId;
            userAward.FromUser = fromUser;
            userAward.UserId = recipient.UserId;
            userAward.User = recipient;
            userAward.AwardDate = DateTime.Now;

            _db.UserAwards.Add(userAward);
            await _db.SaveChangesAsync();
        }

        public async Task<List<UserAward>> GetAllUserAwardsAsync()
        {
            return await _db.UserAwards
                .Include(ua => ua.AwardReceived)
                    .ThenInclude(a => a.Image)
                .Include(ua => ua.AwardSent)
                    .ThenInclude(a => a.Image)
                .Include(ua => ua.FromUser)
                .Include(ua => ua.User)
                .ToListAsync();
        }

        public async Task<UserAward> GetUserAvardByIdAsync(int id)
        {
            return await _db.UserAwards
                .Include(ua => ua.AwardReceived)
                    .ThenInclude(a => a.Image)
                .Include(ua => ua.AwardSent)
                    .ThenInclude(a => a.Image)
                .Include(ua => ua.FromUser)
                .Include(ua => ua.User)
                .FirstOrDefaultAsync(ua => ua.UserAwardId == id);
        }
    }
}
