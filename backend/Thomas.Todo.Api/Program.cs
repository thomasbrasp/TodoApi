using System.Reflection;
using Microsoft.OpenApi.Models;
using Todo.Infrastructure.Extensions;
using TodoApi.Extensions;

var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

//TODO:
//exceptionmapper
// user errorhook
//snackbar

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureModule(builder.Configuration);

// builder.Services.AddDbContext<TodoDbContext>(opt => opt.UseInMemoryDatabase("TodoList"));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMvc();

builder.Services.AddSwaggerGen(options => { options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" }); });
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type =>
            type.FullName!.Replace("Thomas.Todo.Features.", string.Empty).Replace("+", "."));
    options.SupportNonNullableReferenceTypes();
    options.UseAllOfToExtendReferenceSchemas();
    // options.SchemaFilter<SwaggerRequiredSchemaFilter>();
    // options.DocumentFilter<HealthCheckDocumentFilter>();
});

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