using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace course
{
    public partial class readingForm : Form
    {
        static public readingForm Instance;
        public string htmlPath;
        public string tilteBook;
        public readingForm()
        {
            Instance = this;
            InitializeComponent();
        }

        private void readingForm_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate(htmlPath);
        }

        private void readingForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            webBrowser1.Dispose();

            string tempPath = Path.GetDirectoryName(htmlPath);
            Console.WriteLine(tempPath);
            try
            {
                foreach (string filePath in Directory.GetFiles(tempPath))
                {
                    File.Delete(filePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        private void настройкиЧиталкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            readingSettingsFrom readingSettingsFrom = new readingSettingsFrom();
            readingSettingsFrom.ShowDialog();
        }
    }
}
