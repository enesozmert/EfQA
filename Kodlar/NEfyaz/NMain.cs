using System;
using System.Collections.Generic;
using System.Text;

namespace NEfyaz
{
    public static class NMain
    {
        public static List<World> worlds(string Text,int PageIndex=1)
        {
            GWebClient gWebClient = new GWebClient();//Web client sınıfı tanımla
            GQuestionAnswer gQuestionAnswer = new GQuestionAnswer();//soru cevap sınıfı tanımla
            MorphologyAnalizer morphologyAnalizer = new MorphologyAnalizer();//Morfolojik analiz sınıfını oluştur.
            Normalization normalization = new Normalization();//Normalizasyon sınıfını oluştur.

            var cMorpo = morphologyAnalizer.MorpologyNotTdkWord(normalization.NormalizationText(Text).ToString());//Metini normalize et ve ata
            var cMorpoAll = morphologyAnalizer.AllAnalizer(cMorpo);//normalizasyon metnini gramerlerine böl.
            var cMorpoNoun = morphologyAnalizer.AdjAndNounAnalizer(cMorpo);//normalizasyon metninde isimleri bul
            gWebClient.GetWebString(Text, PageIndex, cMorpoAll, cMorpoNoun);//web api ile ara ve anahtar kelimelerle eşleştir siteleri indir.
            var cWebClientGalaxy = gWebClient.GetGalaxies();//Galax listesine linkleri ekle
            var cWebClientSolarSystem = gWebClient.GetSolarSystem();//güneş sistemine bulunan cümleleri ve verilerini ekle
            var cCombines = gQuestionAnswer.GetCombine(cWebClientSolarSystem, cWebClientGalaxy);//Cümleleri birleştir listeye aktar
            var cRate = gQuestionAnswer.GetRate(cCombines, cMorpo);//birleşen cümlelere derce verç
            gQuestionAnswer.GetWorldString(cCombines, cRate);//bütün verileri dünyaya aktar
            var cWorld = gQuestionAnswer.GetWorld();//dünyayı listeye aktar

            return cWorld;//dünya listesini döndür.
        }
    }
}
