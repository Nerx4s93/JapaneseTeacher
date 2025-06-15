using System;
using System.Windows.Forms;

using JapaneseTeacher.СourseData;
using JapaneseTeacher.GUI;

namespace JapaneseTeacher;

internal static class Program
{

    private static readonly DataLoader GlobalData = new();

    [STAThread]
    private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        GlobalData.LoadData();
        Application.Run(new FormMain(GlobalData));
    }
}