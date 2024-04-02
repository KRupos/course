using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace course
{
    public class Fb2Reader : IBookFormatPlugin
    {
        public Fb2Reader() { }

        public bool CanReadFormat(string filePath)
        {
            if (Path.GetExtension(filePath).ToLower() == ".fb2")
            {
                XDocument doc = XDocument.Load(filePath);
                XElement fictionBook = doc.Root;
                if (fictionBook != null && fictionBook.Name.LocalName == "FictionBook")
                {
                    // Проверяем параметр xmlns
                    XNamespace ns = fictionBook.Name.Namespace;
                    if (ns == "http://www.gribuser.ru/xml/fictionbook/2.0")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private readonly XNamespace fbNS = "http://www.gribuser.ru/xml/fictionbook/2.0";
        private readonly XNamespace xlinkNs = "http://www.w3.org/1999/xlink";

        public Book InfoBook(string filePath)
        {
            var namespaceManager = new XmlNamespaceManager(new NameTable());
            namespaceManager.AddNamespace("fb", fbNS.ToString());


            XDocument doc = XDocument.Load(filePath);
            Book book = new Book();
            book.CoverImage = GetBookCover(doc, filePath);
            book.Title = GetBookTitle(doc, Path.GetFileName(filePath));
            book.Authors = GetBookAuthors(doc);
            book.Genre = GetBookGenres(doc);
            book.Year = GetBookYear(doc);
            book.Language = GetBookLanguage(doc);
            book.AddedDate = File.GetCreationTime(filePath);
            book.Size = GetFileSizeInMB(filePath);
            book.Annotation = GetBookAnnotation(doc);
            book.Sequence = GetBookSequence(doc);
            book.Format = Path.GetExtension(filePath);

            return book;
        }

        private (int Number, string Name) GetBookSequence(XDocument doc)
        {
            XElement sequenceElement = doc.Root?.Element(fbNS + "description")?.Element(fbNS + "title-info")?.Element(fbNS + "sequence");

            // Extract number (default to 1 if not found)
            int number = 1;
            string name = "";
            if (sequenceElement != null)
            {
                string numberString = sequenceElement.Attribute("number")?.Value;
                if (!int.TryParse(numberString, out number))
                {
                    number = 1; // Set default if parsing fails
                }
                // Extract name (empty string if not found)
                name = sequenceElement.Attribute("name")?.Value ?? string.Empty;
            }
            else
            {
                number = 0;
                name = null;
            }

            return (number, name);
        }

        private string GetBookYear(XDocument doc)
        {
            XElement yearElement = doc.Root?.Element(fbNS + "description")?.Element(fbNS + "title-info")?.Element(fbNS + "date");
            string dateString = yearElement?.Value ?? string.Empty;
            string year;
            if (!string.IsNullOrEmpty(dateString))
            {
                year = dateString.Substring(0, 4);
            }
            else
            {
                year = string.Empty;
            }
            ;
            return year;
        }

        private string GetBookCover(XDocument doc, string filePath)
        {
            string pattern = @"cover\.\w+\b";
            var coverElement = doc.Root?.Elements(fbNS + "binary").FirstOrDefault(e => Regex.IsMatch(e.Attribute("id")?.Value, pattern));

            if (coverElement != null)
            {
                string base64Image = coverElement.Value;
                byte[] imageBytes = Convert.FromBase64String(base64Image);                
                File.WriteAllBytes(Path.GetDirectoryName(filePath) + "cover.jpg", imageBytes);
                return Path.GetDirectoryName(filePath) + "cover.jpg";
            }
            else 
                return null;
        }

        private string GetBookTitle(XDocument doc, string fileName)
        {
            XElement titleElement = doc.Root?.Element(fbNS + "description")?.Element(fbNS + "title-info")?.Element(fbNS + "book-title");
            string title;
            if (!string.IsNullOrWhiteSpace(titleElement?.Value))
                title = titleElement?.Value.Trim();
            else
                title = Path.GetFileNameWithoutExtension(fileName);
            return title;
        }

        private string GetBookAuthors(XDocument doc)
        {
            var authors = doc.Root?.Element(fbNS + "description")?.Element(fbNS + "title-info")?.Elements(fbNS + "author");
            var authorNames = new List<string>();
            foreach (var author in authors ?? Enumerable.Empty<XElement>())
            {
                var firstName = author.Element(fbNS + "first-name")?.Value.Trim() ?? string.Empty;
                var middleName = author.Element(fbNS + "middle-name")?.Value.Trim() ?? string.Empty;
                var lastName = author.Element(fbNS + "last-name")?.Value.Trim() ?? string.Empty;
                if (!string.IsNullOrWhiteSpace(firstName) || !string.IsNullOrWhiteSpace(middleName) || !string.IsNullOrWhiteSpace(lastName))
                {
                    if (string.IsNullOrWhiteSpace(middleName))
                    {
                        authorNames.Add($"{firstName} {lastName}".Trim());
                    }
                    else
                    {
                        authorNames.Add($"{firstName} {middleName} {lastName}".Trim());
                    }
                    
                }
            }
            return authorNames.Any() ? string.Join(", ", authorNames) : "Неизвестный";
        }

        private string GetBookGenres(XDocument doc)
        {
            var genreElements = doc.Root?.Element(fbNS + "description")?.Element(fbNS + "title-info")?.Elements(fbNS + "genre");
            return string.Join(", ", genreElements?.Select(x => x.Value) ?? new string[0]);
        }

        private string GetBookLanguage(XDocument doc)
        {
            XElement langElement = doc.Root?.Element(fbNS + "description")?.Element(fbNS + "title-info")?.Element(fbNS + "lang");
            return langElement?.Value ?? string.Empty;
        }

        private string GetBookAnnotation(XDocument doc)
        {
            XElement bodyElement = doc.Root?.Element(fbNS + "description")?.Element(fbNS + "title-info")?.Element(fbNS + "annotation");
            return bodyElement?.Value ?? string.Empty;
        }

        private double GetFileSizeInMB(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            return Math.Round(fileInfo.Length / 1048576.0, 2);
        }

        public string ReadBook(string filePath)
        {
            var namespaceManager = new XmlNamespaceManager(new NameTable());
            namespaceManager.AddNamespace("fb", fbNS.ToString());
            XDocument doc = XDocument.Load(filePath);

            XElement bodyElement = doc.Root?.Element(fbNS + "body");
            GetImageAttributes(bodyElement);

            var atrs = bodyElement?.Element(fbNS + "image")?.Attributes();

            if (atrs != null)
            {
                foreach (var attr in atrs)
                {
                    Console.WriteLine($"Атрибут: {attr.Name} = {attr.Value}");
                }
            }
            string path = Path.Combine(Path.GetDirectoryName(filePath), "temp");
            Directory.CreateDirectory(path);

            var imagesElements = doc.Root?.Elements(fbNS + "binary");

            foreach (var imageElement in imagesElements)
            {
                string base64Image = imageElement.Value;
                string imageId = imageElement.Attribute("id").Value;
                byte[] imageBytes = Convert.FromBase64String(base64Image);                
                File.WriteAllBytes(Path.Combine(path,imageId), imageBytes);
            }

            string bodyText = Converting(filePath);

            return bodyText;
        }

        private static void GetImageAttributes(XElement bodyElement)
        {
            GetImageAttributesRecursive(bodyElement);
        }

        private static void GetImageAttributesRecursive(XElement element)
        {
            if (element.Name.LocalName == "image")
            {
                var attributes = element.Attributes();
                if (attributes != null)
                {
                    foreach (var attr in attributes)
                    {
                        Console.WriteLine($"Атрибут: {attr.Name} = {attr.Value}");
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                foreach (var child in element.Elements())
                {
                    GetImageAttributesRecursive(child);
                }
            }
        }

        private string Converting(string inputFilePath)
        {
            XDocument doc = XDocument.Load(inputFilePath);
            StringBuilder html = new StringBuilder();

            html.AppendLine("<!DOCTYPE html>");
            html.AppendLine("<html>");
            html.AppendLine("<head>");
            html.AppendLine("<link rel=\"stylesheet\" href=\"" + AppSettings.DefaultCSSPath() + "\">");
            html.AppendLine("<meta charset=\"utf-8\">");
            html.AppendLine("</head>");
            html.AppendLine("<body>");

            ConvertBody(doc.Root.Element(fbNS + "body"), html);

            html.AppendLine("</body>");
            html.AppendLine("</html>");
            File.WriteAllText(Path.GetDirectoryName(inputFilePath) + "\\temp\\test.html", html.ToString());
            string path = Path.GetDirectoryName(inputFilePath) + "\\temp\\test.html";
            return path;
        }

        private void ConvertBody(XElement bodyElement, StringBuilder html)
        {
            foreach (var node in bodyElement.Nodes())
            {
                switch (node.NodeType)
                {
                    case XmlNodeType.Element:
                        ConvertElement((XElement)node, html);
                        break;
                    case XmlNodeType.Text:
                        html.Append(((XText)node).Value);
                        break;
                }
            }
        }

        private void ConvertElement(XElement element, StringBuilder html)
        {
            switch (element.Name.LocalName)
            {
                case "p":
                    html.AppendLine("<p>" + ConvertChildren(element) + "</p>");
                    break;
                case "v":
                    html.AppendLine("<p>" + ConvertChildren(element) + "</p>");
                    break;
                case "title":
                    html.AppendLine("<h1>" + ConvertChildren(element) + "</h1>");
                    break;
                case "epigraph":
                    html.AppendLine("<blockquote>" + ConvertChildren(element) + "</blockquote>");
                    break;
                case "emphasis":
                    html.Append("<em>" + ConvertChildren(element) + "</em>");
                    break;
                case "section":
                    html.Append("<section>" + ConvertChildren(element) + "</section>");
                    break;
                case "subtitle":
                    html.Append("<h2>" + ConvertChildren(element) + "</h2>");
                    break;
                case "cite":
                    html.Append("<cite>" + ConvertChildren(element) + "</cite>");
                    break;
                case "text-author":
                    html.Append("<p><strong>" + ConvertChildren(element) + "</p></strong>");
                    break;
                case "sup":
                    html.Append("<sup>" + ConvertChildren(element) + "</sup>");
                    break;
                case "sub":
                    html.Append("<sub>" + ConvertChildren(element) + "</sub>");
                    break;
                case "poem":
                    html.Append("<div class=\"poem\">" + ConvertChildren(element) + "</div>");
                    break;
                case "stanza":
                    html.Append("<p>" + ConvertChildren(element) + "</p>");
                    break;
                case "empty-line":
                    html.Append("<br/>");
                    break;
                case "a":
                    html.Append("<a href=\"" + element.Attribute(xlinkNs + "href")?.Value + "\">" + ConvertChildren(element) + "</a>");
                    break;
                case "image":
                    html.AppendLine("<img src=\"" + element.Attribute(xlinkNs + "href")?.Value.Substring(1) + "\" alt=\"" + element.Attribute(xlinkNs + "alt")?.Value + "\" />");
                    break;
                case "strong":
                    html.Append("<strong>" + ConvertChildren(element) + "</strong>");
                    break;
                default:
                    html.Append("<div class=\"" + element.Name.LocalName + "\">" + ConvertChildren(element) + "</div>");
                    break;
            }
        }

        private string ConvertChildren(XElement element)
        {
            StringBuilder childHtml = new StringBuilder();
            ConvertBody(element, childHtml);
            return childHtml.ToString();
        }

        public string FilterFormat()
        {
            throw new NotImplementedException();
        }
    }
}
