using System.ComponentModel.DataAnnotations;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Authorize(Roles = "User")]
        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel? model)
        {
            if (model == null)
            {
                return View(model);
            }

            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Dashboard", "User");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("Password", error.Description);
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                ModelState.AddModelError("Password", "Invalid password");
                return View(request);
            }

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, request.RememberMe, false);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("Password", "Invalid password");
                    return View(request);
                }

                var roles = await _userManager.GetRolesAsync(user);
                
                if (roles.Contains("Admin"))
                {
                    return RedirectToAction("Index", "Book");
                }

                return RedirectToAction("Dashboard", "User");
            }
            return View(request);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Json(new
            {
                Success = true
            });
        }
    }
}
