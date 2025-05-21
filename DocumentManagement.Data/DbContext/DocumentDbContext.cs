

using Microsoft.EntityFrameworkCore;

namespace DocumentManagement.Data.Models
{
    public class DocumentDbContext(DbContextOptions options) : Microsoft.EntityFrameworkCore.DbContext(options)
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Document> Documents { get; set; }

        public DbSet<Folder> Folders { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Profile> Profiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Message>()
            .HasOne(m => m.User)
            .WithMany(u => u.Messages)
            .OnDelete(DeleteBehavior.Restrict); //  STOP the cascade on User

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Document)
                .WithMany(d => d.Messages)
                .OnDelete(DeleteBehavior.Cascade); //  keep if no conflict

            modelBuilder.Entity<Document>()
            .HasOne(d => d.Folder)
            .WithMany(f => f.Documents)
                .OnDelete(DeleteBehavior.Restrict); //  clear and single-direction


            modelBuilder.Entity<User>()
            .HasOne(u => u.profile)
            .WithOne(p => p.user)
            .HasForeignKey<Profile>(p => p.Id); // This resolves the ambiguity

            


        }




    }
}
