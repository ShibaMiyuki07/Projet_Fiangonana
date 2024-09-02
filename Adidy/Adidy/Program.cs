using Adidy.Extensions;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
// Add services to the container.
builder.Services.AddControllersWithViews();

//Inject the dependency
builder.Services.ServiceInjection(config);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".Fiangonana.Session";
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

var env = builder.Environment;

//Set in the configuration used
var appsettings = $"appsettings.{env.EnvironmentName}.json";
config.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(appsettings);

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
