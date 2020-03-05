using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RokoAwards.EFDataAccessLibrary.DataAccess;
using RokoAwards.EFDataAccessLibrary.Models;
using RokoAwards.EFDataAccessLibrary.Utils;

namespace RokoAwards.BusinessLogicLibrary
{
    public class AccountLogic
    {
        private readonly ApplicationContext _db;
        private readonly UserLogic _userLogic;
        private readonly CityLogic _cityLogic;
        private readonly DepartmentLogic _departmentLogic;
        private readonly RoleLogic _roleLogic;
        private readonly ImageLogic _imageLogic;

        public AccountLogic(ApplicationContext applicationContext,
                            UserLogic userLogic, 
                            CityLogic cityLogic, 
                            DepartmentLogic departmentLogic, 
                            RoleLogic roleLogic, 
                            ImageLogic imageLogic)
        {
            _db = applicationContext;
            _userLogic = userLogic;
            _cityLogic = cityLogic;
            _departmentLogic = departmentLogic;
            _roleLogic = roleLogic;
            _imageLogic = imageLogic;
        }

        public async Task<User> GetUserForLoginAsync(string email, string password)
        {
            string hashedPassword = GetHashedPassword(email, password);

            User user = await _userLogic.GetUserAsync(email);

            if (user != null && hashedPassword == user.HashedPassword)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        public async Task<User> GetUserForRegisterAsync(string email)
        {
            return await _userLogic.GetUserAsync(email);
        }

        public async Task<User> RegisterAsync(User user)
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
            user.HashedPassword = GetHashedPassword(user.Email, user.HashedPassword);
            Role role = await _roleLogic.GetDefaultRoleAsync();
            user.RoleId = role.RoleId;
            user.Role = role;
            Image image = await _imageLogic.GetDefaultUserImageAsync();
            user.ImageId = image.ImageId;
            user.Image = image;

            return await _userLogic.AddUserAsync(user);
        }

        public async Task ChangeUserPasswordAsync(User user, string newHashedPassword)
        {
            user.HashedPassword = newHashedPassword;
            user.Status = Status.Active;
            await _db.SaveChangesAsync();
        }

        public string GetHashedPassword(string email, string password)
        {
            var sha256 = new SHA256CryptoServiceProvider();
            byte[] inputBytes = Encoding.UTF8.GetBytes(email + password);
            byte[] hashedBytes = sha256.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes).Replace("-", string.Empty);
        }
    }
}
