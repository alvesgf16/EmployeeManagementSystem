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
            builder.Services.AddSingleton<ManagerDashboardView>();
            builder.Services.AddSingleton<ManagerEmployeeDetailsView>();
            builder.Services.AddSingleton<ManagerEmployeePayView>();
            builder.Services.AddSingleton<ManagerScheduleView>();
            builder.Services.AddSingleton<ManagerWeekDaysSelectionView>();
            builder.Services.AddSingleton<EmployeeDetailsView>();

            //View Models
            builder.Services.AddSingleton<LoadingViewModel>();
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<DashboardViewModel>();
            builder.Services.AddSingleton<EmployeeDetailsViewModel>();
            builder.Services.AddSingleton<EmployeePayViewModel>();
            builder.Services.AddSingleton<ScheduleViewModel>();
            builder.Services.AddSingleton<WeekDaysSelectionViewModel>();

            return builder.Build();
        }
    }
}
