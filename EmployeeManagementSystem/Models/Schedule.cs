using SQLite;
using SQLiteNetExtensions.Attributes;

namespace EmployeeManagementSystem.Models
{
    public class Schedule
    {
        [PrimaryKey, AutoIncrement]
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
}
