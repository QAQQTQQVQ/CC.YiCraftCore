using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CC.YiCraftCore.Models;
using CC.YiCraftCore.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CC.YiCraftCore
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
         
              string server= Configuration["database:server"];
              string db=  Configuration["database:db"];
              string user=  Configuration["database:user"];
            string pwd=  Configuration["database:pwd"];
              var connection = "server=" + server + ";Database=" + db + ";UId=" + user + ";PWD=" + pwd + ";";
            services.AddDbContext<YIContext>(options =>
            {
                options.UseSqlServer(connection);
            });
            services.AddScoped<IUserInfoRepository, UserInfoRepository>();
            services.AddScoped<INoticeInfoRepository, NoticeInfoRepository>();
            services.AddScoped<ICommentInfoRepository, CommentInfoRepository>();
            services.AddScoped<IJurisdictionHelper, JurisdictionHelper>();
            services.AddScoped<IQuestionInfoRepository, QuestionInfoRepository>();
              services.AddSession();
            services.AddHttpContextAccessor();
            services.AddControllersWithViews();


            //services.Configure<CookiePolicyOptions>(options =>

            //{

            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.

            //    options.CheckConsentNeeded = context => false;
            //    option.ExpireTimeSpan = new TimeSpan(1, 0, 0,);
            //    options.MinimumSameSitePolicy = SameSiteMode.None;


            //});
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, option =>
                {
                    option.LoginPath = new PathString("/Login"); //设置登陆失败或者未登录授权的情况下，直接跳转的路径这里
                    //设置cookie只读情况
                    option.Cookie.HttpOnly = true;
                    //cookie过期时间
                    //option.Cookie.Expiration = TimeSpan.FromSeconds(10);//此属性已经过期忽略，使用下面的设置
                    option.ExpireTimeSpan = new TimeSpan(1, 0, 0);//默认14天
                });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
  
            app.UseSession();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Login");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=ActionHome}/{id?}");
            });
          
        }
    }
}
