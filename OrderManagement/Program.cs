using Microsoft.EntityFrameworkCore;
using OrderManagement;
using OrderManagement.Context;
using OrderManagement.Repository.Implementation;
using OrderManagement.Repository.Interface;
using OrderManagement.Service.Implementation;
using OrderManagement.Service.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(p => p.AddPolicy("corsPolicy", build =>
{
    build.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));
builder.Services.AddDbContext<EcommerceContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("OderBlitzConnection")));

builder.Services.AddScoped<ICustomersService, CustomersService>();
builder.Services.AddScoped<IOrdersService, OrdersService>();

builder.Services.AddScoped<ICustomersRepository,CustomersRepository>();
builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corsPolicy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
