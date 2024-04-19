using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;
using System.Collections.ObjectModel;

namespace EmployeeManagementSystem.Views

{
    public partial class DashboardView : ContentPage
    {
        public ObservableCollection<Employee> EmployeeCollection { get; set; } = [];
        public ObservableCollection<string> TopEmployees { get; set; } = [];
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
            EmployeeCollection.Clear();
            TopEmployees.Clear();
            PopulateEmployeeCollection();
            PopulatePayrollDisplay();
            CalculateSickDaysTakenYTD();
            PopulateTopEmployees();
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
        //method to populate the top employees list, calculates the top employees based on the performance rating
        public void PopulateTopEmployees()
        {
            var topEmployees = PayrollService.CalculateTopEmployees().ToList();
            int noOfTopEmployees = topEmployees.Count();
            if (noOfTopEmployees > 5)
            {
                noOfTopEmployees = 5;
            }
            //get the top 5 employees
            topEmployees = topEmployees.OrderByDescending(e => e.Value).Take(noOfTopEmployees).ToList();
            foreach (var employee in topEmployees)
            {
                string employeeID = employee.Key.ToString();
                string employeeName = EmployeeManager.GetEmployeeById(employee.Key).Name;
                string employeePerformance = employee.Value.ToString();
                TopEmployees.Add($"ID: {employeeID} Name: {employeeName} Performance: {employeePerformance}");
            }
            TopEmployeeListView.ItemsSource = TopEmployees;
        }

        private async void TopEmployeeListView_ItemSelected(object sender, EventArgs e)
        {
            string topEmpID;
            if (TopEmployeeListView.SelectedItem != null)
            {
                topEmpID = TopEmployeeListView.SelectedItem.ToString().Split(' ')[1];
                await Shell.Current.GoToAsync($"{nameof(ManageEmployeeView)}" + $"?EmpID={topEmpID}");
            }
        }
    }
}