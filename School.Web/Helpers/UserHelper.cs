using Microsoft.AspNetCore.Identity;
using School.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Web.Helpers
{
    
    //Esta classe cria uma camada intermediária de forma a termos mais controle na manipulação dos usuários.
    //Por ser uma classe criada por nós e não já inserida no asp.netcore, temos de injecta-la no startup
    
    public class UserHelper : IUserHelper 
    {
        private readonly UserManager<User> _userManager;

        public UserHelper(UserManager<User> userManager)
        {
            _userManager = userManager;
        }



        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }




        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
    }
}
