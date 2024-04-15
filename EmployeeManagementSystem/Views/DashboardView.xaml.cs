using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;
using System.Collections.ObjectModel;

namespace EmployeeManagementSystem
{
    public partial class DashboardView : ContentPage
    {
        public DashboardView()
        {
            InitializeComponent();
            PopulateEmployeeCollection();
        }

        public DashboardView(User user)
        {
            InitializeComponent();
            PopulateEmployeeCollection();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            // Navigate to ManageEmployeeView.xaml
            AppShell.Current.GoToAsync(nameof(ManageEmployeeView));
        }




        private async void EmployeeListView_ItemSelected(object sender, EventArgs e)
        {
            if (EmployeeListView.SelectedItem != null)
            {
                await Shell.Current.GoToAsync($"{nameof(ManageEmployeeView)}" + $"?EmpID={((Employee)EmployeeListView.SelectedItem).Id}");
            }
            
        }
    }
}