using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace course
{
    public interface IBookFormatPlugin
    {
        bool CanReadFormat(string filePath);
        Book InfoBook(string filePath);
        string ReadBook(string filePath);
        string FilterFormat();
    }
}
