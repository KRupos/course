using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace course
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        private List<IBookFormatPlugin> _plugins = new List<IBookFormatPlugin>();
        private string test;
        private string titleBook;

        private List<IBookFormatPlugin> LoadPlugins(string pluginsDirectory)
        {
            List<IBookFormatPlugin> plugins = new List<IBookFormatPlugin>();

            // Получение всех файлов .dll из папки pluginsDirectory
            string[] dllFiles = Directory.GetFiles(pluginsDirectory, "*.dll");

            foreach (string dllFile in dllFiles)
            {
                try
                {
                    // Загрузка сборки
                    Assembly assembly = Assembly.LoadFile(dllFile);
                    // Получение всех типов, реализующих интерфейс IBookFormatPlugin
                    Type[] pluginTypes = assembly.GetTypes().Where(t => typeof(IBookFormatPlugin).IsAssignableFrom(t) && !t.IsInterface).ToArray();
                    // Создание экземпляров плагинов и добавление их в список
                    foreach (Type pluginType in pluginTypes)
                    {
                        IBookFormatPlugin plugin = (IBookFormatPlugin)Activator.CreateInstance(pluginType);
                        plugins.Add(plugin);
                    }
                }
                catch (Exception ex)
                {
                    // Обработка ошибок загрузки плагина
                    Console.WriteLine($"Ошибка загрузки плагина из файла {dllFile}: {ex.Message}");
                }
            }

            return plugins;
        }

        private void mainForm_Load(object sender, EventArgs e)
        {            
            CheckIfDataExists();
        }

        private void CheckIfDataExists()
        {
            string path = Path.Combine(AppSettings.LibraryDirectory(), "meta.db");
            if (!File.Exists(path))
            {
                File.Copy(AppSettings.DefaultDBPath(), path, true);
            }
            else
            {
                LookBooksDataGridView();
            }            
        }

        private void LookBooksDataGridView()
        {
            booksDataGridView.DataSource = GetDataFromDataBase();
            //selectedRow();
        }

        private void selectedRow()
        {
            object file_path, title;
            if (booksDataGridView.SelectedRows.Count > 0)
            {
                string coverPath;
                DataGridViewRow selectedRow = booksDataGridView.SelectedRows[0];
                string columnName = "file_path";
                file_path = selectedRow.Cells[columnName].Value;
                columnName = "title";
                title = selectedRow.Cells[columnName].Value;

                if (file_path != null)
                {
                    coverPath = Path.Combine(file_path.ToString(), "cover.jpg");
                    if (File.Exists(coverPath))
                    {
                        bookPictureBox.ImageLocation = coverPath;
                    }
                    else
                    {
                        bookPictureBox.ImageLocation = AppSettings.DefaultCoverPath();
                    }
                    string textAnnotation = convertXml2Text(Path.Combine(file_path.ToString(), "info.xml"));                    
                    bookAnnTextBox.Text = textAnnotation;
                    btnReadBook.Enabled = true;
                    test = Path.Combine(file_path.ToString(), title.ToString() + ".fb2");
                    titleBook = title.ToString();
                }
            }
            else
                btnReadBook.Enabled = false;
        }

        private string convertXml2Text(string v)
        {
            XDocument doc = XDocument.Load(v);
            string result = "";
            XElement authors = doc.Root?.Element("Authors");
            XElement sequenceNumber = doc.Root?.Element("Sequence")?.Element("Item1");
            XElement sequenceName = doc.Root?.Element("Sequence")?.Element("Item2");
            XElement genre = doc.Root?.Element("Genre");
            XElement language = doc.Root?.Element("Language");
            XElement annotation = doc.Root?.Element("Annotation");

            StringBuilder sb = new StringBuilder();
            if (authors != null)
            {
                sb.AppendLine($"Автор: {authors.Value}");
            }
            if (sequenceNumber != null && sequenceName != null)
            {
                sb.AppendLine($"Серия: книга {sequenceNumber.Value} из {sequenceName.Value}");
            }
            if (genre != null)
            {
                sb.AppendLine($"Жанры: {genre.Value}");
            }
            if (language != null)
            {
                sb.AppendLine($"Язык: {language.Value}");
            }
            if (annotation != null)
            {
                sb.AppendLine();
                sb.AppendLine(annotation.Value);
            }
            result = sb.ToString().Trim();
            return result;
        }

        private DataTable GetDataFromDataBase()
        {
            DataTable dtBooks = new DataTable();
            using (SQLiteConnection conn = new SQLiteConnection(AppSettings.ConnectionString()))
            {
                string commandline = "SELECT * FROM view_library";
                using(SQLiteCommand cmd = new SQLiteCommand(commandline, conn))
                {
                    conn.Open();
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    dtBooks.Load(reader);
                }
            }
            return dtBooks;
        }       
        //COMM
        private void filtersTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            
        }
        private void FilterByAuthor(string author)
        {
            //BindingSource bindingSource = new BindingSource();
            //bindingSource.DataSource = lsBooks.Where(b => b.Author.Contains(author)).ToList();
            //booksDataGridView.DataSource = bindingSource;
        }
        private void filtersTreeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            //string selectedAuthor = e.Node.Text;
            //if (selectedAuthor == "Авторы")
            //{
            //    booksDataGridView.DataSource = lsBooks;
            //}
            //else
            //{
            //    FilterByAuthor(selectedAuthor);
            //}
        }
        //COMM
        private void добавитьКнигуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> files = new List<string>();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = AppSettings.OpenBookFilter(_plugins);
            ofd.Multiselect = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in ofd.FileNames)
                {
                    AddBook(file, _plugins);
                }
            }
            LookBooksDataGridView();
        }

        public void AddBook(string filePath, List<IBookFormatPlugin> plugins)
        {
            //Получение информации о книге
            Book book = GetBookInfo(filePath, plugins);           

            if (book != null)
            {
                if (!IsBookExists(book.Title, book.Authors))
                {
                    //Создание директории для книги
                    Translit translit = new Translit();
                    string trAuthor = translit.TranslitFileName(book.Authors);
                    string[] authors = trAuthor.Split(',').Select(a => a.Trim()).ToArray();
                    int maxAuthors = 3; // Максимальное количество авторов
                                        // Проверяем, что авторов не больше maxAuthors
                    string[] firstAuthors = authors.Length <= maxAuthors ? authors : authors.Take(maxAuthors).ToArray();
                    // Объединяем авторов в строку
                    string firstAuthorsString = string.Join(", ", firstAuthors);
                    string authorDir = Path.Combine(AppSettings.LibraryDirectory(), firstAuthorsString);
                    Directory.CreateDirectory(authorDir);

                    string trTitle = translit.TranslitFileName(book.Title);
                    string bookDir = Path.Combine(authorDir, trTitle);
                    Directory.CreateDirectory(bookDir);
                    //Копирование файла книги
                    string extension = Path.GetExtension(filePath).ToLower();
                    string destFilePath = Path.Combine(bookDir, $"{book.Title}{extension}");
                    File.Copy(filePath, destFilePath, true);
                    //Сохранение обложки(если есть)
                    string coverDestPath = Path.Combine(bookDir, "cover.jpg");
                    if (!string.IsNullOrEmpty(book.CoverImage))
                    {
                        File.Copy(book.CoverImage, coverDestPath, true);
                        File.Delete(book.CoverImage);
                        book.CoverImage = coverDestPath.Replace(book.CoverImage, coverDestPath);
                    }
                    //Сериализация информации о книге в XML
                    string infoFilePath = Path.Combine(bookDir, "info.xml");
                    SerializeBookInfo(book, infoFilePath);

                    AddBook2BD(book, bookDir);
                }
                else
                {
                    // Книга уже существует в базе данных, выполните необходимые действия
                    Console.WriteLine($"Книга '{book.Title}' уже существует в базе данных.");
                }
            }
        }

        private void AddBook2BD(Book book, string bookDir)
        {
            int bookId;
            using (SQLiteConnection conn = new SQLiteConnection(AppSettings.ConnectionString()))
            {
                string commandline = "INSERT INTO Bookshelf ([title], [size], [language], [release_year], [file_path]) " +
                    "VALUES (@title, @size, @language, @release_year, @file_path)" +
                    "SELECT last_insert_rowid();";
                using (SQLiteCommand cmd = new SQLiteCommand(commandline, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@title", book.Title);
                    cmd.Parameters.AddWithValue("@size", book.Size);
                    cmd.Parameters.AddWithValue("@language", book.Language);
                    cmd.Parameters.AddWithValue("@release_year", book.Year);
                    cmd.Parameters.AddWithValue("@file_path", bookDir);
                    cmd.ExecuteNonQuery();
                    bookId = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            AddAuthors(book.Authors, bookId);
            AddGenres(book.Genre, bookId);            
        }

        public void AddAuthors(string authorsString, int bookId)
        {
            using (SQLiteConnection conn = new SQLiteConnection(AppSettings.ConnectionString()))
            {
                conn.Open();
                // Разбиение строки авторов на отдельные имена
                var authors = authorsString.Split(',');
                foreach (var author in authors)
                {
                    var authorName = author.Trim();
                    // Проверка наличия автора в таблице Authors
                    var authorId = GetAuthorIdByName(conn, authorName);
                    if (authorId == -1)
                    {
                        // Если автор не найден, добавляем нового автора
                        authorId = InsertAuthor(conn, authorName);
                    }
                    // Связываем автора с книгой в таблице FilterAuthor
                    InsertFilterAuthor(conn, bookId, authorId);
                }
            }
        }
        private int GetAuthorIdByName(SQLiteConnection conn, string authorName)
        {
            using (var command = new SQLiteCommand("SELECT id_author FROM Authors WHERE author_fullname = @AuthorName", conn))
            {
                command.Parameters.AddWithValue("@AuthorName", authorName);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }
            }
            return -1; // Автор не найден
        }
        private int InsertAuthor(SQLiteConnection conn, string authorName)
        {
            using (var command = new SQLiteCommand("INSERT INTO Authors (author_fullname) VALUES (@AuthorName); SELECT last_insert_rowid()", conn))
            {
                command.Parameters.AddWithValue("@AuthorName", authorName);
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }
        private void InsertFilterAuthor(SQLiteConnection conn, int bookId, int authorId)
        {
            using (var command = new SQLiteCommand("INSERT INTO FilterAuthor (id_book, id_author) VALUES (@BookId, @AuthorId)", conn))
            {
                command.Parameters.AddWithValue("@BookId", bookId);
                command.Parameters.AddWithValue("@AuthorId", authorId);
                command.ExecuteNonQuery();
            }
        }

        public void AddGenres(string genressString, int bookId)
        {
            using (SQLiteConnection conn = new SQLiteConnection(AppSettings.ConnectionString()))
            {
                conn.Open();
                // Разбиение строки авторов на отдельные имена
                var genres = genressString.Split(',');
                foreach (var genre in genres)
                {
                    var genreName = genre.Trim();
                    // Проверка наличия автора в таблице Authors
                    var genreId = GetGenreIdByName(conn, genreName);
                    if (genreId == -1)
                    {
                        // Если автор не найден, добавляем нового автора
                        genreId = InsertGenre(conn, genreName);
                    }
                    // Связываем автора с книгой в таблице FilterAuthor
                    InsertFilterGenres(conn, bookId, genreId);
                }
            }
        }
        private int GetGenreIdByName(SQLiteConnection conn, string genreName)
        {
            using (var command = new SQLiteCommand("SELECT id_genre FROM Genres WHERE genre_bname = @GenreName", conn))
            {
                command.Parameters.AddWithValue("@GenreName", genreName);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }
            }
            return -1; // Автор не найден
        }
        private int InsertGenre(SQLiteConnection conn, string genreName)
        {
            GenreTrenslit translitGenre = new GenreTrenslit();
            using (var command = new SQLiteCommand("INSERT INTO Genres (genre_bname, genre_tname) VALUES (@GenreBName, @GenreTName); SELECT last_insert_rowid()", conn))
            {
                command.Parameters.AddWithValue("@GenreBName", genreName);
                command.Parameters.AddWithValue("@GenreTName", translitGenre.TranslitGenre(genreName));
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }
        private void InsertFilterGenres(SQLiteConnection conn, int bookId, int genreId)
        {
            using (var command = new SQLiteCommand("INSERT INTO FilterGenre (id_book, id_genre) VALUES (@BookId, @GenreId)", conn))
            {
                command.Parameters.AddWithValue("@BookId", bookId);
                command.Parameters.AddWithValue("@GenreId", genreId);
                command.ExecuteNonQuery();
            }
        }

        private bool IsBookExists(string title, string authors/*, string format*/)
        {
            using (SQLiteConnection conn = new SQLiteConnection(AppSettings.ConnectionString()))
            {
                string query = "SELECT COUNT(*) FROM Bookshelf WHERE title = @title AND authors = @authors"/* AND format = @format"*/;
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@authors", authors);
                    //cmd.Parameters.AddWithValue("@format", format);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        private Book GetBookInfo(string filePath, List<IBookFormatPlugin> plugins)
        {
            foreach (IBookFormatPlugin plugin in plugins)
            {
                if (plugin.CanReadFormat(filePath))
                {
                    return plugin.InfoBook(filePath);
                }
            }
            Fb2Reader br = new Fb2Reader();
            if (br.CanReadFormat(filePath))
            {   
                br.ReadBook(filePath);
                return br.InfoBook(filePath);
            }
            return null;
        }

        private void SerializeBookInfo(Book book, string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Book));
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(stream, book);
            }
        }

        private void booksDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //selectedRow();
        }

        private void btnReadBook_Click(object sender, EventArgs e)
        {
            Fb2Reader fb = new Fb2Reader();
            string text = fb.ReadBook(test);
            readingForm readingForm = new readingForm();
            readingForm.htmlPath = text;
            readingForm.Show();
        }

        private void booksDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow();
        }

        private void booksDataGridView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                // Определяем номер строки, на которой произошел щелчок
                int rowIndex = booksDataGridView.HitTest(e.X, e.Y).RowIndex;

                // Выделяем строку
                if (rowIndex >= 0)
                {
                    booksDataGridView.ClearSelection();
                    booksDataGridView.Rows[rowIndex].Selected = true;
                    //selectedRow();
                }
            }
        }

        private void читатьКнигуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Fb2Reader fb = new Fb2Reader();
            string text = fb.ReadBook(test);
            readingForm readingForm = new readingForm();
            readingForm.htmlPath = text;
            readingForm.tilteBook = titleBook;
            readingForm.Show();
        }

        private void удалитьКнигуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы уверены, что хотите удалить книгу?", "Подтверждение удаления строки", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Код для выхода из приложения
                Console.WriteLine("Пока не робит");

            }
        }
    }
}
