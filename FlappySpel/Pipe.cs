using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using Raylib_cs;
using FlappySpel;

namespace FlappySpel
{
    // Skapar en klass som ärver från GameObject
    class Pipe : GameObject
    {
        // Variabler för rör
        private Color color = Color.GREEN;  // Rörens färg
        public static int width = 50;  // Rörens bredd
        public static int height = (Game.height / 4) * 5;  // Rörens höjd
        public static int spacing = 140;  // Avståndet mellan rörens öppningar
        public int speed = 150;  // Rörens hastighet
        public int spawnPos = Game.width - (Game.width / 3);  // Positionen där nya rör skapas

        // Antal rör i fönstret som räknas som poäng
        public bool pipeCounter { get; private set; }

        // En lista av rör med dess positioner
        public readonly Dictionary<int, float> pipes;
        private readonly Random rand;

        // Konstruktorn för klassen Pipe
        public Pipe()
        {
            pipes = new Dictionary<int, float>();  // Skapar en ny tom lista av rör
            rand = new Random();  // Skapar en ny instans av Random för att slumpa rörens höjder
            SpawnPipe();  // Skapar första röret
        }

        // Kontrollerar om röret har åkt ut ur fönstret
        
        public bool IsOutOfBounds()
        {
            if (pipes[0] < 0 && !pipeCounter)
            {
                pipeCounter = true;
                return true;
            }

            return false;
        }

        // Uppdaterar rörens position
        public override void Update(float deltaT)
        {
            SpawnPipe();  // Skapar ett nytt rör om det behövs
            MovePipes(deltaT);  // Flyttar alla rör
            RemovePipe();  // Tar bort rör som har åkt ut ur fönstret
        }

        // Ritar ut rören
        public override void Render()
        {
            // Skapar röret
            foreach (var p in pipes)
            {
                int x = p.Key;
                int botY = (int)p.Value;
                int topY = botY - spacing - height;
                DrawRectangle(x, botY, width, height, color);  // Ritar ut bottenröret
                DrawRectangle(x, topY, width, height, color);  // Ritar ut toppröret
            }
        }

        // Flyttar alla rör
        private void MovePipes(float deltaT)
        {
            for (int i = 0; i < pipes.Count; i++)
            {
                var pipetmp = pipes.ElementAt(i);
                pipes[pipetmp.Key] = pipetmp.Value - speed * deltaT;  // Uppdaterar rörets position baserat på tiden som har gått
            }
        }

        // Slumpar höjden på röret
        private float Setheight()
        {
            return rand.Next(Game.height / 3, Game.height - 100);  // Slumpar ett heltal mellan 1/3 och 1 av fönstrets höjd
        }

        // Funktionen skapar nya rör
        private void SpawnPipe()
        {
            int count = pipes.Count;

            // Om det inte finns några rör eller det sista röret är tillräckligt långt bort, 
            // läggs ett nytt rör till på höger sida av fönstret
            if ((count == 0) || (pipes.ElementAt(count - 1).Key <= spawnPos))
            {
                pipes.Add(Game.width, Setheight());
            }
        }

        // Funktionen tar bort alla rör och skapar sedan nya rör på ny slumpad höjd
        public void ClearPipes()
        {
            pipes.Clear();
            SpawnPipe();
        }

        // Funktionen tar bort röret när det har åkt utanför vänsterkanten av fönstret
        private void RemovePipe()
        {
            if (pipes.ElementAt(0).Key + width < 0)
            {
                pipes.Remove(pipes.ElementAt(0).Key);
                pipeCounter = false;
            }
        }
    }
}
