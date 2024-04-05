using System.ComponentModel.DataAnnotations;
// Defining the namespace for the models related to CMHDbContext
namespace CMHDbContext.Models
{
    // Defining the Candidate class
    public class Candidate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfInterview { get; set; }

    }
}
