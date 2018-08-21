using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace ZSMapFormat
{
    public static class FormatStructureTools
    {

        #region zsmapFile
        /// <summary>
        /// Convert a Sandbox map to the Zombie Survival Map format
        /// </summary>
        /// <param name="dataofmap"></param>
        /// <returns>file</returns>
        public static FormatStructure.Zsmapfile MapFileToZSMapFile(object[] dataofmap)
        {

            char[] magic = { 'Z', 'S', 'M', 'A', 'P', 'B', 'O', 'O', 'M'};
            char[] materialLoc = {'M', 'A', 'T', 'S', 'T', 'A', 'R', 'T', 'B', 'O', 'O', 'M'};
            char[] mapDataLoc = {'M', 'A', 'P', 'D', 'A', 'T', 'S', 'T', 'A', 'R', 'T', 'B', 'O', 'O', 'M'};

            
            //Make a new zsmap file
            FormatStructure.Zsmapfile file = new FormatStructure.Zsmapfile
            {
                //Set file information and data
                MagicHeader = magic,
                MapName = "Test Map",
                MapCreator = "BOOM",
                Version = "V1.0",
                MapType = 0,
                AmountOfMaterials = 0,
                MaterialsLocation = materialLoc,
                Materials = new List<MaterialStorage>(),
                MapDataLocation = mapDataLoc,
                MapData = new SampleMapData(SampleMapData.ConvertMapFileToByteArray(@"C:\Users\Carter\Desktop\Textures\gm_flatgrass.bsp"))
            };

            file.Materials.Add(new MaterialStorage(@"C:\Users\Carter\Desktop\Textures\ceiling01.png", MaterialStorage.ConvertImageToByteArray(Image.FromFile(@"C:\Users\Carter\Desktop\Textures\ceiling01.png"))));
            file.Materials.Add(new MaterialStorage(@"C:\Users\Carter\Desktop\Textures\ceiling01_normal.png", MaterialStorage.ConvertImageToByteArray(Image.FromFile(@"C:\Users\Carter\Desktop\Textures\ceiling01_normal.png"))));
            file.Materials.Add(new MaterialStorage(@"C:\Users\Carter\Desktop\Textures\dev_measuregeneric02.png", MaterialStorage.ConvertImageToByteArray(Image.FromFile(@"C:\Users\Carter\Desktop\Textures\dev_measuregeneric02.png"))));


            /*
             
            var offset = (uint) (file.MagicHeader.Length + 8 + file.MapName.Length + 1 +
                                              file.MapCreator.Length + 1 + file.Version.Length + 1 + 1 + 4);


            
            var size = System.Runtime.InteropServices.Marshal.SizeOf(file);

            ulong sizer = (ulong)System.Runtime.InteropServices.Marshal.SizeOf(file);

            file.FileSize = sizer;

            */

            return file;

        }

        public static byte[] ZsMapFileToBytes(FormatStructure.Zsmapfile zs)
        {

            MemoryStream stream = new MemoryStream();

            BinaryWriter binWrite = new BinaryWriter(stream);

            binWrite.Write(zs.MagicHeader);
            //binWrite.Write(zs.FileSize);
            binWrite.Write(zs.MapName);
            binWrite.Write(zs.MapCreator);
            binWrite.Write(zs.Version);
            binWrite.Write(zs.MapType);
            binWrite.Write(zs.AmountOfMaterials);
            binWrite.Write(zs.MaterialsLocation);
            foreach (MaterialStorage material in zs.Materials)
            {

                binWrite.Write("MATERIAL" + zs.Materials.IndexOf(material).ToString());
                binWrite.Write(material.MaterialPath);
                binWrite.Write(material.Material);
                binWrite.Write("ENDMATERIAL" + zs.Materials.IndexOf(material).ToString());

            }
            binWrite.Write(zs.MapDataLocation);
            binWrite.Write(zs.MapData.MapDataStuff);

            return stream.ToArray();

        }

        #endregion

        public static string[] RetrieveZsMapInfo(FormatStructure.Zsmapfile zsmap)
        {

            string[] mapInfo = new string[5];

            mapInfo[0] = zsmap.MapName;
            //mapInfo[1] = zsmap.FileSize.ToString();
            mapInfo[1] = zsmap.MapCreator;
            mapInfo[2] = zsmap.Version;
            mapInfo[3] = zsmap.MapType.ToString();
            mapInfo[4] = zsmap.AmountOfMaterials.ToString();


            return mapInfo;
        }

    }
}
