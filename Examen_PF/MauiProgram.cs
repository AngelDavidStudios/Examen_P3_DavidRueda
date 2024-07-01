using Examen_PF.Data;
using Examen_PF.MVVM.ViewModels;
using Examen_PF.MVVM.Views;
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
        
        builder.Services.AddTransient<VMJokeDbList>();
        builder.Services.AddTransient<VJokeDbList>();
        
        builder.Services.AddTransient<VMJokeDbAdd>();
        builder.Services.AddTransient<VJokeDbAdd>();
        
        builder.Services.AddTransient<VMJokeDbEdit>();
        builder.Services.AddTransient<VJokeDbEdit>();
        
        Routing.RegisterRoute(nameof(VJokeDbEdit), typeof(VJokeDbEdit));
        Routing.RegisterRoute(nameof(VJokeDbAdd), typeof(VJokeDbAdd));
        

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}