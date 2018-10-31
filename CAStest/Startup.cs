using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CAStest.Data;
using CAStest.Models;
using CAStest.Services;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Hosting;

namespace CAStest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, Role>(options => {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
            //services.AddIdentity<ApplicationUser, Role>(options => {
            //    options.Password.RequireDigit = true;
            //    options.Password.RequiredLength = 14;
            //    options.Password.RequireNonAlphanumeric = true;
            //    options.Password.RequireUppercase = true;
            //    options.Password.RequireLowercase = true;
            //})
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();
            
            services.AddSingleton<IHostedService, DataRefreshService>();

            services.AddSingleton<ModelData>();
            services.AddSingleton<TrackingTermTerminationContracts>();
            services.AddSingleton<PasswordTrackingService>();

            services.AddTransient<IUserClaimsPrincipalFactory<ApplicationUser>, ClaimsPrincipalFactory>();

            //время жизни куков. По хорошему при старте проекта нужно куки чистить по тому что там хранится токен и т.д.
            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                options.ValidationInterval = TimeSpan.FromMinutes(1);
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
        
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //ModelData.Initialize(app.);
        }
        public class ClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser>
        {
            public ClaimsPrincipalFactory(UserManager<ApplicationUser> userManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
            {
            }

            public override async Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
            {
                var claims = await base.CreateAsync(user);
                claims.AddIdentity(new ClaimsIdentity(new List<Claim> { new Claim(nameof(user.Fullname), user.Fullname.ToString())}));
              

                return claims;
            }
        }
    }
}
