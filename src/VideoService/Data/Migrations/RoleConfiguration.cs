using System.Data.Entity.Migrations;
using VideoService.Data;
using VideoService.Data.Model;
using VideoService.Features.Users;

namespace VideoService.Migrations
{
    public class RoleConfiguration
    {
        public static void Seed(VideoServiceContext context) {

            context.Roles.AddOrUpdate(x => x.Name, new Role()
            {
                Name = Roles.SYSTEM
            });

            context.Roles.AddOrUpdate(x => x.Name, new Role()
            {
                Name = Roles.ACCOUNT_HOLDER
            });

            context.Roles.AddOrUpdate(x => x.Name, new Role()
            {
                Name = Roles.DEVELOPMENT
            });

            context.SaveChanges();
        }
    }
}
