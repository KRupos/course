--
-- Файл сгенерирован с помощью SQLiteStudio v3.4.4 в Чт мар 21 17:21:07 2024
--
-- Использованная кодировка текста: UTF-8
--
PRAGMA foreign_keys = off;
BEGIN TRANSACTION;

-- Таблица: Authors
CREATE TABLE IF NOT EXISTS Authors (id_author INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, author_fullname TEXT NOT NULL UNIQUE);

-- Таблица: Bookshelf
CREATE TABLE IF NOT EXISTS Bookshelf (id_book INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, book_title TEXT NOT NULL, book_size REAL NOT NULL, book_lang TEXT, book_ryear TEXT, book_file_path TEXT NOT NULL);

-- Таблица: FilterAuthor
CREATE TABLE IF NOT EXISTS FilterAuthor (id_book INTEGER NOT NULL REFERENCES Bookshelf (id_book), id_author INTEGER REFERENCES Authors (id_author) NOT NULL);

-- Таблица: FilterFormat
CREATE TABLE IF NOT EXISTS FilterFormat (id_book INTEGER NOT NULL REFERENCES Bookshelf (id_book), id_format INTEGER REFERENCES Formats (id_format) NOT NULL);

-- Таблица: FilterGenre
CREATE TABLE IF NOT EXISTS FilterGenre (id_book INTEGER NOT NULL REFERENCES Bookshelf (id_book), id_genre INTEGER REFERENCES Genres (id_genre) NOT NULL);

-- Таблица: Formats
CREATE TABLE IF NOT EXISTS Formats (id_format INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, format_name TEXT UNIQUE NOT NULL, format_extention TEXT UNIQUE NOT NULL);

-- Таблица: Genres
CREATE TABLE IF NOT EXISTS Genres (id_genre INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, genre_bname TEXT NOT NULL, genre_tname TEXT);

-- Представление: view_library
CREATE VIEW IF NOT EXISTS view_library AS SELECT
    b.id_book,
    b.book_title,
    b.book_size,
    b.book_lang,
    b.book_ryear,
    b.book_file_path,
    GROUP_CONCAT(DISTINCT a.author_fullname) AS authors,
    GROUP_CONCAT(DISTINCT g.genre_tname) AS genres,
    GROUP_CONCAT(DISTINCT f.format_name) AS formats
FROM
    Bookshelf b
LEFT JOIN
    FilterAuthor fa ON b.id_book = fa.id_book
LEFT JOIN
    Authors a ON fa.id_author = a.id_author
LEFT JOIN
    FilterGenre fg ON b.id_book = fg.id_book
LEFT JOIN
    Genres g ON fg.id_genre = g.id_genre
LEFT JOIN
    FilterFormat ff ON b.id_book = ff.id_book
LEFT JOIN
    Formats f ON ff.id_format = f.id_format
GROUP BY
    b.id_book;

COMMIT TRANSACTION;
PRAGMA foreign_keys = on;
