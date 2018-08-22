using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Linq;

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


            uint amountOfMats = (uint)file.Materials.Count;

            file.AmountOfMaterials = amountOfMats;

            file.MaterialSizes = new List<int>();

            file.MaterialSizes.Add(MaterialStorage.ConvertImageToByteArraySize(Image.FromFile(@"C:\Users\Carter\Desktop\Textures\ceiling01.png")));
            file.MaterialSizes.Add(MaterialStorage.ConvertImageToByteArraySize(Image.FromFile(@"C:\Users\Carter\Desktop\Textures\ceiling01_normal.png")));
            file.MaterialSizes.Add(MaterialStorage.ConvertImageToByteArraySize(Image.FromFile(@"C:\Users\Carter\Desktop\Textures\dev_measuregeneric02.png")));

            file.MapDataSize = (ulong)file.MapData.MapDataStuff.LongLength;

            return file;

        }

        public static byte[] ZsMapFileToBytes(FormatStructure.Zsmapfile zs)
        {

            MemoryStream stream = new MemoryStream();

            BinaryWriter binWrite = new BinaryWriter(stream);

            BinaryFormatter binFormatter = new BinaryFormatter();

            binWrite.Write(zs.MagicHeader);
            //binWrite.Write(zs.FileSize);
            binWrite.Write(zs.MapName);
            binWrite.Write(zs.MapCreator);
            binWrite.Write(zs.Version);
            binWrite.Write(zs.MapType);
            binWrite.Write(zs.AmountOfMaterials);
            binWrite.Write(zs.MaterialsLocation);
            foreach (int materialsize in zs.MaterialSizes)
            {
                binWrite.Write(materialsize);
            }
            foreach (MaterialStorage material in zs.Materials)
            {

                binWrite.Write(material.MaterialPath);
                binWrite.Write(material.Material);

            }
            binWrite.Write(zs.MapDataLocation);
            binWrite.Write(zs.MapDataSize);
            binWrite.Write(zs.MapData.MapDataStuff);

            return stream.ToArray();

        }

        public static FormatStructure.Zsmapfile BytesToZsmapfile(byte[] z)
        {

            MemoryStream source = new MemoryStream(z);

            BinaryReader binRead = new BinaryReader(source);

            FormatStructure.Zsmapfile zsMapFile = new FormatStructure.Zsmapfile();

            zsMapFile.MagicHeader = binRead.ReadChars(9);

            zsMapFile.MapName = binRead.ReadString();

            zsMapFile.MapCreator = binRead.ReadString();

            zsMapFile.Version = binRead.ReadString();

            zsMapFile.MapType = binRead.ReadByte();

            zsMapFile.AmountOfMaterials = binRead.ReadUInt32();

            zsMapFile.MaterialsLocation = binRead.ReadChars(12);

            zsMapFile.MaterialSizes = new List<int>();

            for (int i = 0; i < zsMapFile.AmountOfMaterials; i++)
            {

                zsMapFile.MaterialSizes.Add(binRead.ReadInt32());

            }

            zsMapFile.Materials = new List<MaterialStorage>();

            for (int i = 0; i < zsMapFile.AmountOfMaterials; i++)
            {

                int xfilesize = 0;
                foreach (int matsize in zsMapFile.MaterialSizes)
                {

                    xfilesize = matsize;

                }

                zsMapFile.Materials.Add(new MaterialStorage(
                    binRead.ReadString(),
                    binRead.ReadBytes(xfilesize)));

            }

            zsMapFile.MapDataLocation = binRead.ReadChars(15);

            zsMapFile.MapDataSize = binRead.ReadUInt64();

            zsMapFile.MapData.MapDataStuff = binRead.ReadBytes(zsMapFile.MapDataSize);



            return zsMapFile;

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
