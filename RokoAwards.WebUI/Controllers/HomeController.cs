using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RokoAwards.BusinessLogicLibrary;
using RokoAwards.EFDataAccessLibrary.Models;
using RokoAwards.EFDataAccessLibrary.Utils;
using RokoAwards.WebUI.Models;

namespace RokoAwards.WebUI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserLogic _userLogic;
        private readonly UserAwardLogic _userAwardLogic;
        private const int searchPageSize = 5;
        private const int indexPageSize = 6;

        public HomeController(UserLogic userLogic, 
                              UserAwardLogic userAwardLogic)
        {
            _userLogic = userLogic;
            _userAwardLogic = userAwardLogic;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string search, int page = 1)
        {
            User user = await _userLogic.GetUserAsync(User.Identity.Name);
            if (user != null && user.Status == Status.Invited)
            {
                return RedirectToAction("ChangePassword", "Account");
            }

            List<UserAward> userAwards = await _userAwardLogic.GetAllUserAwardsAsync();
            userAwards = userAwards.OrderByDescending(ua => ua.AwardDate).ToList();

            var count = userAwards.Count;
            var items = userAwards.Skip((page - 1) * indexPageSize).Take(indexPageSize).ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, indexPageSize, search);
            IndexViewModel indexViewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                UserAwards = items
            };

            return View(indexViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string search, int page = 1)
        {
            var users = await _userLogic.GetUsersAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                users = users
                    .Where(u => u.Email.Contains(search, StringComparison.OrdinalIgnoreCase) || 
                                u.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase) || 
                                u.LastName.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            
            var count = users.Count;
            var items = users.Skip((page - 1) * searchPageSize).Take(searchPageSize).ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, searchPageSize, search);
            SearchViewModel searchVewModel = new SearchViewModel
            {
                PageViewModel = pageViewModel,
                Users = items
            };

            return View(searchVewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
