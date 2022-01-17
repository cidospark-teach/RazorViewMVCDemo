using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using RazorViewMVCDemo.Models;

namespace RazorViewMVCDemo.Data
{
    public class Seeder
    {
        private readonly AppDbContext _ctx;
        private readonly UserManager<User> _userMgr;
        private readonly RoleManager<IdentityRole> _roleMgr;

        public Seeder(AppDbContext appDbContext, UserManager<User> userManger, RoleManager<IdentityRole> roleManager)
        {
            _ctx = appDbContext;
            _userMgr = userManger;
            _roleMgr = roleManager;
        }

        public async Task SeedMe()
        {
            _ctx.Database.EnsureCreated();

            try
            {
                var roles = new string[] { "Regular", "Admin" };
                if (!_roleMgr.Roles.Any())
                {
                    foreach(var role in roles)
                    {
                        await _roleMgr.CreateAsync(new IdentityRole(role));
                    }
                }

                var path = "./Data/Seeds.json";

                var data = System.IO.File.ReadAllText(path);
                var readData = JsonConvert.DeserializeObject<List<User>>(data);

                if (!_userMgr.Users.Any())
                {
                    foreach(var item in readData)
                    {
                        item.UserName = item.Email;

                        var identityResult = await _userMgr.CreateAsync(item, "P@ssw0rd");

                        var counter = 0;
                        if (identityResult.Succeeded)
                        {
                            // TODO: Generate email confirmation token here and
                            // TODO: Confirm users

                            var role = roles[1];
                            if (counter > 0)
                                role = roles[0];

                            // add roles
                            await _userMgr.AddToRoleAsync(item, role);

                            counter++;
                        }

                    }
                }

            }
            catch
            {

            }
        }
    }
}
