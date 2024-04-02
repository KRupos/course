using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace course
{
    public partial class settingsForm : Form
    {
        public settingsForm()
        {
            InitializeComponent();
        }
        private void selectedRow()
        {
            object plugin_name;
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                string columnName = "name";
                plugin_name = selectedRow.Cells[columnName].Value;
            }
        }
        List<PluginInfo> pluginInfos;
        private void settingsForm_Load(object sender, EventArgs e)
        {
            //pluginInfos = PluginInfo.GetPluginsInfo();
            dataGridView1.DataSource = pluginInfos;
            dataGridView1.Columns["Version"].HeaderText = "Версия";
            dataGridView1.Columns["libPath"].HeaderText = "Путь к плагину";
            dataGridView1.Columns["Developer"].HeaderText = "Разработчик";
            dataGridView1.Columns["PluginDescription"].HeaderText = "Описание плагина";
            dataGridView1.Columns["FunctionDescription"].HeaderText = "Описание функции";
            dataGridView1.Columns["State"].HeaderText = "Состояние";

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true;
            updateForm();

        }

        private void btnFolderBrowser_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Выберите папку";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.PluginDirPath = dialog.SelectedPath;
                }
                updateForm();
            }
        }

        private void updateForm()
        {
            if (Properties.Settings.Default.PluginDirPath == "Default")
            {
                textBox1.Text = "./plugins";
            }
            else
            {
                textBox1.Text = Properties.Settings.Default.PluginDirPath;
            }
        }
    }
}
