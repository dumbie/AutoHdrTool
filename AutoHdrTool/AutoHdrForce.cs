using Microsoft.Win32;
using System.Diagnostics;

namespace AutoHdrTool
{
    public static class AutoHdrForce
    {
        //Check Auto HDR for unsupported application
        public static bool CheckForceAutoHDR(string executablePath)
        {
            try
            {
                //Set application name
                string d3DName = executablePath;

                //Open Windows registry
                using (RegistryKey regKeyCurrentUser = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32))
                {
                    //Open Microsoft subkey
                    using (RegistryKey microsoftSubKey = regKeyCurrentUser.CreateSubKey("Software\\Microsoft\\Direct3D\\" + d3DName, false))
                    {
                        string currentD3DBehaviors = microsoftSubKey.GetValue("D3DBehaviors")?.ToString();
                        if (currentD3DBehaviors.Contains("BufferUpgradeOverride=1"))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch
            {
                Debug.WriteLine("Failed to check Windows Auto HDR support for application.");
                return false;
            }
        }

        //Enable Auto HDR for unsupported application
        public static void EnableForceAutoHDR(string executablePath)
        {
            try
            {
                //Set application name
                string d3DName = executablePath;
                string d3DBehaviors = "DisableBufferUpgrade=0;BufferUpgradeOverride=1;BufferUpgradeEnable10Bit=1;";

                //Open Windows registry
                using (RegistryKey regKeyCurrentUser = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32))
                {
                    //Open Microsoft subkey
                    using (RegistryKey microsoftSubKey = regKeyCurrentUser.CreateSubKey("Software\\Microsoft\\Direct3D\\" + d3DName, true))
                    {
                        microsoftSubKey.SetValue("Name", d3DName);
                        microsoftSubKey.SetValue("D3DBehaviors", d3DBehaviors);
                    }
                }

                Debug.WriteLine("Enabled Windows Auto HDR support for: " + d3DName + "/" + d3DBehaviors);
            }
            catch
            {
                Debug.WriteLine("Failed to enable Windows Auto HDR support for application.");
            }
        }

        //Disable Auto HDR for unsupported application
        public static void DisableForceAutoHDR(string executablePath)
        {
            try
            {
                //Set application name
                string d3DName = executablePath;

                //Open Windows registry
                using (RegistryKey regKeyCurrentUser = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32))
                {
                    regKeyCurrentUser.DeleteSubKey("Software\\Microsoft\\Direct3D\\" + d3DName, false);
                }

                Debug.WriteLine("Disabled Windows Auto HDR support for: " + d3DName);
            }
            catch
            {
                Debug.WriteLine("Failed to disable Windows Auto HDR support for application.");
            }
        }
    }
}