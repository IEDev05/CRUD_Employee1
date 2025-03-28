using CrudOpration.Entity;
using CrudOpration.Repository;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;

namespace CrudOpration.Business
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string _connectionString;

        public EmployeeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<IEnumerable<EmployeeEntity>> GetEmployees()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryAsync<EmployeeEntity>("Employee_Select", commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<EmployeeEntity> GetEmployeeById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<EmployeeEntity>("Employee_SelectById", new { Id = id }, commandType: CommandType.StoredProcedure);

            }
        }

        public async Task<int> Insert(EmployeeEntity employee)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new
                {
                    Name = employee.Name,
                    Age = employee.Age,
                    Department = employee.Department,
                    Salary = employee.Salary
                };

                return await connection.ExecuteScalarAsync<int>("employee_insert", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public async Task<int> Update(EmployeeEntity employee)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Age = employee.Age,
                    Department = employee.Department,
                    Salary = employee.Salary
                };

                return await connection.ExecuteAsync("Employee_Update", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<bool> Delete(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.ExecuteAsync("Employee_Delete", new { Id = id }, commandType: CommandType.StoredProcedure) > 0;
        }

    }
}
