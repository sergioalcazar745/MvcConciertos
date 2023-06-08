using Amazon.S3;
using CallApi.Helpers;
using MvcConciertos.Helpers;
using MvcConciertos.Models;
using MvcConciertos.Services;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<ServiceConciertos>();
builder.Services.AddTransient<ServiceStorageS3>();
builder.Services.AddAWSService<IAmazonS3>();
builder.Services.AddTransient<HelperCallApi>();
string response = HelperSecretManager.GetSecret("secreto").Result;
Secret secret = JsonConvert.DeserializeObject<Secret>(response);
builder.Services.AddSingleton<Secret>(x => secret);
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Conciertos}/{action=Index}/{id?}");

app.Run();
