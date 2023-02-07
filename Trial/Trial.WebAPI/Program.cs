using System.Reflection;
using System.Xml.XPath;
using Trial.Core;
using Trial.Persistence;
using Trial.WebAPI.Caching;
using Trial.WebAPI.Middleware;

var builder = WebApplication.CreateBuilder();
var services = builder.Services;
var configuration = builder.Configuration;


services.AddPersistence(configuration);
services.AddCoreServices();

services.AddControllers();
services.AddCaching();
services.AddEndpointsApiExplorer();

services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpCacheHeaders();
app.UseResponseCaching();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapDefaultControllerRoute();

app.Run();
