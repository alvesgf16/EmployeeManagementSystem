using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem
{
    public partial class DashboardView : ContentPage
    {
        int count = 0;

        public DashboardView()
        {
            InitializeComponent();
        }

        public DashboardView(User user)
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }

}
