using ArnoldVinkStyles;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AutoHdrTool
{
    partial class MainWindow
    {
        //Handle main menu mouse/touch tapped
        async void lb_Menu_MousePressUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //Check if an actual ListBoxItem is clicked
                if (!AVInterface.ListBoxItemClickCheck((DependencyObject)e.OriginalSource))
                {
                    return;
                }

                //Check which mouse button is pressed
                await lb_Menu_SingleTap();
            }
            catch { }
        }

        //Handle main menu keyboard/controller tapped
        async void lb_Menu_KeyPressUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Space) { await lb_Menu_SingleTap(); }
            }
            catch { }
        }

        //Handle main menu single tap
        async Task lb_Menu_SingleTap()
        {
            try
            {
                if (lb_Menu.SelectedIndex >= 0)
                {
                    StackPanel SelStackPanel = (StackPanel)lb_Menu.SelectedItem;
                    if (SelStackPanel.Name == "menuButtonPreferences") { ShowGridPage(grid_Preferences); }
                    else if (SelStackPanel.Name == "menuButtonApplications") { ShowGridPage(grid_Applications); }
                    else if (SelStackPanel.Name == "menuButtonGeneral") { ShowGridPage(grid_General); }
                }
            }
            catch { }
        }

        //Display a certain grid page
        void ShowGridPage(FrameworkElement elementTarget)
        {
            try
            {
                grid_Preferences.Visibility = Visibility.Collapsed;
                grid_Applications.Visibility = Visibility.Collapsed;
                grid_General.Visibility = Visibility.Collapsed;
                elementTarget.Visibility = Visibility.Visible;
            }
            catch { }
        }
    }
}