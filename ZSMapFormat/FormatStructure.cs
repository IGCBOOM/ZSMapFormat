using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ZSMapFormat
{
    public class FormatStructure
    {

        #region zsmapFile
        /// <summary>
        /// ZS Map format structure
        /// </summary>
        public struct Zsmapfile
        {
            public char[] MagicHeader; //Header
            //public ulong FileSize; //File size
            public string MapName; //Map name
            public string MapCreator; //Level designer
            public string Version; //Version of map
            public byte MapType; //Type of map (regular, objective, etc.)
            public ushort AmountOfMaterials; //Amount of materials stored in file
            public char[] MaterialsLocation; //Where the materials start in file
            public List<MaterialStorage> Materials; //List containing materials and material path
            public char[] MapDataLocation; //Where the Map Data starts in the file
            public SampleMapData MapData; //Actual map data
        }

        #endregion

        

    }
}
