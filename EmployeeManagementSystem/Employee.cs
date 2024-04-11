using Intents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem
{
    public class Employee
    {
        

        public class EmergencyContact
        { 
            public Employee EmployeeID { get; set; }
            public string ContactName { get; set; }
            public string PhoneNumber { get; set; }

            public EmergencyContact (Employee EmployeeID, string contactName, string phoneNumber)
            {
                this.EmployeeID = EmployeeID;
                ContactName = contactName;
                PhoneNumber = phoneNumber;
            }
        }

        public class Payment
        {
            public Employee EmployeeID { get; set; }
            public decimal Salary { get; set; }
            public int HoursWorked { get; set; }

            public int Performance { get; set; }

            public Payment(Employee EmployeeID, decimal salary, int hoursWorked, int performance)
            {
                this.EmployeeID = EmployeeID;
                Salary = salary;
                HoursWorked = hoursWorked;
                Performance = performance;
            }
        }

        public enum Schedule
        {
            Day,
            Swing,
            Graceyard
        }

        public enum Position
        {
            Manager,
            Employee
        }



        public int EmployeeId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public EmergencyContact ContactName { get; set; }

        public Position position { get; set; }

        public decimal Salary { get; set; }

        public int HoursWorked { get; set; }

        public int Performance { get; set; }

        public  int AvailableTimeOff { get; set; }

        public int AvailableSickDays { get; set; }

        public Schedule Shift { get; set; }

        public bool Status { get; set; }

        public Payment earnings { get; set; }


        public Employee(int employeeId, string email, string password, string name, string phoneNumber, string address, EmergencyContact contactName, Position position, decimal salary, int hoursWorked, int performance, int availableTimeOff, int availableSickDays, Schedule shift, bool status, Payment earnings)
        {
            EmployeeId = employeeId;
            Email = email;
            Password = password;
            Name = name;
            PhoneNumber = phoneNumber;
            Address = address;
            ContactName = contactName;
            this.position = position;
            Salary = salary;
            HoursWorked = hoursWorked;
            Performance = performance;
            AvailableTimeOff = availableTimeOff;
            AvailableSickDays = availableSickDays;
            Shift = shift;
            Status = status;
            this.earnings = earnings;
        }

    }
}
