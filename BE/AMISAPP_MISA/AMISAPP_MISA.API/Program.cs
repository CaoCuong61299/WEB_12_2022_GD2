using AMISAPP_MISA.BL.BaseBL;
using AMISAPP_MISA.BL.ColumnTableBL;
using AMISAPP_MISA.BL.WorkingShiftBL;
using AMISAPP_MISA.DL.BaseDL;
using AMISAPP_MISA.DL.ColumnTableDL;
using AMISAPP_MISA.DL.DataContext;
using AMISAPP_MISA.DL.WorkingShiftDL;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
        }
        );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IColumnTableBL, ColumnTableBL>();
builder.Services.AddScoped<IColumnTableDL, ColumTableDL>();
builder.Services.AddScoped<IWorkingShiftBL, WorkingShiftBL>();
builder.Services.AddScoped<IWorkingShiftDL, WorkingShiftDL>();
builder.Services.AddScoped(typeof(IBaseDL<>), typeof(BaseDL<>));
builder.Services.AddScoped(typeof(IBaseBL<>), typeof(BaseBL<>));


DataBaseContext.ConnectionString = builder.Configuration.GetConnectionString("MySql");

builder.Services.AddCors(p => p.AddPolicy("corspolicy", build => //1
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));
// tắt validate mặc định 
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddControllers(
    options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(builder =>
{
    builder.WithOrigins("http://localhost:8080")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
