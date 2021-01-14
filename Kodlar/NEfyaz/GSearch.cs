using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace NEfyaz
{
    public class GSearch
    {
        List<Galaxy> _theGalaxies = new List<Galaxy>(); //liste oluştur.
        string _searchText = "";//aranan 
        string _cx = "005341494126011448547:jmdyddlxr4u"; // google arama moturu cx dosyası
        string _apiKey = "AIzaSyCGjzIXh-aPu4DqJ7vmrzvjhSZmlJKQPVU";//api keyi ekle 
        public GSearch(string SearchText, int PageIndex) // arama metotunu oluştur.
        {
            _searchText = SearchText.ToString();//gelen metini eşle
            if (_searchText != null || _searchText != "" || _searchText != string.Empty)//boş değil 
            {
                Search(PageIndex);//arama metotdunu çağır.
            }
        }
        private void Search(int PageIndex)
        {      
            int IdS = 0;//sayfa int tanımla.
            for (int i = 1; i < PageIndex+1; i++)//sayfaları döndür
            {
                string Link = "https://www.googleapis.com/customsearch/v1?key=" + _apiKey + "&cx=" + _cx + "&q=" + _searchText + "&start=" + Convert.ToString(PageIndex);
                //arama linki oluştur
                var Request = WebRequest.Create(Link);
                //web request tanımla
                HttpWebResponse Response = (HttpWebResponse)Request.GetResponse();
                Stream Data = Response.GetResponseStream();
                StreamReader Reader = new StreamReader(Data);
                string Rs = Reader.ReadToEnd();
                dynamic JsonData = JsonConvert.DeserializeObject(Rs);
                if (JsonData.items != null)
                {
                    foreach (var item in JsonData.items)
                    {
                        IdS++;
                        _theGalaxies.Add(new Galaxy
                        {
                            Id = IdS,
                            Link = item.link,
                            Title = item.title,
                            Snippet = item.snippet,
                        }); ;
                    }
                }
            }
        }
        private void GetSearch()
        {


        }
        private string GetSearchText()
        {
            return "";
        }
        public List<Galaxy> GetGalaxy()
        {
            return _theGalaxies;
        }
    }
}
