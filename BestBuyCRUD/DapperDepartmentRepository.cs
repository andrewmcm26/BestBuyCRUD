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

        public IEnumerable<Department> InsertNewDepartment(string newDepartment )
        {
            return _connection.Query<Department>("INSERT INTO Departments (Name) " +
                "VALUES (@departmentName);", new { departmentName = newDepartment } );
        }
    }
}
