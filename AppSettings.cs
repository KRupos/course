﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course
{
    public class AppSettings
    {
        public static string ConnectionString()        
        {            
            return "Data Source=" + LibraryDirectory() + "\\meta.db;Version=3";
        }
        public static string OpenBookFilter(List<IBookFormatPlugin> plugins)
        {
            StringBuilder sb = new StringBuilder();
            string booksFormat = "Книги формата FB2 | *.fb2";
            sb.Append(booksFormat);
            foreach (IBookFormatPlugin plugin in plugins)
            {
                if (!string.IsNullOrEmpty(plugin.FilterFormat()))
                {
                    sb.Append("|" + plugin.FilterFormat());
                }
            }
            sb.Append("|Все файлы (*.*)|*.*");
            return sb.ToString();
        }

        public static string DefaultCoverPath()
        {
            return "D:\\CodeData\\course\\locale\\DefaultCover.jpg";
        }

        public static string DefaultDBPath()
        {
            return "D:\\CodeData\\course\\locale\\meta.db";
        }

        public static string LibraryDirectory()
        {
            string libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Библиотека ReadEscape";
            if (!Directory.Exists(libraryPath))
            {
                Directory.CreateDirectory(libraryPath);
            }
            return libraryPath;
        }
    }
}
