using Microsoft.EntityFrameworkCore;
using Web_App.Server.Models;

namespace Web_App.Server.Data
{    
    public class QuizContext : DbContext
    {
        public DbSet<QuestionModel> Questions { get; set; }
        public DbSet<QuestionCardModel> QuestionCards { get; set; }
        public DbSet<MCSACardModel> MCSACards { get; set; }

        public QuizContext(DbContextOptions<QuizContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QuestionModel>()
                .HasKey(q => q.QuestionId);

            modelBuilder.Entity<QuestionModel>()
                .ToTable("Questions");

            //Configurations for QuestionCard
            modelBuilder.Entity<QuestionCardModel>()
                .ToTable("QuestionCard");

            //Configurations for MCSACard
            modelBuilder.Entity<MCSACardModel>()
                .ToTable("MCSACard");

            base.OnModelCreating(modelBuilder);
        }
    }
}
