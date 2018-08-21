using System;
using System.IO;

namespace ZSMapFormat
{
    class Program
    {
        static void Main(string[] args)
        {

            object[] test = new object[7];

            FormatStructure.Zsmapfile z = FormatStructureTools.MapFileToZSMapFile(test);

            byte[] filetest = FormatStructureTools.ZsMapFileToBytes(z);
            File.WriteAllBytes("underground.zsmap", filetest);


            Console.WriteLine("All Done!");
            Console.ReadLine();


        }
    }
}
