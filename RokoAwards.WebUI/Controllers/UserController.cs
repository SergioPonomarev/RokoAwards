using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RokoAwards.BusinessLogicLibrary;
using RokoAwards.EFDataAccessLibrary.Models;
using RokoAwards.WebUI.Models;

namespace RokoAwards.WebUI.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly UserLogic _userLogic;
        private readonly ImageLogic _imageLogic;
        private readonly RoleLogic _roleLogic;
        private readonly DepartmentLogic _departmentLogic;
        public UserController(IWebHostEnvironment env, 
                                UserLogic userLogic, 
                                ImageLogic imageLogic, 
                                RoleLogic roleLogic,
                                DepartmentLogic departmentLogic)
        {
            _env = env;
            _userLogic = userLogic;
            _imageLogic = imageLogic;
            _roleLogic = roleLogic;
            _departmentLogic = departmentLogic;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> DefaultUserImage()
        {
            Image image = await _imageLogic.GetDefaultUserImageAsync();
            return View(image);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> DefaultUserImage(IFormFile newImage)
        {
            if (newImage != null)
            {
                await _imageLogic.UploadDefaultUserImageAsync(newImage, _env.WebRootPath);
            }

            return RedirectToAction("DefaultUserImage", "User");
        }

        [HttpGet]
        public async Task<IActionResult> UserProfile(string email)
        {
            List<Role> roles = await _roleLogic.GetRolesAsync();

            ViewBag.Roles = new SelectList(roles.Select(r => r.RoleName).ToArray());

            User user = await _userLogic.GetUserAsync(email);

            return View(user);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SetRoleToUser(string email, string roleName)
        {
            await _userLogic.SetRoleToUserAsync(email, roleName);
            return RedirectToAction("UserProfile", "User", new { email });
        }

        [HttpPost]
        public async Task<IActionResult> SetUserImage(string email, IFormFile newImage)
        {
            if (newImage != null)
            {
                await _imageLogic.AddImageToUserAsync(newImage, email, _env.WebRootPath);
            }

            return RedirectToAction("UserProfile", "User", new { email });
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> EditUserProfile(string email)
        {
            List<Department> departments = await _departmentLogic.GetDepartmentsAsync();
            ViewBag.Departments = new SelectList(departments.Select(d => d.DepartmentName).ToArray());

            User user = await _userLogic.GetUserAsync(email);
            EditUserModel editUserModel = new EditUserModel
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfJoining = user.DateOfJoining,
                PositionName = user.PositionName,
                DepartmentName = user.Department.DepartmentName,
                ReportingManagerEmail = user.ReportingManagerEmail,
                City = user.City.CityName
            };

            return PartialView("_EditUserProfilePartial", editUserModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUserProfile(EditUserModel editUserModel)
        {
            if (ModelState.IsValid)
            {
                User user = await _userLogic.GetUserAsync(editUserModel.Email);

                if (user != null)
                {
                    user.FirstName = editUserModel.FirstName;
                    user.LastName = editUserModel.LastName;
                    user.Department.DepartmentName = editUserModel.DepartmentName;
                    user.PositionName = editUserModel.PositionName;
                    user.ReportingManagerEmail = editUserModel.ReportingManagerEmail;
                    user.City.CityName = editUserModel.City;
                    user.DateOfJoining = editUserModel.DateOfJoining;

                    await _userLogic.UpdateUserProfileAsync(user);
                }
            }

            List<Department> departments = await _departmentLogic.GetDepartmentsAsync();
            ViewBag.Departments = new SelectList(departments.Select(d => d.DepartmentName).ToArray());
            return PartialView("_EditUserProfilePartial", editUserModel);
        }
    }
}