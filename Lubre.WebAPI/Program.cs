using AutoMapper;
using Lubre.Abstractions;
using Lubre.Application;
using Lubre.DataAccess;
using Lubre.Repository;
using Lubre.WebAPI.Mapping;
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
//builder.Services.AddScoped(typeof(IApplication<>), typeof(Lubre.Application<>)); 
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IDbContext<>), typeof(DbContext<>));
builder.Services.AddScoped(typeof(IApplication<>), typeof(Application<>));

//mapper
var mapperConfig = new MapperConfiguration(mapperConfig =>
{
    mapperConfig.AddProfile(new MappingProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);


var app = builder.Build();

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
