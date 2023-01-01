using FoodDeliveryShop.MVC.DataManagement;
using FoodDeliveryShop.MVC.Interfaces;
using FoodDeliveryShop.MVC.Models;
using FoodDeliveryShop.MVC.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.WebHost.UseDefaultServiceProvider(options => options.ValidateScopes = false);

builder.Services.AddControllersWithViews();
builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
builder.Services.AddDbContext<FoodDeliveryShopContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("FoodDeliveryShopProducts"))
);

builder.Services.AddDbContext<AppIdentityDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("FoodDeliveryShopIdentity"));
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppIdentityDbContext>()
    .AddDefaultTokenProviders();


builder.Services.AddTransient<IOrderRepository, EFOrderRepository>();
builder.Services.AddTransient<IProductRepository, EFProductRepository>();

builder.Services.AddScoped(sp => SessionCart.GetCart(sp));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddMemoryCache();
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();
app.UseStatusCodePages();
app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute("pagination", "Products/Page{page}", new { Controller = "Product", Action = "List" });

app.UseSession();

app.UseMvc(routes =>
{
    routes.MapRoute(
    name: null,
    template: "{category}/Page{productPage:int}",
    defaults: new { controller = "Product", action = "List" });

    routes.MapRoute(
    name: null,
    template: "Page{productPage:int}",
    defaults: new
    {
        controller = "Product",
        action = "List",
        productPage = 1
    });

    routes.MapRoute(
    name: null,
    template: "{category}",
    defaults: new
    {
        controller = "Product",
        action = "List",
        productPage = 1
    });

    routes.MapRoute(
    name: null,
    template: "",
    defaults: new
    {
        controller = "Product",
        action = "List",
        productPage = 1
    });

    routes.MapRoute(
        name: null,
        template: "{controller}/{action}/{id?}");
});

SeedData.EnsurePopulated(app);
IdentitySeedData.EnsurePopulated(app);

app.Run();
