using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TranspotationAPI.Config;
using TranspotationAPI.DbContexts;
using TranspotationAPI.Repositories;
using TranspotationWebAPI.Model.Dto;
using TranspotationWebAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add automapper
builder.Services.AddAutoMapper(typeof(MappingConfig).Assembly);
// Add Services and Repositories
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IDepartRepository, DepartRepository>();
// Add Authorentication to the project
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});




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

//app.MapPost("/login",
//    (UserLoginResDto user, IAccountRepository service) => Login(user, service));

//IResult Login(UserLoginResDto user, IAccountRepository service)
//{
//    if(!string.IsNullOrEmpty(user.userName) && !string.IsNullOrEmpty(user.password))
//    {
//        var loggedInUser = service.GetUserByEmailAndPassword(user);
//        if (loggedInUser is null) return Results.NotFound("User Not Found!");

//        var token = new JwtSecurityToken
//        (
//            issuer: builder.Configuration["Jwt:Issuer"],
//            audience: builder.Configuration["Jwt:Audience"],
//            //add claim here
//            expires: DateTime.UtcNow.AddDays(60),
//            notBefore: DateTime.UtcNow,
//            signingCredentials: new SigningCredentials(
//                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
//                SecurityAlgorithms.HmacSha256)
//        );        
//    }
//}



app.UseCors("corspolicy");

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
