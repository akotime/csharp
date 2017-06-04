using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Snake2
{
    class Gra
    {
        // Flaga sprawdzajaca czy aktualnie gramy w grę
        static bool graTrwa = true;

        // Plansza
        Plansza plansza = new Plansza();

        // Waz
        Waz waz = new Waz();

        // Punkt początkowy (glowa)
        Punkt punktPoczatkowy = new Punkt();

        // Nagroda = jedzenie
        Nagroda nagroda = new Nagroda();

        // Zdobyte punkty
        static int punkty = 0;

        // Start
        static DateTime start = DateTime.Now;

        // Rozpoczecie nowej gry,
        // inicjalizacja gry i granie
        public void NowaGra()
        {
            ConsoleColor kolorTlaMenu = Console.BackgroundColor;
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.Clear();
            Console.CursorVisible = false;
            graTrwa = true;
            punkty = 0;
            plansza.RysujPlansze();
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Red;
            
            waz.zrobWeza();
            nagroda.rysujNagrode();
            Graj();
            Console.BackgroundColor = kolorTlaMenu;
            Console.Clear();
        }

        // Petla gry
        private void Graj()
        {
            while (graTrwa) 
            {
                WczytajKlawisz();
                if (!graTrwa) break;
                nowaNagroda();
                wazZjadlSie();
                if (!graTrwa) break;
                wazIdzie();
                if (!graTrwa) break;
                zjadlNagrode();
                if (!graTrwa) break;
                Thread.Sleep(100);
            }
        }

        // Metoda sprwdza czy klawisz sterujacy wezem
        // zostal wciśniety lub klawisz wyjscia z gry.
        private void WczytajKlawisz()
        {
            // cos jest w strumieniu wejsciowym
            if (Console.KeyAvailable)
            {
                // Odczytanie wcisnietego klawisza
                ConsoleKeyInfo klawisz = Console.ReadKey(true);

                // Wcisnieto strzalke w lewo.
                // Jesli idzie w lewo to nie moze skrecic w prawo.
                if (klawisz.Key == ConsoleKey.LeftArrow && Waz.gdzieSkrecic != Waz.Kierunek.prawo)
                {
                    Waz.gdzieSkrecic = Waz.Kierunek.lewo;
                }
                // Wcisnieto strzalke w prawo.
                // Jesli idzie w prawo to nie moze skrecic w lewo.
                if (klawisz.Key == ConsoleKey.RightArrow && Waz.gdzieSkrecic != Waz.Kierunek.lewo)
                {
                    Waz.gdzieSkrecic = Waz.Kierunek.prawo;
                }
                // Wcisnięto strzałkę w górę.
                // Jesli idzie w gore to nie moze skrecic w dół.
                if (klawisz.Key == ConsoleKey.UpArrow && Waz.gdzieSkrecic != Waz.Kierunek.dol)
                {
                    Waz.gdzieSkrecic = Waz.Kierunek.gora;
                }
                // Wciśnięto strzałkę w dół.
                // Jesli idzie w dół to nie może skręcić w górę.
                if (klawisz.Key == ConsoleKey.DownArrow && Waz.gdzieSkrecic != Waz.Kierunek.gora)
                {
                    Waz.gdzieSkrecic = Waz.Kierunek.dol;
                }
                // Wciśnięto Escape - koniec gry
                if (klawisz.Key == ConsoleKey.Escape)
                {
                    graTrwa = false;
                    // GameOver();
                }
                // Wyczyszczenie strumienia bufora wejściowego
                while (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                }
               
            }
        }

        // Nowa nagroda co 10 sek
        private void nowaNagroda()
        {
            if (start <= DateTime.Now.Subtract(TimeSpan.FromSeconds(10)))
            {
                nagroda.wyczyscNagrode();
                start = DateTime.Now;
                nagroda = new Nagroda();
                nagroda.rysujNagrode();
            }
        }

        // Zjadł Nagrode - wydłuż węża,
        // zwiększ lub wyzeruj punkty
        public void zjadlNagrode()
        {
            if (waz.czyJestNaNagrodzie(nagroda.pozNagrody))
            {
                if (nagroda.WartoscPunktowa == 0) // wyzeruj punkty
                {
                    punkty = 0;
                }
                else // dodaj punkty i wydłuż węża
                {
                    punkty += nagroda.WartoscPunktowa;
                    waz.wydluzWeza();
                }
                
                // wypisanie punktow na planszy oraz zrobienie nowej nagrody
                plansza.rysujPunkty(punkty);
                nagroda = new Nagroda();
                nagroda.rysujNagrode();
            }
        }

        private void wazZjadlSie()
        {
            if (waz.czyWazSieZjadl())
            {
                GameOver();
            }
        }

        private void wazIdzie()
        {
            if (!waz.dobryRuch())
            {
                GameOver();
            }
        }

        public void GameOver()
        {
            graTrwa = false;
            ConsoleColor kolor = Console.ForegroundColor;
            Console.SetCursorPosition(plansza.minKolumna + 35, plansza.minWiersz + 10);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("GAME OVER");
            Console.ForegroundColor = kolor;
            waz.wyczyscWeza();
        }
    }
}
