using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using auto.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("autoContext")
    ?? throw new InvalidOperationException("Connection string 'autoContext' not found.");


builder.Services.AddRazorPages(options =>
{
    
    options.Conventions.AuthorizeFolder("/");
});


builder.Services.AddDbContext<autoContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddDbContext<AutoIdentityContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()  
    .AddEntityFrameworkStores<AutoIdentityContext>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("AllowAll");

app.UseRouting();


app.UseAuthentication();
app.UseAuthorization(); 

app.MapRazorPages();
app.MapControllers();
app.Run();