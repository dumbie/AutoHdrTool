using Microsoft.Win32;
using System.Diagnostics;

namespace AutoHdrTool
{
    public static class AutoHdrNotification
    {
        //Set Auto HDR notification enabled
        public static void SetAutoHdrNotificationEnabled(bool targetEnabled)
        {
            try
            {
                //Open Windows registry
                using (RegistryKey regKeyCurrentUser = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32))
                {
                    //Open Microsoft subkey
                    using (RegistryKey microsoftSubKey = regKeyCurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Notifications\\Settings\\Windows.SystemToast.Graphics.AutoHDR", true))
                    {
                        //Get notification value
                        int enabledValue = targetEnabled ? 1 : 0;

                        //Set notification value
                        microsoftSubKey.SetValue("Enabled", enabledValue);
                    }
                }

                Debug.WriteLine("Set Auto HDR notification enabled: " + targetEnabled);
            }
            catch
            {
                Debug.WriteLine("Failed to set Auto HDR notification enabled.");
            }
        }
    }
}