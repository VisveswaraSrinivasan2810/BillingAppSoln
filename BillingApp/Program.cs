using BillApp.Models.Data;
using BillApp.Services.Repositories;
using BillApp.Services.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Database
builder.Services.AddDbContext<BillAppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
});
#endregion
#region Dependency Injection
builder.Services.AddTransient(typeof(IGenericRepository<>),typeof(GenericService<>));
builder.Services.AddTransient<ICustomerRepository,CustomerService>();
builder.Services.AddTransient<IProductRepository,ProductService>(); ;
builder.Services.AddTransient<IInvoiceRepository,InvoiceService>();
#endregion

var app = builder.Build();

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
