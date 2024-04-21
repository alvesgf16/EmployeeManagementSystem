namespace EmployeeManagementSystem.Models;

// Comments
/*
    Dominic Goncalves, April 20th, 2024.
    Schedule Enum Declaration.

    Declares enum list for Employee.Schedule.
    This ensures that every employee is always assigned 1 of only 3 possible values.
    The Schedule value shows the times of day when an employee is typically expected to work, and is referenced by managers to make work schedules.
 */

public enum Schedule
{
    Day,
    Swing,
    Graveyard
}
