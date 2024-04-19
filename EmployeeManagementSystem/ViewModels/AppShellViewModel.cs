using CommunityToolkit.Mvvm.Input;
using EmployeeManagementSystem.Views;

namespace EmployeeManagementSystem.ViewModels;

public partial class AppShellViewModel : BaseViewModel
{
    [RelayCommand]
    private async Task SignOut()
    {
        if (await SecureStorage.Default.GetAsync("userId") is not null)
        {
            SecureStorage.Default.Remove("userId");
        }

        await Shell.Current.GoToAsync($"//{nameof(LoginView)}");
    }
}
