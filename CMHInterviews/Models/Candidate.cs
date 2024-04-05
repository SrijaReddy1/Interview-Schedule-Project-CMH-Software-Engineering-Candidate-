using System.ComponentModel.DataAnnotations;
// Defining the namespace for the models related to CMHDbContext
namespace CMHDbContext.Models
{
    // Defining the Candidate class
    public class Candidate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // Data annotation specifying the data type for formatting purposes
        [DataType(DataType.Date)]
        // Property for the date of the interview
        public DateTime DateOfInterview { get; set; }

    }
}
