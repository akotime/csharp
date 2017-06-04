using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake2
{
    class Menu
    {
        Plansza plansza = new Plansza();
        Gra gra = new Gra();

        static ConsoleColor kolorMenu = Console.BackgroundColor;

        public void glowneMenu()
        {
            Console.Title = "Konsolowa gra w węża";
            Console.SetBufferSize(plansza.maxKolumna, plansza.maxWiersz + 3);
            Console.SetWindowSize(plansza.maxKolumna, plansza.maxWiersz + 1);

            while (true)
            {
                Console.Clear();
                Console.CursorVisible = false;
                Console.BackgroundColor = kolorMenu;
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.SetCursorPosition(plansza.minKolumna + 25, plansza.minWiersz + 5);
                Console.Write("!!! Konsolowa gra w węża !!!");
                Console.SetCursorPosition(plansza.minKolumna + 28, plansza.minWiersz + 10);
                Console.Write("1 - Rozpocznij nową grę");
                Console.SetCursorPosition(plansza.minKolumna + 28, plansza.minWiersz + 12);
                Console.Write("2 - Exit");
                ConsoleKeyInfo klawisz = Console.ReadKey();

                switch (klawisz.Key)
                {
                    case ConsoleKey.D1 :
                        Console.Clear();
                        gra.NowaGra();
                        break;
                    case ConsoleKey.D2 :
                        Console.Clear();
                        return;
                    default :
                        break;
                }
            }
        }
    }
}
