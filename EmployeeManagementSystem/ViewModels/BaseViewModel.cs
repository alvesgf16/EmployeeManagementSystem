using CommunityToolkit.Mvvm.ComponentModel;

namespace EmployeeManagementSystem.ViewModels;

public abstract partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _isBusy;

    [ObservableProperty]
    private string? _title;
}
