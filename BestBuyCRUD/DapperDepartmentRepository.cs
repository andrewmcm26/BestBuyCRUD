using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace BestBuyCRUD
{
    public class DapperDepartmentRepository
    {
        private readonly IDbConnection _connection;

        public DapperDepartmentRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            return _connection.Query<Department>("SELECT * FROM Departments;").ToList();
        }
    }
}
