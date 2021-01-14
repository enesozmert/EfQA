using System;
using System.Collections.Generic;
using System.Text;

namespace NEfyaz
{
    public static class ConvertNew
    {
        public static string Utf8ToUtf16(string utf8String)
        {
            // Al uft-8 bayt olarak oku ansiye çevir.
            byte[] utf8Bytes = Encoding.Default.GetBytes(utf8String);

            // uft8-uft16 dönüştür.
            byte[] utf16Bytes = Encoding.Convert(Encoding.UTF8, Encoding.Unicode, utf8Bytes);

            // uft16'yı döndür.
            return Encoding.Unicode.GetString(utf16Bytes);
        }
    }
}
