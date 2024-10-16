using MediatR;
using Microsoft.AspNetCore.Mvc;
using Survey.Application.Features.Commands.Universities.StudentClass;
using Survey.Application.Features.Commands.Users.SignIn;
using Survey.Application.Features.Commands.Users.SignOut;
using Survey.Application.Features.Commands.Users.SignUp;
using Survey.Application.Features.Queries.Universities.Classes;
using WebApp.Extensions;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            var classes = await _mediator.Send(new GetAllClassesQuery());

            var model = new RegisterViewModel
            {
                Classes = classes.Result
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            string userPassword = model.ConfirmPassword;
            var result = await _mediator.Send(new SignUpCommand
            {
                Email = model.Email,
                Password = userPassword,
                StudentNumber = model.StudentNumber,
                IsProfessor = model.IsProfessor,
                Username = model.StudentNumber
            });

            if (result.Succeeded)
            {
                if (model.IsProfessor)
                {
                    await _mediator.Send(new SignInCommand
                    {
                        Username = model.StudentNumber,
                        Password = userPassword
                    });
                    return RedirectToAction("Index", "Professor");
                }
                else
                {
                    await _mediator.Send(new SignInCommand
                    {
                        Username = model.StudentNumber,
                        Password = userPassword
                    });

                    var @class = await _mediator.Send(new GetClassByNameQuery { ClassName = model.SelectedClassName });
                    await _mediator.Send(new AddStudentToClassCommand { StudentId = User.GetUserId(), ClassId = @class.Result.Id });

                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.IsLoggedIn())
            {
                if (User.IsProfessor())
                {
                    return RedirectToAction("Index", "Professor");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(new SignInCommand
                {
                    Username = model.StudentNumber,
                    Password = model.Password
                });

                if (result.Succeeded)
                {
                    var isProfessor = User.IsProfessor();
                    if (isProfessor)
                    {
                        return RedirectToAction("Index", "Professor");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _mediator.Send(new SignOutCommand());
            return RedirectToAction("Login", "User");
        }
    }
}