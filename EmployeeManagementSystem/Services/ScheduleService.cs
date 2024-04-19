using SQLite;
using EmployeeManagementSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Services
{
    public class ScheduleService
    {
        private SQLiteConnection _database;

        public ScheduleService()
        {
            _database = new SQLiteConnection(Constants.DatabasePath);
            _database.CreateTable<WorkDays>();
        }
        
        //Methods
        public void SaveWorkDay(WorkDays workDay) => _database.Insert(workDay);
        public void DeleteWorkDay(WorkDays workDay) => _database.Delete(workDay);
        public void UpdateWorkDay(WorkDays workDay) => _database.Update(workDay);
        public List<WorkDays> GetWorkDayByEmployeeId(int Id)
        {
            foreach(var workDay in _database.Table<WorkDays>())
            {
                if (workDay.EmployeeID == Id)
                {
                    return _database.Table<WorkDays>().Where(w => w.EmployeeID == Id).ToList();
                }
            }
            return [];
        }
        public WorkDays GetWorkDayById(int workDayId) => _database.Find<WorkDays>(workDayId);

        public List<WorkDays> GetAllWorkDays() => _database.Table<WorkDays>().ToList();
        public List<WorkDays> GetWorkDaysByDate(DateTime date) => _database.Table<WorkDays>().Where(w => w.Date == date).ToList();
        public List<WorkDays> GetWorkDaysByDay(DaysofWeek day) => _database.Table<WorkDays>().Where(w => w.Day == day).ToList();
        
    }
}
