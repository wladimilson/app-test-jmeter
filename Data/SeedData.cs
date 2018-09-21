using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using app_test_jmeter.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace app_test_jmeter.Data
{
    public class SeedData: ISeedData
    {
        private struct User {
            public string Email { get; set; }
            public string Password { get; set; }
            public string Role { get;set; }
        }

        private List<User> Users { get; set; }

        private string[] _defaultRoles = new string[] { "admin", "user" };

        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public  static async Task Run(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var instance = serviceScope.ServiceProvider.GetRequiredService<ISeedData>();
                await instance.Initialize();
            }
        }

        public SeedData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager, IConfiguration config)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            
            Users = new List<User>();

            Users.Add(new User { 
                        Email = config.GetValue<string>("Admin:Email"), 
                        Password = config.GetValue<string>("Admin:Password"),
                        Role = "admin"
            });

            foreach(var aluno in config.GetSection("Alunos").GetChildren())
            {
                Users.Add(new User { 
                            Email = aluno.GetValue<string>("Aluno:Email"), 
                            Password = aluno.GetValue<string>("Aluno:Password"),
                            Role = "user"
                });
            }

        }

        public async Task Initialize()
        {
            await EnsureRoles();
            await EnsureDefaultUser();
        }

        public async Task EnsureRoles()
        {
            foreach (var role in _defaultRoles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole<Guid>(role));
                }
            }
        }

        public async Task EnsureDefaultUser()
        {
            foreach(var user in Users) {
                var usr = await _userManager.FindByEmailAsync(user.Email);

                if (usr == null)
                {
                    var appUsr = new ApplicationUser()
                    {
                        Id = Guid.NewGuid(),
                        Email = user.Email,
                        UserName = user.Email
                    };

                    var result = await _userManager.CreateAsync(appUsr, user.Password);
                    await _userManager.AddToRoleAsync(appUsr, user.Role);
                }
            }
        }
    }
}