
using System.IO;

namespace ZSMapFormat
{
    public class SampleMapData
    {

        public byte[] MapDataStuff;

        public SampleMapData(byte[] data)
        {

            MapDataStuff = data;

        }

        public static byte[] ConvertMapFileToByteArray(string x)
        {

            byte[] bytes = File.ReadAllBytes(x);

            return bytes;

        }

    }
}
