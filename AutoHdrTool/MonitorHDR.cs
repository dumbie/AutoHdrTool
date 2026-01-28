using System;
using System.Linq;
using System.Windows.Forms;
using static ArnoldVinkCode.AVDisplayMonitor;

namespace AutoHdrTool
{
    public static class MonitorHDR
    {
        //Enable or disable monitor HDR
        public static void MonitorHDREnabled(bool enableHDR)
        {
            try
            {
                //Switch hdr for all monitors
                int screenCount = Screen.AllScreens.Count();
                for (int i = 0; i < screenCount; i++)
                {
                    SetMonitorHDR(i, enableHDR);
                }
            }
            catch { }
        }
    }
}