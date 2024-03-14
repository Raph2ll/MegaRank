using api.Data;
using api.Mappings;
// using api.Model;
// using api.Controller;
// using api.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
        services.AddScoped<IEntityMap, UserMap>();
        services.AddScoped<IEntityMap, RolesMap>();

        services.AddSingleton<DataContext>(sp =>
        {
            var entityMaps = sp.GetServices<IEntityMap>();
            var connection = new DataContext("sua-string-de-conexao", entityMaps);

            connection.OnModelCreating();

            return connection;
        });
        // services.AddSingleton<IProductRepository, ProductRepository>();
        // services.AddSwaggerGen(c =>
        // {
        //     c.SwaggerDoc("v1", new OpenApiInfo { Title = "Store", Version = "v1" });
        // });
        // services.AddSingleton<IProductService, ProductService>();

        // services.AddControllers(options =>
        // {
        // });
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
