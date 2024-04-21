using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Exceptions
{
    internal class ManageWeeklyExceptions : Exception
    {
        public ManageWeeklyExceptions() { }

        public ManageWeeklyExceptions(string? message) : base(message) { }

        public ManageWeeklyExceptions(string? message, Exception? innerException) : base(message, innerException) { }

    }
}
