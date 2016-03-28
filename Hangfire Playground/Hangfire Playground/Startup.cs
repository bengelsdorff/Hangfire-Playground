using Hangfire;
using Hangfire.Dashboard;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[assembly: OwinStartup(typeof(Hangfire_Playground.Startup))]

namespace Hangfire_Playground
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage("HangfireConnectionString");

            app.UseHangfireDashboard("/hangfire", GetDashboardOptions());
            app.UseHangfireServer();
        }

       private static DashboardOptions GetDashboardOptions()
        {
            var options = new DashboardOptions
            {
                AuthorizationFilters = new[]
                   {
                    new BasicAuthAuthorizationFilter(
                        new BasicAuthAuthorizationFilterOptions
                        {
                            // Require secure connection for dashboard
                            RequireSsl = false,
                            SslRedirect = false,
                            // Case sensitive login checking
                            LoginCaseSensitive = true,
                            // Users
                            Users = new[]
                            {
                                new BasicAuthAuthorizationUser
                                {
                                    Login = "admin",
                                    // Password as SHA1 hash (admin)
                                    Password = new byte[] { 0xd0,0x33,0xe2,0x2a,0xe3,0x48,
                                                            0xae,0xb5,0x66,0x0f,0xc2,0x14,
                                                            0x0a,0xec,0x35,0x85,0x0c,0x4d,
                                                            0xa9,0x97 }
                                },
                            }
                        })
                }
            };

            return options;

        }
    }
}
