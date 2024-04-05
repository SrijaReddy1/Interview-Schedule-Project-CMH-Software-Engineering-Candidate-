using Microsoft.EntityFrameworkCore;
using CMHInterviews.Models;
using CMHDbContextData = CMHDbContext.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//configure and register the CMHDbContext with the dependency injection container (builder.Services). It specifies that the CMHDbContext should be used as the database context for Entity Framework Core.
builder.Services.AddDbContext<CMHDbContextData.CMHDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("CandidatesDataConnectionString")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
