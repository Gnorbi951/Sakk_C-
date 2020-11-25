using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beadando.v2
{
    class Babu
    {
        public Babu(string name, int hp, int shield, int attack, int movement, Position pozi)
        {
            this.name = name;
            this.hp = hp;
            this.shield = shield;
            this.attack = attack;
            this.movement = movement;
            this.pozi = pozi;
        }

        //bábuk adati, alapértékek beállítása
        public string name { get; set; }
        public int hp { get; set; }
        public int shield { get; set; }
        public int attack { get; set; }
        public int movement { get; set; }
        public Position pozi { get; set; }


    }
}
