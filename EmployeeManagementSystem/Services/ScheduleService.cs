using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Services
{
    public class ScheduleService : BaseService
    {   
        // Methods - Schedule
        public List<Schedule> GetAllSchedules() => [.. _database.Table<Schedule>()];
        
        public Schedule GetScheduleById(int workDayId) => _database.Find<Schedule>(workDayId);
        
        public void SaveSchedule(Schedule schedule) => _database.Insert(schedule);
        
        public void UpdateSchedule(Schedule schedule) => _database.Update(schedule);
        
        public void DeleteSchedule(Schedule schedule) => _database.Delete(schedule);
        
        public List<Schedule> GetScheduleByEmployeeId(int employeeId)
        {
            foreach(var schedule in _database.Table<Schedule>())
            {
                if (schedule.EmployeeID == employeeId)
                {
                    return [.. _database.Table<Schedule>().Where(s => s.EmployeeID == employeeId)];
                }
            }
            return [];
        }

        public List<Schedule> GetSchedulesByDate(DateTime date) => [.. _database.Table<Schedule>().Where(s => s.Date == date)];
        
        public List<Schedule> GetSchedulesByDay(DaysofWeek day) => [.. _database.Table<Schedule>().Where(s => s.Day == day)];
    }
}
