using EfBooksDataAccess;
using EfWebApp.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
  
// Add services to the container.

string connection = builder.Configuration.GetConnectionString("Books")!;

// builder.Services.AddAuthorization();
// builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BooksContext>(options =>
{
    options.UseSqlServer(connection, b => b.MigrationsAssembly("EfBooksDataAccess"));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

StartupInfo? startupInfo = null;
builder.Services.AddSingleton<Func<StartupInfo>>(() => startupInfo);

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

try
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider
            .GetRequiredService<BooksContext>();
        Console.WriteLine("--> Executing migrations");
        dbContext.Database.Migrate();
        Console.WriteLine("--> Database updated");
    }
    startupInfo = StartupInfo.Ok();
    // builder.Services.AddSingleton(StartupInfo.Ok());
}
catch (Exception e)
{
    Console.WriteLine("---->>> Updating database failed");
    startupInfo = StartupInfo.Error(e.Message);
    // builder.Services.AddSingleton(StartupInfo.Error(e.Message));
    Console.WriteLine(e);
    
}


app.Run();

