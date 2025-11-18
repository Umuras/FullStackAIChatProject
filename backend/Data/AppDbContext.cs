using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class AppDbContext : DbContext
    {
        //Constructor oluşturuldu, bu Constructor sayesinde, DbContext'e ConnectionString ve hangi SqlServer'ına bağlanacağımız
        //gibi bilgileri söylüyoruz.
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureUser(modelBuilder);
            ConfigureMessage(modelBuilder);
        }

        private void ConfigureUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User")
                      .HasKey(u => u.Id);

                entity.Property(u => u.UserName)
                      .IsRequired()
                      .HasMaxLength(50);
            });
        }

        private void ConfigureMessage(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Message")
                      .HasKey(m => m.Id);

                entity.Property(m => m.MessageText)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(m => m.SentimentLabel)
                      .IsRequired()
                      .HasMaxLength(20);

                entity.Property(m => m.SentimentScore)
                      .IsRequired();

                entity.HasOne(m => m.User)
                      .WithMany(u => u.Messages)
                      .HasForeignKey(m => m.UserId);
            });
        }
    }
}
