using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;

namespace NEfyaz
{
    public class HtmlTagFilter
    {
        List<string> _htmlTag = new List<string>();
        int _htmlTagCount;
        public HtmlTagFilter()
        {
            // getir çalışma dosyasının adresini (i.e. \bin\Debug)
            string _workingDirectory = Environment.CurrentDirectory;
            // getir proje adı yolunu
            string _projectDirectory = Directory.GetParent(_workingDirectory).Parent.Parent.FullName;
            ReadTag(_projectDirectory);
        }
        private void ReadTag(string ProjectDirectory)
        {
            StreamReader SW = new StreamReader(ProjectDirectory + "\\Html5ElementList.txt");//oku 
            string satir;
            int say = 0;
            while ((satir = SW.ReadLine()) != null)
            {
                _htmlTag.Add(satir);
                say++;
            }
            SW.Close();
            SW.Dispose();
            _htmlTagCount = say;
        }
        public string Filter(string HText)
        {
            string result = "";

            foreach (var item in _htmlTag)
            {
                if (HText.IndexOf(item) > -1 || HText.IndexOf("<" + item) > -1 || HText.IndexOf(item + ">") > -1 || HText.IndexOf("/" + item + ">") > -1 || HText.IndexOf("&nbsp;") > -1)
                {
                    result = Regex.Replace(HText, "<.*?>", String.Empty);
                }
                else if (HText.Length > 2000)
                {
                    result = "";
                }
                else if (HText.IndexOf("function") > -1 || HText.IndexOf("webkit") > -1 || HText.IndexOf("content") > -1 || HText.IndexOf("padding") > -1 || HText.IndexOf("null") > -1 || HText.IndexOf("margin") > -1)
                {
                    result = "";
                }
                else if (HText.IndexOf("http") > -1 || HText.IndexOf("https") > -1)
                {
                    result = "";
                }
            }
            result = HttpUtility.HtmlDecode(result);
            result = Regex.Replace(result, "<.*?>", String.Empty);
            result = Regex.Replace(result, "{.*?}", String.Empty);
            return result;
        }
    }
}
