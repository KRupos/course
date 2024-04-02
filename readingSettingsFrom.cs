using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace course
{
    public partial class readingSettingsFrom : Form
    {
        public readingSettingsFrom()
        {
            
            InitializeComponent();
        }

        private void Black_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void Gray_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Sepia_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Lite_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void readingSettingsFrom_Load(object sender, EventArgs e)
        {
            Lite.Checked = true;
        }
    }
}
