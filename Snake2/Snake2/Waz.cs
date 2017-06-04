using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake2
{
    class Waz
    {
        public enum Kierunek
        {
            prawo,
            lewo,
            gora,
            dol
        }

        public static Kierunek gdzieSkrecic;

        Plansza plansza = new Plansza();

        // wąż
        public LinkedList<Punkt> cialoWeza = new LinkedList<Punkt>();

        public void zrobWeza()
        {
            for (int i = 1; i <= 10; i++)
            {
                // waż stworzony w wierszu 10 i kolumnie 1 
                // o długości 10
                cialoWeza.AddFirst(new Punkt(i, plansza.minWiersz + 6)); 
            }
            gdzieSkrecic = Kierunek.prawo;
            rusujWeza();
        }

        public void rysujPunkt(Punkt punkt)
        {
            Console.SetCursorPosition(punkt.X, punkt.Y);
            Console.Write("*");
        }

        public void wyczyscPunkt(Punkt punkt)
        {
            Console.SetCursorPosition(punkt.X, punkt.Y);
            Console.Write(" ");
        }

        // Metoda rysuje weża na podstawie jego 
        // ciala czyli listy LinkedList
        public void rusujWeza()
        {
            foreach (Punkt segmentWeza in cialoWeza)
            {
                rysujPunkt(segmentWeza);
            }
        }

        // Czyszczenie weża
        public void wyczyscWeza()
        {
            foreach (Punkt segmentWeza in cialoWeza)
            {
                // Czyszczenie weza na ekranie
                wyczyscPunkt(segmentWeza);
            }
            // Wyczyszczenie listy z wezlow
            cialoWeza.Clear();
        }

        // Ustalenie przesunięcia głowy węża
        // Przesuwa się o jedną pozycję w zależności, w którą stronę idzie.
        public void ustalPrzesuniecieGlowyWeza(ref int wPionie, ref int wPoziomie)
        {
            if (gdzieSkrecic == Kierunek.prawo)
                wPoziomie = 1;
            if (gdzieSkrecic == Kierunek.lewo)
                wPoziomie = -1;
            if (gdzieSkrecic == Kierunek.gora)
                wPionie = -1;
            if (gdzieSkrecic == Kierunek.dol)
                wPionie = 1;
        }

        // Ustalenie przesunięcia dla ogona węża
        // Potrzebne aby było wiadomo gdzie dodawać kolejne elementy
        // węża który będzie wydłużany
        public void ustalPrzesuniecieOgonaWeza(ref int wPionie, ref int wPoziomie)
        {
            ustalPrzesuniecieGlowyWeza(ref wPionie, ref wPoziomie);
            // wartosci odwrotne niz dla glowy bo do ogona beda dodawane kolejne 
            // elementy wyduzajacego sie ciała
            wPionie = wPionie * (-1);
            wPoziomie = wPoziomie * (-1);
        }

        // Metoda wydłuża węża - dorysowuje mu element na konsoli
        // oraz dodaje do listy "cialoWeza" na ostatniej pozycji.
        public void wydluzWeza()
        {
            int przesunWPionie = 0, przesunWPoziomie = 0;
            Punkt punkt = new Punkt();
            ustalPrzesuniecieOgonaWeza(ref przesunWPionie, ref przesunWPoziomie);
            punkt.X = cialoWeza.Last.Value.X + przesunWPoziomie;
            punkt.Y = cialoWeza.Last.Value.Y + przesunWPionie;
            cialoWeza.AddLast(punkt);
            rysujPunkt(punkt);
        }

        // Czy waz natrafił na nagrode?
        public bool czyJestNaNagrodzie(Punkt punktNagrody)
        {
            bool wynik = false;
            foreach (Punkt punktWeza in cialoWeza)
            {
                if (punktWeza.X == punktNagrody.X && punktWeza.Y == punktNagrody.Y)
                {
                    wynik = true;
                    break;
                }
            }
            return wynik;
        }

        // Wprawienie węża w ruch
        public void wykonajRuch()
        {
            int przesunWPionie = 0, przesunWPoziomie = 0;
            wyczyscPunkt(cialoWeza.Last.Value);
            cialoWeza.RemoveLast();
            ustalPrzesuniecieGlowyWeza(ref przesunWPionie, ref przesunWPoziomie);
            Punkt punkt = new Punkt();
            punkt.X = cialoWeza.First.Value.X + przesunWPoziomie;
            punkt.Y = cialoWeza.First.Value.Y + przesunWPionie;
            cialoWeza.AddFirst(punkt);
            rysujPunkt(punkt);
        }

        // Poruszanie w granicach planszy
        // Metoda sprawdza czy wąż nie przekroczył granic planszy
        public bool dobryRuch()
        {
            bool ruchDozwolony = true;
            if( (gdzieSkrecic == Kierunek.prawo && cialoWeza.First.Value.X >= plansza.maxKolumna - 2) ||
                (gdzieSkrecic == Kierunek.lewo && cialoWeza.First.Value.X <= plansza.minKolumna + 1) ||
                (gdzieSkrecic == Kierunek.dol && cialoWeza.First.Value.Y >= plansza.maxWiersz - 1) ||
                (gdzieSkrecic == Kierunek.gora && cialoWeza.First.Value.Y <= plansza.minWiersz + 1)
                )
            {
                // Granice planczy zostały przekroczone
                ruchDozwolony = false;
            }
            else
            {
                // Waz w granicach planszy - wykonaj ruch.
                wykonajRuch();
            }
            return ruchDozwolony;
        }

        // Sprawdzamy czy glowa węża natrafiła na inny element ciała
        public bool czyWazSieZjadl()
        {
            bool wynik = false;
            bool pominGlowe = true;

            foreach (Punkt punkt in cialoWeza)
            {
                if (!pominGlowe)
                {
                    if (cialoWeza.First.Value.X == punkt.X && cialoWeza.First.Value.Y == punkt.Y)
                    {
                        wynik = true;
                        break;
                    }
                }
                pominGlowe = false;
            }
            return wynik;
        }
    }
}
