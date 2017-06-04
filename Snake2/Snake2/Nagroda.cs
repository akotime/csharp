using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake2
{
    // Klasa Nagroda reprezentuje losowe polozenie jedzenia dla węża
    // oraz jego wartość punktową
    class Nagroda
    {
        // Pozycja nagrody na planszy
        public Punkt pozNagrody = new Punkt();
        // Wartość punktowa
        public int WartoscPunktowa { get; set; }
        // Odniesienie do Planszy (granice planszy)
        Plansza board = new Plansza();

        public Nagroda()
        {
            Random generatorLosowy = new Random();
            WartoscPunktowa = generatorLosowy.Next(0, 4); //0 - pkt, 1 - pkt, 2 - pkt, 3 - pkt
            // Wylosowanie pozycji nagrody na ekranie
            pozNagrody.X = generatorLosowy.Next(board.minKolumna + 1, board.maxKolumna - 2);
            pozNagrody.Y = generatorLosowy.Next(board.minWiersz + 1, board.maxWiersz - 1);
        }

        // Metoda rysuje nagrode na planszy.
        // Nagrody to @ w różnych kolorach.
        // 0pkt - Black
        // 1pkt - 
        // 2pkt - 
        // 3pkt - 
        public void rysujNagrode() 
        {
            ConsoleColor kolor = Console.ForegroundColor;
            Console.SetCursorPosition(pozNagrody.X, pozNagrody.Y);

            switch (WartoscPunktowa)
            {
                case 0: Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case 1: Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;
                case 2: Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                case 3: Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                default: Console.ForegroundColor = ConsoleColor.White; // ta nie powinna nigdy wystąpić
                    break;
            }
            Console.Write("@");
            Console.ForegroundColor = kolor;
        }

        // Metoda czyści nagrode po zjedzenie przez weza
        public void wyczyscNagrode()
        {
            Console.SetCursorPosition(pozNagrody.X, pozNagrody.Y);
            Console.Write(" ");
        }
    }
}
