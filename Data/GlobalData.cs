using System.IO;

namespace JapaneseTeacher.Data
{
    internal class GlobalData
    {
        public void LoadData()
        {
            if (Directory.Exists("Theme"))
            {
                Directory.CreateDirectory("Theme");
            }
        }
    }
}
