using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem
{
    public class Constants
    {
        public const string DatabaseFilename = "EMS.db3";

        public static string DatabasePath
        {
            get
            {
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                DirectoryInfo directory = new DirectoryInfo(baseDirectory);

                while (directory.Name != "EmployeeManagementSystem")
                {
                    directory = directory.Parent;

                    if (directory == null)
                    {
                        throw new InvalidOperationException("Could not find the project root directory.");
                    }
                }

                string projectRoot = directory.FullName;
                string dbDirectory = Path.Combine(projectRoot, "DB");

                return Path.Combine(dbDirectory, DatabaseFilename);
            }
        }
        public Constants()
        {
        }
    }
}
