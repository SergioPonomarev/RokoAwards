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
    public class UserLogic
    {
        private readonly ApplicationContext _db;
        private readonly RoleLogic _roleLogic;
        private readonly CityLogic _cityLogic;
        private readonly DepartmentLogic _departmentLogic;

        public UserLogic(ApplicationContext context, 
                        RoleLogic roleLogic, 
                        CityLogic cityLogic,
                        DepartmentLogic departmentLogic)
        {
            _db = context;
            _roleLogic = roleLogic;
            _cityLogic = cityLogic;
            _departmentLogic = departmentLogic;
        }

        public async Task<User> GetUserAsync(string email)
        {
            var user = await _db.Users
                .Include(u => u.City)
                .Include(u => u.CreatedAwards)
                .Include(u => u.Department)
                .Include(u => u.Image)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user != null)
            {
                var userAwardsReceived = await _db.UserAwards
                    .Include(ua => ua.AwardReceived)
                        .ThenInclude(a => a.Image)
                    .Include(ua => ua.AwardSent)
                        .ThenInclude(a => a.Image)
                    .Include(ua => ua.FromUser)
                    .Include(ua => ua.User)
                    .Where(ua => ua.User.Email == email)
                    .ToListAsync();

                var userAwardsSent = await _db.UserAwards
                    .Include(ua => ua.AwardReceived)
                        .ThenInclude(a => a.Image)
                    .Include(ua => ua.AwardSent)
                        .ThenInclude(a => a.Image)
                    .Include(ua => ua.FromUser)
                    .Include(ua => ua.User)
                    .Where(ua => ua.FromUser.Email == email)
                    .ToListAsync();

                user.UserAwardsReceived = userAwardsReceived;
                user.UserAwardsSent = userAwardsSent;
            }

            return user;
        }

        public async Task<User> AddUserAsync(User user)
        {
            _db.Add(user);
            await _db.SaveChangesAsync();
            return await GetUserAsync(user.Email);
        }

        public async Task SetRoleToUserAsync(string email, string roleName)
        {
            User user = await GetUserAsync(email);
            Role role = await _roleLogic.GetRoleAsync(roleName);

            if (user != null && role != null)
            {
                user.RoleId = role.RoleId;
                user.Role = role;
                await _db.SaveChangesAsync();
            }
        }

        public async Task UpdateUserProfileAsync(User user)
        {
            City city = await _cityLogic.GetCityByNameAsync(user.City.CityName);

            if (city == null)
            {
                city = await _cityLogic.AddCityAsync(user.City.CityName);
                user.CityId = city.CityId;
                user.City = city;
            }
            else
            {
                user.CityId = city.CityId;
                user.City = city;
            }

            Department department = await _departmentLogic.GetDepartmentByNameAsync(user.Department.DepartmentName);
            user.DepartmentId = department.DepartmentId;
            user.Department = department;

            await _db.SaveChangesAsync();
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _db.Users
                .Include(u => u.City)
                .Include(u => u.CreatedAwards)
                .Include(u => u.Department)
                .Include(u => u.Image)
                .Include(u => u.Role)
                .Include(u => u.UserAwardsReceived)
                .Include(u => u.UserAwardsSent)
                .ToListAsync();
        }
    }
}
