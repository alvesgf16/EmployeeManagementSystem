using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
    public class WorkDays
    {
        [PrimaryKey]
        public int WorkDayId { get; set; }
        [NotNull]
        public DateTime Date { get; set; }
        [ForeignKey(typeof(Employee))]
        public int EmployeeID { get; set; }
        public DaysofWeek Day { get; set; }
        public double RegHours { get; set; }
        public double OverTimeHours { get; set; }
        public double TotalHours { get; set; }
    }

    public enum DaysofWeek
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }
}
