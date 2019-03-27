using System;
using Npgsql;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_DB_
{
    class Program
    {
        static void Main(string[] args)
        {
            string invariant = "Npgsql";
            DbProviderFactory factory = DbProviderFactories.GetFactory(invariant);

            var connection = factory.CreateConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["Park"].ConnectionString;

            var command = factory.CreateCommand();
            command.Connection = connection;
            command.CommandText = "Select * from cars";

            using (connection)
            {
                connection.Open();

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine(
                        string.Format("Brand: {0}, Model: {1}",
                        reader["brand"], reader["model"]));
                }

                Console.ReadLine();
                connection.Close();
            }
        }
    }
}
