using EnergyTubo.Interface;
using EnergyTubo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TurboDbConnection")));

// Add services to the container.

//builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddHttpClient();

builder.Services.AddScoped<IStateService, StateService>();
builder.Services.AddScoped<ILgaService, LgaService>();
builder.Services.AddScoped<IBankService, BankService>();


builder.Services.AddIdentity<ApplicationUser,IdentityRole>()
.AddEntityFrameworkStores<AppDBContext>()
.AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 8;
    options.User.RequireUniqueEmail = true;
});



builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
//var connectionString = $"Data Source={dbHost};Initial Catalog={dbName};Trusted_Connection=True;Encrypt=false;";
var connectionString = $"Data Source={dbHost};Initial Catalog={dbName};User ID=sa;Password=MyVeryStrongPassword1#2*3!;Encrypt=false;Trustservercertificate=true;";
builder.Services.AddDbContext<AppDBContext>(opt => opt.UseSqlServer((connectionString),
    sqlServerOptions => sqlServerOptions.EnableRetryOnFailure()));

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseSession();


app.MapControllers();

app.Run();
