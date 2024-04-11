﻿namespace EmployeeManagementSystem
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //register routes of pages
            Routing.RegisterRoute(nameof(DashboardView), typeof(DashboardView));
            Routing.RegisterRoute(nameof(StartupPageView), typeof(StartupPageView));
        }
    }
}
