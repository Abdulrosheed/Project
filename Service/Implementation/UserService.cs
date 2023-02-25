using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Dto;
using Project.Model;
using Project.Repository.Interface;
using Project.Service.Interface;

namespace Project.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public UserDto AddUser(CreateUserRequestModel model , string role)
        {
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PassWord = model.PassWord,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                Role = role
            };
            var res = _repository.AddUser(user);
            return new UserDto(res.Id , res.FirstName,res.LastName,res.PhoneNumber,res.Role,res.Email);
        }

        public UserDto GetUserByEmailAndPassword(string email , string passWord)
        {
            var usr = _repository.GetUserByEmailAndPassword(email,passWord);
            if(usr == null) return null;
            return new UserDto(usr.Id , usr.FirstName,usr.LastName,usr.PhoneNumber,usr.Role,usr.Email);

        }

        public IList<UserDto> ListAllUsers()
        {
            return _repository.ListAllUsers().Select(a => new UserDto(a.Id , a.FirstName,a.LastName,a.PhoneNumber,a.Role,a.Email)).ToList();
        }
    }
}