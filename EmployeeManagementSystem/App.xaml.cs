using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        public static Employee? AuthenticatedUser;

        protected override Window CreateWindow(IActivationState? activationState)
        {
            Window window = base.CreateWindow(activationState);
            
            window.Width = 900;
            window.Height = 1000;

            if (window != null)
            {
                window.Title = "Group 3 - Employee Management System";
            }
            
            return window;
        }
    }
}
