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
            birdplayer.Update(deltaTime);
            pipe.Update(deltaTime);
            UpdateScore();

            CollideCheck();
        }
        private void CollideCheck()
        {
            if (Hit())
            {
                RestartGame();
            }
        }

        private void UpdateScore()
        {
            if (pipe.IsOutOfBounds())
            {
                birdplayer.IncreaseScore();
            }
        }


        private bool Hit()
        {
            var pipes = pipe.pipes;
            int playerSize = Player.size;
            int playerX = Player.posX;
            int playerY = birdplayer.posY;
            bool hasHit = false;

            // Check if the player collides with any pipe
            foreach (var p in pipes)
            {
                int pipeX = p.Key;
                float pipeY = p.Value;
                if ((playerX + playerSize >= pipeX)
                    && (playerX <= pipeX + Pipe.width))
                {
                    if ((playerY + playerSize >= pipeY)
                        || (playerY <= pipeY - Pipe.spacing))
                    {
                        hasHit = true;
                        break;
                    }
                }
            }

            return hasHit;
        }

        private void RestartGame()
        {
            birdplayer.InitPlayer();
            pipe.ClearPipes();
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
