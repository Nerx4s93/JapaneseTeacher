namespace JapaneseTeacher.СourseData;

internal class Word
{
    public string Text { get; set; }
    public string Reading { get; set; }
    public string Translation { get; set; }

    public int Encounters { get; set; }
    public int Mistakes { get; set; }
    public int StreakForMistakes { get; set; }
    public int Streak { get; set; }
    public int TimesNotSeen { get; set; }

    public string Level;
}