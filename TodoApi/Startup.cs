using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using TodoApi.Models;

namespace TodoApi
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
            services.AddDbContext<TodoContext>(opt=>opt.UseInMemoryDatabase("TodoList"));
            services.AddMvc();
            //Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c=> {
                c.SwaggerDoc("v1", new Info {
                    Title = "TodoApi",
                    Version = "v1",
                    Description="A simple .net core v2.0 Todo web api with CRUD",
                    TermsOfService="None",
                    Contact=new Contact { Name="Abel Masila",Email="abelmasila@gmail.com", Url= "https://abel-masila.github.io/" },
                    License = new License { Name = "Use under GPLv3 ", Url = "https://choosealicense.com/licenses/gpl-3.0/" }
                });
                // Set the comments path for the Swagger JSON and UI.
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "TodoApi.xml");
                c.IncludeXmlComments(xmlPath);
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c=> {
                c.SwaggerEndpoint("/swagger/v1/swagger.json","TodoApi V1");
            });
            app.UseMvc();
        }
    }
}
