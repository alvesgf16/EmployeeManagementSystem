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
            _database.CreateTable<PTORequest>();
        }
        
        //Methods - WorkDays
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
