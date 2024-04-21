using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Services;

public class PaymentService : BaseService
{
    public Payment GetEmployeePay(int employeeId) => _database.Find<Payment>(employeeId);
    
    public void SavePayment(Payment payment) => _database.Insert(payment);
    
    public void UpdatePayment(Payment payment) => _database.Update(payment);

    public void SetAllPaymentsToZero() => _database.Table<Payment>()
                                                   .ToList()
                                                   .ForEach((payment) =>
                                                   {
                                                       payment.TotalHours = 0;
                                                       payment.RegHours = 0;
                                                       payment.OverTimeHours = 0;
                                                       payment.Performance = 0;
                                                   });

    // Calculate payroll for all employees
    public double CalculatePayroll() =>
        _database.Table<Employee>()
                 .Select(GetOrCreatePayment)
                 .Aggregate(0.0, (acc, cur) => acc + (cur.Salary * cur.RegHours) + (cur.Salary * cur.OverTimeHours * 1.5));

    //Method to get all employees ID's and performance rating
    public Dictionary<int, int> GetEmployeesIDandPR() =>
        _database.Table<Employee>()
                 .Select(GetOrCreatePayment)
                 .ToDictionary((payment) => payment.EmployeeID, (payment) => payment.Performance);

    //Method to calculate an employees accrued PTO
    public int CalculatePTO(Employee employee)
    {
        Payment payment = GetOrCreatePayment(employee);

        return Convert.ToInt32(payment.TotalHours * 0.06);
    }

    private Payment GetOrCreatePayment(Employee employee) =>
        GetEmployeePay(employee.Id) ??
        new Payment
        {
            EmployeeID = employee.Id,
            Salary = 0,
            TotalHours = 0,
            RegHours = 0,
            OverTimeHours = 0,
            Performance = 0
        };

    //private double CalculateTax(double pay)
    //{
    //    var taxRate = pay switch
    //    {
    //        double p when p < 2137 => 0.15,
    //        double p when p < 4297 => 0.205,
    //        double p when p < 6661 => 0.26,
    //        double p when p < 9490 => 0.29,
    //        _ => 0.33,
    //    };
    //    double tax = pay * taxRate;
    //    return tax;
    //}
}
