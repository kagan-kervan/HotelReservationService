using ConnectingApps.SmartInject;
using HotelReservationService;
using HotelReservationService.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;using Microsoft.AspNetCore.Http;
using JavaScriptEngineSwitcher.V8;
using JavaScriptEngineSwitcher.Extensions.MsDependencyInjection;
using React.AspNet;
using EntityFrameworkCore.UseRowNumberForPaging;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddReact();

// Make sure a JS engine is registered, or you will get an error!
builder.Services.AddJsEngineSwitcher(options => options.DefaultEngineName = V8JsEngine.EngineName)
  .AddV8();

builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnectionString"), o => o.UseRowNumberForPaging()));
builder.Services.AddTransient<CustomerService>();
builder.Services.AddLazyTransient<CustomerService, CustomerService>();
builder.Services.AddTransient<AddressService>();
builder.Services.AddLazyTransient<AddressService, AddressService>();
builder.Services.AddTransient<HotelService>();
builder.Services.AddLazyTransient<HotelService, HotelService>();
builder.Services.AddTransient<HotelFeaturesService>();
builder.Services.AddTransient<RoomService>();
builder.Services.AddLazyTransient<RoomService, RoomService>();
builder.Services.AddTransient<RoomTypeService>();
builder.Services.AddTransient<ReservationService>();
builder.Services.AddLazyTransient<ReservationService, ReservationService>();
builder.Services.AddTransient<OwnerService>();
builder.Services.AddLazyTransient<OwnerService, OwnerService>();
builder.Services.AddTransient<ReviewService>();
builder.Services.AddScoped<TableRelationService>();
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:44491");
    });
});
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("CorsPolicy");
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();// Initialise ReactJS.NET. Must be before static files.
app.UseReact(config =>
{
    // If you want to use server-side rendering of React components,
    // add all the necessary JavaScript files here. This includes
    // your components as well as all of their dependencies.
    // See http://reactjs.net/ for more information. Example:
    //config
    //  .AddScript("~/js/First.jsx")
    //  .AddScript("~/js/Second.jsx");
    config.SetLoadBabel(false).AddScriptWithoutTransform("~/js/bundle.server.js");
});

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
AppDBInitializer.Seed(app);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

//app.MapFallbackToFile("index.html"); ;


app.Run();
