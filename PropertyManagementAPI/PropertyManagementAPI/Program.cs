using Microsoft.EntityFrameworkCore;
using Npgsql;
using PropertyManagement.DataAccess.Data;
using PropertyManagement.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Extract connection details from the URL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// If the connection string starts with "postgres://", parse it properly
if (connectionString.StartsWith("postgres://"))
{
    // Parse the connection string components
    var uri = new Uri(connectionString);
    var userInfo = uri.UserInfo.Split(':');

    // Build a proper Npgsql connection string
    var npgsqlBuilder = new NpgsqlConnectionStringBuilder
    {
        Host = uri.Host,
        Port = uri.Port,
        Database = uri.AbsolutePath.TrimStart('/'),
        Username = userInfo[0],
        Password = userInfo[1],
        SslMode = SslMode.Require, // Railway likely requires SSL
        TrustServerCertificate = true
    };

    connectionString = npgsqlBuilder.ToString();
}

// Register the DbContext with the properly formatted connection string
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// Add this to your existing service registrations
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
builder.Services.AddScoped<IUnitRepository, UnitRepository>();
builder.Services.AddScoped<ITenantRepository, TenantRepository>();
builder.Services.AddScoped<ILeaseRepository, LeaseRepository>();
// Add others as needed

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
