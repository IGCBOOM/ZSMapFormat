using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ZSMapFormat
{
    public class MaterialStorage
    {

        public string MaterialPath;
        public byte[] Material;
        

        public MaterialStorage(string matpath, byte[] mat)
        {

            MaterialPath = matpath;
            Material = mat;

        }

        public static byte[] ConvertImageToByteArray(Image x)
        {

            var stream = new MemoryStream();

            if (x.RawFormat.Equals(ImageFormat.Png))
            {
                x.Save(stream, ImageFormat.Png);
            }
            else if (x.RawFormat.Equals(ImageFormat.Jpeg))
            {
                x.Save(stream, ImageFormat.Jpeg);
            }
            else if (x.RawFormat.Equals(ImageFormat.Bmp))
            {
                x.Save(stream, ImageFormat.Bmp);
            }
            else if (x.RawFormat.Equals(ImageFormat.Gif))
            {
                x.Save(stream, ImageFormat.Gif);
            }
            else
            {
                Console.WriteLine("Image format not supported, please use: PNG, JPEG, BMP, or GIF");
            }

            //x.Save(stream, ImageFormat.Png);

            return stream.ToArray();

        }

        [System.Obsolete("Do not use, this method returns incorrect info.", true)]
        public static ulong GetImageSize(string path)
        {

            var fileSize = new FileInfo(path).Length;

            return (ulong)fileSize;

        }

        public static int ConvertImageToByteArraySize(Image x)
        {

            var stream = new MemoryStream();

            if (x.RawFormat.Equals(ImageFormat.Png))
            {
                x.Save(stream, ImageFormat.Png);
            }
            else if (x.RawFormat.Equals(ImageFormat.Jpeg))
            {
                x.Save(stream, ImageFormat.Jpeg);
            }
            else if (x.RawFormat.Equals(ImageFormat.Bmp))
            {
                x.Save(stream, ImageFormat.Bmp);
            }
            else if (x.RawFormat.Equals(ImageFormat.Gif))
            {
                x.Save(stream, ImageFormat.Gif);
            }
            else
            {
                Console.WriteLine("Image format not supported, please use: PNG, JPEG, BMP, or GIF");
            }

            //x.Save(stream, ImageFormat.Png);

            return stream.ToArray().Length;

        }

        public static Image ConvertByteArrayToImage(byte[] b)
        {

            using (var ms = new MemoryStream(b))
            {
                return Image.FromStream(ms);
            }

        }

    }
}
