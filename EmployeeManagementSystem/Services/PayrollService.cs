using EmployeeManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Services
{
    class PayrollService
    {
        public PayrollService() { }

        public static double CalculatePayroll()
        {
            // Calculate payroll for all employees
            EmployeeService employeeManager = new();
            var employees = employeeManager.GetAllEmployees();
            double totalPayroll = 0;
            foreach (var employee in employees)
            {
                // Calculate payroll for each employee
                
                Payment payment = employeeManager.GetEmployeePay(employee.Id);
                if (payment is null)
                {
                    payment = new Payment
                    {
                        EmployeeID = employee.Id,
                        Salary = 0,
                        TotalHours = 0,
                        RegHours = 0,
                        OverTimeHours = 0,
                        Performance = 0
                        
                    };
                }

                // Calculate gross pay
                totalPayroll += ((payment.Salary * payment.RegHours) 
                    + (payment.Salary * payment.OverTimeHours * 1.5));
            }
            return totalPayroll;
        }
        //Method to get all employees ID's and performance rating
        public static Dictionary<int, int> GetEmployeesIDandPR()
        {
            EmployeeService employeeManager = new();
            var employees = employeeManager.GetAllEmployees();
            Dictionary<int, int> EmployeeRatingList = [];
            foreach (var employee in employees)
            {
                Payment payment = employeeManager.GetEmployeePay(employee.Id);
                if (payment is null)
                {
                    payment = new Payment
                    {
                        EmployeeID = employee.Id,
                        Salary = 0,
                        TotalHours = 0,
                        RegHours = 0,
                        OverTimeHours = 0,
                        Performance = 0
                    };
                }
                EmployeeRatingList.Add(payment.EmployeeID, payment.Performance);
            }
            return EmployeeRatingList;
        }

        //Method to calculate an employees accrued PTO
        public static int CalculatePTO(Employee employee)
        {
            EmployeeService employeeManager = new();
            Payment payment = employeeManager.GetEmployeePay(employee.Id);
            if (payment is null)
            {
                payment = new Payment
                {
                    EmployeeID = employee.Id,
                    Salary = 0,
                    TotalHours = 0,
                    RegHours = 0,
                    OverTimeHours = 0,
                    Performance = 0
                };
            }
            int PTO = Convert.ToInt32(payment.TotalHours * 0.06);
            return PTO;
        }

        

    }
}
