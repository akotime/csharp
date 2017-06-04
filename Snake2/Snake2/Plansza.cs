using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake2
{
    public class Plansza
    {
        // Granice bufora konsoli
        public int minWiersz = 0;
        public int minKolumna = 0;
        public int maxWiersz = 23;
        public int maxKolumna = 80;

        // Metoda rysujace plansze na consoli
        public void RysujPlansze()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            string ramaPlanszy = string.Empty;

            // Krawędź górna
            Console.SetCursorPosition(minKolumna, minWiersz);
            Console.Write(ramaPlanszy.PadLeft(maxKolumna));

            // Krawędź dolna
            Console.SetCursorPosition(minKolumna, maxWiersz);
            Console.Write(ramaPlanszy.PadLeft(maxKolumna));

            // Krawędzie boczne
            for(int i=1; i < maxWiersz; i++){
                Console.SetCursorPosition(minKolumna, i);
                Console.Write(" ");
                Console.SetCursorPosition(maxKolumna-1, i);
                Console.Write(" ");
            }
            rysujPunkty(0);
            Console.SetWindowPosition(0, 0);
        }

        public void rysujPunkty(int punkty)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(minKolumna + 1, maxWiersz);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Punkty: {0}", punkty);
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Red;
        }
    }
}
