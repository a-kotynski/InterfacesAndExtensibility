using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfacesAndExtensibility
{
    public class FileLogger : ILogger
    {
        public FileLogger(string path)
        {
            Path = path;
        }

        public string Path { get; }

        public void LogError(string message)
        {
            Log(message, "ERROR");
        }
         
        public void LogInfo(string message)
        {
            Log(message, "INFO");
        }
        private void Log(string message, string messageType)
        {
            using (var streamWriter = new StreamWriter(Path, true))
            {
                streamWriter.WriteLine(messageType + ": " + message);
            }
        }
    }
    public class ConsoleLogger : ILogger
    {
        public void LogError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
        }

        public void LogInfo(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
        }
    }
    public interface ILogger
    {
        void LogError(string message);
        void LogInfo(string message);
    }
    public class DbMigrator
    {
        public DbMigrator(ILogger logger) // dependency injection
        {
            Logger = logger;
        }

        public ILogger Logger { get; }

        public void Migrate()
        {
            Logger.LogInfo($"Migration started at {DateTime.Now}");

            //details of migrating the database

            Logger.LogInfo($"Migration finished at {DateTime.Now}");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var dbMigrator = new DbMigrator(new ConsoleLogger());
            dbMigrator.Migrate();
            Console.ReadLine();
        }
    }
}
