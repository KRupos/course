namespace course
{
    partial class mainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Авторы");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Язык");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Формат");
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.добавитьКнигуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкаПрограммыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкаЧтенияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.booksFolderBD = new System.Windows.Forms.FolderBrowserDialog();
            this.bookInfoPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnReadBook = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.bookPictureBox = new System.Windows.Forms.PictureBox();
            this.bookAnnTextBox = new System.Windows.Forms.RichTextBox();
            this.filtersTreeView = new System.Windows.Forms.TreeView();
            this.booksDataGridView = new System.Windows.Forms.DataGridView();
            this.file_path = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AuthorsBook = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Size = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.release_year = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.genres = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.formats = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Language = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStripDataGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.читатьКнигуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьКнигуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bdBooksSource = new System.Windows.Forms.BindingSource(this.components);
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.MainMenu.SuspendLayout();
            this.bookInfoPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bookPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.booksDataGridView)).BeginInit();
            this.contextMenuStripDataGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdBooksSource)).BeginInit();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.настройкиToolStripMenuItem,
            this.справкаToolStripMenuItem});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(1264, 24);
            this.MainMenu.TabIndex = 0;
            this.MainMenu.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьКнигуToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // добавитьКнигуToolStripMenuItem
            // 
            this.добавитьКнигуToolStripMenuItem.Name = "добавитьКнигуToolStripMenuItem";
            this.добавитьКнигуToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.добавитьКнигуToolStripMenuItem.Text = "Добавить книги";
            this.добавитьКнигуToolStripMenuItem.Click += new System.EventHandler(this.добавитьКнигуToolStripMenuItem_Click);
            // 
            // настройкиToolStripMenuItem
            // 
            this.настройкиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.настройкаПрограммыToolStripMenuItem,
            this.настройкаЧтенияToolStripMenuItem});
            this.настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
            this.настройкиToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.настройкиToolStripMenuItem.Text = "Настройки";
            // 
            // настройкаПрограммыToolStripMenuItem
            // 
            this.настройкаПрограммыToolStripMenuItem.Name = "настройкаПрограммыToolStripMenuItem";
            this.настройкаПрограммыToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.настройкаПрограммыToolStripMenuItem.Text = "Настройка программы";
            // 
            // настройкаЧтенияToolStripMenuItem
            // 
            this.настройкаЧтенияToolStripMenuItem.Name = "настройкаЧтенияToolStripMenuItem";
            this.настройкаЧтенияToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.настройкаЧтенияToolStripMenuItem.Text = "Настройка чтения";
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.оПрограммеToolStripMenuItem});
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.справкаToolStripMenuItem.Text = "Справка";
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.оПрограммеToolStripMenuItem.Text = "О программе";
            // 
            // bookInfoPanel
            // 
            this.bookInfoPanel.Controls.Add(this.panel1);
            this.bookInfoPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.bookInfoPanel.Location = new System.Drawing.Point(898, 24);
            this.bookInfoPanel.Name = "bookInfoPanel";
            this.bookInfoPanel.Size = new System.Drawing.Size(366, 657);
            this.bookInfoPanel.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(366, 657);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnReadBook);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 630);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(366, 27);
            this.panel2.TabIndex = 2;
            // 
            // btnReadBook
            // 
            this.btnReadBook.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnReadBook.Enabled = false;
            this.btnReadBook.Location = new System.Drawing.Point(0, 0);
            this.btnReadBook.Name = "btnReadBook";
            this.btnReadBook.Size = new System.Drawing.Size(366, 27);
            this.btnReadBook.TabIndex = 0;
            this.btnReadBook.Text = "Читать книгу";
            this.btnReadBook.UseVisualStyleBackColor = true;
            this.btnReadBook.Click += new System.EventHandler(this.btnReadBook_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.bookPictureBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.bookAnnTextBox);
            this.splitContainer1.Size = new System.Drawing.Size(366, 657);
            this.splitContainer1.SplitterDistance = 381;
            this.splitContainer1.TabIndex = 1;
            // 
            // bookPictureBox
            // 
            this.bookPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bookPictureBox.Location = new System.Drawing.Point(0, 0);
            this.bookPictureBox.Margin = new System.Windows.Forms.Padding(10);
            this.bookPictureBox.Name = "bookPictureBox";
            this.bookPictureBox.Size = new System.Drawing.Size(366, 381);
            this.bookPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bookPictureBox.TabIndex = 0;
            this.bookPictureBox.TabStop = false;
            // 
            // bookAnnTextBox
            // 
            this.bookAnnTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bookAnnTextBox.Location = new System.Drawing.Point(0, 0);
            this.bookAnnTextBox.Name = "bookAnnTextBox";
            this.bookAnnTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.bookAnnTextBox.Size = new System.Drawing.Size(366, 272);
            this.bookAnnTextBox.TabIndex = 1;
            this.bookAnnTextBox.Text = "";
            // 
            // filtersTreeView
            // 
            this.filtersTreeView.CheckBoxes = true;
            this.filtersTreeView.Dock = System.Windows.Forms.DockStyle.Left;
            this.filtersTreeView.Location = new System.Drawing.Point(0, 24);
            this.filtersTreeView.Name = "filtersTreeView";
            treeNode1.Name = "authorFilter";
            treeNode1.Text = "Авторы";
            treeNode2.Name = "languageFilter";
            treeNode2.Text = "Язык";
            treeNode3.Name = "formatFilter";
            treeNode3.Text = "Формат";
            this.filtersTreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            this.filtersTreeView.Size = new System.Drawing.Size(249, 657);
            this.filtersTreeView.TabIndex = 2;
            this.filtersTreeView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.filtersTreeView_AfterCheck);
            this.filtersTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.filtersTreeView_AfterSelect);
            // 
            // booksDataGridView
            // 
            this.booksDataGridView.AllowUserToAddRows = false;
            this.booksDataGridView.AllowUserToDeleteRows = false;
            this.booksDataGridView.AllowUserToResizeRows = false;
            this.booksDataGridView.AutoGenerateColumns = false;
            this.booksDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.booksDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.file_path,
            this.title,
            this.AuthorsBook,
            this.Size,
            this.release_year,
            this.genres,
            this.formats,
            this.Language});
            this.booksDataGridView.ContextMenuStrip = this.contextMenuStripDataGrid;
            this.booksDataGridView.DataSource = this.bdBooksSource;
            this.booksDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.booksDataGridView.Location = new System.Drawing.Point(249, 24);
            this.booksDataGridView.Name = "booksDataGridView";
            this.booksDataGridView.ReadOnly = true;
            this.booksDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.booksDataGridView.Size = new System.Drawing.Size(649, 657);
            this.booksDataGridView.TabIndex = 3;
            this.booksDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.booksDataGridView_CellContentClick);
            this.booksDataGridView.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.booksDataGridView_RowEnter);
            this.booksDataGridView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.booksDataGridView_MouseDown);
            // 
            // file_path
            // 
            this.file_path.DataPropertyName = "file_path";
            this.file_path.HeaderText = "";
            this.file_path.Name = "file_path";
            this.file_path.ReadOnly = true;
            this.file_path.Visible = false;
            // 
            // title
            // 
            this.title.DataPropertyName = "title";
            this.title.HeaderText = "Название";
            this.title.Name = "title";
            this.title.ReadOnly = true;
            // 
            // AuthorsBook
            // 
            this.AuthorsBook.DataPropertyName = "authors";
            this.AuthorsBook.HeaderText = "Автор(ы)";
            this.AuthorsBook.Name = "AuthorsBook";
            this.AuthorsBook.ReadOnly = true;
            this.AuthorsBook.Width = 121;
            // 
            // Size
            // 
            this.Size.DataPropertyName = "size";
            this.Size.HeaderText = "Размер (МБ)";
            this.Size.Name = "Size";
            this.Size.ReadOnly = true;
            this.Size.Width = 122;
            // 
            // release_year
            // 
            this.release_year.DataPropertyName = "ryear";
            this.release_year.HeaderText = "Год выхода";
            this.release_year.Name = "release_year";
            this.release_year.ReadOnly = true;
            this.release_year.Width = 121;
            // 
            // genres
            // 
            this.genres.DataPropertyName = "genres";
            this.genres.HeaderText = "Жанр(ы)";
            this.genres.Name = "genres";
            this.genres.ReadOnly = true;
            // 
            // formats
            // 
            this.formats.DataPropertyName = "formats";
            this.formats.HeaderText = "Формат(ы)";
            this.formats.Name = "formats";
            this.formats.ReadOnly = true;
            // 
            // Language
            // 
            this.Language.DataPropertyName = "lang";
            this.Language.HeaderText = "Язык";
            this.Language.Name = "Language";
            this.Language.ReadOnly = true;
            this.Language.Width = 121;
            // 
            // contextMenuStripDataGrid
            // 
            this.contextMenuStripDataGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.читатьКнигуToolStripMenuItem,
            this.удалитьКнигуToolStripMenuItem});
            this.contextMenuStripDataGrid.Name = "contextMenuStripDataGrid";
            this.contextMenuStripDataGrid.Size = new System.Drawing.Size(153, 48);
            // 
            // читатьКнигуToolStripMenuItem
            // 
            this.читатьКнигуToolStripMenuItem.Name = "читатьКнигуToolStripMenuItem";
            this.читатьКнигуToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.читатьКнигуToolStripMenuItem.Text = "Читать книгу";
            this.читатьКнигуToolStripMenuItem.Click += new System.EventHandler(this.читатьКнигуToolStripMenuItem_Click);
            // 
            // удалитьКнигуToolStripMenuItem
            // 
            this.удалитьКнигуToolStripMenuItem.Name = "удалитьКнигуToolStripMenuItem";
            this.удалитьКнигуToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.удалитьКнигуToolStripMenuItem.Text = "Удалить книгу";
            this.удалитьКнигуToolStripMenuItem.Click += new System.EventHandler(this.удалитьКнигуToolStripMenuItem_Click);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(895, 24);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 657);
            this.splitter1.TabIndex = 4;
            this.splitter1.TabStop = false;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.booksDataGridView);
            this.Controls.Add(this.filtersTreeView);
            this.Controls.Add(this.bookInfoPanel);
            this.Controls.Add(this.MainMenu);
            this.MainMenuStrip = this.MainMenu;
            this.Name = "mainForm";
            this.Text = "Библиотека ReadEscape";
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.bookInfoPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bookPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.booksDataGridView)).EndInit();
            this.contextMenuStripDataGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bdBooksSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem добавитьКнигуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem настройкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog booksFolderBD;
        private System.Windows.Forms.ToolStripMenuItem настройкаПрограммыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem настройкаЧтенияToolStripMenuItem;
        private System.Windows.Forms.Panel bookInfoPanel;
        private System.Windows.Forms.TreeView filtersTreeView;
        private System.Windows.Forms.DataGridView booksDataGridView;
        private System.Windows.Forms.RichTextBox bookAnnTextBox;
        private System.Windows.Forms.PictureBox bookPictureBox;
        private System.Windows.Forms.BindingSource bdBooksSource;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnReadBook;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripDataGrid;
        private System.Windows.Forms.ToolStripMenuItem читатьКнигуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьКнигуToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn file_path;
        private System.Windows.Forms.DataGridViewTextBoxColumn title;
        private System.Windows.Forms.DataGridViewTextBoxColumn AuthorsBook;
        private System.Windows.Forms.DataGridViewTextBoxColumn Size;
        private System.Windows.Forms.DataGridViewTextBoxColumn release_year;
        private System.Windows.Forms.DataGridViewTextBoxColumn genres;
        private System.Windows.Forms.DataGridViewTextBoxColumn formats;
        private System.Windows.Forms.DataGridViewTextBoxColumn Language;
        private System.Windows.Forms.Splitter splitter1;
    }
}

