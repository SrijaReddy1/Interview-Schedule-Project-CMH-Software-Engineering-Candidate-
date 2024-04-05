using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using CMHDbContextData = CMHDbContext.Data;
using CMHInterviews.Models;
using CMHDbContext.Models;
using Microsoft.AspNetCore.Routing.Matching;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel;
using Microsoft.AspNetCore.Http.HttpResults;
//necessary namespaces are being imported for the controller. 
namespace CMHInterviews.Controllers
{
    //attribute is used to indicate that this controller responds to web API requests and also performs automatic model validation.
    [ApiController]
    //attribute specifies the route template for the controller.
    [Route("api/[controller]")]
    // defines the CandidateController class as it inherits from Controller, indicating that it's a controller class for handling HTTP requests. 
    public class CandidateController : Controller
    {
        //The constructor CandidateController takes an instance of CMHDbContext (aliased as CMHDbContextData.CMHDbContext) as a parameter.
        //This is the database context used for interacting with the database.
        private readonly CMHDbContextData.CMHDbContext dbContext;
        public CMHDbContextData.CMHDbContext DbContext { get; }

        public CandidateController(CMHDbContextData.CMHDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        // This method is decorated with the [HttpGet] attribute, indicating that it handles HTTP GET requests.

        //It retrieves all candidates from the database asynchronously using Entity Framework Core's ToListAsync() method and returns them as an OkObjectResult with HTTP status code 200.
        [HttpGet]
        public async Task<IActionResult> GetCandidates()
        {
            return Ok(await dbContext.Candidates.ToListAsync());
        }
        //This method is also a GET endpoint, but it takes a parameter DateOfInterview from the route.

        //It retrieves candidates from the database whose DateOfInterview matches the provided date(DateOfInterview) using Where method.
        [HttpGet("{DateOfInterview}")]
        
        public async Task<IActionResult> GetCandidates([FromRoute] DateTimeOffset DateOfInterview)
        {
            var candidates = await dbContext.Candidates
                                      .Where(c => c.DateOfInterview.Date == DateOfInterview.Date)
                                      .ToListAsync();

            if (candidates == null || candidates.Count == 0)
            {
                //If no candidates are found, it returns a NotFound response.
                return NotFound();
            }
            // Otherwise, it returns the list of candidates as an OkObjectResult.
            return Ok(candidates);

        }
        [HttpPost("count")]
        public async Task<IActionResult> CountInterviews([FromBody] DateTimeOffset dateOfInterview)
        {
            var candidates = await dbContext.Candidates.ToListAsync();//retrieves all candidates from the database asynchronously and stores them in the candidates variable.
            int count = candidates.Count(c => c.DateOfInterview.Date == dateOfInterview.Date);//calculates the number of candidates whose interview dates match the provided dateOfInterview.
            return Ok(new { numberOfInterviews = count });
        }
        //This method is a POST endpoint for adding a new candidate.

        //It takes an AddCandidateRequest object as a parameter, which contains the necessary data for creating a new candidate.

        //It creates a new Candidate object with the provided data and adds it to the Candidates DbSet in the database context.

        [HttpPost]
        public async Task<IActionResult> AddCandidate(AddCandidateRequest addCandidateRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var candidate = new Candidate
            {
               
                Name = addCandidateRequest.Name,
                DateOfInterview = addCandidateRequest.DateOfInterview,
            };
            //It saves the changes to the database asynchronously using SaveChangesAsync().

            //Finally, it returns the newly created candidate as an OkObjectResult
            await dbContext.Candidates.AddAsync(candidate);
            await dbContext.SaveChangesAsync();

            return Ok(candidate);


        }


    }
}
