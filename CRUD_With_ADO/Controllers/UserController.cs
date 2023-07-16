using BusinessLogic.Interfaces;
using CRUD_With_ADO.Models;
using DataAccess.DTOs;
using DataAccess.DTOs.RequestDTO;
using DataAccess.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_With_ADO.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserLogic _userLogic;

        public UserController(IUserLogic userLogic)
        {
            this._userLogic = userLogic;
        }
        [HttpGet]
        public IActionResult Login(LoginDTO loginDTO)
        {
            ModelState.Clear(); // Clear the model state to prevent validation errors on initial page load
            if (TempData.ContainsKey("Email"))
            {
                loginDTO.Email = TempData["Email"] as string;
            }

            // Check if TempData contains the "Password" key
            if (TempData.ContainsKey("Password"))
            {
                loginDTO.Password = TempData["Password"] as string;
            }
            TempData.Remove("Email");
            TempData.Remove("Password");


            return View(loginDTO);
        }
        [HttpPost]
        public IActionResult LoginUser(LoginDTO loginDTO)
        {
            var result = _userLogic.Login(loginDTO);

            if(result != null)
            {
                if(result.IsSuccess)
                {
                    return View(result.Data);
                }
                else
                {
                    loginDTO.Error = result.Message;
                    return RedirectToAction("Login", loginDTO);
                }
            }

            return View("GeneralErrorPage");
        }

        [HttpGet]
        public IActionResult GetRegisterUserPage(UserDTO userDTO)
        {
            ModelState.Clear();
            return View(userDTO);
        }

        [HttpPost]
        public IActionResult RegisterUser(UserDTO userDTO)
        {
            var result = _userLogic.Registration(userDTO);

            if(result.IsSuccess )
            {
                return View(userDTO);
            }
            if(result.Message == "User already Exist")
            {
                TempData["Email"] = userDTO.Email;
                TempData["Password"] = userDTO.PassWord;
                return RedirectToAction("Login");
            }

            return RedirectToAction("GetRegisterUserPage", userDTO);
        }
    }
}
