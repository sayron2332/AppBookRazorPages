using Chapter02;
using Chapter02.Core;
using Chapter02.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

var builder = WebApplication.CreateBuilder();

builder.Services.AddRazorPages();

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});

string? connString = builder.Configuration.GetConnectionString("DefaultConnection");

//Add Coockie
builder.Services.ConfigureCoockie();


//AddRateLimiter
builder.Services.ConfigureRateLimmiter();

//Add Database
builder.Services.AddDatabase(connString!);

//Add Infrastructure Services
builder.Services.AddInfrastructureServices();
//Add pattern Repository
builder.Services.AddRepository();
//Add Core Services
builder.Services.AddCoreServices();
//Add Auto Maper
builder.Services.AddAutoMappers();
//Add Fluent Validation
builder.Services.AddValidation();
var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
  
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

