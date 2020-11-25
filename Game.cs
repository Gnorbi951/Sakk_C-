using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace beadando.v2
{
    class Game
    {
        public Babu[,] table { get; set; }

        public Game()
        {
            table = TableCreate();
        }

        private Babu[,] TableCreate()
        {
           //adatok beolvasása a fájlból
            StreamReader sr = new StreamReader("data.txt", Encoding.UTF8);
            int szam1 = int.Parse(sr.ReadLine());
            sr.Close();

            Babu[,] table = new Babu[szam1, szam1];

            string[] sor = new string[2*szam1+1];
            int i = 0;

            sr = new StreamReader("data.txt", Encoding.UTF8);
            while (i < 2 * szam1+1)
            {
                sor[i] = sr.ReadLine();
                i++;

            }
            sr.Close();


            //beolvasott fájl tördelése, tömbök/változók feltöltése
            for (i = 0; i < sor.Length; i++) 
            {
                if (i > 0) //0. sor kiszűrése
                {
                    string[] sordarab = sor[i].Split(',');
                    string[] pozicio = sordarab[5].Split('-');
                    Position pozi = new Position(int.Parse(pozicio[0]), int.Parse(pozicio[1]));
                    table[pozi.sorszam, pozi.oszlopszam] = new Babu(sordarab[0], int.Parse(sordarab[1]), int.Parse(sordarab[2]), int.Parse(sordarab[3]), int.Parse(sordarab[4]), pozi);
                }
            }

            return table;
        }

        private void Lepes(Position honnan, Position hova)
        {
            if (hova == null)
            {
                table[honnan.oszlopszam, honnan.sorszam] = null;
                table[hova.oszlopszam, hova.sorszam] = table[honnan.oszlopszam, honnan.sorszam];
            }
            else
            {
                table[hova.oszlopszam, hova.sorszam].shield = table[hova.oszlopszam, hova.sorszam].shield - table[honnan.oszlopszam, honnan.sorszam].attack;

                if (table[hova.oszlopszam, hova.sorszam].shield < 0)
                {
                    table[hova.oszlopszam, hova.sorszam].hp = table[hova.oszlopszam, hova.sorszam].hp - table[hova.oszlopszam, hova.sorszam].shield;
                }

            }


        }

        public void Play()
        {
            //információ, szabály kiírása a felhasználónak
            Console.WriteLine("\tFantasy Sakk");
            Console.Write("\nAz a játékos nyer aki leüti az ellenfél összes bábuját a tábláról.\nA bábukkal maximum 1 mezőt lehet haladni, előre, hátra, balra, jobbra.\n");
            Console.Write("Minden bábunak van életereje és pajzsa. Amíg a pajzs értéke nem nulla addig abból vonódik le a támadások értéke.\nHa egy bábu életereje eléri a nullát kiesik, meghal. \n\n");
            Console.WriteLine("Minden Gyalognak [G] 100 életereje és 150 pajzsa van, sebzésük 50. A Vezéreknek [V] 200 életereje, 150 pajzsa és 70 sebzése van.\n Az egyes játékos jelölése a [W], a második jelölése pedig a [B].");
            StreamReader sr = new StreamReader("data.txt", Encoding.UTF8);
            int szam1 = int.Parse(sr.ReadLine());
            sr.Close();

            //tábla kiírása a kezdőpozíciókkal
            for (int i = 0; i < szam1; i++)
            {
                if (i == 0)
                {
                    Console.Write("\t    " + i);
                }
                else
                {
                    Console.Write("  " + i);
                }

            }
            Console.WriteLine();
           
            for (int i = 0; i < table.GetLength(0); i++)
            {
                Console.Write("\t" + i + " ");
                for (int j = 0; j < table.GetLength(1); j++)
                {

                    if (table[i, j] != null)
                    {
                        Console.Write(" " + table[i, j].name.Substring(0, 1) + table[i, j].name.Last());
                    }
                    else
                    {
                        Console.Write(" []");
                    }

                }
                Console.WriteLine();
            }

            Console.WriteLine("\nA kezdéshez nyomj egy [ENTER] -t");
            Console.ReadLine();
            Console.Clear();

            string kivalasztottBabu;
            //int bekertszami;
            //int bekertszamj;

            #region tablakiiras
            for (int i = 0; i < szam1; i++)
                {
                    if (i == 0)
                    {
                        Console.Write("\t    " + i);
                    }
                    else
                    {
                        Console.Write("  " + i);
                    }

                }
                Console.WriteLine();

            for (int i = 0; i < table.GetLength(0); i++)
                {
                    Console.Write("\t" + i + " ");
                    for (int j = 0; j < table.GetLength(1); j++)
                    {

                        if (table[i, j] != null)
                        {
                            Console.Write(" " + table[i, j].name.Substring(0, 1) + table[i, j].name.Last());
                        }
                        else
                        {
                            Console.Write(" []");
                        }

                    }
                    Console.WriteLine();
                }
            #endregion

            Console.Write("\nAz 1-es játékos [W] következik\nAdd meg annak a bábunak a koordinátáját amivel lépni szeretnél kötőjellel elválasztva: "); //babu koordinataja
            kivalasztottBabu = Console.ReadLine();

            string[] pos1pos2 = new string[kivalasztottBabu.Length];

            for (int i = 0; i < kivalasztottBabu.Length-1; i++) // kotojel miatt -1
            {
                
                pos1pos2 = kivalasztottBabu.Split('-');
                int pos1pos2index = int.Parse(pos1pos2[i]);
                if (!(pos1pos2index >= 0 && pos1pos2index <3)) {
                    Console.Write("\nRosszul adtad meg a koordinátát, próbáld újra: ");
                    kivalasztottBabu = Console.ReadLine();
                    i = 0;
                }         
            }

            string kivalasztottMezo;
            string[] pos1pos2New = new string[kivalasztottBabu.Length];

            Console.Write("\nAdd meg melyik mezőre akarsz lépni kötőjellel elválasztva: ");
            kivalasztottMezo = Console.ReadLine();

            // kotojel miatt -1
            for (int i = 0; i < kivalasztottMezo.Length - 1; i++) {

                pos1pos2New = kivalasztottMezo.Split('-');
                if (!(pos1pos2New[i] == "0" || pos1pos2New[i] == "1" || pos1pos2New[i] == "2")) {
                    Console.Write("\nRosszul adtad meg a koordinátát, próbáld újra: ");
                    kivalasztottMezo = Console.ReadLine();
                    i = 0;
                }
            }


        }
    }
}
