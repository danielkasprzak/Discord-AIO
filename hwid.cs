using System.Management;

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
            ManagementObjectSearcher mbs = new ManagementObjectSearcher("Select * From Win32_processor");
            ManagementObjectCollection mbsList = mbs.Get();
            string id = "";
            foreach (ManagementObject mo in mbsList)
            {
                id = mo["ProcessorID"]?.ToString() ?? string.Empty;
            }
            return id;
        }
        public static string getDiskC()
        {
            ManagementObject dsk = new ManagementObject(@"win32_logicaldisk.deviceid=""c:""");
            dsk.Get();
            string id = dsk["VolumeSerialNumber"]?.ToString() ?? string.Empty;
            return id;
        }
    }
}