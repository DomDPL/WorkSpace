using Microsoft.Data.Sqlite;
using System;

namespace DPLRef.eCommerce.Database
{
    internal class SqliteSeedDataAccessor : ISeedDataAccessor
    {
        private readonly SqliteConnection _connection;

        public SqliteSeedDataAccessor(string connectionString)
        {
            _connection = new SqliteConnection(connectionString);
            _connection.Open();
        }

        public int CreateCatalog(int sellerId, string catalogName)
        {
            var sqlite = @"
                insert into Catalogs
                    (Name, SellerId, Description)
                values
                    (@name, @sellerid, @description); 

                select last_insert_rowid();
            ";

            using var cmd = new SqliteCommand(sqlite, _connection);
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
            var sqlite = @"
                insert into Products
                    (Name, CatalogId, IsDownloadable, Price, Shippingweight, IsAvailable, Summary, Detail, SupplierName)
                values
                    (@name, @catalogid, @isdownloadable, @price, 1.25, @isavailable, @summary, @detail, @supplier); 

                select last_insert_rowid();
            ";

            if (name.Length > 50)
            {
                name = name.Substring(0, 50);
            }

            using var cmd = new SqliteCommand(sqlite, _connection);
            _ = cmd.Parameters.AddWithValue("name", name);
            _ = cmd.Parameters.AddWithValue("catalogid", catalogId);
            _ = cmd.Parameters.AddWithValue("isavailable", isAvailable);
            _ = cmd.Parameters.AddWithValue("isdownloadable", isDownloadable);
            _ = cmd.Parameters.AddWithValue("price", price * 1.00M);
            _ = cmd.Parameters.AddWithValue("summary", summary);
            _ = cmd.Parameters.AddWithValue("detail", detail);
            _ = cmd.Parameters.AddWithValue("supplier", supplier);

            using var reader = cmd.ExecuteReader();
            _ = reader.Read();
            var id = Convert.ToInt32(reader[0]);
            return id;
        }

        public int CreateSeller(string username, string sellerName)
        {
            var sqlite = @"
                insert into Sellers
                    (Name, UserName)
                values
                    (@name, @username);
        
                select last_insert_rowid();
            ";

            using var cmd = new SqliteCommand(sqlite, _connection);
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