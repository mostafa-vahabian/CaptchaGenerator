using System.Drawing;

namespace CaptchaGenerator
{
    public class GenerateImageOptions
    {
        public string Text { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Color BackColor { get; set; }
        public Color ForeColor { get; set; }
    }
}
