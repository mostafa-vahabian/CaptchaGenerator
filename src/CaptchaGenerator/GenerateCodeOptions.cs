namespace CaptchaGenerator
{
    public class GenerateCodeOptions
    {
        public GenerateCodeOptions()
        {
            Lenght = 5;
            LettersCase = LettersCase.LowerCase;
            IncludeNumbers = true;
            IncludeLetters = true;
            IncludeSymbols = true;
        }

        public int Lenght { get; set; }
        public LettersCase LettersCase { get; set; }
        public bool IncludeNumbers { get; set; }
        public bool IncludeLetters { get; set; }
        public bool IncludeSymbols { get; set; }
    }

    public enum LettersCase
    {
        UpperCase,
        LowerCase,
        Both
    }
}
