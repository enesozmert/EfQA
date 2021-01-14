using System;
using System.Collections.Generic;
using System.Text;

namespace NEfyaz
{
    public class SplitSentence
    {
        public SplitSentence()
        {

        }
        public String[] Word(string Sentences)
        {
            string[] word;
            word =Sentences.Split(' ');
            return word;
        }
    }
}
