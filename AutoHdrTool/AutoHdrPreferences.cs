using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AutoHdrTool
{
    public static class AutoHdrPreferences
    {
        //Get application Auto HDR enabled
        public static bool GetAppAutoHdrEnabled(string executablePath)
        {
            bool result = false;
            try
            {
                //Open Windows registry
                using (RegistryKey regKeyCurrentUser = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32))
                {
                    //Open Microsoft subkey
                    using (RegistryKey microsoftSubKey = regKeyCurrentUser.CreateSubKey("Software\\Microsoft\\DirectX\\UserGpuPreferences", true))
                    {
                        //Get app settings value
                        string appSettingsString = microsoftSubKey.GetValue(executablePath)?.ToString();

                        //Get auto hdr enable
                        Match regex = Regex.Match(appSettingsString, @"AutoHDREnable=(.*?);", RegexOptions.Singleline);
                        int enableValue = Convert.ToInt32(regex.Groups[1].Value);
                        result = enableValue > 0;
                    }
                }

                Debug.WriteLine("Get application Auto HDR enabled: " + executablePath + " / " + result);
                return result;
            }
            catch
            {
                Debug.WriteLine("Failed to get application Auto HDR enabled.");
                return result;
            }
        }

        //Set application Auto HDR enabled
        public static void SetAppAutoHdrEnabled(string executablePath, bool targetEnabled)
        {
            try
            {
                //Open Windows registry
                using (RegistryKey regKeyCurrentUser = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32))
                {
                    //Open Microsoft subkey
                    using (RegistryKey microsoftSubKey = regKeyCurrentUser.CreateSubKey("Software\\Microsoft\\DirectX\\UserGpuPreferences", true))
                    {
                        //Get app settings value
                        string appSettingsString = microsoftSubKey.GetValue(executablePath)?.ToString();
                        string hdrEnabledValue = targetEnabled ? "1" : "0";

                        //Set auto hdr intensity
                        if (!string.IsNullOrWhiteSpace(appSettingsString) && appSettingsString.Contains("AutoHDREnable"))
                        {
                            appSettingsString = Regex.Replace(appSettingsString, @"AutoHDREnable=(.*?);", "AutoHDREnable=" + hdrEnabledValue + ";");
                        }
                        else
                        {
                            appSettingsString = "AutoHDREnable=" + hdrEnabledValue + ";" + appSettingsString;
                        }
                        microsoftSubKey.SetValue(executablePath, appSettingsString);
                    }
                }

                Debug.WriteLine("Set application Auto HDR enabled: " + executablePath + "/" + targetEnabled);
            }
            catch
            {
                Debug.WriteLine("Failed to set application Auto HDR enabled.");
            }
        }

        //Get application Auto HDR intensity
        public static int GetAppAutoHdrIntensity(string executablePath)
        {
            int result = -1;
            try
            {
                //Open Windows registry
                using (RegistryKey regKeyCurrentUser = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32))
                {
                    //Open Microsoft subkey
                    using (RegistryKey microsoftSubKey = regKeyCurrentUser.CreateSubKey("Software\\Microsoft\\DirectX\\UserGpuPreferences", true))
                    {
                        //Get app settings value
                        string appSettingsString = microsoftSubKey.GetValue(executablePath)?.ToString();

                        //Get auto hdr intensity
                        Match regex = Regex.Match(appSettingsString, @"AutoHDRStrength=(.*?);", RegexOptions.Singleline);
                        result = Convert.ToInt32(regex.Groups[1].Value);
                    }
                }

                Debug.WriteLine("Get application Auto HDR intensity: " + executablePath + " / " + result);
                return result;
            }
            catch
            {
                Debug.WriteLine("Failed to get application Auto HDR intensity.");
                return result;
            }
        }

        //Set application Auto HDR intensity
        public static void SetAppAutoHdrIntensity(string executablePath, int targetIntensity)
        {
            try
            {
                //Open Windows registry
                using (RegistryKey regKeyCurrentUser = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32))
                {
                    //Open Microsoft subkey
                    using (RegistryKey microsoftSubKey = regKeyCurrentUser.CreateSubKey("Software\\Microsoft\\DirectX\\UserGpuPreferences", true))
                    {
                        //Get app settings value
                        string appSettingsString = microsoftSubKey.GetValue(executablePath)?.ToString();

                        //Set auto hdr intensity
                        if (!string.IsNullOrWhiteSpace(appSettingsString) && appSettingsString.Contains("AutoHDRStrength"))
                        {
                            appSettingsString = Regex.Replace(appSettingsString, @"AutoHDRStrength=(.*?);", "AutoHDRStrength=" + targetIntensity + ";");
                        }
                        else
                        {
                            appSettingsString = "AutoHDRStrength=" + targetIntensity + ";" + appSettingsString;
                        }
                        microsoftSubKey.SetValue(executablePath, appSettingsString);
                    }
                }

                Debug.WriteLine("Set application Auto HDR intensity: " + executablePath + "/" + targetIntensity);
            }
            catch
            {
                Debug.WriteLine("Failed to set application Auto HDR intensity.");
            }
        }

        //Enable Windows Auto HDR feature
        public static void EnableWindowsAutoHDRFeature()
        {
            try
            {
                //Open Windows registry
                using (RegistryKey regKeyCurrentUser = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32))
                {
                    //Open Microsoft subkey
                    using (RegistryKey microsoftSubKey = regKeyCurrentUser.CreateSubKey("Software\\Microsoft\\DirectX\\UserGpuPreferences", true))
                    {
                        //Get global settings value
                        string globalSettingsString = microsoftSubKey.GetValue("DirectXUserGlobalSettings")?.ToString();

                        //Set global settings value
                        if (!string.IsNullOrWhiteSpace(globalSettingsString))
                        {
                            if (globalSettingsString.Contains("AutoHDREnable=0"))
                            {
                                globalSettingsString = globalSettingsString.Replace("AutoHDREnable=0", "AutoHDREnable=1");
                            }
                            if (globalSettingsString.Contains("SwapEffectUpgradeEnable=0"))
                            {
                                globalSettingsString = globalSettingsString.Replace("SwapEffectUpgradeEnable=0", "SwapEffectUpgradeEnable=1");
                            }

                            if (!globalSettingsString.Contains("AutoHDREnable"))
                            {
                                globalSettingsString = "AutoHDREnable=1;" + globalSettingsString;
                            }
                            if (!globalSettingsString.Contains("SwapEffectUpgradeEnable"))
                            {
                                globalSettingsString = "SwapEffectUpgradeEnable=1;" + globalSettingsString;
                            }
                            microsoftSubKey.SetValue("DirectXUserGlobalSettings", globalSettingsString);
                        }
                        else
                        {
                            microsoftSubKey.SetValue("DirectXUserGlobalSettings", "AutoHDREnable=1;SwapEffectUpgradeEnable=1;");
                        }
                    }
                }

                Debug.WriteLine("Enabled Windows Auto HDR feature.");
            }
            catch
            {
                Debug.WriteLine("Failed to enable Windows Auto HDR feature.");
            }
        }

        //Disable Windows Auto HDR feature
        public static void DisableWindowsAutoHDRFeature()
        {
            try
            {
                //Open Windows registry
                using (RegistryKey regKeyCurrentUser = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32))
                {
                    //Open Microsoft subkey
                    using (RegistryKey microsoftSubKey = regKeyCurrentUser.CreateSubKey("Software\\Microsoft\\DirectX\\UserGpuPreferences", true))
                    {
                        //Get global settings value
                        string globalSettingsString = microsoftSubKey.GetValue("DirectXUserGlobalSettings")?.ToString();

                        //Set global settings value
                        if (!string.IsNullOrWhiteSpace(globalSettingsString))
                        {
                            if (globalSettingsString.Contains("AutoHDREnable=1"))
                            {
                                globalSettingsString = globalSettingsString.Replace("AutoHDREnable=1", "AutoHDREnable=0");
                                microsoftSubKey.SetValue("DirectXUserGlobalSettings", globalSettingsString);
                            }
                        }
                    }
                }

                Debug.WriteLine("Disabled Windows Auto HDR feature.");
            }
            catch
            {
                Debug.WriteLine("Failed to disable Windows Auto HDR feature.");
            }
        }
    }
}