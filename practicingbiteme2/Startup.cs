using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using practicingbiteme2.Models;

[assembly: OwinStartupAttribute(typeof(practicingbiteme2.Startup))]
namespace practicingbiteme2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRoles();  //
        }


        // Δημιουργεί ρόλους στην βάση αν δεν υπάρχουν ήδη. Καλείται στην έναρξη του app, απο πάνω.
        private void CreateRoles()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            if (!roleManager.RoleExists("Admin"))
            {

                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

            }
            if (!roleManager.RoleExists("User"))
            {

                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "User";
                roleManager.Create(role);

            }
        }
    }
}
