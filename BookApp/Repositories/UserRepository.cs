using BookApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private BookContext _bookContext;
        public UserRepository() { 
        
            _bookContext = new BookContext();
        }

        public void AddUser(User user)
        {
            _bookContext.User.Add(user);
            _bookContext.SaveChanges();
        }

        public User AuthenticateUser(string email, string password)
        {
            var user = _bookContext.User.SingleOrDefault(e=>e.Email == email && e.Password == password);
            return user;
        }

        public void DeleteUser(int userid)
        {
            var res = _bookContext.User.SingleOrDefault(e=>e.UserId==userid);
            _bookContext.User.Remove(res);
            _bookContext.SaveChanges();
        }

        public User GetUserDetail(int userid)
        {
            var res = _bookContext.User.SingleOrDefault(e=>e.UserId==userid);
            return res;
        }

        public void UpdateUser(User user)
        {
            var res = _bookContext.User.SingleOrDefault(e => e.UserId == user.UserId);
            if (res != null) { 
                res.Email = user.Email;
                res.Name = user.Name;
                _bookContext.SaveChanges();
            
            
            }

        }
    }
}