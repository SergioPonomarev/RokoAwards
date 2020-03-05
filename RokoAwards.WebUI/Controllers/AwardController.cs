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
using RokoAwards.EFDataAccessLibrary.Utils;
using RokoAwards.WebUI.Models;

namespace RokoAwards.WebUI.Controllers
{
    [Authorize]
    public class AwardController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly AwardLogic _awardLogic;
        private readonly ImageLogic _imageLogic;
        private readonly UserLogic _userLogic;
        private readonly UserAwardLogic _userAwardLogic;
        public AwardController(IWebHostEnvironment env, 
                                AwardLogic awardLogic, 
                                ImageLogic imageLogic,
                                UserLogic userLogic,
                                UserAwardLogic userAwardLogic)
        {
            _env = env;
            _awardLogic = awardLogic;
            _imageLogic = imageLogic;
            _userLogic = userLogic;
            _userAwardLogic = userAwardLogic;
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpGet]
        public async Task<IActionResult> DefaultAwardImage()
        {
            Image image = await _imageLogic.GetDefaultAwardImageAsync();
            return View(image);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> DefaultAwardImage(IFormFile newImage)
        {
            if (newImage != null)
            {
                await _imageLogic.UploadDefaultAwardImageAsync(newImage, _env.WebRootPath);
            }

            return RedirectToAction("DefaultAwardImage", "Award");
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpGet]
        public IActionResult CreateAward()
        {
            var awardTypes = Enum.GetValues(typeof(AwardType))
                .Cast<AwardType>()
                .Select(at => at.ToString())
                .Where(at => at != "None")
                .ToArray();

            ViewBag.AwardTypes = new SelectList(awardTypes);
            return View();
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPost]
        public async Task<IActionResult> CreateAward(IFormFile newImage, CreateAwardModel model)
        {
            if (ModelState.IsValid)
            {
                Award award = await _awardLogic.GetAwardByNameAsync(model.AwardTitle);

                if (award == null)
                {
                    award = new Award
                    {
                        AwardTitle = model.AwardTitle,
                        Description = model.Description,
                        AwardType = model.AwardType,
                        Creater = new User { Email = User.Identity.Name }
                    };

                    await _awardLogic.CreateAwardAsync(newImage, award, _env.WebRootPath);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Award with this title already exists");
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddAward()
        {
            List<Award> awards = await _awardLogic.GetAwardsAsync();
            if (User.IsInRole("User"))
            {
                ViewBag.Awards = new SelectList(awards
                    .Where(a => a.AwardType == AwardType.Thanks)
                    .Select(a => a.AwardTitle)
                    .OrderBy(a => a)
                    .ToArray());
            }
            else
            {
                ViewBag.Awards = new SelectList(awards
                    .Select(a => a.AwardTitle)
                    .OrderBy(a => a)
                    .ToArray());
            }

            List<User> users = await _userLogic.GetUsersAsync();
            var recepients = users
                .Where(u => u.Email != User.Identity.Name && u.Status != Status.Special)
                .Select(u => u.Email)
                .OrderBy(u => u)
                .ToArray();

            string[] usersFrom;
            if (User.IsInRole("User"))
            {
                usersFrom = users
                    .Where(u => u.Email == User.Identity.Name)
                    .Select(u => u.Email)
                    .OrderBy(u => u)
                    .ToArray();
            }
            else
            {
                usersFrom = users
                    .Where(u => u.Email == User.Identity.Name || u.Status == Status.Special)
                    .Select(u => u.Email)
                    .OrderBy(u => u)
                    .ToArray();
            }

            ViewBag.Recepients = new SelectList(recepients);
            ViewBag.UsersFrom = new SelectList(usersFrom);

            AddAwardModel model = new AddAwardModel
            {
                FromEmail = usersFrom.FirstOrDefault(u => u == User.Identity.Name)
            };

            return PartialView("_AddAwardPartial", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAward(AddAwardModel model)
        {
            if (ModelState.IsValid)
            {
                UserAward userAward = new UserAward();
                userAward.AwardReceived = new Award { AwardTitle = model.AwardTitle };
                userAward.User = new User { Email = model.RecepientEmail };
                userAward.Description = model.Description;
                userAward.FromUser = new User { Email = model.FromEmail };

                await _userAwardLogic.AddAwardAsync(userAward);
            }

            List<Award> awards = await _awardLogic.GetAwardsAsync();
            if (User.IsInRole("User"))
            {
                ViewBag.Awards = new SelectList(awards
                    .Where(a => a.AwardType == AwardType.Thanks)
                    .Select(a => a.AwardTitle)
                    .OrderBy(a => a)
                    .ToArray());
            }
            else
            {
                ViewBag.Awards = new SelectList(awards
                    .Select(a => a.AwardTitle)
                    .OrderBy(a => a)
                    .ToArray());
            }

            List<User> users = await _userLogic.GetUsersAsync();
            var recepients = users
                .Where(u => u.Email != User.Identity.Name && u.Status != Status.Special)
                .Select(u => u.Email)
                .OrderBy(u => u)
                .ToArray();

            string[] usersFrom;
            if (User.IsInRole("User"))
            {
                usersFrom = users
                    .Where(u => u.Email == User.Identity.Name)
                    .Select(u => u.Email)
                    .OrderBy(u => u)
                    .ToArray();
            }
            else
            {
                usersFrom = users
                    .Where(u => u.Email == User.Identity.Name || u.Status == Status.Special)
                    .Select(u => u.Email)
                    .OrderBy(u => u)
                    .ToArray();
            }

            ViewBag.Recepients = new SelectList(recepients);
            ViewBag.UsersFrom = new SelectList(usersFrom);

            return PartialView("_AddAwardPartial", model);
        }

        [HttpGet]
        public async Task<IActionResult> DisplayUserAward(int userAwardId)
        {
            UserAward userAward = await _userAwardLogic.GetUserAvardByIdAsync(userAwardId);

            if (userAward != null)
            {
                return PartialView("_DisplayUserAwardPartial", userAward);
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> AwardList()
        {
            List<Award> awards = await _awardLogic.GetAwardsAsync();

            return View(awards);
        }

        [HttpGet]
        public async Task<IActionResult> DisplayAward(int awardId)
        {
            Award award = await _awardLogic.GetAwardByIdAsync(awardId);

            if (award != null)
            {
                return PartialView("_DisplayAwardPartial", award);
            }

            return NotFound();
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> EditAward(int awardId)
        {
            Award award = await _awardLogic.GetAwardByIdAsync(awardId);

            if (award != null)
            {
                var awardTypes = Enum.GetValues(typeof(AwardType))
                    .Cast<AwardType>()
                    .Select(at => at.ToString())
                    .Where(at => at != "None")
                    .ToArray();

                ViewBag.AwardTypes = new SelectList(awardTypes);

                EditAwardModel model = new EditAwardModel
                {
                    AwardId = award.AwardId,
                    AwardTitle = award.AwardTitle,
                    AwardType = award.AwardType,
                    Description = award.Description,
                    ImagePath = award.Image.ImagePath
                };

                return View(model);
            }

            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAward(IFormFile newImage, EditAwardModel model)
        {
            if (ModelState.IsValid)
            {
                Award award = await _awardLogic.GetAwardByIdAsync(model.AwardId);

                if (award != null)
                {
                    award.AwardTitle = model.AwardTitle;
                    award.AwardType = model.AwardType;
                    award.Description = model.Description;

                    await _awardLogic.EditAwardAsync(newImage, award, _env.WebRootPath);

                    return RedirectToAction("AwardList", "Award");
                }
                else
                {
                    ModelState.AddModelError("", "Award does not exists");
                }
            }

            var awardTypes = Enum.GetValues(typeof(AwardType))
                    .Cast<AwardType>()
                    .Select(at => at.ToString())
                    .Where(at => at != "None")
                    .ToArray();

            ViewBag.AwardTypes = new SelectList(awardTypes);

            return View(model);
        }
    }
}