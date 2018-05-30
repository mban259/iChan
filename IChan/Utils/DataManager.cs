namespace IChan.Utils
{
    static class DataManager
    {

        public static IChan.Datas.Data Data { private set; get; }

        private static void GetFileData()
        {
            Data = Saver.LoadData(EnvManager.SavedataDir, $"{EnvManager.SavedataFilename}.json");

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
