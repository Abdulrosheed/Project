using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Dto;
using Project.Model;

namespace Project.Repository.Interface
{
    public interface IUserRepository
    {
        User AddUser (User model);
        IList<User> ListAllUsers();
        User GetUserByEmailAndPassword (string email , string passWord);
    }
}