using Microsoft.EntityFrameworkCore;
using System.Globalization;
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

            if (env.IsDevelopment())
            {
                // Swagger was here
            }

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