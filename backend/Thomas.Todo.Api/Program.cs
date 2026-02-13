using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Todo.Extensions;
using Todo.Infrastructure.Data;
using TodoApi;
using TodoApi.Extensions;

//Todo: mediatr
//Todo: eslint bct
//Todo: prettier bct -> format on save
//Todo: imports zoals bct


var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureModule();

builder.Services.AddDbContext<TodoDbContext>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMvc();

builder.Services.AddSwaggerGen(options => { options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" }); });

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(myAllowSpecificOrigins);

app.MapGroup("api")
        .ConfigureEndpoints(Assembly.GetExecutingAssembly());


app.Run();

// command is POST PUT DELETE
// query is GET