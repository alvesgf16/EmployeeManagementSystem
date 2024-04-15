using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;
using System.Collections.ObjectModel;

namespace EmployeeManagementSystem
{
    public partial class DashboardView : ContentPage
    {
        public double Payroll { get; set; }
        public EmployeeService employeeManager = new();
        public DashboardView()
        {
            InitializeComponent();
        }

        public DashboardView(User user)
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Retrieve all employees from the database
            List<Employee> employees = employeeManager.GetAllEmployees();
            ObservableCollection<Employee> employeeCollection = new ObservableCollection<Employee>(employees);
            EmployeeListView.ItemsSource = employeeCollection;
            // Calculate the total payroll
            foreach (Employee employee in employees)
            {
                Payment payment = employeeManager.GetEmployeePay(employee.Id);
                if(payment is null)
                {
                    continue;
                }
                Payroll += (payment.Salary * payment.TotalHours);
            }
            payroll_display.Text = Payroll.ToString();
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