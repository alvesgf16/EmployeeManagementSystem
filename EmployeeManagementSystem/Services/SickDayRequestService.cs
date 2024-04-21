using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Services
{
    public class SickDayRequestService : BaseService
    {
        // Methods - Sick Day Request
        public List<SickDayRequest> GetAllSickDayRequests() => _database.Table<SickDayRequest>().ToList();

        public SickDayRequest GetSickDayRequestById(int sickDayRequestId) => _database.Find<SickDayRequest>(sickDayRequestId);

        public void SaveSickDayRequest(SickDayRequest sickDayRequest) => _database.Insert(sickDayRequest);

        public void UpdateSickDayRequest(SickDayRequest sickDayRequest) => _database.Update(sickDayRequest);

        public void DeleteSickDayRequest(SickDayRequest sickDayRequest) => _database.Delete(sickDayRequest);

        public List<SickDayRequest> GetSickDayRequestsByEmployeeId(int employeeId) => _database.Table<SickDayRequest>().Where(s => s.EmployeeID == employeeId).ToList();

        public List<SickDayRequest> GetSickDayRequestsByDate(DateTime date) => _database.Table<SickDayRequest>().Where(s => s.RequestedDate == date).ToList();
        public SickDayRequest GetSickDayRequestsByEmployeeIdAndDate(int employeeId, DateTime date) => _database.Table<SickDayRequest>().Where(s => s.EmployeeID == employeeId && s.RequestedDate == date).FirstOrDefault();
    }
}
