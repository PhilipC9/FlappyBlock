using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using Raylib_cs;


namespace FlappySpel
{
    // Arv, fågel med jetpack
    class JetpackBird : Player
    {
        // Boolvärde för jetpack
        public bool hasJetpack = true;

        // Ritar ut spelare med färgen röd
        public void Render()
        {
            DrawRectangle(100, posY, size, size, RED);
        }
    }
}
