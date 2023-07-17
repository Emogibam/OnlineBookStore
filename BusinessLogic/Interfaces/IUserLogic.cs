using DataAccess.DTOs;
using DataAccess.DTOs.RequestDTO;
using DataAccess.Entities;
using DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IUserLogic
    {
        ServiceResult<string> Login(LoginDTO loginDetails);
        ServiceResult<UserDTO> Registration(UserDTO userDTO);
    }
}
