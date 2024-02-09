using MediatR;
using Microsoft.EntityFrameworkCore;
//TODO remove QuizLibrary - integrate it completely into the API
//TODO make sure that the API ALWAYS starts before e.g. the console app. If API doesn't have time to start, console app will break.
using System.Reflection;
using Web_App.Server.Data;
using Web_App.Server.Services;

namespace Web_App.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            services.AddHttpClient("ApiClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7140/api/quiz");
            });

            services.AddScoped<QuizService>();

            services.AddDbContext<QuizContext>(options =>
            {
                options.UseSqlServer(Environment.GetEnvironmentVariable("QuizConnection"));
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {           
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("/index.html");
            });
        }
    }
}