using UserService.API;
using UserService.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAPIServices(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "User Service API V1"));
}

app.UseRouting();
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<RefreshTokenExpirationCheckMiddleware>();

app.MapControllers();

app.Run();
