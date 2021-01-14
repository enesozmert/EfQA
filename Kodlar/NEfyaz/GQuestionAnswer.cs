using System;
using System.Collections.Generic;
using System.Text;

namespace NEfyaz
{
    public class GQuestionAnswer
    {
        List<World> _worlds = new List<World>();
        public GQuestionAnswer()
        {

        }
        public List<Rates> GetRate(List<Combines> Combine, List<Words> MorpologyNotTdkWord)
        {
            int rate = 0;
            List<Rates> rates = new List<Rates>();
            string[] words;
            foreach (var item in Combine)
            {
                words = item.ATTQ.Split(' ');
                for (int i = 0; i < words.Length - 1; i++)
                {
                    foreach (var morpo in MorpologyNotTdkWord)
                    {
                        if (words[i].IndexOf(morpo.WGetStem) > -1)
                        {
                            rate++;
                        }
                    }
                }
                rates.Add(new Rates { Link = item.Link, Rate = rate });
                rate = 0;
            }
            return rates;
        }
        public List<Combines> GetCombine(List<SolarSystem> solarSystems, List<Galaxy> galaxies)
        {
            List<Combines> resultCombine = new List<Combines>();
            string aTTQ = "";
            foreach (var item in galaxies)
            {
                foreach (var item1 in solarSystems)
                {
                    if (item1.WebSite == item.Link && aTTQ.IndexOf(item1.Sentence) <= -1)
                    {
                        aTTQ += item1.Sentence;
                    }
                    else if (item1.WebSite != item.Link && !string.IsNullOrEmpty(aTTQ))
                    {
                        resultCombine.Add(new Combines { Link = item.Link.ToString(), ATTQ = aTTQ.ToString(), KeyWord = item1.KeyWord });
                        aTTQ = "";
                    }
                }
            }
            return resultCombine;
        }
        public void GetWorldString(List<Combines> combines, List<Rates> rates)
        {
            for (int i = 0; i < combines.Count - 1; i++)
            {
                _worlds.Add(new World { Id = i, WebSite = combines[i].Link, KeyWord = combines[i].KeyWord, AnswerToTheQuestion = combines[i].ATTQ, Rank = rates[i].Rate });
            }
        }
        public List<World> GetWorld()
        {
            return _worlds;
        }
    }
    public class Combines
    {
        public string Link { get; set; }
        public string ATTQ { get; set; }
        public string KeyWord { get; set; }
    }
    public class Rates
    {
        public string Link { get; set; }
        public int Rate { get; set; }
    }
}
