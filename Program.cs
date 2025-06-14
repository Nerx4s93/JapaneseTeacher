using System;
using System.Windows.Forms;

using JapaneseTeacher.СourseData;
using JapaneseTeacher.GUI;

namespace JapaneseTeacher
{
    internal static class Program
    {
        private static DataLoader _globalData = new DataLoader();

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            _globalData.LoadData();
            Application.Run(new FormMain(_globalData));
        }
    }
}
