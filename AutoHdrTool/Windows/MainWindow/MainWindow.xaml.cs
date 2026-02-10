using ArnoldVinkCode;
using ArnoldVinkStyles;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using static ArnoldVinkCode.AVProcess;

namespace AutoHdrTool
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override async void OnSourceInitialized(EventArgs e)
        {
            try
            {
                //Event functions
                lb_Menu.PreviewKeyUp += lb_Menu_KeyPressUp;
                lb_Menu.PreviewMouseUp += lb_Menu_MousePressUp;
                checkbox_Preferences_Enabled.Click += Checkbox_Preferences_Enabled_Click;
                slider_Preferences_Intensity.ValueChanged += Slider_Preferences_Intensity_ValueChanged;
                slider_Windows_SdrBrightness.ValueChanged += Slider_Windows_SdrBrightness_ValueChanged;
                combobox_Preferences_Applications.SelectionChanged += Combobox_Preferences_Applications_SelectionChanged;
                button_Preferences_Applications_Refresh.Click += Button_Preferences_Applications_Refresh_Click;
                button_Support_BrowseFiles.Click += Button_Support_BrowseFiles_Click;
                button_Support_SelectProcess.Click += Button_Support_SelectProcess_Click;
                button_Support_Add.Click += Button_Support_Add_Click;
                button_Support_Remove.Click += Button_Support_Remove_Click;
                button_MonitorHdrEnable.Click += Button_MonitorHdrEnable_Click;
                button_MonitorHdrDisable.Click += Button_MonitorHdrDisable_Click;
                button_AutoHdrEnable.Click += Button_AutoHdrEnable_Click;
                button_AutoHdrDisable.Click += Button_AutoHdrDisable_Click;
                button_AutoHdrNotificationEnable.Click += Button_AutoHdrNotificationEnable_Click;
                button_AutoHdrNotificationDisable.Click += Button_AutoHdrNotificationDisable_Click;
                Closing += MainWindow_Closing;

                //List applications
                ListApplications(false);

                //Set application information
                textblock_AppInformation.Text = "Application made by Arnold Vink\r\nVersion " + AVFunctions.ApplicationVersion();
            }
            catch { }
        }

        private void Button_Preferences_Applications_Refresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //List applications
                ListApplications(true);
            }
            catch { }
        }

        private void Button_AutoHdrNotificationEnable_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AutoHdrNotification.SetAutoHdrNotificationEnabled(true);

                //Show status
                ShowStatusMessage("Auto HDR notification enabled");
                Debug.WriteLine("Auto HDR notification enabled");
            }
            catch { }
        }

        private void Button_AutoHdrNotificationDisable_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AutoHdrNotification.SetAutoHdrNotificationEnabled(false);

                //Show status
                ShowStatusMessage("Auto HDR notification disabled");
                Debug.WriteLine("Auto HDR notification disabled");
            }
            catch { }
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                Application.Current.Shutdown(0);
                Debug.WriteLine("Exiting application.");
            }
            catch { }
        }

        private void Combobox_Preferences_Applications_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string executablePath = (string)combobox_Preferences_Applications.SelectedItem;
                bool hdrEnabled = AutoHdrPreferences.GetAppAutoHdrEnabled(executablePath);
                int hdrIntensity = AutoHdrPreferences.GetAppAutoHdrIntensity(executablePath);

                AppVariables.BlockEvent = true;
                checkbox_Preferences_Enabled.IsChecked = hdrEnabled;
                slider_Preferences_Intensity.Value = hdrIntensity;
                AppVariables.BlockEvent = false;
            }
            catch { }
        }

        private void ListApplications(bool showStatus)
        {
            try
            {
                List<string> supportApps = AutoHdrList.List_Support_Applications();
                supportApps.Sort();
                combobox_Support_Applications.ItemsSource = supportApps;
                combobox_Support_Applications.SelectedIndex = 0;

                List<string> preferencesApps = AutoHdrList.List_Preferences_Applications();
                preferencesApps.Sort();
                combobox_Preferences_Applications.ItemsSource = preferencesApps;
                combobox_Preferences_Applications.SelectedIndex = 0;

                //Show status
                if (showStatus)
                {
                    ShowStatusMessage("Listed applications");
                }
                Debug.WriteLine("Listed applications");
            }
            catch { }
        }

        private void Button_AutoHdrDisable_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Set auto hdr enabled
                AutoHdrPreferences.SetAppAutoHdrEnabled("DirectXUserGlobalSettings", false);

                //Show status
                ShowStatusMessage("Windows Auto HDR disabled");
            }
            catch { }
        }

        private void Button_AutoHdrEnable_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Set auto hdr enabled
                AutoHdrPreferences.SetAppAutoHdrEnabled("DirectXUserGlobalSettings", true);

                //Show status
                ShowStatusMessage("Windows Auto HDR enabled");
            }
            catch { }
        }

        private void Button_MonitorHdrDisable_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Set monitor hdr enabled
                MonitorHDR.MonitorHDREnabled(false);

                //Show status
                ShowStatusMessage("Monitor HDR disabled");
            }
            catch { }
        }

        private void Button_MonitorHdrEnable_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Set monitor hdr enabled
                MonitorHDR.MonitorHDREnabled(true);

                //Show status
                ShowStatusMessage("Monitor HDR enabled");
            }
            catch { }
        }

        private void Slider_Windows_SdrBrightness_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                Debug.WriteLine("Slider_Windows_SdrBrightness_ValueChanged");
            }
            catch { }
        }

        private void Button_Support_Remove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Remove application
                string executablePath = (string)combobox_Support_Applications.SelectedItem;
                AutoHdrForce.DisableForceAutoHDR(executablePath);

                //List applications
                ListApplications(false);

                //Show status
                ShowStatusMessage("Application removed");
            }
            catch { }
        }

        private void Button_Support_Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Check executable
                string executablePath = textbox_Support_Executable.Text;
                string placeholder = (string)textbox_Support_Executable.GetValue(TextboxPlaceholder.PlaceholderProperty);
                if (string.IsNullOrWhiteSpace(executablePath))
                {
                    ShowStatusMessage("Empty executable");
                    Debug.WriteLine("Empty executable");
                    return;
                }
                else if (!executablePath.Contains("."))
                {
                    ShowStatusMessage("Extension missing");
                    Debug.WriteLine("Extension missing");
                    return;
                }
                else if (executablePath == placeholder)
                {
                    ShowStatusMessage("Placeholder text");
                    Debug.WriteLine("Placeholder text");
                    return;
                }

                //Add application
                AutoHdrForce.EnableForceAutoHDR(executablePath);

                //List applications
                ListApplications(false);

                //Show status
                ShowStatusMessage("Application added");
            }
            catch { }
        }

        private void Button_Support_BrowseFiles_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "Select an executable file";
                openFileDialog.Filter = "Exe files|*.exe|Bin files|*.bin";
                if (openFileDialog.ShowDialog() == false)
                {
                    return;
                }
                else
                {
                    //Get application file path
                    string selectedFilePath = openFileDialog.FileName;
                    string selectedFileName = Path.GetFileName(selectedFilePath);

                    //Add application to registry
                    AutoHdrForce.EnableForceAutoHDR(selectedFileName);

                    //Enable application Auto HDR
                    AutoHdrPreferences.SetAppAutoHdrEnabled(selectedFilePath, true);

                    //List applications
                    ListApplications(false);

                    //Show status
                    ShowStatusMessage("Application added");
                }
            }
            catch { }
        }

        private void Button_Support_SelectProcess_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //List processes
                List<string> processSelect = [];
                var processList = AVProcess.Get_ProcessesMultiAll();
                foreach (ProcessMulti processMulti in processList)
                {
                    try
                    {
                        //Check if process is valid
                        if (!processMulti.Validate())
                        {
                            continue;
                        }

                        //Get application main window handle
                        IntPtr windowHandleMain = processMulti.WindowHandleMain();

                        //Check if application has valid main window
                        if (windowHandleMain == IntPtr.Zero)
                        {
                            continue;
                        }

                        //Add process
                        processSelect.Add(processMulti.ExeName + " (" + processMulti.Identifier + ")");
                    }
                    catch { }
                }

                //Show messagebox
                string selectedProcessString = AVMessageBox.Popup(this, "Select process", string.Empty, processSelect);

                //Get application file path
                foreach (ProcessMulti processMulti in processList)
                {
                    try
                    {
                        string matchProcessString = processMulti.ExeName + " (" + processMulti.Identifier + ")";
                        if (matchProcessString == selectedProcessString)
                        {
                            //Add application to registry
                            AutoHdrForce.EnableForceAutoHDR(processMulti.ExeName);

                            //Enable application Auto HDR
                            AutoHdrPreferences.SetAppAutoHdrEnabled(processMulti.ExePath, true);

                            //List applications
                            ListApplications(false);

                            //Show status
                            ShowStatusMessage("Application added");

                            //Return
                            return;
                        }
                    }
                    catch { }
                }
            }
            catch { }
        }

        private void Checkbox_Preferences_Enabled_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Set auto hdr enabled
                CheckBox senderCheckBox = (CheckBox)sender;
                string executablePath = (string)combobox_Preferences_Applications.SelectedItem;
                AutoHdrPreferences.SetAppAutoHdrEnabled(executablePath, (bool)senderCheckBox.IsChecked);

                //Show status
                ShowStatusMessage("App Auto HDR " + ((bool)senderCheckBox.IsChecked ? "enabled" : "disabled"));
            }
            catch { }
        }

        private void Slider_Preferences_Intensity_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                //Check if event is blocked
                if (AppVariables.BlockEvent)
                {
                    return;
                }

                //Set auto hdr intensity
                Slider senderSlider = (Slider)sender;
                string executablePath = (string)combobox_Preferences_Applications.SelectedItem;
                AutoHdrPreferences.SetAppAutoHdrIntensity(executablePath, (int)senderSlider.Value);

                //Show status
                ShowStatusMessage("App intensity set to " + (int)senderSlider.Value);

                //Fix find way to trigger Auto HDR to update in running application without having to restart it
            }
            catch { }
        }
    }
}