using System;
using Raylib_cs;
using static Raylib_cs.Color;
using static Raylib_cs.Raylib;
using static Raylib_cs.KeyboardKey;
using System.Numerics;

namespace FlappySpel
{
    class Program
    {

        static void Main(string[] args)
        {

            Game Flappygame = new Game();
            Flappygame.Run();
        }
    }
}
