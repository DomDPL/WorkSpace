using System;
using System.Data.SqlClient;

namespace DPLRef.eCommerce.Database
{
    internal class SqlSeedDataAccessor : ISeedDataAccessor
    {
        private readonly SqlConnection _connection;

        public SqlSeedDataAccessor(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();
        }

        public int CreateCatalog(int sellerId, string catalogName)
        {
            var sql = @"
                insert into Catalogs
                    (Name, SellerId, Description)
                values
                    (@name, @sellerid, @description) 

                select scope_identity()
            ";

            using var cmd = new SqlCommand(sql, _connection);
            _ = cmd.Parameters.AddWithValue("name", catalogName);
            _ = cmd.Parameters.AddWithValue("sellerid", sellerId);
            _ = cmd.Parameters.AddWithValue("description", $"{catalogName} description");
            using var reader = cmd.ExecuteReader();
            _ = reader.Read();
            var id = Convert.ToInt32(reader[0]);
            return id;
        }

        public int CreateProduct(int catalogId, string name, bool isAvailable, bool isDownloadable, string summary,
            string detail, string supplier, decimal price)
        {
            var sql = @"
                insert into Products
                    (Name, CatalogId, IsDownloadable, Price, Shippingweight, IsAvailable, Summary, Detail, SupplierName)
                values
                    (@name, @catalogid, @isdownloadable, @price, 1.25, @isavailable, @summary, @detail, @supplier) 

                select scope_identity()
            ";

            if (name.Length > 50)
            {
                name = name.Substring(0, 50);
            }

            using var cmd = new SqlCommand(sql, _connection);
            _ = cmd.Parameters.AddWithValue("name", name);
            _ = cmd.Parameters.AddWithValue("catalogid", catalogId);
            _ = cmd.Parameters.AddWithValue("isavailable", isAvailable);
            _ = cmd.Parameters.AddWithValue("isdownloadable", isDownloadable);
            _ = cmd.Parameters.AddWithValue("summary", summary);
            _ = cmd.Parameters.AddWithValue("detail", detail);
            _ = cmd.Parameters.AddWithValue("supplier", supplier);
            _ = cmd.Parameters.AddWithValue("price", price);

            using var reader = cmd.ExecuteReader();
            _ = reader.Read();
            var id = Convert.ToInt32(reader[0]);
            return id;
        }

        public int CreateSeller(string username, string sellerName)
        {
            var sql = @"
                insert into Sellers
                    (Name, UserName)
                values
                    (@name, @username) 

                select scope_identity()
            ";

            using var cmd = new SqlCommand(sql, _connection);
            _ = cmd.Parameters.AddWithValue("name", sellerName);
            _ = cmd.Parameters.AddWithValue("username", username);

            using var reader = cmd.ExecuteReader();
            _ = reader.Read();
            var id = Convert.ToInt32(reader[0]);
            return id;
        }

        public void Dispose() => _connection?.Dispose();
    }
}