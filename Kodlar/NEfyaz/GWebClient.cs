using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using ZemberekDotNet.Tokenization;
using System.IO;
using HtmlAgilityPack;
using System.Linq;

namespace NEfyaz
{
    public class GWebClient
    {
        List<SolarSystem> _matchingSentences = new List<SolarSystem>();
        List<Galaxy> _matchingWebSite = new List<Galaxy>();
        TurkishSentenceExtractor extractor = TurkishSentenceExtractor.Default;
        HtmlTagFilter htmlTagFilter = new HtmlTagFilter();
        Normalization normalization = new Normalization();
        public GWebClient()
        {

        }
        public void GetWebString(string SearchText, int PageIndex, List<string> SearchWord, List<string> KeyWord)
        {

            List<string> MatchSearchSentence = new List<string>();
            for (int i = 0; i < SearchWord.Count - 1; i++)
            {
                MatchSearchSentence.Add(SearchWord[i].ToString());
                for (int i1 = 0; i1 < SearchWord.Count - 1; i1++)
                {
                    if (i != i1)
                    {
                        MatchSearchSentence.Add(SearchWord[i].ToString() + " " + SearchWord[i1].ToString());
                    }
                }
            }

            GSearch gSearch = new GSearch(SearchText.ToString(), PageIndex);
            var GridSearch = gSearch.GetGalaxy();
            foreach (var item in GridSearch)
            {
                _matchingWebSite.Add(item);
                string Source = GetSourceCode(item.Link, SearchWord);
                List<String> Sentences = extractor.FromDocument(Source);
                string FindSentence = "";
                int IntSentence = 0;
                foreach (string item1 in Sentences)
                {
                    IntSentence++;
                    FindSentence = item1.ToString();
                    for (int i = 0; i < MatchSearchSentence.Count - 1; i++)
                    {
                        if (FindSentence.IndexOf(MatchSearchSentence[i]) > -1 && FindSentence.IndexOf("?") <= -1)
                        {
                            _matchingSentences.Add(new SolarSystem { Id = IntSentence, WebSite = item.Link.ToString(), Sentence = ConvertNew.Utf8ToUtf16(htmlTagFilter.Filter(FindSentence.ToString())), KeyWord = MatchSearchSentence[i].ToString() });
                        }
                    }
                }
            }
        }
        public List<SolarSystem> GetSolarSystem()
        {
            return _matchingSentences;
        }
        public List<Galaxy> GetGalaxies()
        {
            return _matchingWebSite;
        }
        private string GetSourceCode(string Url, List<string> SearchWord)
        {
            string hex = "";
            try
            {
                string _url = Url.ToString();
                // Http servere istek gönderilir.
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_url);
                // Özellikler ayarlanır
                request.Timeout = 10000; // Web site bağlantı projesini 10000ms olarak ayarlanır
                request.UserAgent = "Code Sample Web Client";
                // Bağlantının max min limitleri ayarlanır
                request.MaximumAutomaticRedirections = 4;
                request.MaximumResponseHeadersLength = 4;
                // Requestin credentials chach'i ayarlanır
                request.Credentials = CredentialCache.DefaultCredentials;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse(); //******* Error this line
                // Gelen responsa stream'a dönüştürülür
                Stream receiveStream = response.GetResponseStream();
                // respons Stream reader ile okunmak için newlenir
                StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                // Response okunur ve hex'e aktarılır.
                hex = readStream.ReadToEnd();
                var yourDoc = new HtmlDocument();
                yourDoc.LoadHtml(hex);
                var words = yourDoc.DocumentNode?.SelectNodes("//body//text()")?.Select(x => x.InnerText);
                hex += words != null ? string.Join(" ", words) : String.Empty;
                //}

                // stream bağlatısı kapatılır
                response.Close();
                readStream.Close();
                // readstream okunan bağlantı kapatılır.

                response.Dispose();
                readStream.Dispose();
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.ToString());
            }
            
            return ConvertNew.Utf8ToUtf16(htmlTagFilter.Filter(hex));
        }
    }
}
