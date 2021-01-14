using System;
using System.IO;
using System.Collections.Generic;
using ZemberekDotNet.Core.Logging;
using ZemberekDotNet.Morphology.Analysis;
using ZemberekDotNet.Morphology.Lexicon.TR;
using ZemberekDotNet.Morphology.Lexicon;
using ZemberekDotNet.Core.Turkish;
using ZemberekDotNet.Morphology;
using ZemberekDotNet.Tokenization;

namespace NEfyaz
{
    public class MorphologyAnalizer
    {
        Dictionary<int, string> Keys = new Dictionary<int, string>();
        int morpo = 0;
        private TurkishMorphology GetMorphology(params string[] lines)
        {
            return TurkishMorphology
                .Builder()
                .SetLexicon(lines)
                .DisableCache()
                .Build();
        }
        public Dictionary<int, String> MorphologyWord(string GetWord)
        {
            TurkishMorphology TrMorpo = GetMorphology(GetWord); //nlp morpoloji sınıfını tanımla
            WordAnalysis result = TrMorpo.Analyze(GetWord.ToString());//kelime analizi sınıfını tanımal
            foreach (SingleAnalysis analysis in result)//tekil analizi yap
            {
                morpo++;
                var Key = analysis.FormatLong();
                Keys.Add(morpo, Key.ToString());
            }

            return Keys;
        }
        public void NounsTest(string Noun)
        {

        }
        public List<Words> MorpologyNotTdkWord(string GetSentence)//tdk dışı kelimleri analiz yap
        {
            SplitSentence splitSentence = new SplitSentence();//cümleyi böl
            List<Words> Words = new List<Words>();//kelim listesi oluştur
            TurkishMorphology m;//morpoloji tanımla
            TurkishMorphology.TurkishMorphologyBuilder builder = new TurkishMorphology.TurkishMorphologyBuilder();//morpolojiyi build et.
            builder.SetLexicon(RootLexicon.GetDefault());//lexion tanımla
            builder.UseInformalAnalysis();//builde analizi çalıştır
            m = builder.Build();// morpolojiyi build et .
            m.AnalyzeAndDisambiguate(GetSentence.ToString()).BestAnalysis().ForEach(x => Words.Add(new Words {WGetStem=x.GetStem(), WSurfaceForm=x.SurfaceForm(), WFormatLong=x.FormatLong() }));
            //AdjAndNounAnalizer(Words);
            return Words;
        }
        public List<string> AllAnalizer(List<Words> Nouns)//bütün analizleri yap isimler lsiteyi getir 
        {
            List<string> AdjAndNoun = new List<string>();//yeni liste oluştur isimler için
            foreach (var item in Nouns)//gelen listedeki itemleri
            {
                if (item.WFormatLong.IndexOf("Noun") > -1 || item.WFormatLong.IndexOf("Num") > -1 || item.WFormatLong.IndexOf("Adv") > -1 || item.WFormatLong.IndexOf("Verb") > -1)//içinde buyapılar varsa ekle
                {
                    AdjAndNoun.Add(item.WSurfaceForm);//listeye kaydet
                }
            }
            return AdjAndNoun;//listey döndür.
        }
        public List<string> AdjAndNounAnalizer(List<Words> Nouns)//isimleri analiz et
        {
            List<string> Noun = new List<string>();
            foreach (var item in Nouns)
            {
                if (item.WFormatLong.IndexOf("Noun") > -1 /*|| item.WFormatLong.IndexOf("Adj") > -1*/)
                {
                    Noun.Add(item.WSurfaceForm);//liste ekle
                }
            }
            return Noun;//döndür liste
        }
    }
    public class Words
    {
        public string WSurfaceForm { get; set; }
        public string WFormatLong { get; set; }
        public string WGetStem { get; set; }
    }
}
