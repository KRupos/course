using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace course
{
    public partial class LicenseKeyManagerForm : Form, IDisposable
    {
        static public LicenseKeyManagerForm Instance;
        public LicenseKeyManagerForm()
        {
            InitializeComponent();
            Instance = this;

            if (LicenseManager.currentKeyInfo.Version == "Trial")
            {
                textBox_AppVersion.Text = "Trial";
                mainForm.Instance.Text = "ReadEscape (Trial verison)";
                dateTimePicker1.Value = DateTime.Now;
            }
            else
            {
                textBox_AppVersion.Text = LicenseManager.currentKeyInfo.Version;
                textBox_FlashInfo.Text = LicenseManager.currentDeviceInfo.Flash_Company + " " + LicenseManager.currentDeviceInfo.Flash_Name + " " + LicenseManager.currentDeviceInfo.Flash_SerialNumber;
                dateTimePicker1.Value = DateTime.Parse(LicenseManager.currentKeyInfo.ExpandedDate);
                if (DateTime.Parse(LicenseManager.currentKeyInfo.ExpandedDate) > DateTime.Now)
                {
                    mainForm.Instance.Text = "ReadEscape (Full verison)";
                }
            }

            LicenseManager.UsedKeyRemoved += LicenseKeyManagerForm_UsedKeyRemoved;
            LicenseManager.FoundVerificatedKey += LicenseManager_FoundVerificatedKey;

            this.FormClosed += (s, e) => Dispose_form();
        }


        private void LicenseManager_FoundVerificatedKey(object sender, EventArgs e)
        {
            textBox_AppVersion?.Invoke(new Action(() => textBox_AppVersion.Text = LicenseManager.currentKeyInfo.Version));
            textBox_FlashInfo?.Invoke(new Action(() => textBox_FlashInfo.Text = LicenseManager.currentDeviceInfo.Flash_Company + " " + LicenseManager.currentDeviceInfo.Flash_Name + " " + LicenseManager.currentDeviceInfo.Flash_SerialNumber));
            dateTimePicker1?.Invoke(new Action(() => dateTimePicker1.Value = DateTime.Parse(LicenseManager.currentKeyInfo.ExpandedDate)));
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now;
            textBox_AppVersion.Text = "Trial";
            mainForm.Instance.Text = "ReadEscape (Trial verison)";

            LicenseManager.RefreshDevices();
            LicenseManager.TryFindKey();
        }

        private void LicenseKeyManagerForm_UsedKeyRemoved(object sender, EventArgs e)
        {
            ClearForm();
        }

        public void Dispose_form()
        {
            LicenseManager.UsedKeyRemoved -= LicenseKeyManagerForm_UsedKeyRemoved;
            LicenseManager.FoundVerificatedKey -= LicenseManager_FoundVerificatedKey;
        }
        public void ClearForm()
        {
            textBox_AppVersion?.Invoke(new Action(() => textBox_AppVersion.Text = ""));
            textBox_FlashInfo?.Invoke(new Action(() => textBox_FlashInfo.Text = ""));
            dateTimePicker1?.Invoke(new Action(() => dateTimePicker1.Value = DateTime.Now));
        }

        private void ShowLogicalDevicesInfo()
        {
            ManagementObjectCollection collection;
            using (var searcher = new ManagementObjectSearcher(@"SELECT * FROM Win32_LogicalDisk"))
                collection = searcher.Get();
            foreach (var device in collection)
            {
                if ((string)device.GetPropertyValue("Description") == "Съемный диск")
                {
                    textBox1.Text += "1 " + (string)device.GetPropertyValue("Caption") + "\r\n";
                    textBox1.Text += "2 " + (string)device.GetPropertyValue("CreationClassName") + "\r\n";
                    textBox1.Text += "3 " + (string)device.GetPropertyValue("Description") + "\r\n";
                    textBox1.Text += "4 " + (string)device.GetPropertyValue("DeviceID") + "\r\n";             //Equal Caption
                    textBox1.Text += "5 " + (string)device.GetPropertyValue("ErrorDescription") + "\r\n";
                    textBox1.Text += "6 " + (string)device.GetPropertyValue("ErrorMethodology") + "\r\n";
                    textBox1.Text += "7 " + (string)device.GetPropertyValue("FileSystem") + "\r\n";
                    textBox1.Text += "8 " + (string)device.GetPropertyValue("Name") + "\r\n";                 //Equal Caption
                    textBox1.Text += "9 " + (string)device.GetPropertyValue("PNPDeviceID") + "\r\n";
                    textBox1.Text += "10 " + (string)device.GetPropertyValue("ProviderName") + "\r\n";
                    textBox1.Text += "11 " + (string)device.GetPropertyValue("Purpose") + "\r\n";
                    textBox1.Text += "12 " + (string)device.GetPropertyValue("Status") + "\r\n";
                    textBox1.Text += "13 " + (string)device.GetPropertyValue("SystemCreationClassName") + "\r\n";
                    textBox1.Text += "14 " + (string)device.GetPropertyValue("SystemName") + "\r\n";
                    textBox1.Text += "15 " + (string)device.GetPropertyValue("VolumeName") + "\r\n";
                    textBox1.Text += "16 " + (string)device.GetPropertyValue("VolumeSerialNumber") + "\r\n";

                    textBox1.Text += "\r\n";
                    textBox1.Text += "\r\n";
                }
            }
        }
        private void ShowDiskDriveDevicesInfo()
        {
            ManagementObjectCollection collection;
            using (var searcher = new ManagementObjectSearcher(@"SELECT * FROM Win32_DiskDrive WHERE InterfaceType='USB'"))
                collection = searcher.Get();

            foreach (var device in collection)
            {
                textBox1.Text += "1 " + (string)device.GetPropertyValue("PNPDeviceID") + "\r\n";
                textBox1.Text += "2 " + (string)device.GetPropertyValue("Caption") + "\r\n";
                textBox1.Text += "3 " + (string)device.GetPropertyValue("CompressionMethod") + "\r\n";
                textBox1.Text += "4 " + (string)device.GetPropertyValue("CreationClassName") + "\r\n";
                textBox1.Text += "5 " + (string)device.GetPropertyValue("Description") + "\r\n";
                textBox1.Text += "6 " + (string)device.GetPropertyValue("DeviceID") + "\r\n";
                textBox1.Text += "7 " + (string)device.GetPropertyValue("ErrorDescription") + "\r\n";
                textBox1.Text += "8 " + (string)device.GetPropertyValue("ErrorMethodology") + "\r\n";
                textBox1.Text += "9 " + (string)device.GetPropertyValue("FirmwareRevision") + "\r\n";
                textBox1.Text += "11 " + (string)device.GetPropertyValue("InterfaceType") + "\r\n";
                textBox1.Text += "12 " + (string)device.GetPropertyValue("Manufacturer") + "\r\n";
                textBox1.Text += "13 " + (string)device.GetPropertyValue("MediaType") + "\r\n";
                textBox1.Text += "14 " + (string)device.GetPropertyValue("Model") + "\r\n";
                textBox1.Text += "15 " + (string)device.GetPropertyValue("Name") + "\r\n";
                textBox1.Text += "16 " + (string)device.GetPropertyValue("PNPDeviceID") + "\r\n";

                textBox1.Text += "17 " + (string)device.GetPropertyValue("SerialNumber") + "\r\n";

                string tmp = LicenseManager.GetDriveLetterAndLabelFromID((string)device.GetPropertyValue("SerialNumber"));
                if (tmp != null)
                {
                    textBox1.Text += tmp + "\r\n";
                }

                textBox1.Text += "18 " + (string)device.GetPropertyValue("Status") + "\r\n";
                textBox1.Text += "19 " + (string)device.GetPropertyValue("SystemCreationClassName") + "\r\n";
                textBox1.Text += "20 " + (string)device.GetPropertyValue("SystemName") + "\r\n";

                textBox1.Text += "\r\n";
                textBox1.Text += "\r\n";

            }
        }
        private void ShowDiskPartitionInfo()
        {
            ManagementObjectCollection collection;
            using (var searcher = new ManagementObjectSearcher(@"SELECT * FROM Win32_DiskPartition"))
                collection = searcher.Get();

            foreach (var device in collection)
            {
                textBox1.Text += "1 " + ((string[])device.GetPropertyValue("IdentifyingDescriptions"))[0] + "\r\n";
                textBox1.Text += "2 " + (string)device.GetPropertyValue("Caption") + "\r\n";
                textBox1.Text += "3 " + (string)device.GetPropertyValue("Description") + "\r\n";
                textBox1.Text += "4 " + (string)device.GetPropertyValue("DeviceID") + "\r\n";
                textBox1.Text += "5 " + (string)device.GetPropertyValue("ErrorMethodology") + "\r\n";
                textBox1.Text += "6 " + (string)device.GetPropertyValue("ErrorDescription") + "\r\n";
                textBox1.Text += "7 " + (string)device.GetPropertyValue("Name") + "\r\n";
                textBox1.Text += "8 " + (string)device.GetPropertyValue("PNPDeviceID") + "\r\n";
                textBox1.Text += "9 " + (string)device.GetPropertyValue("Purpose") + "\r\n";
                textBox1.Text += "10 " + (string)device.GetPropertyValue("Status") + "\r\n";
                textBox1.Text += "11 " + (string)device.GetPropertyValue("SystemCreationClassName") + "\r\n";
                textBox1.Text += "12 " + (string)device.GetPropertyValue("SystemName") + "\r\n";
                textBox1.Text += "13 " + (string)device.GetPropertyValue("Type") + "\r\n";
                textBox1.Text += "14 " + (string)device.GetPropertyValue("CreationClassName") + "\r\n";

                textBox1.Text += "\r\n";
                textBox1.Text += "\r\n";
            }
        }

    }
    static public class LicenseManager
    {
        static public readonly string keyName = "ReadEscapeKey.txt";
        static public event EventHandler UsedKeyRemoved;
        static public event EventHandler FoundVerificatedKey;
        static LicenseManager()
        {
            activeFullVersion = false;


            if (currentKeyInfo == null)
            {
                currentKeyInfo = new KeyInfo();
                currentKeyInfo.Version = "Trial";
                currentKeyInfo.ExpandedDate = DateTime.Now.ToString();
            }

            UsedKeyRemoved += LicenseKeyManagerForm_UsedKeyRemoved;

            RefreshDevices();

            USBObserver.DeviceInserted += USBObserver_DeviceInserted;
            USBObserver.OnDeviceRemoved += USBObserver_OnDeviceRemoved;

            TryFindKey();

        }
        static public string XorDecode(string source, string key)
        {
            char C;

            string res = "";
            for (int i = 0; i < (source.Length / 2); i++)
            {
                try
                {
                    C = (char)(Convert.ToInt32(source.Substring((i * 2), 2), 16));
                }
                catch
                {
                    C = (char)32;
                }

                if (key.Length > 0)
                {
                    C = (char)((byte)(key[(i % key.Length)]) ^ (byte)(C));
                }
                res += C;
            }

            return res;
        }
        static public string GetMD5Hash(string input)
        {
            var md5 = MD5.Create();

            byte[] encodedPassword = new UTF8Encoding().GetBytes(input);

            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);

            // string representation (similar to UNIX format)
            string encoded = BitConverter.ToString(hash)
               // without dashes
               .Replace("-", string.Empty)
               // make lowercase
               .ToLower();

            return encoded;
        }
        static public string GetDriveLetterAndLabelFromID(string id)
        {
            ManagementClass devs = new ManagementClass(@"Win32_Diskdrive");
            {
                ManagementObjectCollection moc = devs.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    string a = (string)mo["SerialNumber"];
                    if (a == id)
                    {
                        foreach (ManagementObject b in mo.GetRelated("Win32_DiskPartition"))
                        {
                            foreach (ManagementBaseObject c in b.GetRelated("Win32_LogicalDisk"))
                            {
                                //HardDrive name, HardDrive letter
                                string result = $"{c["VolumeName"]};{c["DeviceID"]}";
                                return result;
                            }
                        }
                    }
                }
            }
            return null;
        }
        static public void TryFindKey()
        {
            foreach (var device in devices)
            {
                var directoryInfo = new DirectoryInfo(device.Letter);
                var files = directoryInfo.GetFiles("*.txt");

                //LicenseKeyManagerForm.Instance.textBox1.Invoke(new Action(() =>
                //{
                //    LicenseKeyManagerForm.Instance.textBox1.Text += "================================================================\r\n";
                //    LicenseKeyManagerForm.Instance.textBox1.Text += "DISK : " + device.Letter + " " + device.Flash_PNPDeviceID + "\r\n";
                //    LicenseKeyManagerForm.Instance.textBox1.Text += "================================================================\r\n";
                //}
                //));
                for (int i = 0; i < files.Length; i++)
                {
                    if (Path.GetFileName(files[i].FullName) == keyName)
                    {
                        LicenseKeyManagerForm.Instance.textBox1.Invoke(new Action(() =>
                        {
                            LicenseKeyManagerForm.Instance.textBox1.Text += files[i].FullName + "\r\n";
                        }));

                        KeyInfo keyInfo;
                        string message;
                        if (TryVerificateKey(device, File.ReadAllText(files[i].FullName), out keyInfo, out message) == true)
                        {
                            currentKeyInfo = keyInfo;
                            currentDeviceInfo = device;

                            if (keyInfo.Version == "Full" && DateTime.Parse(keyInfo.ExpandedDate) > DateTime.Now)
                            {
                                activeFullVersion = true;
                            }
                            else
                            {
                                activeFullVersion = false;
                            }

                            FoundVerificatedKey?.Invoke(null, null);

                            break;
                        }
                        else
                        {
                            LicenseKeyManagerForm.Instance.textBox1.Invoke(new Action(() =>
                            {
                                LicenseKeyManagerForm.Instance.textBox1.Text += device.Flash_Company + " " + device.Flash_Name + " " + device.Flash_SerialNumber;
                                LicenseKeyManagerForm.Instance.textBox1.Text += message + "\r\n";
                            }));
                        }
                    }
                }
            }
        }
        static private List<USBDeviceInfo> devices = new List<USBDeviceInfo>();
        static public void RefreshDevices()
        {
            devices = GetDevices();

           // LicenseKeyManagerForm.Instance.textBox1.Invoke(new Action(() =>
           //{
           //    LicenseKeyManagerForm.Instance.textBox1.Text = string.Empty;
           //    foreach (var device in devices)
           //    {
           //        LicenseKeyManagerForm.Instance.textBox1.Text += device.Flash_Company + " " + device.Flash_Name + " " + device.Flash_SerialNumber + "\r\n";
           //        LicenseKeyManagerForm.Instance.textBox1.Text += device.Letter + " " + device.Name + "\r\n";
           //    }
           //}));
        }
        static public List<USBDeviceInfo> GetDevices()
        {
            List<USBDeviceInfo> tmpDevices = new List<USBDeviceInfo>();

            ManagementObjectCollection collection;
            using (var searcher = new ManagementObjectSearcher(@"SELECT * FROM Win32_DiskDrive WHERE InterfaceType='USB'"))
                collection = searcher.Get();
            foreach (var device in collection)
            {
                tmpDevices.Add(new USBDeviceInfo((string)device.GetPropertyValue("PNPDeviceID"), (string)device.GetPropertyValue("SerialNumber")));
            }

            return tmpDevices;
        }
        static private void USBObserver_OnDeviceRemoved(object sender, string e)
        {
            RefreshDevices();
            if (currentDeviceInfo != null)
            {
                if (devices.Find(p => p.Flash_PNPDeviceID == currentDeviceInfo.Flash_PNPDeviceID) == null)
                {
                    currentDeviceInfo = null;

                    UsedKeyRemoved?.Invoke(null, null);
                }
            }
        }
        static private void USBObserver_DeviceInserted(object sender, string e)
        {
            RefreshDevices();

            if (currentDeviceInfo == null || DateTime.Parse(currentKeyInfo.ExpandedDate) <= DateTime.Now)
            {
                TryFindKey();
            }
        }
        static private void LicenseKeyManagerForm_UsedKeyRemoved(object sender, EventArgs e)
        {
            activeFullVersion = false;
            currentKeyInfo = new KeyInfo();
            currentKeyInfo.Version = "Trial";
            currentKeyInfo.ExpandedDate = DateTime.Now.ToString();
        }
        static public bool TryVerificateKey(USBDeviceInfo uSBDeviceInfo, string text, out KeyInfo keyInfo, out string message)
        {
            message = null;
            keyInfo = null;
            string flashParams = uSBDeviceInfo.Flash_Company + uSBDeviceInfo.Flash_Name + uSBDeviceInfo.Flash_SerialNumber;

            string md5Flash = GetMD5Hash(flashParams);

            string decodedFile = XorDecode(text, md5Flash);

            string[] data = decodedFile.Split('\n');
            if (data.Length != 2)
            {
                message = "Не удалось дешифровать ключ" + "\r\n";
                return false;
            }
            else
            {
                string md5 = GetMD5Hash(data[1]);

                if ((md5.ToLower()) != (data[0].ToLower().Trim()))
                {
                    message = "Ключ был подделан.";
                    return false;
                }
                else
                {
                    string decodedData = XorDecode(data[1], md5Flash);
                    try
                    {
                        keyInfo = JsonConvert.DeserializeObject<KeyInfo>(decodedData);

                        return true;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }
        }

        static public class USBObserver
        {
            static public event EventHandler<string> OnDeviceRemoved;
            static public event EventHandler<string> DeviceInserted;

            static private void DeviceInsertedEvent(object sender, EventArrivedEventArgs e)
            {
                ManagementBaseObject instance = (ManagementBaseObject)e.NewEvent["TargetInstance"];
                string PNPDeviceID = (string)instance.Properties["PNPDeviceID"].Value;
                DeviceInserted?.Invoke(null, PNPDeviceID);
            }

            static private void DeviceRemovedEvent(object sender, EventArrivedEventArgs e)
            {
                ManagementBaseObject instance = (ManagementBaseObject)e.NewEvent["TargetInstance"];
                string PNPDeviceID = (string)instance.Properties["PNPDeviceID"].Value;
                OnDeviceRemoved?.Invoke(null, PNPDeviceID);
            }
            static USBObserver()
            {
                WqlEventQuery insertQuery = new WqlEventQuery("SELECT * FROM __InstanceCreationEvent WITHIN 2 WHERE TargetInstance ISA 'Win32_DiskDrive'");

                ManagementEventWatcher insertWatcher = new ManagementEventWatcher(insertQuery);
                insertWatcher.EventArrived += new EventArrivedEventHandler(DeviceInsertedEvent);
                insertWatcher.Start();

                WqlEventQuery removeQuery = new WqlEventQuery("SELECT * FROM __InstanceDeletionEvent WITHIN 2 WHERE TargetInstance ISA 'Win32_DiskDrive'");
                ManagementEventWatcher removeWatcher = new ManagementEventWatcher(removeQuery);
                removeWatcher.EventArrived += new EventArrivedEventHandler(DeviceRemovedEvent);
                removeWatcher.Start();
            }
        }
        public class KeyInfo
        {
            public string UserName;
            public string OrganisationName;
            public string ExpandedDate;
            public string FlashDescription;
            public string FlashVedden;
            public string FlashName;
            public string FlashSerialNumber;
            public string Version;
        }
        static public bool activeFullVersion = false;
        static public KeyInfo currentKeyInfo;
        static public USBDeviceInfo currentDeviceInfo;
    }
    public class USBDeviceInfo
    {
        public USBDeviceInfo() { }
        public USBDeviceInfo(string PNPDeviceID, string SerialNumber)
        {
            this.Flash_PNPDeviceID = PNPDeviceID;

            string[] data = PNPDeviceID.Split('&');
            this.Flash_Company = data[1].Substring(4, data[1].Length - 4);

            if (string.IsNullOrEmpty(this.Flash_Company))
            {
                this.Flash_Company = "Unknown";
            }

            this.Flash_Name = data[2].Substring(5, data[2].Length - 5);
            this.Flash_SerialNumber = data[3].Split('\\')[1];

            string letterAndName = LicenseManager.GetDriveLetterAndLabelFromID(SerialNumber);
            if (letterAndName != null)
            {
                data = letterAndName.Split(';');
                this.Name = data[0];
                this.Letter = data[1];
            }

            //ADATA USB Flash Drive USB Device
            //USBSTOR\DISK&VEN_ADATA&PROD_USB_FLASH_DRIVE&REV_1100\29C131316005014F&0
            //
            //UFD 2.0 Silicon-Power16G USB Device
            //USBSTOR\DISK&VEN_UFD_2.0&PROD_SILICON-POWER16G&REV_1100\1400839803700164&0

            //Производитель: UFD_2.0   Название: SILICON - POWER16G   Серийник: 1400839803700164
        }
        public string Flash_Company;
        public string Flash_Name;
        public string Flash_SerialNumber;
        public string Flash_PNPDeviceID;
        public string Name;
        public string Letter;
    }
}
