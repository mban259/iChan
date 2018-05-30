using IChan.Datas;

namespace IChan.Utils
{
    static class DataManager
    {

        public static Data Data { private set; get; }

        private static void GetFileData()
        {
            Data d;
            Saver.TryLoadData(out d);
            Data = d;
        }
        public static void GetData()
        {
            GetFileData();
        }

        public static void SaveData()
        {
            Saver.Save(Data, EnvManager.SavedataDir, $"{EnvManager.SavedataFilename}.json");
        }
    }
}
