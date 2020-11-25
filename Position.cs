using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beadando.v2
{
    class Position
    {
        //kezdő pizíció felvétele
        public int sorszam { get; set; }
        public int oszlopszam { get; set; }

        public Position(int sorszam_, int oszlopszam_)
        {
            this.sorszam = sorszam_;
            this.oszlopszam = oszlopszam_;
        }
    }
}
