using api.Data;
using api.Data.Repositories;
using api.Mappings;
using api.Services;
// using api.Model;
// using api.Controller;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

public class Startup
{
    public IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<DataContext>(sp =>
        {
            var entityMaps = new List<IEntityMap>
            {
                new RolesMap(),
                new UserMap()
            };
 
            var connection = new DataContext("Server=localhost;Port=3306;Uid=root;Pwd=user123", entityMaps);
            connection.OnModelCreating();

            return connection;
        });

        services.TryAddScoped<IUserRepository, UserRepository>();
        services.TryAddScoped<IUserService, UserService>();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Store", Version = "v1" });
        });

        services.AddControllers();

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();
        app.UseSwagger();

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nome da Sua API V1");
        });

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}
