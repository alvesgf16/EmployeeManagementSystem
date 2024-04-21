using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Exceptions
{
    internal class ManagePayException : Exception
    {
        public ManagePayException() { }

        public ManagePayException(string? message) : base(message) { }

        public ManagePayException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
