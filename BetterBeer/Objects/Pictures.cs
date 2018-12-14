using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BetterBeer
{
    class Pictures
    {

        string Data { get; set; }
        public Pictures(string data)
        {
            string Data = data;
        }
        
        public static byte[] ImgToByte(MediaFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.GetStream().CopyTo(memoryStream);
                //file.Dispose;
                return memoryStream.ToArray();
            }
        }

        public static string convert_image_to64(MediaFile file)
        {
            
            var stream = file.GetStream();
            byte[] filebytearray = new byte[stream.Length];
            stream.Read(filebytearray, 0, (int)stream.Length);
            string base64 = Convert.ToBase64String(filebytearray);
            if (String.IsNullOrEmpty(base64))
            {
                return null;
            }
            return base64;
        }


    }
}
