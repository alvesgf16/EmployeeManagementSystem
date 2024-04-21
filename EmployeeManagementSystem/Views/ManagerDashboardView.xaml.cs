using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;
using System.Collections.ObjectModel;

namespace EmployeeManagementSystem.Views

{
    public partial class ManagerDashboardView : ContentPage
    {
        private readonly EmployeeService _employeeService = new();

        private readonly PaymentService _paymentService = new();

        public ManagerDashboardView()
        {
            InitializeComponent();
        }

        public ManagerDashboardView(Employee employee)
        {
            InitializeComponent();
        }

        public ObservableCollection<Employee> Employees { get; set; } = [];

        public ObservableCollection<string> TopEmployees { get; set; } = [];

        public ObservableCollection<string> WorstEmployees { get; set; } = [];

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Employees.Clear();
            TopEmployees.Clear();
            WorstEmployees.Clear();
            PopulateEmployeeCollection();
            PopulatePayrollDisplay();
            CalculateSickDaysTakenYTD();
            PopulateTopEmployees();
            PopulateWorstEmployees();
        }

        private void PopulateEmployeeCollection()
        {
            _employeeService.GetAllEmployees().ForEach(Employees.Add);
            EmployeeListView.ItemsSource = Employees;
        }
        private void PopulatePayrollDisplay()
        {
            double totalPayroll = _paymentService.CalculatePayroll();
            payroll_display.Text = $"${totalPayroll}";
        }
        private void CalculateSickDaysTakenYTD()
        {
            int availableSickDays = Employees.Aggregate(0, (acc, cur) => acc + cur.AvailableSickDays);
            int sickDaysTakenYTD = (Employees.Count() * 10) - availableSickDays;
            SickDaysYTD.Text = $"{sickDaysTakenYTD}";
        }

        private async void EmployeeListView_ItemSelected(object sender, EventArgs e)
        {
            if (EmployeeListView.SelectedItem is not null)
            {
                await Shell.Current.GoToAsync($"{nameof(ManagerEmployeeDetailsView)}" +
                    $"?EmpID={((Employee)EmployeeListView.SelectedItem).Id}");
            }

        }
        //method to populate the top employees list, calculates the top employees based on the performance rating
        private void PopulateTopEmployees()
        {
            var topEmployees = _paymentService.GetEmployeesIDandPR().ToList();
            int noOfTopEmployees = topEmployees.Count;
            if (noOfTopEmployees > 5)
            {
                noOfTopEmployees = 5;
            }

            //get the top 5 employees
            topEmployees = topEmployees.OrderByDescending(e => e.Value).Take(noOfTopEmployees).ToList();
            foreach (var employee in topEmployees)
            {
                string employeeID = employee.Key.ToString();
                string employeeName = _employeeService.GetEmployeeById(employee.Key).Name;
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
                await Shell.Current.GoToAsync($"{nameof(ManagerEmployeeDetailsView)}" + $"?EmpID={topEmpID}");
            }
        }

        //method to populate the worst employees list, calculates the worst employees based on the performance rating
        private void PopulateWorstEmployees()
        {
            var worstEmployees = _paymentService.GetEmployeesIDandPR().ToList();
            int noOfWorstEmployees = worstEmployees.Count;
            if (noOfWorstEmployees > 5)
            {
                noOfWorstEmployees = 5;
            }
            //get the worst 5 employees
            worstEmployees = worstEmployees.OrderBy(e => e.Value).Take(noOfWorstEmployees).ToList();
            foreach (var employee in worstEmployees)
            {
                string employeeID = employee.Key.ToString();
                string employeeName = _employeeService.GetEmployeeById(employee.Key).Name;
                string employeePerformance = employee.Value.ToString();
                WorstEmployees.Add($"ID: {employeeID} Name: {employeeName} Performance: {employeePerformance}");
            }
            WorstEmployeeListView.ItemsSource = WorstEmployees;
        }

        private async void WorstEmployeeListView_ItemSelected(object sender, EventArgs e)
        {
            string worstEmpID;
            if (WorstEmployeeListView.SelectedItem != null)
            {
                worstEmpID = WorstEmployeeListView.SelectedItem.ToString().Split(' ')[1];
                await Shell.Current.GoToAsync($"{nameof(ManagerEmployeeDetailsView)}" + $"?EmpID={worstEmpID}");
            }
        }
    }
}