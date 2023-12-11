using Calculator.Cache.Logic;
using Calculator.Context;
using Calculator.Dal;
using Calculator.Logic;
using Calculator.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddDbContext<CalculatorContext>(opts => opts.UseSqlServer(Configuration["ConnectionString:EmployeeDB"]));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<CalculatorLogic>();
builder.Services.AddSingleton<DalLayer>();
builder.Services.AddSingleton<CacheLogic>();


var app = builder.Build();

app.UseCors("AllowAll");
app.UseCors(x => x.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true).AllowCredentials());



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
