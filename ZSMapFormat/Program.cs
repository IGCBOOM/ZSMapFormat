using System;
using System.IO;

namespace ZSMapFormat
{
    class Program
    {
        static void Main(string[] args)
        {

            object[] test = new object[7];

            /*FormatStructure.Zsmapfile z = FormatStructureTools.MapFileToZSMapFile(test);

            byte[] filetest = FormatStructureTools.ZsMapFileToBytes(z);
            File.WriteAllBytes("bob.zsmap", filetest);*/

            FormatStructure.Zsmapfile newZsmapfile =
                FormatStructureTools.BytesToZsmapfile(File.ReadAllBytes("test.zsmap"));

            /*string[] testarray = FormatStructureTools.RetrieveZsMapInfo(newZsmapfile);

            foreach (var s in testarray)
            {
                Console.WriteLine(s);
            }*/

            File.WriteAllBytes("testest.zsmap", FormatStructureTools.ZsMapFileToBytes(newZsmapfile));

            Console.WriteLine("All Done!");
            Console.ReadLine();


        }
    }
}
