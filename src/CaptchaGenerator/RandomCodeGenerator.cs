using System;
using System.Text;

namespace CaptchaGenerator
{
    public static class RandomCodeGenerator
    {
        private const string LowerCaseLetters = "abcdefghijklmnopqrstuvwxyz";
        private const string UpperCaseLetters = "abcdefghijklmnopqrstuvwxyz";
        private const string Numbers = "0123456789";
        private const string Symbols = "!@#$%^&*+_-";

        public static string Generate(GenerateCodeOptions options)
        {
            options.ValidateOptions();

            var chars = new StringBuilder();

            if (options.IncludeLetters)
            {
                switch (options.LettersCase)
                {
                    case LettersCase.UpperCase:
                        chars.Append(UpperCaseLetters);
                        break;

                    case LettersCase.LowerCase:
                        chars.Append(LowerCaseLetters);
                        break;

                    case LettersCase.Both:
                        chars.Append(LowerCaseLetters);
                        chars.Append(UpperCaseLetters);
                        break;
                }
            }

            if (options.IncludeNumbers) chars.Append(Numbers);
            if (options.IncludeSymbols) chars.Append(Symbols);

            var random = new Random();
            var randomString = new StringBuilder();

            for (int i = 0; i < options.Lenght; i++)
                randomString.Append(chars[random.Next(chars.Length)]);

            return randomString.ToString();
        }

        private static void ValidateOptions(this GenerateCodeOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            if (options.Lenght < 1)
                throw new ArgumentOutOfRangeException("options.Length", "Length was out of range. Must be non-negative and more than zero.");

            if (!options.IncludeSymbols &&
                !options.IncludeLetters &&
                !options.IncludeNumbers)
                throw new ArgumentException("At least one include option must be true.");
        }
    }
}
