using JwtBearerAspNet.Models;
using JwtBearerAspNet.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<TokenService>();

var app = builder.Build();

var userCurrent = new User(
  1,
  "teste@gmail.com",
  "123",
  new[] { "student", "premium" }
);

app.MapGet("/", ([FromServices] TokenService service) => service.Generete(userCurrent));

app.Run();
