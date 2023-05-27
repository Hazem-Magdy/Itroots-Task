using Itroots_Task.Models;
using Itroots_Task.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Itroots_Task.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public HomeController(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<IActionResult> Index(string searchQuery)
        {
            List<User> usersList = await _userRepository.GetAllUsersAsync();
            if (!string.IsNullOrEmpty(searchQuery))
            {
                usersList = usersList.Where(u =>
                u.FullName.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0 ||
                u.UserName.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0 ||
                u.Email.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0 ||
                u.Phone.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0 ||
                u.Role.Name.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0
                ).ToList();
            }
            ViewBag.SearchQuery = searchQuery;
            return View(usersList);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int id)
        {
            User userDetails = await _userRepository.GetUserByIdAsync(id);
            if (userDetails == null)
            {
                return View("NotFound");
            }
            return View(userDetails);
        }
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Create()
        {

            ViewBag.Roles = await _roleRepository.getAllRolesAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                await _userRepository.AddUserAsync(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            User userDetails = await _userRepository.GetUserByIdAsync(id);
            if (userDetails == null) return View("NotFound");

            return View(userDetails);

        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            User userDetails = await _userRepository.GetUserByIdAsync(id);
            if (userDetails != null)
            {
                await _userRepository.DeleteUserAsync(id);
                return RedirectToAction("Index");
            }
            return View("NotFound");

        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            User existUser = await _userRepository.GetUserByIdAsync(id);
            if (existUser == null) return View("NotFound");
            ViewBag.Roles = await _roleRepository.getAllRolesAsync();
            return View(existUser);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(User user)
        {
            if (ModelState.IsValid)
            {
                await _userRepository.UpdateUserAsync(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }
    }
}