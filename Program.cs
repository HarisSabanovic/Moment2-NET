var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(); // aktiverar MVC


//används för att session ska fungera
builder.Services.AddDistributedMemoryCache();

//används också för att session ska fungera
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(2);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

//används också för att session ska fungera
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=index}/{id?}"
);

app.Run();
