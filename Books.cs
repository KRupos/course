using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course
{   
    
    public class Book
    {
        public string Title { get; set; }
        public string Authors { get; set; }
        public string Language { get; set; }
        public DateTime AddedDate { get; set; }
        public double Size { get; set; }
        public string LocalPath { get; set; }
        public string Genre { get; set; }
        public string Annotation { get; set; }
        public string CoverImage { get; set; }
        public string Year { get; set; }
        public (int Number, string Name) Sequence { get; set; }
        public string Format {  get; set; }

        public Book(string title, string authors, string language, 
            DateTime addedDate, double size, string localPath, 
            string genre, string annotation, string year)
        {
            Title = title;
            Authors = authors;
            Language = language;
            AddedDate = addedDate;
            Size = size;
            LocalPath = localPath;
            Genre = genre;
            Annotation = annotation;
            Year = year;
        }
        
        public Book() { }
    }
}
