using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Review.DAL;

var builder = WebApplication.CreateBuilder( args );

// Add services to the container.
builder.Services.AddControllers();

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add the database context for in-memory database
builder.Services.AddDbContext<CarManagerDbContext>( options =>
    options.UseInMemoryDatabase( databaseName: "CarManager" ) );

// Add any additional services here
// builder.Services.AddScoped<ISomeService, SomeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if( app.Environment.IsDevelopment() ) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Ensure the database is created (in-memory database does not require migration)
using( var scope = app.Services.CreateScope() ) {
    var dbContext = scope.ServiceProvider.GetRequiredService<CarManagerDbContext>();
    dbContext.Database.EnsureCreated();
}

app.Run();
