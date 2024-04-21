using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Exceptions
{
    internal class InvalidActionException : Exception
    {
        public InvalidActionException() { }

        public InvalidActionException(string? message) : base(message) { }

        public InvalidActionException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
