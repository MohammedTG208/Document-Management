using Asp.Versioning.ApiExplorer;
using DocumentManagementAPI.ExceptionHandling;
using DocumentManagementAPI.Extensions.Service;
using Microsoft.AspNetCore.Diagnostics;
using DocumentManagement.Data;
using Serilog;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    //This for update one Field from 
    .AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//inject the services and dbcontext from this method.
builder.Services.AddIdentityServices(builder.Configuration);

//This for Display and write the logger at console.
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();
builder.Host.UseSerilog();

//DataSeeder.AddNewUser();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.AllowAnyOrigin() 
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();
var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(build =>
    {
        var descriptions=app.DescribeApiVersions();
        foreach (var desc in descriptions)
        {
            build.SwaggerEndpoint(
                $"/swagger/{desc.GroupName}/swagger.json",
                desc.GroupName.ToUpperInvariant());
        }
    });
}





app.UseHttpsRedirection();
app.UseCors("AllowAngularApp");
app.UseRouting();
app.UseMiddleware<ExceptionHandlingMiddleWare>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
