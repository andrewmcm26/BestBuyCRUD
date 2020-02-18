using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace BestBuyCRUD
{
    public class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _connection;

        public DapperProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _connection.Query<Product>("SELECT * FROM Products;");
        }


        public void CreateProduct(string name, double price, int categoryID)
        {
            _connection.Execute("INSERT INTO Products (Name, Price, CategoryID) " +
                "VALUES (@name, @price, @categoryID);",
                new
                {
                    name = name,
                    price = price,
                    categoryID = categoryID
                });
        }

        public Product GetOneProduct(int productID)
        {
            return _connection.QuerySingle<Product>("SELECT * FROM Products WHERE productID = @productID;",
                new
                {
                    productID = productID
                });
        }

        public void UpdateProduct(int productID, string name, double price, int categoryID)
        {
            _connection.Query<Product>("UPDATE products SET name = @name, price = @price, " +
                "categoryID = @categoryID WHERE productID = @productID;",
                new
                {
                    productID = productID,
                    name = name,
                    price = price,
                    categoryID = categoryID
                });
        }

        public void DeleteProduct (int productID)
        {
            //Need to delete from tables where the product is a Foreign key FIRST
            _connection.Execute("DELETE FROM Reviews WHERE ProductID = @productID;",
                new
                {
                    productID

                });
            _connection.Execute("DELETE FROM Sales WHERE ProductID = @productID;",
                new
                {
                    productID

                });
            _connection.Execute("DELETE FROM Products WHERE ProductID = @productID;",
                new
                {
                    productID

                });
        }
    }
}

