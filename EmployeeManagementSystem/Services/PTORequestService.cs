using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Services;

internal class PTORequestService : BaseService
{
    public void SavePTORequest(PTORequest ptoRequest) => _database.Insert(ptoRequest);
    
    public void UpdatePTORequest(PTORequest ptoRequest) => _database.Update(ptoRequest);
    
    public void DeletePTORequest(PTORequest ptoRequest) => _database.Delete(ptoRequest);
    
    public List<PTORequest> GetPTORequestByEmployeeId(int Id)
    {
        foreach (var ptoRequest in _database.Table<PTORequest>())
        {
            if (ptoRequest.EmployeeID == Id)
            {
                return [.. _database.Table<PTORequest>().Where(w => w.EmployeeID == Id)];
            }
        }
        return [];
    }
    public PTORequest GetPTORequestById(int ptoRequestId) => _database.Find<PTORequest>(ptoRequestId);
    
    public List<PTORequest> GetAllPTORequests() => [.. _database.Table<PTORequest>()];
    
    public List<PTORequest> GetPTORequestsByDate(DateTime date) => [.. _database.Table<PTORequest>().Where(w => w.RequestedDate == date)];
    
    public PTORequest GetPTORequestByDateAndId(DateTime date, int Id) => _database.Table<PTORequest>().Where(w => w.RequestedDate == date && w.EmployeeID == Id).FirstOrDefault();
}
