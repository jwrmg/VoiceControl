namespace VoiceControl.Speech
{
    public class Word
    {
        public string Text { get; set; }

        public Word()
        {
            Text = "";
        }

        public Word(string other)
        {
            Text = other;
        }
    }
}