using MediatR;
using Microsoft.EntityFrameworkCore;
//TODO remove QuizLibrary - integrate it completely into the API
//TODO make sure that the API ALWAYS starts before e.g. the console app. If API doesn't have time to start, console app will break.
using System.Reflection;
using Web_App.Server.Data;
using Web_App.Server.Services;

namespace Web_App.Server;

public class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        services.AddAutoMapper(typeof(MappingProfile));

        services.AddHttpClient("ApiClient", client =>
        {
            client.BaseAddress = new Uri("https://localhost:7140/api/quiz");
        });

        services.AddScoped<QuizService>();

        services.AddDbContext<QuizContext>(options =>
        {
            options.UseSqlServer(Environment.GetEnvironmentVariable("QuizConnection"));
        });

        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder
                    .WithOrigins("http://localhost:5500", "http://localhost:3000")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseDefaultFiles();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseCors("CorsPolicy");

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapFallbackToFile("/index.html");
        });
    }
}