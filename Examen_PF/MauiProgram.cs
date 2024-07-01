using Examen_PF.Data;
using Microsoft.Extensions.Logging;

namespace Examen_PF;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        
        var DbContext = new AppDbContext();
        DbContext.Database.EnsureCreated();
        DbContext.Dispose();
        
        builder.Services.AddDbContext<AppDbContext>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}