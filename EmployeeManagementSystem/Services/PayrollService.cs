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
                        HoursWorkedThisWeek = 0,
                        OvertimeHoursWorkedThisWeek = 0,
                        Performance = 0
                        
                    };
                }

                // Calculate gross pay
                totalPayroll += ((payment.Salary * payment.HoursWorkedThisWeek) 
                    + (payment.Salary * payment.OvertimeHoursWorkedThisWeek * 1.5));
            }
            return totalPayroll;
        }

        public static Dictionary<int, int> CalculateTopEmployees()
        {
            // Calculate top employees based on performance rating
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
                        HoursWorkedThisWeek = 0,
                        OvertimeHoursWorkedThisWeek = 0,
                        Performance = 0
                    };
                }
                EmployeeRatingList.Add(payment.EmployeeID, payment.Performance);
            }
            return EmployeeRatingList;
        }

    }
}
