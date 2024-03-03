using Movies.Domain.Interfaces;
using Movies.Domain.Services;
using Movies.Repository.Interfaces;
using Movies.Repository.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region Dependency Injection
#region Repository
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
#endregion

#region
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IMovieService, MovieService>();
#endregion
#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    {
        c.DescribeAllParametersInCamelCase();
    });

builder.Services.AddOutputCache(options =>
{
    options.AddPolicy("DropdownListPolicy",basePolicy => basePolicy.Expire(TimeSpan.FromHours(1)));
});

builder.Services.AddCors();

var app = builder.Build();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000"));
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseOutputCache();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();