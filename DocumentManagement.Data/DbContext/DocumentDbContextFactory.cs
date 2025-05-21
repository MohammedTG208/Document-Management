using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;

namespace DocumentManagement.Data.Models
{
    public class DocumentDbContextFactory() : IDesignTimeDbContextFactory<DocumentDbContext>
    {
        public DocumentDbContext CreateDbContext(string[] args)
        {
            
            var optionsBuilder = new DbContextOptionsBuilder<DocumentDbContext>();
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=DocumentManagement;Trusted_Connection=True;")
                .LogTo(Console.WriteLine, [DbLoggerCategory.Database.Command.Name])
                .EnableSensitiveDataLogging();
            }
            return new DocumentDbContext(optionsBuilder.Options);
        }
    }
}
