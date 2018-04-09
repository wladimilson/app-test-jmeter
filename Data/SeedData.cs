using System;
using System.Linq;
using System.Threading.Tasks;
using app_test_jmeter.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace app_test_jmeter.Data
{
    public class SeedData: ISeedData
    {
        private const string _adminRoleName = "admin";
        private string _adminEmail;
        private string _adminPassword;

        private string[] _defaultRoles = new string[] { _adminRoleName, "user" };

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

            _adminEmail = config.GetValue<string>("Admin:Email");
            _adminPassword = config.GetValue<string>("Admin:Password");
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
            var adminUsers = await _userManager.GetUsersInRoleAsync(_adminRoleName);

            if (!adminUsers.Any())
            {
                var adminUser = new ApplicationUser()
                {
                    Id = Guid.NewGuid(),
                    Email = _adminEmail,
                    UserName = _adminEmail
                };

                var result = await _userManager.CreateAsync(adminUser, _adminPassword);
                await _userManager.AddToRoleAsync(adminUser, _adminRoleName);
            }
        }
    }
}