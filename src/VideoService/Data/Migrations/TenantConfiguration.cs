using System.Data.Entity.Migrations;
using VideoService.Data;
using VideoService.Data.Model;
using System;

namespace VideoService.Migrations
{
    public class TenantConfiguration
    {
        public static void Seed(VideoServiceContext context) {

            context.Tenants.AddOrUpdate(x => x.Name, new Tenant()
            {
                Name = "Default",
                UniqueId = new Guid("50848e1d-f3ec-486a-b25c-7f6cf1ef7c93")
            });

            context.SaveChanges();
        }
    }
}
