using System;
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
        public static string OpenBookFilter()
        {
            string booksFormat = "fb2 files | *.fb2";
            return booksFormat;
        }

        public static string DefaultCoverPath()
        {
            return "D:\\CodeData\\course\\locale\\DefaultCover.jpg";
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
