using MemberAdministrationLedub;
using MemberAdministrationLedubCore.Interfaces;
using MemberAdministrationLedubCore.Services;
using MemberAdministrationLedubDAL;
using MemberAdministrationLedubDAL.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using WebSocketSharp;
using Microsoft.AspNetCore.Http;
using WebSocketSharp.Server;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MemberAdministrationConnection")));

// Add SignalR services
builder.Services.AddSignalR();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
});

builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();

builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<ITeamRepository, TeamRepository>();

var app = builder.Build();

app.UseCors(c => c.WithOrigins("http://localhost:3000", "http://localhost:8080")
    .AllowAnyHeader()
    .WithMethods("GET", "POST", "PUT", "DELETE")
    .AllowCredentials());

// Configure SignalR
app.MapHub<HubClass>("/hub");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();