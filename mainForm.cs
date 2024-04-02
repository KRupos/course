using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
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
        static public mainForm Instance;
        public mainForm()
        {
            Instance = this;
            InitializeComponent();
        }

        public List<IBookFormatPlugin> _plugins = new List<IBookFormatPlugin>();
        private string test;
        private string titleBook;

        private void LicenseManager_FoundVerificatedKey(object sender, EventArgs e)
        {
            if (DateTime.Parse(LicenseManager.currentKeyInfo.ExpandedDate) > DateTime.Now)
            {
                mainForm.Instance.Invoke(new Action(() => mainForm.Instance.Text = "ReadEscape (Full verison)"));
            }
            else
            {
                mainForm.Instance.Invoke(new Action(() => mainForm.Instance.Text = "ReadEscape (Trial verison)"));
            }
        }

        private void UsedKeyRemoved(object sender, EventArgs args)
        {
            mainForm.Instance.Invoke(new Action(() => mainForm.Instance.Text = "ReadEscape (Trial verison)"));
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            LicenseManager.UsedKeyRemoved += UsedKeyRemoved;
            LicenseManager.FoundVerificatedKey += LicenseManager_FoundVerificatedKey; ;

            LicenseManager.RefreshDevices();
            LicenseManager.TryFindKey();

            if (DateTime.Parse(LicenseManager.currentKeyInfo.ExpandedDate) > DateTime.Now)
            {
                mainForm.Instance.Text = "ReadEscape (Full verison)";
            }
            else
            {
                mainForm.Instance.Text = "ReadEscape (Trial verison)";
            }
            PluginInfo pluginInfo = new PluginInfo();
            _plugins = pluginInfo.LoadPlugins(AppSettings.pluginsDirectory());

            Console.WriteLine(AppSettings.OpenBookFilter(_plugins));

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
            selectedRow();
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
                if (!IsBookExists(book.Title, book.Authors, book.Format))
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
                    string pathBook = book.LocalPath;
                    if (IsBookExistsButFormatMissing(book.Title, book.Authors, book.Format))
                    {
                        // Копирование файла книги в новый формат
                        string extension = Path.GetExtension(filePath).ToLower();
                        string destFilePath = Path.Combine(pathBook, $"{book.Title} ({book.Format}){extension}");
                        File.Copy(filePath, destFilePath, true);

                        // Обновление информации о книге в XML
                        string infoFilePath = Path.Combine(pathBook, "info.xml");
                        Book existingBook = DeserializeBookInfo(infoFilePath);
                        SerializeBookInfo(existingBook, infoFilePath);
                    }
                    else
                    {
                        // Книга уже существует в базе данных, выполните необходимые действия
                        Console.WriteLine($"Книга '{book.Title}' уже существует в базе данных.");
                    }                    
                }
            }
        }

        private Book DeserializeBookInfo(string infoFilePath)
        {
            throw new NotImplementedException();
        }

        private void AddBook2BD(Book book, string bookDir)
        {
            int bookId;
            using (SQLiteConnection conn = new SQLiteConnection(AppSettings.ConnectionString()))
            {
                string commandline = "INSERT INTO Bookshelf ([book_title], [book_size], [book_lang], [book_ryear], [book_file_path]) " +
                    "VALUES (@title, @size, @language, @release_year, @file_path);";

                using (SQLiteCommand cmd = new SQLiteCommand(commandline, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@title", book.Title);
                    cmd.Parameters.AddWithValue("@size", book.Size);
                    cmd.Parameters.AddWithValue("@language", book.Language);
                    cmd.Parameters.AddWithValue("@release_year", book.Year);
                    cmd.Parameters.AddWithValue("@file_path", bookDir);
                    cmd.ExecuteNonQuery();
                }
                string selectline = "SELECT last_insert_rowid();";
                using (SQLiteCommand cmd = new SQLiteCommand(selectline, conn))
                {
                    bookId = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            AddAuthors(book.Authors, bookId);
            AddGenres(book.Genre, bookId);
            AddBookFormat(book.Format, bookId);
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

        public void AddBookFormat(string formatExtension, int bookId)
        {
            using (SQLiteConnection conn = new SQLiteConnection(AppSettings.ConnectionString()))
            {
                conn.Open();
                int formatId = GetFormatIdByExtension(conn, formatExtension);
                if (formatId != -1)
                {
                    InsertFilterFormat(conn, bookId, formatId);
                }
                else
                {
                    Console.WriteLine($"Формат с расширением {formatExtension} не найден в базе данных.");
                }
            }
        }
        private int GetFormatIdByExtension(SQLiteConnection conn, string formatExtension)
        {
            using (SQLiteCommand command = new SQLiteCommand("SELECT id_format FROM Formats WHERE format_extention = @FormatExtension", conn))
            {
                command.Parameters.AddWithValue("@FormatExtension", formatExtension);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }
            }

            return -1; // Формат не найден
        }
        private void InsertFilterFormat(SQLiteConnection conn, int bookId, int formatId)
        {
            using (SQLiteCommand command = new SQLiteCommand("INSERT INTO FilterFormat (id_book, id_format) VALUES (@BookId, @FormatId)", conn))
            {
                command.Parameters.AddWithValue("@BookId", bookId);
                command.Parameters.AddWithValue("@FormatId", formatId);
                command.ExecuteNonQuery();
            }
        }

        private bool IsBookExists(string title, string authors, string format)
        {
            using (SQLiteConnection conn = new SQLiteConnection(AppSettings.ConnectionString()))
            {
                conn.Open();
                string query = @"SELECT COUNT(*) FROM Bookshelf b 
                                INNER JOIN FilterAuthor fa ON b.id_book = fa.id_book 
                                INNER JOIN Authors a ON fa.id_author = a.id_author 
                                INNER JOIN FilterFormat ff ON b.id_book = ff.id_book 
                                INNER JOIN Formats f ON ff.id_format = f.id_format 
                                WHERE b.book_title = @title AND a.author_fullname = @authors AND f.format_name = @format 
                                GROUP BY b.id_book;";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@authors", authors);
                    cmd.Parameters.AddWithValue("@format", format);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        private bool IsBookExistsButFormatMissing(string title, string authors, string format)
        {
            using (SQLiteConnection conn = new SQLiteConnection(AppSettings.ConnectionString()))
            {
                conn.Open();

                string query = @"SELECT COUNT(*) 
                                FROM Bookshelf b
                                INNER JOIN FilterAuthor fa ON b.id_book = fa.id_book
                                INNER JOIN Authors a ON fa.id_author = a.id_author
                                WHERE b.book_title = @title
                                AND a.author_fullname = @authors
                                AND NOT EXISTS (
                                    SELECT 1
                                    FROM FilterFormat ff
                                    INNER JOIN Formats f ON ff.id_format = f.id_format
                                    WHERE ff.id_book = b.id_book
                                    AND f.format_name = @format
                                )
                                GROUP BY b.id_book;";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@authors", authors);
                    cmd.Parameters.AddWithValue("@format", format);

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
            selectedRow();
        }

        private void btnReadBook_Click(object sender, EventArgs e)
        {
            Fb2Reader fb = new Fb2Reader();
            string text = fb.ReadBook(test);
            readingForm readingForm = new readingForm();
            readingForm.htmlPath = text;
            readingForm.Instance.Text = "Чтение " + titleBook;
            readingForm.Show();
            readingForm.Shown += ReadingForm_Shown;
            readingForm.Closed += ReadingForm_Closed;
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
                    selectedRow();
                }
            }
        }

        private void читатьКнигуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Fb2Reader fb = new Fb2Reader();
            string text = fb.ReadBook(test);
            readingForm readingForm = new readingForm();
            readingForm.htmlPath = text;
            readingForm.Instance.Text = "Чтение " + titleBook;
            readingForm.Show();
            readingForm.Shown += ReadingForm_Shown;
            readingForm.Closed += ReadingForm_Closed;
        }

        private int openReadingFormCount = 0;
        private void ReadingForm_Closed(object sender, EventArgs e)
        {
            openReadingFormCount--;
            if (openReadingFormCount == 0)
            {
                удалитьКнигуToolStripMenuItem.Enabled = true;
            }
        }
        private void ReadingForm_Shown(object sender, EventArgs e)
        {
            openReadingFormCount++;
            if (openReadingFormCount > 0)
            {
                удалитьКнигуToolStripMenuItem.Enabled = false;
            }
        }

        private void удалитьКнигуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы уверены, что хотите удалить книгу?", "Подтверждение удаления строки", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Код для выхода из приложения
                string pathBook = Path.GetDirectoryName(test);
                Console.WriteLine(pathBook);
                int bookId = GetBookIdByFilePath(pathBook);
                if (bookId != -1)
                {
                    // Книга найдена, bookId содержит ее идентификатор
                    DeleteBook(bookId);
                    LookBooksDataGridView();
                }
                else
                {
                    // Книга не найдена
                    MessageBox.Show("Книга не найдена в базе данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public int GetBookIdByFilePath(string filePath)
        {
            int bookId = -1;
            using (SQLiteConnection conn = new SQLiteConnection(AppSettings.ConnectionString()))
            {
                conn.Open();
                string query = "SELECT id_book FROM Bookshelf WHERE book_file_path = @FilePath";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FilePath", filePath);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        bookId = Convert.ToInt32(result);
                    }
                }
            }
            return bookId;
        }
        public void DeleteBook(int bookId)
        {
            using (SQLiteConnection conn = new SQLiteConnection(AppSettings.ConnectionString()))
            {
                conn.Open();
                SQLiteTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Получение информации о книге
                    string bookDirPath = GetBookDirectoryPath(conn, bookId, transaction);
                    if (string.IsNullOrEmpty(bookDirPath))
                    {
                        throw new Exception("Не удалось получить путь к директории книги.");
                    }
                    // Удаление записи из таблицы Bookshelf
                    DeleteFromBookshelf(conn, bookId, transaction);
                    // Удаление связей с авторами, жанрами и форматами
                    DeleteBookRelations(conn, bookId, transaction);
                    // Удаление файлов книги
                    DeleteBookFiles(bookDirPath);
                    // Удаление авторов и жанров, если они не используются в других книгах
                    DeleteUnusedAuthorsAndGenres(conn, bookId, transaction);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Ошибка при удалении книги: " + ex.Message);
                }
            }
        }
        private string GetBookDirectoryPath(SQLiteConnection conn, int bookId, SQLiteTransaction transaction)
        {
            string query = "SELECT book_file_path FROM Bookshelf WHERE id_book = @BookId";
            using (SQLiteCommand cmd = new SQLiteCommand(query, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@BookId", bookId);
                object result = cmd.ExecuteScalar();
                return result != null ? result.ToString() : string.Empty;
            }
        }
        private void DeleteFromBookshelf(SQLiteConnection conn, int bookId, SQLiteTransaction transaction)
        {
            string query = "DELETE FROM Bookshelf WHERE id_book = @BookId";
            using (SQLiteCommand cmd = new SQLiteCommand(query, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@BookId", bookId);
                cmd.ExecuteNonQuery();
            }
        }
        private void DeleteBookRelations(SQLiteConnection conn, int bookId, SQLiteTransaction transaction)
        {
            string query = "DELETE FROM FilterAuthor WHERE id_book = @BookId; " +
                           "DELETE FROM FilterGenre WHERE id_book = @BookId; " +
                           "DELETE FROM FilterFormat WHERE id_book = @BookId;";
            using (SQLiteCommand cmd = new SQLiteCommand(query, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@BookId", bookId);
                cmd.ExecuteNonQuery();
            }
        }
        private void DeleteBookFiles(string bookDirPath)
        {
            if (Directory.Exists(bookDirPath))
            {
                Directory.Delete(bookDirPath, true);
            }
        }
        private void DeleteUnusedAuthorsAndGenres(SQLiteConnection conn, int bookId, SQLiteTransaction transaction)
        {
            string deleteUnusedAuthorsQuery = "DELETE FROM Authors WHERE id_author NOT IN (SELECT id_author FROM FilterAuthor GROUP BY id_author)";
            string deleteUnusedGenresQuery = "DELETE FROM Genres WHERE id_genre NOT IN (SELECT id_genre FROM FilterGenre GROUP BY id_genre)";

            using (SQLiteCommand cmd = new SQLiteCommand(deleteUnusedAuthorsQuery, conn, transaction))
            {
                cmd.ExecuteNonQuery();
            }

            using (SQLiteCommand cmd = new SQLiteCommand(deleteUnusedGenresQuery, conn, transaction))
            {
                cmd.ExecuteNonQuery();
            }
        }

        private void настройкаПрограммыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settingsForm settingsForm = new settingsForm();
            settingsForm.ShowDialog();
        }

        private void лицензияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LicenseKeyManagerForm licenseForm = new LicenseKeyManagerForm();
            licenseForm.ShowDialog();
        }

        private void настройкаЧтенияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            readingSettingsFrom readingSettingsFrom = new readingSettingsFrom();
            readingSettingsFrom.ShowDialog();
        }
    }
}
