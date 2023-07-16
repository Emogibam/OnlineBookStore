using BusinessLogic.ExtensionMethods;
using BusinessLogic.Interfaces;
using DataAccess.DTOs;
using DataAccess.DTOs.RequestDTO;
using DataAccess.Entities;
using DataAccess.Helpers;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.BusinessLogics
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserRepository _userRepository;
        private readonly HashingPassword _hashingPassword;

        public UserLogic(IUserRepository userRepository, HashingPassword hashingPassword)
        {
            _userRepository = userRepository;
            this._hashingPassword = hashingPassword;
        }

        public ServiceResult<string> Login(LoginDTO loginDetails)
        {
            User user = _userRepository.GetUserByEmail(loginDetails.Email);

            if (user == null || string.IsNullOrWhiteSpace(user.Email))
            {
                return new ServiceResult<string>("User not found", 404, "User doesn't Exist");
            }
            else if (_hashingPassword.VerifyPassword(user.Password, user.Salt, loginDetails.Password))
            {
                return new ServiceResult<string>(string.Format($"Welcome {user.FirstName}"), 200, "User exist");
            }
            else if(!_hashingPassword.VerifyPassword(user.Password, user.Salt, loginDetails.Password))
            {
                return new ServiceResult<string>("Incorrect Passowrd", 404, "User password doesn't exist");
            }
            else if(user.Email!= loginDetails.Email)
            {
                return new ServiceResult<string>($"Incorrect Email: {loginDetails.Email}", 404, "The Email user is not correct");
            }

            return new ServiceResult<string>("Error", 404, "AN error occured we are working on it");
        }
        

        public ServiceResult<UserDTO> Registration(UserDTO userDTO)
        {
            User existingUser = _userRepository.GetUserByEmail(userDTO.Email);

            if(existingUser !=null)
            {
                return new ServiceResult<UserDTO>(userDTO, 400, "User already Exist");
            }

            string salt = "";
            string password = _hashingPassword.HashPassword(userDTO.PassWord, out salt);
            User user = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                Email = userDTO.Email,
                Address = userDTO.Address,
                PhoneNumber = userDTO.PhoneNumber,
                UserName = userDTO.Email.Split('@')[0],
                Salt = salt,
                Password = password,
                IsEmailConfirmed = false,
                IsPhoneNumberActive= false,
            };

            bool result = _userRepository.CreateUser(user);
            if(result)
            {
                userDTO.Id = user.Id;
                string body = "<h1>Hello, this is a test email!</h1>";
                EmailHelper.SendEmail(userDTO.Email, "Confirm Email", body);
                return new ServiceResult<UserDTO>(userDTO, 200, "SUcessfully Created");
            }

            return new ServiceResult<UserDTO>(userDTO, 400, "Not Created");
        }
        

        }

    }

