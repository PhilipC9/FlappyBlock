using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappySpel
{
    // Abstrakt basklass för spelobjekt
    abstract class GameObject
    {
        public abstract void Update(float deltaT);
        public abstract void Render();
    }
}
