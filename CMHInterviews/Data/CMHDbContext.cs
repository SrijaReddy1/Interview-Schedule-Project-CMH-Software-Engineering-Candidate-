using Microsoft.EntityFrameworkCore;
using CMHInterviews.Models;
using CMHDbContext.Models;


//// Defining the namespace for the data-related classes
namespace CMHDbContext.Data
{
    // Defining the CMHDbContext class, which inherits from DbContext
    public class CMHDbContext : DbContext
    {
        // Constructor for CMHDbContext class, accepting DbContextOptions as parameter
        public CMHDbContext(DbContextOptions options) : base(options)
        {

        }
        // DbSet property for the Candidate entity, representing the Candidates table in the database

        public DbSet<Candidate> Candidates { get; set; }
    }
}





