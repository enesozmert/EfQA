using System;
using NEfyaz;

namespace ConsoleNEfyazDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-Soru cümlesi yazınız:-");
            Console.WriteLine("-Sonuç görüntülenme 1 dk kadar sürer-");
            Console.WriteLine("-Örnek: Kuantum fiziği nedir?-");
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
