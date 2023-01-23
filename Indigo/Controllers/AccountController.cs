using Indigo.Models;
using Indigo.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Indigo.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterVM userLoginVM)
        {
            if (!ModelState.IsValid) return View(userLoginVM);
            AppUser user = null;
            user =await _userManager.FindByNameAsync(userLoginVM.UserName);
            if(user != null)
            {
                ModelState.AddModelError("UserName", "Username already taken");
                return View();
            }
            user=await _userManager.FindByEmailAsync(userLoginVM.Email);
            if (user != null)
            {
                ModelState.AddModelError("Email", "Email already taken");
                return View();
            }
            user = new AppUser
            {
                UserName = userLoginVM.UserName,
                Email = userLoginVM.Email,
                FullName = userLoginVM.FullName,
                
            };
            var result=await _userManager.CreateAsync(user, userLoginVM.Password);

            if (!result.Succeeded)
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                    return View();
                }
            }
            await  _userManager.AddToRoleAsync(user,"Member");


            return RedirectToAction("index","home");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginVM userLoginVM)
        {
            if (!ModelState.IsValid) return View();
            AppUser user =await _userManager.FindByNameAsync(userLoginVM.UserName);
            if (user == null)
            {
                ModelState.AddModelError("", "UserName or password incorrect");
                return View();
            }
            var result =await _signInManager.PasswordSignInAsync(user, userLoginVM.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "UserName or password incorrect");
                return View();
            }
            return RedirectToAction("index","home");
        }
        public async Task<IActionResult> Logout()
        {
           await _signInManager.SignOutAsync();
            return RedirectToAction("login", "account");
        }

    }
}
