using Microsoft.EntityFrameworkCore;
using TranspotationAPI.Config;
using TranspotationAPI.DbContexts;
using TranspotationAPI.Repositories;
using TranspotationWebAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add automapper
builder.Services.AddAutoMapper(typeof(MappingConfig).Assembly);
// Add Services and Repositories
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IDepartRepository, DepartRepository>();





// Add DbContext using SQL Server Provider
var connectionString = $"Data Source=BPAKHANG;Initial Catalog=Transpotation;User ID=Hasmaga;Password=Ankhang28";
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

//Add Cors
builder.Services.AddCors(p => p.AddPolicy("corspolicy", build =>
{
    build.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
}));

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corspolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
