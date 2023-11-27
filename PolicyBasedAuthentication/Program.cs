using Microsoft.AspNetCore.Authorization;
using PolicyBasedAuthentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddSingleton<IAuthorizationHandler, MinimumAgeHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, AdminRoleRequirementHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, CustomerRoleRequirementHandler>();
//builder.Services.AddSingleton<IAuthorizationHandler, SecretKeyRequirementHandler>();

builder.Services.AddAuthorization(options =>
{
    options.InvokeHandlersAfterFailure = false;

    options.AddPolicy("CustomerRolePolicy",
                          policy => policy
                          .AddRequirements(new CustomerRequirement("testApiKey", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJSb2xlIjoiU29jY2VyIiwiaWF0IjoxNTE2MjM5MDIyfQ.qDpR6UVqCDICLy0WNj_9JacaM5Bthl38joIEmK9yIYE")));

    options.AddPolicy("AdminRolePolicy",
                          policy => policy
                          .AddRequirements(new AdminRequirement("testApiKey", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJSb2xlIjoiQmFza2V0YmFsbCIsImlhdCI6MTUxNjIzOTAyMn0.XhN6MBzlcJJud0Ux0xQ-7jcwFGITUHsVrUs5qqIvw1g")));
});

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
