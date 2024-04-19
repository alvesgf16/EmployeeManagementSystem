using EmployeeManagementSystem.ViewModels;
using EmployeeManagementSystem.Views;
using Microsoft.Extensions.Logging;

namespace EmployeeManagementSystem
{
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

#if DEBUG
            builder.Logging.AddDebug();
#endif
            //Views
            builder.Services.AddSingleton<LoadingView>();
            builder.Services.AddSingleton<LoginView>();
            builder.Services.AddSingleton<DashboardView>();
            builder.Services.AddSingleton<ManageEmployeeView>();
            builder.Services.AddSingleton<ManagePayView>();
            builder.Services.AddSingleton<ScheduleView>();
            builder.Services.AddSingleton<WeekDaysSelectionView>();

            //View Models
            builder.Services.AddSingleton<LoadingViewModel>();
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<DashboardViewModel>();
            builder.Services.AddSingleton<ManageEmployeeViewModel>();
            builder.Services.AddSingleton<ManagePayViewModel>();
            builder.Services.AddSingleton<ScheduleViewModel>();
            builder.Services.AddSingleton<WeekDaysSelectionViewModel>();

            return builder.Build();
        }
    }
}
