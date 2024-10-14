using Microsoft.EntityFrameworkCore;
using Core.Entities; 

namespace Infrastructure.Data
{
    public class ReportDbContext : DbContext
    {
        public ReportDbContext(DbContextOptions<ReportDbContext> options) : base(options)
        {
        }

        public DbSet<Report> Reports { get; set; }
        public DbSet<ReportDetails> ReportDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ReportDetails>()
                .HasOne(rd => rd.Report)
                .WithMany(r => r.ReportDetails)
                .HasForeignKey(rd => rd.ReportId);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
              
                optionsBuilder.UseNpgsql("Host=localhost;Database=reportdb;Username=umut.civrizoglu;Password=istanbul123;SSL Mode=Disable");
            }
        }
    }
}
