using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using SecretsShare.Managers.Managers;
using SecretsShare.Managers.ManagersInterfaces;
using SecretsShare.Profiles;
using SecretsShare.Repositories.Interfaces;
using SecretsShare.Repositories.Repositories;

namespace SecretsShare
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
            services.AddDbContext<DataContext>(opt => 
                opt.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IFilesManager, FilesManager>();
            services.AddScoped<IFilesRepository, FilesRepository>();
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile<UserProfile>();
                mc.AddProfile<FileProfile>();
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddControllers();
            services.AddSwaggerGen(c => 
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "SecretsShare",
                    Version = "v1"
                }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            //app.UseStaticFiles();

            app.UseRouting();
            app.UseMiddleware<FileDownload>();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
