using Case.Repositry;
using Case.Services;
using Case.web.Factories.UserFactory;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace CaseManagment
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
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
            {
                option.LoginPath = "/User/Login";
                option.Cookie.Name = "UserCookies";
                option.AccessDeniedPath = "/User/Login";
            });
            services.AddMvc();
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICaseService, CaseService>();
            services.AddTransient<ICaseCategoryService, CaseCategoryService>();
            services.AddTransient<IPoliceStationService, PoliceStationService>();
            services.AddTransient<IUserRoleService, UserRoleService>();
            services.AddTransient<ICaseDocumentService, CaseDocumentService>();
            services.AddTransient<IRolesService, RolesService>();
            services.AddTransient<IUserModelFactory, UserModelFactory>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IDistirictService, DistirictService>();
            services.AddTransient<ICourtService, CourtService>();
            services.AddTransient<ITermConditionService, TermConditionService>();
            services.AddTransient<ICarouselService, CarouselService>();
            services.AddTransient<IProvinceService, ProvinceService>();
            services.AddTransient<ICityService, CityService>();

            //Admin Factories
            services.AddTransient<Case.web.Areas.Admin.Factories.IUserModelFactory, Case.web.Areas.Admin.Factories.UserModelFactory>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCookiePolicy();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
