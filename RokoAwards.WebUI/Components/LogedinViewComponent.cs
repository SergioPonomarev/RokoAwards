using Microsoft.AspNetCore.Mvc;
using RokoAwards.BusinessLogicLibrary;
using RokoAwards.EFDataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RokoAwards.WebUI.Components
{
    public class LogedinViewComponent : ViewComponent
    {
        private readonly UserLogic _userLogic;
        public LogedinViewComponent(UserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            User user = await _userLogic.GetUserAsync(User.Identity.Name);

            return View("Logedin", user);
        }
    }
}
