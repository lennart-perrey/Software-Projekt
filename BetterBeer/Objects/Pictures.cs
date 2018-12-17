using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace BetterBeer
{
    class Pictures
    {
        public static string imgToBase64(string path)
        {

            // provide read access to the file
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            // Create a byte array of file stream length
            byte[] ImageData = new byte[fs.Length];
            //Read block of bytes from stream into the byte array
            fs.Read(ImageData, 0, System.Convert.ToInt32(fs.Length));
            //Close the File Stream
            fs.Close();
            string _base64String = Convert.ToBase64String(ImageData);
            return _base64String;
        }

        public static void stringToFile(string FileRespone)
        {
            string filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ProfilPic.jpg");
            byte[] data = Convert.FromBase64String(FileRespone);
            File.WriteAllBytes(filepath, data);
        }


        //base64 string to file


        //string Data { get; set; }
        //public Pictures(string data)
        //{
        //    string Data = data;
        //}
        
        
        //public static byte[] ImgToByte(MediaFile file)
        //{
        //    using (var memoryStream = new MemoryStream())
        //    {
        //        file.GetStream().CopyTo(memoryStream);
        //        //file.Dispose;
        //        return memoryStream.ToArray();
        //    }
        //}
        //public static byte[] GetBytesFromImage(String ImageFilePath)
        //{
        //    byte[] b = File.ReadAllBytes(ImageFilePath);
        //    return b;
        //}
        //Bitmap


        //public static string convert_image_to64(MediaFile file)
        //{
            
        //    var stream = file.GetStream();
        //    byte[] filebytearray = new byte[stream.Length];
        //    stream.Read(filebytearray, 0, (int)stream.Length);
        //    string base64 = Convert.ToBase64String(filebytearray);
        //    if (String.IsNullOrEmpty(base64))
        //    {
        //        return null;
        //    }
        //    return base64;
        //}


    }
}
