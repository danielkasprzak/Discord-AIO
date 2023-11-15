using System.Management;
using System;

namespace discord_aio_release
{
    internal class HWID
    {
        public static string getHWID()
        {
            string cDiskID = getDiskC();
            string processorID = getProcessor();
            string compiledID = cDiskID + processorID;
            return compiledID;
        }
        public static string getProcessor()
        {
            ManagementObjectCollection mbsList = null;
            ManagementObjectSearcher mbs = new ManagementObjectSearcher("Select * From Win32_processor");
            mbsList = mbs.Get();
            string id = "";
            foreach (ManagementObject mo in mbsList)
            {
                id = mo["ProcessorID"].ToString();
            }
            return id;
        }
        public static string getDiskC()
        {
            ManagementObject dsk = new ManagementObject(@"win32_logicaldisk.deviceid=""c:""");
            dsk.Get();
            string id = dsk["VolumeSerialNumber"].ToString();
            return id;
        }
    }
}