﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace BestBuyCRUD
{
    class Program
    {
        static void Main(string[] args)
        {
            IDbConnection conn = new MySqlConnection();
            conn.ConnectionString = System.IO.File.ReadAllText("ConnectionString.txt");

            var repo = new DapperDepartmentRepository(conn);
            var departments = repo.GetAllDepartments();
            foreach (var dept in departments)
            {
                Console.WriteLine($"{dept.DepartmentID} {dept.Name}");
            }

        }

        static IEnumerable GetAllDepartments()
        {
            MySqlConnection conn = new MySqlConnection(); 
            conn.ConnectionString = System.IO.File.ReadAllText("ConnectionString.txt"); //Give connection a connection string

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Name FROM Departments;"; // Our SQL query

            using (conn)
            {
                conn.Open();
                List<string> allDepartments = new List<string>();

                MySqlDataReader reader = cmd.ExecuteReader(); //To read through the result of the cmd query

                while (reader.Read() == true) //Means while there are more rows to read
                {
                    var currentDepartment = reader.GetString("Name");
                    allDepartments.Add(currentDepartment);
                }

                return allDepartments;
            }
        }
    }
}

