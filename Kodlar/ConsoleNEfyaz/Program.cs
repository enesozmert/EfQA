using System;
using System.Collections.Generic;
using NEfyaz;
using ZemberekDotNet.Morphology;
using ZemberekDotNet.Morphology.Analysis;
using ZemberekDotNet.Tokenization;
namespace ConsoleNEfyaz
{
    class Program
    {
        static void Main(string[] args)
        {
            var Reader = Console.ReadLine();//ekrandaki soru cümlesini oku
            var cWorld = NMain.worlds(Reader);//NMainde bütün işlemleri yap ve liste olarak cWorld'a aktar.
            foreach (var item in cWorld)//Listele
            {
                Console.WriteLine("id: / " + item.Id + "/\n" + "WebSite: /" + item.WebSite + "/\n" + "AnswerToTheQuestion: /" + item.AnswerToTheQuestion + "/\n" + "KeyWord: /" + item.KeyWord + "/\n" + "Rank: /" + item.Rank + "/\n" + "\n");
            }
            Console.Read();
        }
    }
}
