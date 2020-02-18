using System;
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

            var productRepo = new DapperProductRepository(conn);

            Console.WriteLine("\nThe current products are: \nID | Name | Price");
            var products = productRepo.GetAllProducts();
            foreach(var product in products)
            {
                Console.WriteLine($"{product.ProductID} {product.Name} ${product.Price}");
            }

            Console.WriteLine("\nWhat would you like to do? (add/update/delete/exit)");
            var resp = Console.ReadLine().ToLower();
            while (resp == "add" || resp == "update" || resp == "delete")
            {

                if (resp == "add")
                {
                    Console.WriteLine("\nWhat is the name of the new product?");
                    var name = Console.ReadLine();
                    Console.WriteLine("\nWhat is the price of the new product?");
                    var price = double.Parse(Console.ReadLine());
                    Console.WriteLine("\nWhat is the category ID of the new product?");
                    var categoryID = Int32.Parse(Console.ReadLine());

                    productRepo.CreateProduct(name, price, categoryID);
                }

                if (resp == "update")
                {
                    Console.WriteLine("\nWhat is the product ID of the product you want to update?");
                    var productID = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("\nWhat is the updated name?");
                    var name = Console.ReadLine();
                    Console.WriteLine("\nWhat is the updated price?");
                    var price = double.Parse(Console.ReadLine());
                    Console.WriteLine("\nWhat is the updated CategoryID?");
                    var categoryID = Int32.Parse(Console.ReadLine());

                    productRepo.UpdateProduct(productID, name, price, categoryID);

                    Console.WriteLine("\nYour updated product is:");
                    var product = productRepo.GetOneProduct(productID);
                    Console.WriteLine($"\nID: {product.ProductID} \nName: {product.Name} " +
                    $"\nPrice: {product.Price} \nCategory ID: {product.CategoryID}");
                }

                if (resp == "delete")
                {
                    Console.WriteLine("\nWhat is the product ID of the product you would like to delete?");
                    var productID = Int32.Parse(Console.ReadLine());
                    productRepo.DeleteProduct(productID);
                }

                Console.WriteLine("\nThe updated list of products is: \nID | Name | Price");
                products = productRepo.GetAllProducts();
                foreach (var product in products)
                {
                    Console.WriteLine($"{product.ProductID} {product.Name} ${product.Price}");
                }

                Console.WriteLine("\nWould you like to do anything else? (add/update/delete/exit)");
                resp = Console.ReadLine().ToLower();
            }

            Console.WriteLine("\nHave a great day!");

        }
//------------------------------------------------------------------------------------------------------

        //Example of creating a method without Dapper
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

