using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
    public class WorkDays
    {
        public int EmployeeID { get; set; }
        public string Day { get; set; }
        public double HoursWorked { get; set; }
        public double OvertimeHoursWorked { get; set; }
    }


}
