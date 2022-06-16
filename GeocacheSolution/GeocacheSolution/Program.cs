using GeocacheSolution.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<GeocacheContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
    {
        Title = "Geocache Test API",
        Version = "v1"
    });
});

var app = builder.Build();

static void CreateDbIfNoneExists(IHost app)
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<GeocacheContext>();
            DbInitializer.Initialize(context);
        }
        catch (Exception e)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(e, "An Error occured creating the DB");
        }
    }
}

CreateDbIfNoneExists(app);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSwagger(options =>
{
    options.RouteTemplate = "/api/{documentName}/swagger.json";
});

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/api/v1/swagger.json", "Geocache API Testing Visual");
    options.RoutePrefix = "";
});

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("Welcome to Geocache Test API!");
    });
});

app.Run();
