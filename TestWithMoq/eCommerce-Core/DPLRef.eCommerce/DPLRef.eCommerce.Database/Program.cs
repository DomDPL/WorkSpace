using DbUp;
using DbUp.Engine;
using DPLRef.eCommerce.Common.Shared;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;

namespace DPLRef.eCommerce.Database
{
    internal class Program
    {
        private static void CleanupTables(string connectionString)
        {
            var sql = @"
                if exists (select * from INFORMATION_SCHEMA.TABLES  t where t.TABLE_NAME='SchemaVersions')
                    drop table SchemaVersions;

                if exists (select * from INFORMATION_SCHEMA.TABLES  t where t.TABLE_NAME='OrderLines')
                    drop table OrderLines;

                if exists (select * from INFORMATION_SCHEMA.TABLES  t where t.TABLE_NAME='Orders')
                    drop table Orders;

                if exists (select * from INFORMATION_SCHEMA.TABLES  t where t.TABLE_NAME='CartItems')
                    drop table CartItems;

                if exists (select * from INFORMATION_SCHEMA.TABLES  t where t.TABLE_NAME='Carts')
                    drop table Carts;

                if exists (select * from INFORMATION_SCHEMA.TABLES  t where t.TABLE_NAME='Products')
                    drop table Products;

                if exists (select * from INFORMATION_SCHEMA.TABLES  t where t.TABLE_NAME='Catalogs')
                    drop table Catalogs;

                if exists (select * from INFORMATION_SCHEMA.TABLES  t where t.TABLE_NAME='Sellers')
                    drop table Sellers;

                if exists(select * from INFORMATION_SCHEMA.ROUTINES t where t.ROUTINE_NAME = 'SalesTotalForSeller')
                    drop procedure SalesTotalForSeller;

                if exists(select * from INFORMATION_SCHEMA.ROUTINES t where t.ROUTINE_NAME = 'BackendSalesTotals')
                    drop procedure BackendSalesTotals;
            ";

            using var conn = new SqlConnection(connectionString);
            conn.Open();

            using var cmd = new SqlCommand(sql, conn);
            _ = cmd.ExecuteNonQuery();
        }

        private static void CleanupTablesSqlite(string filepath) => File.Delete(filepath);

        private static int Main(string[] args)
        {
            var sqlServer = true;
            var connectionString = Config.SqlServerConnectionString;

            if (string.IsNullOrEmpty(connectionString))
            {
                // Example value for eCommerceDatabaseSqlite: 
                // Data Source=C:\Workspaces\eCommerce-Core\DPLRef.eCommerce\eCommerce.sqlite
                connectionString = Config.SqliteConnectionString;
                sqlServer = false;

                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new InvalidOperationException("Connection string environment variable missing.");
                }
            }

            // add ability to override connection string using command line argument
            if (args != null && args.Length >= 2)
            {
                sqlServer = args[0] != "sqlite";

                connectionString = args[1];
            }

            DatabaseUpgradeResult result = null;

            if (sqlServer)
            {
                EnsureDatabase.For.SqlDatabase(connectionString);

                CleanupTables(connectionString);

                var upgrader =
                    DeployChanges.To
                        .SqlDatabase(connectionString)
                        .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(), scriptname => scriptname.StartsWith("DPLRef.eCommerce.Database.Scripts"))
                        .LogToConsole()
                        .Build();

                result = upgrader.PerformUpgrade();
            }
            else
            {
                var filePath = connectionString.Split("Data Source=")[1];
                CleanupTablesSqlite(filePath);

                var upgrader =
                    DeployChanges.To
                        .SQLiteDatabase(connectionString)
                        .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(), scriptname => scriptname.StartsWith("DPLRef.eCommerce.Database.SqliteScripts"))
                        .LogToConsole()
                        .Build();

                result = upgrader.PerformUpgrade();
            }

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
                return -1;
            }

            using (var accessor = CreateSeedDataAccessor(connectionString, sqlServer))
            {
                var seedData = new SeedDataManager(accessor);
                seedData.Add();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();

            Console.WriteLine("press enter to exit");
            _ = Console.ReadLine();

            return 0;
        }

        private static ISeedDataAccessor CreateSeedDataAccessor(string connectionString, bool sqlServer) => sqlServer ? new SqlSeedDataAccessor(connectionString) : new SqliteSeedDataAccessor(connectionString);
    }
}