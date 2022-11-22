using AgenciaOpen.Api.Helpers;
using AgenciaOpen.Common.Helpers;
using AgenciaOpen.Database;
using AgenciaOpen.Services.Auth;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.ComponentModel;
using System.Reflection;

namespace AgenciaOpen.Api
{
    public class Startup
    {
        public Startup(IHostEnvironment hostEnvironment)
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(hostEnvironment.ContentRootPath)
              .AddJsonFile("appsettings.json", true, true)
              .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
              .AddEnvironmentVariables();

            Configuration = builder.Build();
            HostEnvironment = hostEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IHostEnvironment HostEnvironment { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            var apiAssembly = typeof(Startup).GetTypeInfo().Assembly;
            DependencyInjectionHelper.AddByConvention(services, apiAssembly);

            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

            var connection = Configuration["ConnectionStrings:Sqlite"];

            services.AddDbContext<AgenciaOpenContext>(options =>
                options.UseSqlite(connection)
            );

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            // Add services to the container.
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();



            services.AddAuthentication("BasicAuthentication")
                   .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            services.AddScoped<IUserService, UserService>();

            services.AddSwaggerGen(swagger =>
            {
                swagger.EnableAnnotations(enableAnnotationsForInheritance: true, enableAnnotationsForPolymorphism: true);
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "AgenciaOpen Api V1.0",
                    Version = "v1"
                });

                //swagger.CustomSchemaIds(x => x.FullName);
                swagger.CustomSchemaIds(x => x.GetCustomAttributes<DisplayNameAttribute>()?.SingleOrDefault()?.DisplayName ?? x.FullName);

                swagger.DescribeAllParametersInCamelCase();

                var XMLPath = AppDomain.CurrentDomain.BaseDirectory + "AgenciaOpen.Api" + ".xml";
                if (File.Exists(XMLPath))
                {
                    swagger.IncludeXmlComments(XMLPath);
                }

                string basePath = AppContext.BaseDirectory;

                var files = Directory.GetFiles(string.Format("{0}/", basePath), "*.xml").ToList();

                Directory.GetFiles(string.Format("{0}/", basePath), "*.xml").ToList().ForEach(file =>
                {
                    swagger.IncludeXmlComments(file, true);
                });
                swagger.AddSecurityDefinition("basic", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    In = ParameterLocation.Header,
                    Description = "Basic Authorization header using the Bearer scheme."
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "basic"
                            }
                        },
                        new string[] {}
                    }
                });
            });


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (HostEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BasicAuth v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
