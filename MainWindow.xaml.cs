using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;
using Lynx2TCP;

namespace Lynx2TCP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            // Load settings when the application starts
            IpAddressTextBox.Text = Lynx2TCP.Settings.Default.IPAddress;
            PortTextBox.Text = Lynx2TCP.Settings.Default.Port;
            DeviceNameTextBox.Text = Lynx2TCP.Settings.Default.DeviceName;


            // Save settings when the "Save Config" button is clicked
            SaveConfigButton.Click += (s, e) =>
            {
                Lynx2TCP.Settings.Default.IPAddress = IpAddressTextBox.Text;
                Lynx2TCP.Settings.Default.Port = PortTextBox.Text;
                Lynx2TCP.Settings.Default.DeviceName = DeviceNameTextBox.Text;
                Lynx2TCP.Settings.Default.Save();
            };
            



            List<Result> results = new List<Result>
            {
                new Result { Pos = 1, Bib = 101, Firstname = "John", Lastname = "Doe", Fullname = "John Doe", FinishTime = "00:30:00.123"},
                new Result { Pos = 2, Bib = 102, Firstname = "Jane", Lastname = "Doe", Fullname = "Jane Doe", FinishTime = "00:30:00.880"},
                new Result { Pos = 3, Bib = 102, Firstname = "Jane", Lastname = "Doe", Fullname = "Jane Doe", FinishTime = "00:30:01.990"},
                // Add more results as needed
            };

            // Calculate delta time for each result
            for (int i = 1; i < results.Count; i++)
            {
                TimeSpan finishTimeCurrent = TimeSpan.Parse(results[i].FinishTime);
                TimeSpan finishTimePrevious = TimeSpan.Parse(results[i - 1].FinishTime);
                TimeSpan deltaTime = finishTimeCurrent - finishTimePrevious;

                results[i].DeltaTime = deltaTime.ToString(@"hh\:mm\:ss\.fff");
            }

            ResultGrid.ItemsSource = results;

        }


        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            // Create a new window
            Window settingsWindow = new Window
            {
                Title = "Settings",
                Width = 200,
                Height = 300,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                ResizeMode = ResizeMode.NoResize,
                WindowStyle = WindowStyle.ToolWindow // Hide the minimize and maximize buttons
            };

            // Create a StackPanel to hold the CheckBoxes
            StackPanel stackPanel = new StackPanel();

            // Create a CheckBox for each column in the DataGrid
            foreach (var column in ResultGrid.Columns)
            {
                CheckBox checkBox = new CheckBox
                {
                    Content = column.Header,
                    IsChecked = column.Visibility == Visibility.Visible
                };

                // Handle the Checked and Unchecked events to show/hide the column
                checkBox.Checked += (s, args) => column.Visibility = Visibility.Visible;
                checkBox.Unchecked += (s, args) => column.Visibility = Visibility.Collapsed;

                stackPanel.Children.Add(checkBox);
            }

            // Set the content of the window to the StackPanel
            settingsWindow.Content = stackPanel;

            // Show the window
            settingsWindow.Show();
        }

        private void BrowseLynxFolder_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == Microsoft.WindowsAPICodePack.Dialogs.CommonFileDialogResult.Ok)
            {
                LynxFolderPathTextBox.Text = dialog.FileName;
            }
        }

        private void BrowseResultFile_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                ResultFilePathTextBox.Text = dialog.FileName;
            }
        }



    }

}


public class Result
{
    public int Pos { get; set; }
    public int Bib { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Fullname { get; set; }
    public string FinishTime { get; set; }
    public string DeltaTime { get; set; }

    public bool DeltaTimeAsTimeSpan
    {
        get
        {
            TimeSpan deltaTime = TimeSpan.ParseExact(DeltaTime, "hh\\:mm\\:ss\\.fff", CultureInfo.InvariantCulture);
            return deltaTime.TotalSeconds >= 1 && deltaTime.TotalSeconds <= 1.2;
        }
    }
}