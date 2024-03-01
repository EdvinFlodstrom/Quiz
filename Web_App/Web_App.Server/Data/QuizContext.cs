using Microsoft.EntityFrameworkCore;
using Web_App.Server.Models;

namespace Web_App.Server.Data
{
    public class QuizContext(DbContextOptions<QuizContext> options) : DbContext(options)
    {
        public DbSet<QuestionModel> Questions { get; set; }
        public DbSet<QuestionCardModel> QuestionCards { get; set; }
        public DbSet<MCSACardModel> MCSACards { get; set; }
        public DbSet<PlayerStatisticsModel> PlayerStatistics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QuestionModel>()
                .ToTable("Questions")
                .HasKey(q => q.QuestionId);

            //Configurations for QuestionCard
            modelBuilder.Entity<QuestionCardModel>()
                .ToTable("QuestionCard");

            //Configurations for MCSACard
            modelBuilder.Entity<MCSACardModel>()
                .ToTable("MCSACard");

            //Configurations for PlayerStatistics
            modelBuilder.Entity<PlayerStatisticsModel>()
                .ToTable("PlayerStatistics")
                .HasKey(p => p.PlayerName);

            base.OnModelCreating(modelBuilder);
        }
    }
}
