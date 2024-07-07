using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Wanderpets.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:4200")
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});
builder.Services.AddDbContext<RegisterDetailContext>(options =>options.UseMySql(builder.Configuration.GetConnectionString("DevConnection"),
new MySqlServerVersion(new Version(8, 0, 21))));
builder.Services.AddDbContext<PostMessagesContext>(options => options.UseMySql(builder.Configuration.GetConnectionString("DevConnection"),
new MySqlServerVersion(new Version(8, 0, 21))));
builder.Services.AddDbContext<PictureContext>(options => options.UseMySql(builder.Configuration.GetConnectionString("DevConnection"),
new MySqlServerVersion(new Version(8, 0, 21))));
builder.Services.AddDbContext<ContactDetailsContext>(options => options.UseMySql(builder.Configuration.GetConnectionString("DevConnection"),
new MySqlServerVersion(new Version(8, 0, 21))));
builder.Services.AddDbContext<ProfileContext>(options => options.UseMySql(builder.Configuration.GetConnectionString("DevConnection"),
new MySqlServerVersion(new Version(8, 0, 21))));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
        app.UseSwagger();
        app.UseSwaggerUI();
}



app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
