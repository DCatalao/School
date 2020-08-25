using Microsoft.AspNetCore.Identity;
using School.Web.Data.Entities;
using School.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        //private readonly UserManager<User> _userManager; // Classe do ASP.Net Core Identity que faz toda a manipulção dos Users que não vamos utilizar porque criamos uma classe intermediaria
        //ao qual chamamos UserHelper que ela sim vai utilizar a UserManager para manipular os usuarios.
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync(); //Verifica se existe uma database

            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Customer");
            await _userHelper.CheckRoleAsync("User");


            var user = await _userHelper.GetUserByEmailAsync("catalao.daniel@gmail.com"); //Verifica se existe este usuario na base de dados
            if(user == null) //no caso de não existir
            {
                user = new User // cria-se um novo com os dados abaixo, ele vai ser o primeiro user para podermos ter acesso
                {
                    FirstName = "Daniel",
                    LastName = "Cardozo",
                    Email = "catalao.daniel@gmail.com",
                    UserName = "catalao.daniel@gmail.com",
                    PhoneNumber = "926613583"
                };

                var result = await _userHelper.AddUserAsync(user, "123456"); // Aqui se insere o user na base de dados com os dados do user e a password dele               

                if(result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user seeder.");
                }

                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);

                var isInRole = await _userHelper.IsUserInRoleAsync(user, "Admin");
                if (!isInRole)
                {
                    await _userHelper.AddUserToRoleAsync(user, "Admin");
                }
            }

            if(!_context.Courses.Any())
            {
                this.AddCourse("CET-TPSI", user);
                this.AddCourse("CET-GRSI", user);
                this.AddCourse("CET-ARCI", user);
                this.AddCourse("CET-DPM", user);
                await _context.SaveChangesAsync();
            }
        }

        private void AddCourse(string name, User user)
        {
            _context.Courses.Add(new Course 
            {
                CourseName=name,
                Description="Por preencher",
                User = user
            });
        }
    }
}
