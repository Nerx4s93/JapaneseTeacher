using System;
using System.Windows.Forms;

using JapaneseTeacher.Data;
using JapaneseTeacher.GUI;

namespace JapaneseTeacher
{
    internal static class Program
    {
        private static GlobalData _globalData;

        [STAThread]
        static void Main()
        {
            _globalData.LoadData();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}
