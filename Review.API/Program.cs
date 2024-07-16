using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Review.DAL;
using Microsoft.AspNetCore.Identity;
using Review.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder( args );

// Add services to the container.
builder.Services.AddControllers();

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure authentication using JWT Bearer tokens
builder.Services.AddAuthentication( JwtBearerDefaults.AuthenticationScheme )
    .AddJwtBearer( options => {
        options.TokenValidationParameters = new TokenValidationParameters {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey( Encoding.UTF8.GetBytes( builder.Configuration["Jwt:Key"] ) )
        };
    } );

// Add authorization
builder.Services.AddAuthorization();

// Add the database context for in-memory database
builder.Services.AddDbContext<CarManagerDbContext>( options =>
    options.UseNpgsql( builder.Configuration.GetConnectionString( "CarManagerDbContext" ) ) );

// Add any additional services here
// builder.Services.AddScoped<ISomeService, SomeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if( app.Environment.IsDevelopment() ) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

// Ensure the database is created (in-memory database does not require migration)
using( var scope = app.Services.CreateScope() ) {
    var dbContext = scope.ServiceProvider.GetRequiredService<CarManagerDbContext>();
    dbContext.Database.EnsureCreated();
}

app.Run();
