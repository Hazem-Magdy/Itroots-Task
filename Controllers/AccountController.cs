using Itroots_Task.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Itroots_Task.Data;
using Itroots_Task.Services;
using Itroots_Task.Models;

namespace Itroots_Task.Controllers
{
    public class AccountController : Controller
    {

        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRoleRepository _userRoleRepository;

        public AccountController(AppDbContext _db, IUserRepository userRepository, IRoleRepository roleRepository, IUserRoleRepository userRoleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
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
                Models.User user = await _userRepository.existingUserAsync(model.UserName, model.Password);
                if (user != null)
                {
                    Claim c1 = new Claim(ClaimTypes.Name, user.UserName);
                    Claim c2 = new Claim(ClaimTypes.Email, user.Email);
                    Claim c3 = new Claim(ClaimTypes.Role, user.Role.Name);

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    claimsIdentity.AddClaim(c1);
                    claimsIdentity.AddClaim(c2);
                    claimsIdentity.AddClaim(c3);

                    ClaimsPrincipal cP = new ClaimsPrincipal();
                    cP.AddIdentity(claimsIdentity);
                    await HttpContext.SignInAsync(cP);
                    return RedirectToAction("Index", "Home");

                }
                ModelState.AddModelError("", "Incorrect Password or Email");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterationViewModel registerModel)
        {
            if (ModelState.IsValid)
            {
                //check if username exist
                User userExist = await _userRepository.GetUserByUserNameAsync(registerModel.UserName);

                if (userExist != null)
                {
                    ModelState.AddModelError("UserName", "Username is already taken.");
                    return View(registerModel);
                }

                // check if email exist
                User emailExist = await _userRepository.GetUserByEmailAsync(registerModel.Email);

                if (emailExist != null)
                {
                    ModelState.AddModelError("Email", "Email is already registered.");
                    return View(registerModel);
                }

                Role adminRole = await _roleRepository.getRoleAsync("Admin");

                Role userRole = await _roleRepository.getRoleAsync("User");

                // create newuser object
                Models.User newUser = new Models.User()
                {
                    FullName = registerModel.FullName,
                    Email = registerModel.Email,
                    UserName = registerModel.UserName,
                    Phone = registerModel.Phone,
                    Password = registerModel.Password
                };

                // First user registered will be the admin, other users will be normal user
                List<User> users = await _userRepository.GetAllUsersAsync();

                if (users.Count > 0)
                    newUser.RoleId = userRole.Id;
                else 
                    newUser.RoleId = adminRole.Id;


                // save new user object in database 
                User addedUser = await _userRepository.AddUserAsync(newUser);

                if (adminRole != null || userRole != null)
                {
                    // asign role to user
                    Models.UserRole assignUserToRule = new Models.UserRole()
                    {
                        UserId = addedUser.Id,
                        RoleId = addedUser.RoleId
                    };
                    await _userRoleRepository.AddUserRoleAsync(assignUserToRule);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
