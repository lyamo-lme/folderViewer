using Microsoft.EntityFrameworkCore;
using WebApplication1.Db.Repository;
using WebApplication1.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DBContext>(
    op => op.UseSqlServer(
        builder.Configuration.GetConnectionString("MsSql")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IFolderRepository, FolderRepository>();

var app = builder.Build();

app.UseExceptionHandler("/Error/Error");

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Folder}/{action=GetFolderView}/{id?}");

app.Run();