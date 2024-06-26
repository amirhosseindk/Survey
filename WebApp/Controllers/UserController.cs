﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly AppDbContext _appDbContext;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, AppDbContext appDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public IActionResult Register()
        {
            var classes = _appDbContext.Classes
                                       .GroupBy(c => c.Name)
                                       .Select(g => g.First())
                                       .ToList();
            var model = new RegisterViewModel
            {
                Classes = classes
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

                var user = new User
                {
                    UserName = model.StudentNumber,
                    Email = model.Email,
                    EmailConfirmed = true,
                    IsProfessor = model.IsProfessor,
                    StudentNumber = model.StudentNumber,
                };

                string userPassword = model.ConfirmPassword;
                var result = await _userManager.CreateAsync(user, userPassword);
                if (result.Succeeded)
                {
                    if (model.IsProfessor)
                    {
                        await _userManager.AddToRoleAsync(user, "Professor");
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Index", "Professor");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "Student");
                        await _signInManager.SignInAsync(user, isPersistent: false);

                        var selectedClassNames = _appDbContext.Classes
                                                              .Where(c => c.Name == model.SelectedClassName)
                                                              .ToList();

                        foreach (var classEntity in selectedClassNames)
                        {
                            classEntity.Students.Add(user);
                        }

                        await _appDbContext.SaveChangesAsync();

                        return RedirectToAction("Index", "Home");
                    }
                }

                model.Classes = _appDbContext.Classes
                                              .GroupBy(c => c.Name)
                                              .Select(g => g.First())
                                              .ToList();
                return View(model);

            model.Classes = _appDbContext.Classes
                                          .GroupBy(c => c.Name)
                                          .Select(g => g.First())
                                          .ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.StudentNumber, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.StudentNumber);
                    if (user != null)
                    {
                        if (await _userManager.IsInRoleAsync(user, "Professor"))
                        {
                            return RedirectToAction("Index", "Professor");
                        }
                        else if (await _userManager.IsInRoleAsync(user, "Student"))
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "User");
        }
    }
}