using BlogApi.Models;
using BlogApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Registering the BlogDatabaseSettings object with the dependency injection container, and binding the BlogDatabase settings of the appsetting.json to this instance
builder.Services.Configure<BlogDatabaseSettings>(
    builder.Configuration.GetSection("BlogDatabase"));
builder.Services.AddControllers().AddJsonOptions(
    options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

// Registering the PostsService with the dependency injection container, lifetime is singleton per mongo client guidelines
builder.Services.AddSingleton<PostsService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(builder => builder.WithOrigins("*"));

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
