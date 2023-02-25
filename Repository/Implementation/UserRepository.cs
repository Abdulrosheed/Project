using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.ApplicationContext;
using Project.Dto;
using Project.Model;
using Project.Repository.Interface;

namespace Project.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ContextClass _context;

        public UserRepository(ContextClass context)
        {
            _context = context;
        }

        public User AddUser(User model)
        {
            
            _context.Users.Add(model);
            _context.SaveChanges();
            return model;
        }

        public User GetUserByEmailAndPassword(string email , string passWord)
        {
            return _context.Users.FirstOrDefault(a => a.Email == email && a.PassWord == passWord );
        }

        public IList<User> ListAllUsers()
        {
            return _context.Users.ToList();
        }
    }
}