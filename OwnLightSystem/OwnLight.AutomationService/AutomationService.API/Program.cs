using AutomationService.API;
using AutomationService.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAPIServices(builder.Configuration);

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("CorsPolicy");
app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Automation Service API v1"));

app.MapControllers();

app.UseMiddleware<JwtMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();
