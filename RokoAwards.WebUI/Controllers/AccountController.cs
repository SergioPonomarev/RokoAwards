using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RokoAwards.BusinessLogicLibrary;
using RokoAwards.EFDataAccessLibrary.Models;
using RokoAwards.EFDataAccessLibrary.Utils;
using RokoAwards.WebUI.Models;

namespace RokoAwards.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountLogic _accountLogic;
        private readonly UserLogic _userLogic;
        private readonly DepartmentLogic _departmentLogic;

        public AccountController(AccountLogic accountLogic, UserLogic userLogic, DepartmentLogic departmentLogic)
        {
            _accountLogic = accountLogic;
            _userLogic = userLogic;
            _departmentLogic = departmentLogic;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _accountLogic.GetUserForLoginAsync(model.Email, model.Password);

                if (user != null)
                {
                    await AuthenticateAsync(user);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Wrong login or password");
            }

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userLogic.GetUserAsync(User.Identity.Name);

                string hashedPassword = _accountLogic.GetHashedPassword(user.Email, model.Password);

                if (user.HashedPassword == hashedPassword)
                {
                    if (user != null)
                    {
                        string newHashedPassword = _accountLogic.GetHashedPassword(user.Email, model.NewPassword);

                        if (newHashedPassword == user.HashedPassword)
                        {
                            ModelState.AddModelError("", "You cannot set same password");
                        }
                        else
                        {
                            await _accountLogic.ChangeUserPasswordAsync(user, newHashedPassword);
                            return RedirectToAction("Logout", "Account");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Wrong password");
                }
            }

            return View(model);
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            List<Department> departments = await _departmentLogic.GetDepartmentsAsync();

            ViewBag.Departments = new SelectList(departments.Select(d => d.DepartmentName).ToArray());

            return View();
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _accountLogic.GetUserForRegisterAsync(model.Email);

                if (user == null)
                {
                    user = new User
                    {
                        Email = model.Email,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Department = new Department { DepartmentName = model.DepartmentName },
                        PositionName = model.PositionName,
                        ReportingManagerEmail = model.ReportingManagerEmail,
                        City = new City { CityName = model.City },
                        DateOfJoining = model.DateOfJoining,
                        HashedPassword = model.Password
                    };

                    if (model.SpecialUser)
                    {
                        user.Status = Status.Special;
                    }
                    else
                    {
                        user.Status = Status.Invited;
                    }

                    user = await _accountLogic.RegisterAsync(user);

                    if (user != null)
                    {
                        await AuthenticateAsync(user);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Cannot register new user");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "User is already exists");
                }
            }

            List<Department> departments = await _departmentLogic.GetDepartmentsAsync();

            ViewBag.Departments = new SelectList(departments.Select(d => d.DepartmentName).ToArray());

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        private async Task AuthenticateAsync(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.RoleName)
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}