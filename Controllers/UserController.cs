using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Project.Dto;
using Project.Service.Interface;

namespace Project.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View(_userService.ListAllUsers());
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateUserRequestModel model)
        {

            _userService.AddUser(model , "User");
            return RedirectToAction("LoginPage");
        }
         public IActionResult CreateAdmin()
        {

            return View();
        }
        [HttpPost]
        public IActionResult CreateAdmin(CreateUserRequestModel model)
        {

            _userService.AddUser(model , "Admin");
            return RedirectToAction("LoginPage");
        }
       
        public IActionResult UserIndex()
        {
           ViewData["role"] = User.FindFirst(ClaimTypes.Role).Value;
            return View();
        }
        public IActionResult LoginPage()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult LoginPage(LoginUserRequestModel model)
        {
            var user = _userService.GetUserByEmailAndPassword(model.Email, model.PassWord);
            if (user == null)
            {
                return View();
            }
            var claims = new List<Claim>
            {
                new Claim (ClaimTypes.Email , user.Email),
                new Claim(ClaimTypes.Role , user.Role)
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authenticationProperties = new AuthenticationProperties();
            var principal = new ClaimsPrincipal(claimsIdentity);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authenticationProperties);

            return RedirectToAction("UserIndex"); ;
        }
    }
}