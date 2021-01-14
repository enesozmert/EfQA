using System;
using System.Collections.Generic;
using System.Text;
using ZemberekDotNet.Morphology;
using ZemberekDotNet.Normalization.Deasciifier;
using ZemberekDotNet.Normalization;
namespace NEfyaz
{
    public class Normalization
    {
        public Normalization()
        {

        }
        public string NormalizationText(string asciiStrings)
        {
            string NewText = new ZemberekDotNet.Normalization.Deasciifier.Deasciifier(asciiStrings).ConvertToTurkish();
            return NewText;
        }
    }
}
