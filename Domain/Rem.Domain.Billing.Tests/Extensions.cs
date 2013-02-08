using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using OopFactory.X12.Parsing.Model;

namespace OopFactory.X12.Tests.Unit
{
    public static class Extensions
    {
        public static Stream GetEdi(string resourcePath)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath);
        }

        public static void PrintToFile(this FileStream fs, string content)
        {
            StreamWriter writer = new StreamWriter(fs);
            writer.WriteLine(content);
            writer.Close();
            fs.Close();
        }
        public static void PrintHtmlToFile(this FileStream fs, string html)
        {
            StreamWriter writer = new StreamWriter(fs);
            writer.WriteLine("<html><body>");
            writer.WriteLine(html);
            writer.WriteLine("</body></html>");
            writer.Close();
            fs.Close();
        }

        public static string GetCompositeElement(this TypedSegment typedSegment, params string[] components)
        {
            string result = string.Empty;

            if(components != null && components.Length > 0)
            {
                result = components.Aggregate((i, j) => i + typedSegment.Delimiters.SubElementSeparator + j);
            }

            return result;
        }
    }
}
