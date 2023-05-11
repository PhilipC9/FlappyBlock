using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using Raylib_cs;
using FlappySpel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappySpel
{
    class Player
    {
        // Spelar variabler
        public int size = 25;                        // Spelar storlek
        public int jumpHeight = 450;                 // hopphöjd
        private float gravity = 20f;                 // gravitation
        private float ySpeed;                        // fall hastighet
        public bool hasJetpack = false;              // Boolean för jetpack



        // Generisk lista med spelar färger
        List<Color> colorList = new List<Color>()
        {
          Color.WHITE,
          Color.GREEN,
          Color.BLUE,
          Color.YELLOW,
          Color.ORANGE,
          Color.PURPLE,
          Color.GOLD,
          Color.VIOLET,
          Color.GRAY,
          Color.BROWN
        };

        // Använder random för att slumpa färg
        private Random random = new Random();
        private Color color;

        public Player()
        {
            SetRandomColor();
        }

        // Väljer en slumpad färg utifrån listan
        public void SetRandomColor()
        {
            color = colorList[random.Next(colorList.Count)];
        }


        // Poäng
        public int Score { get; private set; }

        // Spelarens position
        public int posY { get; private set; }

        // Boolean som hanterar när spelaren har startat spelet
        public bool fall { get; set; }

        // Återställer alla variabler när spelaren förlorar
        public void InitPlayer()
        {
            posY = (Game.height / 2) - (size / 2);
            Score = 0;
            ySpeed = 0f;
            fall = false;
        }

        // Funktion för när spelaren hoppar
        public void Jump()
        {
            // När spelaren hoppar för första gången kommer fågeln att börja falla
            if (!fall)          
            {
                fall = true;
            }
            ySpeed = -jumpHeight;
        }

        // Uppdaterar fågelns position
        public void Update(float deltaT)
        {
            // Om spelaren faller
            if (fall)
            {
                // Sätter den nya positionen efter hastighet och gravitation
                posY += (int)(deltaT * ySpeed);
                ySpeed += gravity;
            }
        }
        
        // Ritar ut "fågeln" som just nu är en rektangel
        public void Render()
        {

            DrawRectangle(100, posY, size, size, color);
        }

    }
}
