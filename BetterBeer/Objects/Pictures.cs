using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BetterBeer
{
    class Pictures
    {

        public static byte[] imgToByte(MediaFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.GetStream().CopyTo(memoryStream);
                //file.Dispose;
                return memoryStream.ToArray();
            }
        }
    }
}
