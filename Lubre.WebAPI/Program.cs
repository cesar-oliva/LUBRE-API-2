using AutoMapper;
using Lubre.Abstractions;
using Lubre.DataAccess;
using Lubre.Repository;
using Lubre.Repository.Abstractions;
using Lubre.Repository.Mapping;
using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Lubre.API", Version = "v1" });
    //Set the comments path for the Swagger JSON an UI
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(
    options =>
        options.UseNpgsql(
            builder.Configuration.GetConnectionString("DefaultConnection"),
            x => x.MigrationsAssembly("Lubre.DataAccess")));



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

/*When an application type object is requested, it creates the instance (dependency injection).*/
builder.Services.AddScoped(typeof(IEmployeeRepository), typeof(EmployeeRepository));
builder.Services.AddScoped(typeof(IGenderRepository), typeof(GenderRepository));
builder.Services.AddScoped(typeof(IUnitRepository), typeof(UnitRepository));
builder.Services.AddScoped(typeof(IPositionRepository), typeof(PositionRepository));
builder.Services.AddScoped(typeof(IAddressRepository), typeof(AddressRepository));
builder.Services.AddScoped(typeof(ITownRepository), typeof(TownRepository));
builder.Services.AddScoped(typeof(IStateRepository), typeof(StateRepository));
builder.Services.AddScoped(typeof(ICountryRepository), typeof(CountryRepository));
//mapper
var mapperConfig = new MapperConfiguration(mapperConfig =>
{
    mapperConfig.AddProfile(new EmployeeProfile());
    mapperConfig.AddProfile(new GenderProfile());
    mapperConfig.AddProfile(new UnitProfile());
    mapperConfig.AddProfile(new PositionProfile());
    mapperConfig.AddProfile(new AddressProfile());
    mapperConfig.AddProfile(new TownProfile());
    mapperConfig.AddProfile(new StateProfile());
    mapperConfig.AddProfile(new CountryProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);


var app = builder.Build();
 // CORS
    // https://docs.asp.net/en/latest/security/cors.html
    app.UseCors(builder =>
            builder.WithOrigins("http://localhost:4200", "http://www.myclientserver.com")
                .AllowAnyHeader()
                .AllowAnyMethod());
                
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lubre.WebAPI v1");
            });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
