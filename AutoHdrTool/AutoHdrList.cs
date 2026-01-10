using Microsoft.Win32;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AutoHdrTool
{
    public static class AutoHdrList
    {
        //List applications for preferences page
        public static List<string> List_Preferences_Applications()
        {
            List<string> list_Apps = new List<string>();
            try
            {
                //Open Windows registry
                using (RegistryKey regKeyCurrentUser = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32))
                {
                    //Create application subkey
                    using (RegistryKey applicationSubKey = regKeyCurrentUser.CreateSubKey("Software\\Microsoft\\DirectX\\UserGpuPreferences"))
                    {
                        foreach (string key in applicationSubKey.GetValueNames())
                        {
                            if (key != "DirectXUserGlobalSettings")
                            {
                                list_Apps.Add(key);
                            }
                        }
                    }
                }
            }
            catch { }
            Debug.WriteLine("Listed applications for preferences page: " + list_Apps.Count());
            return list_Apps;
        }

        //List applications for applications page
        public static List<string> List_Support_Applications()
        {
            List<string> list_Apps = new List<string>();
            try
            {
                //Open Windows registry
                using (RegistryKey regKeyCurrentUser = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32))
                {
                    //Create application subkey
                    using (RegistryKey applicationSubKey = regKeyCurrentUser.CreateSubKey("Software\\Microsoft\\Direct3D"))
                    {
                        foreach (string key in applicationSubKey.GetSubKeyNames())
                        {
                            list_Apps.Add(key);
                        }
                    }
                }
            }
            catch { }
            Debug.WriteLine("Listed applications for applications page: " + list_Apps.Count());
            return list_Apps;
        }
    }
}