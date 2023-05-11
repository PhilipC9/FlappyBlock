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

    class Game
    {
        // Fönster
        public int width = 900;
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
                if(birdplayer.hasJetpack == true)
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
                birdplayer.Jump();
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
        }

        private void Render()
        {
            BeginDrawing();

            // Bakgrundsfärg
            ClearBackground(BLACK);

            // Rita Score text (Bakom allt)
            DisplayScore();

            // Rita spelare
            birdplayer.Render();

            EndDrawing();
        }

        // Visar poäng i bakgrunden
        private void DisplayScore()
        {
            DrawText($"{birdplayer.Score}", (width / 2) - 60, (height / 2) - 80, 100, GRAY);
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
        }
    }
}
