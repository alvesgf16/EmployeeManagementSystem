using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
namespace EmployeeManagementSystem.Models;

public partial class Employee : ObservableObject
{
    private int _id;
    private string _email;
    private string? _password;
    private string? _name;
    private string? _phoneNumber;
    private string? _address;
    private string? _eContactName;
    private string? _eContactPhone;
    private Position _position;
    private int _availablePTODays;
    private int _availableSickDays;
    private WorkShift _shift;
    private bool _isActive;

    //Properties Getters and Setters
    [PrimaryKey, AutoIncrement]
    public int Id
    {
        get { return _id; }
        set
        {
            if (_id != value)
            {
                _id = value;
                OnPropertyChanged();
            }
        }
    }
    [NotNull]
    public string Email
    {
        get { return _email; }
        set
        {
            if (_email != value)
            {
                _email = value;
                OnPropertyChanged();
            }
        }
    }

    public string Password
    {
        get { return _password; }
        set
        {
            if (_password != value)
            {
                _password = value;
                OnPropertyChanged();
            }
        }
    }
    public string Name
    {
        get { return _name; }
        set
        {
            if (_name != value)
            {
                _name = value;
                OnPropertyChanged();
            }
        }
    }

    public string PhoneNumber
    {
        get { return _phoneNumber; }
        set
        {
            if (_phoneNumber != value)
            {
                _phoneNumber = value;
                OnPropertyChanged();
            }
        }
    }

    public string Address
    {
        get { return _address; }
        set
        {
            if (_address != value)
            {
                _address = value;
                OnPropertyChanged();
            }
        }
    }

    public string EContactName
    {
        get { return _eContactName; }
        set
        {
            if (_eContactName != value)
            {
                _eContactName = value;
                OnPropertyChanged();
            }
        }
    }

    public string EContactPhone
    {
        get { return _eContactPhone; }
        set
        {
            if (_eContactPhone != value)
            {
                _eContactPhone = value;
                OnPropertyChanged();
            }
        }
    }

    public Position Position
    {
        get { return _position; }
        set
        {
            if (_position != value)
            {
                _position = value;
                OnPropertyChanged();
            }
        }
    }

    public int AvailablePTODays
    {
        get { return _availablePTODays; }
        set
        {
            if (_availablePTODays != value)
            {
                _availablePTODays = value;
                OnPropertyChanged();
            }
        }
    }

    public int AvailableSickDays
    {
        get { return _availableSickDays; }
        set
        {
            if (_availableSickDays != value)
            {
                _availableSickDays = value;
                OnPropertyChanged();
            }
        }
    }

    public WorkShift Shift
    {
        get { return _shift; }
        set
        {
            if (_shift != value)
            {
                _shift = value;
                OnPropertyChanged();
            }
        }
    }

    public bool IsActive
    {
        get { return _isActive; }
        set
        {
            if (_isActive != value)
            {
                _isActive = value;
                OnPropertyChanged();
            }
        }
    }

    //override toString method
    public override string ToString() => $"ID: {Id} Name: {Name} Position: {Position} Shift: {Shift}";
}
