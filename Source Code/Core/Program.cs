namespace Neuro
{
    using System;

    public static class Program
    {
        public const float FPS = 60;

        [STAThread]
        static void Main()
        {
            using (var game = new App())
                game.Run();
        }
    }
}
