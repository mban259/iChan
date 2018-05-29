using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetEnv;
using IChan.Data;
using NBitcoin.Protocol;

namespace IChan
{
    static class DataManager
    {

        public static IChan.Data.Data Data { private set; get; }

        private static void GetFileData()
        {
            Data = SaveManager.LoadData(EnvManager.SavedataDir, $"{EnvManager.SavedataFilename}.json");

        }
        public static void GetData()
        {
            GetFileData();
        }

        public static void SaveData()
        {
            SaveManager.Save(Data, EnvManager.SavedataDir, $"{EnvManager.SavedataFilename}.json");
        }
    }
}
