using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandmadeFinal.DAL;
using HandmadeFinal.Models;
using HandmadeFinal.Utilities;
using HandmadeFinal.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HandmadeFinal.Areas.Handmade.Controllers
{[Area("Handmade")]
    public class AccountController : Controller
    {
        private AppDbContext _context;
        private UserManager<UserRegister> _userManager;
        private SignInManager<UserRegister> _signInManager;
        private RoleManager<IdentityRole> _roleManager;
        public AccountController(AppDbContext context, UserManager<UserRegister> userManager, SignInManager<UserRegister> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Register()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterSellerView registersellerview)

        {
            if (ModelState.IsValid) return View(registersellerview);

            UserRegister user = new UserRegister()
            {
                Name = registersellerview.Name,
                Surname = registersellerview.Surname,
                Email = registersellerview.Email,
                UserName = registersellerview.UserName,
                PhoneNumber = registersellerview.Phone

            };
            IdentityResult identityResult = await _userManager.CreateAsync(user, registersellerview.Password);

            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(registersellerview);
            }


            await _userManager.AddToRoleAsync(user, Utility.Roles.Admin.ToString());
            await _signInManager.SignInAsync(user, true);
            return RedirectToAction("Index", "AdminDashboard");


            

        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "AdminDashboard");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginView loginView)
        {
            if (!ModelState.IsValid) return View(loginView);
            UserRegister userRegister = await _userManager.FindByEmailAsync(loginView.Email);
            if (userRegister == null)
            {
                ModelState.AddModelError("", "Email və ya parol səhvdir");
                return View(loginView);

            }
            Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(userRegister, loginView.Password, loginView.RememberMe, true);

            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError("", "Email və ya parol səhvdir");
                return View(loginView);
            }
            return RedirectToAction("Index", "AdminDashboard");
        }

        public async Task RoleSeeder()
        {
            if (!await _roleManager.RoleExistsAsync(Utility.Roles.Admin.ToString()))
            {
                await _roleManager.CreateAsync(new IdentityRole(Utility.Roles.Admin.ToString()));


            }
            if (!await _roleManager.RoleExistsAsync(Utility.Roles.Member.ToString()))
            {
                await _roleManager.CreateAsync(new IdentityRole(Utility.Roles.Member.ToString()));

            }
        }
    }
}