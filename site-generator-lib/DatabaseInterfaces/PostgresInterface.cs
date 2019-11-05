using Npgsql;
using System.Linq;
using site_generator_lib.Models;
using System.Collections.Generic;
using System;
using System.Data;
using site_generator_lib.Shared;
using System.Globalization;

namespace site_generator_lib.DatabaseInterfaces
{
    public static class PostgresInterface
    {
        public static Website GetWebsite(string databaseName)
        {
            Website website = new Website();

            try
            {
                string connectionString = $"Server=127.0.0.1;Port=5432;User Id=postgres;Password=postgres;Database={databaseName};";
                DataRowCollection tables = null;
                    

                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        tables = conn.GetSchema("Tables").Rows;
                    }
                    conn.Close();
                }

                foreach (DataRow tableRow in tables)
                {
                    string tableName = tableRow.ItemArray[2].ToString();
                    WebsiteEntity websiteEntity = new WebsiteEntity() { Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(tableName).Replace("_","") };

                    using (NpgsqlConnection conn2 = new NpgsqlConnection(connectionString))
                    {
                        conn2.Open();
                        if (conn2.State == ConnectionState.Open)
                        {
                            NpgsqlCommand command = new NpgsqlCommand($"select column_name, data_type from information_schema.columns where table_name = '{tableName}'", conn2);
                            NpgsqlDataReader dr = command.ExecuteReader();

                            while (dr.Read())
                            {
                                websiteEntity.Fields.Add(
                                            new WebsiteEntityField()
                                            {
                                                Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dr[0].ToString()).Replace("_", ""),
                                                Type = dr[1].ToString()
                                            });
                            }
                        }
                        conn2.Close();
                    }
                                                       
                    website.Entities.Add(websiteEntity);
                }
                 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message);
            }

            return website;
        }

    }
}
