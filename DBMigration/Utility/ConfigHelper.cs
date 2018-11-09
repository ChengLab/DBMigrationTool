using System.Configuration;
using System.IO;

namespace CodeGen.Utility
{
    internal static class ConfigHelper
    {
        public static readonly string OutputPath;
        public static readonly string ConnectionString;

        static ConfigHelper()
        {
            OutputPath = ConfigurationManager.AppSettings["OutputPath"];
            ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
        }

        public static string GetCreateTableFilePath()
        {
            return Path.Combine(OutputPath, "CreateTable.txt");
        }

        public static string GetDeleteTableFilePath()
        {
            return Path.Combine(OutputPath, "DeleteTable.txt");
        }
    }
}