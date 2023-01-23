using Indigo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;

namespace Indigo.areas.manage.Services
{
    public class LayoutService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public LayoutService(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,IHttpContextAccessor contextAccessor )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _contextAccessor = contextAccessor;
        }


        public async Task<AppUser> GetUser()
        {
            string name = _contextAccessor.HttpContext.User.Identity.Name;
            AppUser appUser = null;
            appUser = await _userManager.FindByNameAsync(name);
            return appUser;
        }
    }
}
