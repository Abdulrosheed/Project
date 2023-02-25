using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Dto;

namespace Project.Service.Interface
{
    public interface IUserService
    {
        UserDto AddUser (CreateUserRequestModel model , string role);
        IList<UserDto> ListAllUsers();
        UserDto GetUserByEmailAndPassword (string email , string passWord);
    }
}