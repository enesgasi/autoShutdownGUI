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
            if (int.TryParse(UserTimeEntry.Text, out var minutes))
            {
                var seconds = minutes * 60;
                Functions.Shutdown(seconds);
            }
            else
            {
                // Handle the case where the input is not a valid integer
                DisplayAlert("Invalid Input", "Please enter a valid integer for the shutdown time.", "OK");
            }
        }

        private void OnRestartClicked(object sender, EventArgs e)
        {
            if (int.TryParse(UserTimeEntry.Text, out var minutes))
            {
                var seconds = minutes * 60;
                Functions.Restart(seconds);
            }
            else
            {
                // Handle the case where the input is not a valid integer
                DisplayAlert("Invalid Input", "Please enter a valid integer for the restart time.", "OK");
            }
        }

        private void OnCancelClicked(object sender, EventArgs e)
        {
            Functions.Cancel();
        }

        // Functions class
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