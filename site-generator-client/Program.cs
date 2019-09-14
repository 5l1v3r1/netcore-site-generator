using site_generator_lib;
using System;

namespace site_generator_client
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length != 2)
            {
                Console.WriteLine("Usage: dotnet run [databaseType] [databaseName]");
                Console.WriteLine(".NET Core site-generator allows creating a working CRUD website with separated frontend and api server from a Database model.");
                Console.WriteLine("");
                Console.WriteLine(" Arguments:");
                Console.WriteLine(" [databaseType]: (postgres)");
                Console.WriteLine(" [databaseName]: Name of the Postgres database to fetch website model structure.");
                Console.WriteLine(" [databaseName]: Name of the Postgres database to fetch website model structure.");
                Console.WriteLine("");
                return;
            }

            SiteBuilder.BuildWebsite(args[0], args[1]);
        }
    }
}
