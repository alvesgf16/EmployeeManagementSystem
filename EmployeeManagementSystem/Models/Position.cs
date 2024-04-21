namespace EmployeeManagementSystem.Models;

// Comments
/*
    Dominic Goncalves, April 20th, 2024.
    Postion Enum Declaration.

    Declares enum list for Employee.Position.
    This ensures that every employee is always assigned 1 of only 2 possible values.
    The position value of an employee is how their system interaction is determined - managers are given more rights and access to more information, whereas the opposite is true for employees.
 */
public enum Position
{
    Manager,
    Employee
}
