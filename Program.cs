using System;
using System.Windows.Forms;

using JapaneseTeacher.Data;
using JapaneseTeacher.GUI;

namespace JapaneseTeacher
{
    internal static class Program
    {
        private static GlobalData _globalData = new GlobalData();

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                _globalData.LoadData();
                Application.Run(new FormMain(_globalData));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
