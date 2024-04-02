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
    public partial class readingForm : Form
    {
        public string htmlPath;
        public string tilteBook;
        public readingForm()
        {
            InitializeComponent();

        }

        private void readingForm_Load(object sender, EventArgs e)
        {
            
            this.Text = "Чтение " + tilteBook;
            webBrowser1.Navigate(htmlPath);
        }
    }
}
