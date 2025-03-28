using CrudOpration.Entity;

namespace CrudOpration.Repository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeEntity>> GetEmployees();
        Task<EmployeeEntity> GetEmployeeById(int id);
        Task<int> Insert(EmployeeEntity employee);
        Task<int> Update(EmployeeEntity employee);
        Task<bool> Delete(int id);
    }
}
