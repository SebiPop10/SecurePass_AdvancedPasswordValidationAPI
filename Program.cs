using Day4_Day3Refactoring.Data;
using Day4_Day3Refactoring.Middleware;
using Day4_Day3Refactoring.Models;
using Day4_Day3Refactoring.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Day4_Day3Refactoring.Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

var cs = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ValidationDbContext>(opt =>
    opt.UseMySql(cs, ServerVersion.AutoDetect(cs)));
//PasswordValidatorService  psd= new PasswordValidatorService(builder.Configuration, new ValidationRepository(new ValidationDbContext(opt => opt.UseMySql(cs, ServerVersion.AutoDetect(cs)))));
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "PasswordsAPI",
        Description = "ValidationOfPasswords.",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        { Name = "Contact Title", Email = "" }
    });
    var filePath = Path.Combine(AppContext.BaseDirectory, "Day4-Day3Refactoring.xml");
    c.IncludeXmlComments(filePath);
});

builder.Services.AddScoped<PasswordValidatorService>();
builder.Services.AddScoped<IValidationRepository, ValidationRepository>();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandling>();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();


}

app.UseHttpsRedirection();

app.UseMiddleware<RequestLoggingMiddleware>();

app.MapGet("/history", async (bool? isValid, IValidationRepository repo, HttpContext _context) =>
{
    var results=isValid.HasValue ? await repo.GetByValidity(isValid.Value) : await repo.GetAll();
    _context.Response.Headers.Add("X-Total-Count", results.Count().ToString());
     
    return results.Any()? Results.Ok(results): Results.NoContent();
});


app.MapPost("/validate",  (PasswordRequest request, PasswordValidatorService validator) =>
{
    return validator.ValidatePassword(request);
});

app.MapDelete("/history",  async (IValidationRepository repo) =>
{
    await repo.DeleteAllAsync();
    return  Results.Ok();
});



app.Run();

