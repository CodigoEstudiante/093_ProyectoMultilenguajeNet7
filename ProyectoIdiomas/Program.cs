
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddViewLocalization();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

const string CulturaPorDefecto = "es";
var CulturasSoportadas = new[] {
    new CultureInfo(CulturaPorDefecto),
    new CultureInfo("en-us"),
    new CultureInfo("ja")
};

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture(CulturaPorDefecto);
    options.SupportedCultures = CulturasSoportadas;
    options.SupportedUICultures = CulturasSoportadas;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
