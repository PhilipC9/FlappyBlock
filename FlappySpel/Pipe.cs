using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using Raylib_cs;
using FlappySpel;
using System.Numerics;

namespace FlappySpel
{
    class Pipe
    {
        // Variabler för rör
        private Color COLOR = GREEN;
        public const int width = 50;
        public const int height = (Game.height / 4) * 5;
        public const int spacing = 140;
        public const int speed = 150;
        public const int spawnPos = Game.width - (Game.width / 3);

        // Antal rör i fönstret
        public bool countPipes { get; private set; }

        // Slumpad plats för röret
        private readonly List<Vector2> _pipes;
        private readonly Random _rand;

        // Returnar antal rör som existerar
        public List<Vector2> GetPipes()
        {
            return _pipes;
        }

        public Pipe()
        {
            _pipes = new List<Vector2>();
            _rand = new Random();
            SpawnPipe();
        }

        // När röret uåker ut från fönstret
        public bool IsOutOfBounds()
        {
            if (_pipes[0].X < 0 && !countPipes)
            {
                countPipes = true;
                return true;
            }

            return false;
        }

        public void Update(float deltaT)
        {
            // Anropar SpawnPipe-metoden för att skapa nya rör
            SpawnPipe();
            // Anropar MovePipes-metoden för att flytta rören
            MovePipes(deltaT);
            // Anropar RemovePipe-metoden för att ta bort rören som åkt ut ur bild
            RemovePipe();
        }

        public void Render()
        {
            // Skapar röret
            foreach (var p in _pipes)
            {
                // Bestämmer position och storlek för rören
                int x = (int)p.X;
                int botY = (int)p.Y;
                int topY = botY - spacing - height;

                // Ritar ut det nedre röret
                DrawRectangle(x, botY, width, height, COLOR);
                // Ritar ut det övre röret
                DrawRectangle(x, topY, width, height, COLOR);
            }
        }
 
        // Flyttar alla rör till vänster
        private void MovePipes(float deltaT)
        {
            for (int i = 0; i < _pipes.Count; i++)
            {
                var pipetmp = _pipes[i];
                pipetmp.X -= speed * deltaT;
                _pipes[i] = pipetmp;
            }
        }

        // Slumpar höjden på röret
        private float Setheight()
        {
            // Returnerar en slumpmässig höjd mellan 1/3 av spelhöjden och 100 pixlar från botten
            return _rand.Next(Game.height / 3, Game.height - 100);
        }

        // Skapar nya rör om det inte finns några eller om det sista röret har åkt förbi spawnPos
        private void SpawnPipe()
        {
            int count = _pipes.Count;

            if ((count == 0) || (_pipes[count - 1].X <= spawnPos))
            {
                // Lägger till ett nytt rör med höjden som slumpas av Setheight-metoden
                _pipes.Add(new Vector2(Game.width, Setheight()));
            }
        }

        // Tar bort alla rör och ritar ut dom igen med ny position
        public void ClearPipes()
        {
            _pipes.Clear();
            SpawnPipe();
        }


        // Tar bort röret när det åker ut ur bild
        private void RemovePipe()
        {
            if (_pipes[0].X + width < 0)
            {
                _pipes.RemoveAt(0);
                countPipes = false;
            }
        }
    }
}