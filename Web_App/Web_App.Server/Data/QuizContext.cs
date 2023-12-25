using Microsoft.EntityFrameworkCore;
using Web_App.Server.Models;

namespace Web_App.Server.Data
{    
    public class QuizContext : DbContext
    {
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionCard> QuestionCards { get; set; }
        public DbSet<MCSACard> MCSACards { get; set; }

        public QuizContext(DbContextOptions<QuizContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>()
                .HasKey(q => q.QuestionId);

            modelBuilder.Entity<Question>()
                .ToTable("Questions");

            //Configurations for QuestionCard
            modelBuilder.Entity<QuestionCard>()
                .ToTable("QuestionCard");

            //Configurations for MCSACard
            modelBuilder.Entity<MCSACard>()
                .ToTable("MCSACard");

            base.OnModelCreating(modelBuilder);
        }
    }
}
