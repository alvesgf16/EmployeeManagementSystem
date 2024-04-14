using EmployeeManagementSystem.Views;

namespace EmployeeManagementSystem
{
    public partial class DashboardView : ContentPage
    {
        public DashboardView()
        {
            InitializeComponent();
        }

        // Event handler for the "Employees" button
        private void OnEmployeesButtonClicked(object sender, EventArgs e)
        {
            // Navigate to ManageEmployeeView.xaml
            AppShell.Current.GoToAsync(nameof(ManageEmployeeView));
        }

        // Event handler for the "Pay" button
        private void OnPayButtonClicked(object sender, EventArgs e)
        {
            // Navigate to ManagePayView.xaml
            AppShell.Current.GoToAsync(nameof(ManagePayView));
        }

        // Event handler for the "Schedule" button
        private void OnScheduleButtonClicked(object sender, EventArgs e)
        {
            // Navigate to ScheduleView.xaml
            AppShell.Current.GoToAsync(nameof(ScheduleView));
        }
    }
}