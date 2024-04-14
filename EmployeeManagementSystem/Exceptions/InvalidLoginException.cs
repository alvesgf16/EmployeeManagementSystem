namespace EmployeeManagementSystem.Exceptions;

internal class InvalidLoginException : Exception
{
    public InvalidLoginException()
    {
    }

    public InvalidLoginException(string? message) : base(message)
    {
    }

    public InvalidLoginException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
