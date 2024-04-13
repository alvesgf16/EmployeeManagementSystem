﻿namespace EmployeeManagementSystem
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            Window window = base.CreateWindow(activationState);
            
            window.Width = 800;
            window.Height = 600;
            
            return window;
        }
    }
}
