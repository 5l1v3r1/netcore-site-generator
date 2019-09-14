using site_generator_lib.DatabaseInterfaces;
using site_generator_lib.Generators;
using System;

namespace site_generator_lib
{
    public static class SiteBuilder
    {
        public static bool BuildWebsite(string databaseType, string databaseName)
        {
            switch(databaseType)
            {
                case "postgres":
                    CoreApiGenerator.GenerateWebsite(PostgresInterface.GetWebsite(databaseName));
                    Console.WriteLine("Website generated successfully.");
                    break;
                default:
                    Console.WriteLine($"Database type not recognized: {databaseType}");
                    break;
            }
            return true;
        }
    }
}
