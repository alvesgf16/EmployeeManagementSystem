﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Exceptions
{
    internal class InvalidParameterException : Exception
    {
        public InvalidParameterException() { }

        public InvalidParameterException(string? message) : base(message) { }

        public InvalidParameterException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
