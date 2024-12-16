using BookApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Repositories
{
    internal interface IUserRepository
    {
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int userid);

        User AuthenticateUser(string email , string password);

        User GetUserDetail(int userid);


    }
}
