using System;
using System;
using Raylib_cs;
using static Raylib_cs.Color;
using static Raylib_cs.Raylib;
using static Raylib_cs.KeyboardKey;
using System.Numerics;

namespace FlappySpel
{
    class Game
    {
        // Fönster
        public const int width = 900;
        public const int height = 600;
        private int fps = 60;
        private string title = "Flappy Cube";
        private bool jetpack = false;

        // DeltaTid (Frames)
        private float deltaTime;

        // Bool för start
        private bool isPaused;

        // Spel objekt
        private Player birdplayer;
        private Pipe pipe;

        


        public Game()
        {
            CreateWindow();
            LoadGame();
        }

        public void Run()
        {
            while (!WindowShouldClose())
            {
                // Väntar på att spelaren trycker på space
                if (birdplayer.hasJetpack == true)
                {
                    PlayEventJetpack();
                }
                else
                {
                    PlayEventBird();
                }

                // Delta tid
                UpdateDeltaTime();

                // Uppdatera tills spelaren förlorar
                if (birdplayer.fall && !isPaused)
                {
                    Update();
                }
                Render();
            }
        }

        // När space klickas och spelet inte är pausat så flyger spelaren upp
        private void PlayEventJetpack()
        {
            if (IsKeyDown(KEY_SPACE) && !isPaused)
            {
                birdplayer.Jump();
            }
        }

        // När space klickas och spelet inte är pausat så flyger spelaren upp
        private void PlayEventBird()
        {
            if (IsKeyPressed(KEY_SPACE) && !isPaused)
            {
                try
                {
                    birdplayer.Jump();
                }
                catch
                {

                }
            }
        }

        // Lagrar delta tiden i en variabel, (varje frame)
        private void UpdateDeltaTime()
        {
            deltaTime = GetFrameTime();
        }

        // För varje frame kommer spelaren att uppdateras.
        private void Update()
        {
            birdplayer.Update(deltaTime); // Uppdatera fågels position
            pipe.Update(deltaTime); // Uppdatera rörens position
            UpdateScore(); // Uppdatera spelarens poäng

            CollideCheck(); // Kolla om fågeln kolliderar med rör
        }
        private void CollideCheck()
        {
            if (Hit()) // Om fågeln träffar ett rör
            {
                RestartGame(); // Starta om spelet
            }
        }

        private void UpdateScore()
        {
            if (pipe.IsOutOfBounds()) // Om röret har lämnat spelområdet
            {
                birdplayer.IncreaseScore(); // Öka spelarens poäng
            }
        }


        private bool Hit()
        {
            var pipes = pipe.GetPipes(); // Hämta rören från Pipe-objektet


            int playerSize = Player.size; // Hämta storleken på spelaren
            int playerX = Player.posX; // Hämta spelarens X-position
            int playerY = birdplayer.posY; // Hämta spelarens Y-position
            bool hasHit = false;

            // Kolla om spelaren kolliderar med något rör
            foreach (var p in pipes)
            {
                int pipeX = (int)p.X; // Hämta X-positionen för röret
                float pipeY = p.Y; // Hämta Y-positionen för röret
                if ((playerX + playerSize >= pipeX)
                    && (playerX <= pipeX + Pipe.width))
                {
                    if ((playerY + playerSize >= pipeY)
                        || (playerY <= pipeY - Pipe.spacing))
                    {
                        hasHit = true; // Kollidering har skett
                        break; 
                    }
                }
            }

            return hasHit;
        }

        private void RestartGame()
        {
            birdplayer.InitPlayer(); // Återställ fågelns position och hastighet
            pipe.ClearPipes(); // Återställ alla rör till ursprungsläget
        }


        private void Render()
        {
            BeginDrawing();

            // Bakgrundsfärg
            ClearBackground(BLACK);

            // Rita Score text (Bakom allt)
            DisplayScore();

            // Ritar rör
            pipe.Render();
            // Rita spelare
            birdplayer.Render();

            EndDrawing();
        }

        // Visar poäng i bakgrunden
        private void DisplayScore()
        {
            DrawText($"{birdplayer.score}", (width / 2) - 60, (height / 2) - 80, 100, GRAY);
        }

        // Skapar fönster
        private void CreateWindow()
        {
            // Ritar fönster utifrån bredd, höjd och titel
            InitWindow(width, height, title);

            // Sätter FPS
            SetTargetFPS(fps);
        }

        // Laddar in spelet, kommer att lägga till rör här
        private void LoadGame()
        {
            // Spelvariabler
            isPaused = false;
            deltaTime = 0;

            // Skapar spelare
            birdplayer = new Player();
            pipe = new Pipe();


        //Startar om spel
        RestartGame();

        }
    }
}
