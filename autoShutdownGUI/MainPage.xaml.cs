using System.ComponentModel.Design;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace autoShutdownGUI
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnShutdownClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UserTimeEntry.Text))
            {
                DisplayAlert("Invalid Input", "Please enter a value for the shutdown time.", "OK");
                return;
            }

            if (int.TryParse(UserTimeEntry.Text, out var minutes))
            {
                var seconds = minutes * 60;
                Functions.Shutdown(seconds);

                DisplayAlert($"Your Computer will be shut down after {minutes} minutes", "Press 'Cancel Selected Event' button to cancel process", "OK");
                return;

            }
            else
            {
                DisplayAlert("Invalid Input", "Please enter a valid integer for the shutdown time.", "OK");
            }
        }

        private void OnRestartClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UserTimeEntry.Text))
            {
                DisplayAlert("Invalid Input", "Please enter a value for the restart time.", "OK");
                return;
            }

            if (int.TryParse(UserTimeEntry.Text, out var minutes))
            {
                var seconds = minutes * 60;
                Functions.Restart(seconds);
                DisplayAlert($"Your Computer Will Restart After {minutes} Minutes", "Press 'Cancel Selected Event' button to cancel process", "OK");
                return;
            }
            else
            {
                DisplayAlert("Invalid Input", "Please enter a valid integer for the restart time.", "OK");
            }
        }

        private void OnCancelClicked(object sender, EventArgs e)
        {
            Functions.Cancel();
            DisplayAlert("Process Cancelled", "Process cancelled successfully", "OK");
            return;
        }

        public static class Functions
        {
            public static void Shutdown(int time)
            {
                try
                {
                    Process.Start("shutdown", $"/s /f /t {time}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during shutdown: {ex.Message}");
                }
            }
            public static void Restart(int time) 
            {
                try
                {
                    Process.Start("shutdown", $"/r /f /t {time}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during restart: {ex.Message}");
                }
            }
            public static void Cancel()
            {
                try
                {
                    Process.Start("shutdown", "/a");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during cancel: {ex.Message}");
                }
            }
        }
    }
}