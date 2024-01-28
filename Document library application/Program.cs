using Document_library_application.Repositories;
using Document_library_application.Repository;
using Document_library_application.Services;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

if (configuration != null)
{
    builder.Services.AddControllersWithViews();

    // Add the database connection string
    var connectionString = configuration.GetConnectionString("DefaultConnection");

    if (connectionString != null)
    {
        builder.Services.AddScoped<IAttachmentRepository, AttachmentRepository>(provider =>
        {
            var logger = provider.GetRequiredService<ILogger<AttachmentRepository>>();
            return new AttachmentRepository(connectionString, logger);
        });

        builder.Services.AddScoped<AttachmentService>();

        builder.Services.AddScoped<IShareAttachmentRepository, ShareAttachmnetRepository>(provider =>
        {
            var logger = provider.GetRequiredService<ILogger<ShareAttachmnetRepository>>();
            return new ShareAttachmnetRepository(connectionString, logger);
        });
        builder.Services.AddScoped<ShareAttachmentService>();

        builder.Services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.AddConsole();
            loggingBuilder.AddDebug();
        });

    }
    else
    {
        throw new Exception("Connection string 'DefaultConnection' not found in configuration.");
    }
}
else
{
    throw new Exception("Configuration object is null.");
}

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
