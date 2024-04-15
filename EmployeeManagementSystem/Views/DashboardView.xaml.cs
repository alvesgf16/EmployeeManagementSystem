using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;
using System.Collections.ObjectModel;

namespace EmployeeManagementSystem.Views

{
    public partial class DashboardView : ContentPage
    {
        public ObservableCollection<Employee> EmployeeCollection { get; set; } = new ObservableCollection<Employee>();
        public EmployeeService EmployeeManager = new();
        public DashboardView()
        {
            InitializeComponent();
        }
        public DashboardView(Employee employee)
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            PopulateEmployeeCollection();
            PopulatePayrollDisplay();
            CalculateSickDaysTakenYTD();
        }
       

        public void PopulateEmployeeCollection()
        {
            EmployeeManager.GetAllEmployees().ForEach(EmployeeCollection.Add);
            EmployeeListView.ItemsSource = EmployeeCollection;
        }
        public void PopulatePayrollDisplay()
        {
            double totalPayroll = PayrollService.CalculatePayroll();
            payroll_display.Text = $"${totalPayroll}";
        }
        public void CalculateSickDaysTakenYTD() 
        {
            int availableSickDays = 0;
            foreach (var employee in EmployeeCollection)
            {
                availableSickDays += employee.AvailableSickDays;
            }
            int sickDaysTakenYTD = (EmployeeCollection.Count() * 10) - availableSickDays;
            SickDaysYTD.Text = $"{sickDaysTakenYTD}";
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