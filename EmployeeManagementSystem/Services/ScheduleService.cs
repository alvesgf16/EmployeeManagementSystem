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
        
        //Methods - PTORequest
        public void SavePTORequest(PTORequest ptoRequest) => _database.Insert(ptoRequest);
        public void DeletePTORequest(PTORequest ptoRequest) => _database.Delete(ptoRequest);
        public void UpdatePTORequest(PTORequest ptoRequest) => _database.Update(ptoRequest);
        public List<PTORequest> GetPTORequestByEmployeeId(int Id)
        {
            foreach(var ptoRequest in _database.Table<PTORequest>())
            {
                if (ptoRequest.EmployeeID == Id)
                {
                    return _database.Table<PTORequest>().Where(w => w.EmployeeID == Id).ToList();
                }
            }
            return [];
        }
        public PTORequest GetPTORequestById(int ptoRequestId) => _database.Find<PTORequest>(ptoRequestId);
        public List<PTORequest> GetAllPTORequests() => _database.Table<PTORequest>().ToList();
        public List<PTORequest> GetPTORequestsByDate(DateTime date) => _database.Table<PTORequest>().Where(w => w.RequestedDate == date).ToList();
        public PTORequest GetPTORequestByDateAndId(DateTime date, int Id) => _database.Table<PTORequest>().Where(w => w.RequestedDate == date && w.EmployeeID == Id).FirstOrDefault();
    }
}
