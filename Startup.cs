using Microsoft.AspNetCore.Authentication.Cookies;
using INTEX2.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML.OnnxRuntime;
using System.Net;
using Microsoft.Extensions.Primitives;

namespace INTEX2
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

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential 
                // cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                // requires using Microsoft.AspNetCore.Http;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //onnx stuff. Put onnx model in main directory like sqlite. 
            services.AddSingleton<InferenceSession>(
                new InferenceSession("wwwroot/final_model2.onnx"));

            // DbContext for the Crash Data  
            services.AddDbContext<CrashDbContext>(options =>
            {
                options.UseMySql(Configuration["ConnectionStrings:CrashDataDbConnection"]);
            });

            // DbContext for Identity 
            services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseMySql(Configuration["ConnectionStrings:IdentityDbConnection"]);
            });

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<AppIdentityDbContext>();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 13;
                options.Password.RequiredUniqueChars = 8;
            });

            services.AddControllersWithViews();
            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(365);
            });

            //services.AddHttpsRedirection(options =>
            //{
            //    options.RedirectStatusCode = StatusCodes.Status301MovedPermanently;
            //    options.HttpsPort = 443;
            //});

            services.AddRazorPages();
            services.AddScoped<ICrashRepository, EFCrashRepository>();
            services.AddServerSideBlazor();


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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();

            app.Use(async (ctx, next) =>
            {
                ctx.Response.Headers.Add("Content-Security-Policy",
                "default-src 'self'; style-src * 'unsafe-inline'; frame-src *; font-src *; script-src * 'unsafe-inline' 'unsafe-eval';");
                await next();
            });

            app.UseStaticFiles();
            app.UseRouting();
            app.UseMiddleware<SecurityHeadersMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCookiePolicy();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "Paging",
                    "Page{pageNum}",
                    new { Controller = "Home", action = "Temp", pageNum = 1 });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/admin/{*catchall}", "/admin/Index");
                endpoints.MapFallbackToPage("/display/{*catchall}", "/display/Index2");
            });
        }

        public sealed class SecurityHeadersMiddleware
        {
            private readonly RequestDelegate _next;

            public SecurityHeadersMiddleware(RequestDelegate next)
            {
                _next = next;
            }

            public Task Invoke(HttpContext context)
            {
                
                context.Response.Headers.Add("strict-transport-security", new StringValues("max-age = 31536000"));

                return _next(context);
            }
        }
    }
}