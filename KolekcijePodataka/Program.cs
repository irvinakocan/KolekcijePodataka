using System;
using System.ComponentModel;

namespace KolekcijePodataka
{
    class Program
    {
        static int[] ocjene = new int[15]
        {
            6, 6, 6, 7, 7, 7, 8, 8, 8, 8, 8, 9, 9, 10, 10
        };

        static Tuple<int, double>[] ponavljanjaOcjena = new Tuple<int, double>[5]
        {
            new Tuple<int, double>(6, 0),
            new Tuple<int, double>(7, 0),
            new Tuple<int, double>(8, 0),
            new Tuple<int, double>(9, 0),
            new Tuple<int, double>(10, 0)
        };

        // inicijalizacija liste studenata
        static List<Tuple<string, int>> studenti = new List<Tuple<string, int>>()
        {
            new Tuple<string, int>("Emina", 16000),
            new Tuple<string, int>("Amina", 15000),
            new Tuple<string, int>("Edina", 18000),
            new Tuple<string, int>("Emin", 17000),
            new Tuple<string, int>("Edin", 19001),
            new Tuple<string, int>("Edin", 19000),
            new Tuple<string, int>("Amina", 15001)
        };

        static List<Tuple<string, int>> noviStudenti = new List<Tuple<string, int>>()
        {
            new Tuple<string, int>("Emina", 16000),
            new Tuple<string, int>("Amina", 15000),
            new Tuple<string, int>("Edina", 18000),
            new Tuple<string, int>("Emin", 17000),
            new Tuple<string, int>("Edin", 19001),
            new Tuple<string, int>("Edin", 19000),
            new Tuple<string, int>("Amina", 15001)
        };

        static void Main(string[] args)
        {
            // racunanje broja ponavljanja ocjena
            foreach (var ocjena in ocjene)
                ponavljanjaOcjena[ocjena - 6] = new Tuple<int, double>(ocjena,
                    ponavljanjaOcjena[ocjena - 6].Item2 + 1);

            // pretvaranje broja ponavljanja u postotke
            for (int i = 0; i < ponavljanjaOcjena.Length; i++)
                ponavljanjaOcjena[i] = new Tuple<int, double>(
                    ponavljanjaOcjena[i].Item1,
                    ponavljanjaOcjena[i].Item2 / ocjene.Length * 100);

            // prikaz rezultata
            foreach (var ocjena in ponavljanjaOcjena)
                Console.WriteLine("Ocjena: " + ocjena.Item1 + ", Postotak: "
                    + ocjena.Item2 + "% studenata");




            // brisanje studenata sa indeksom manjim od 16000
            for (int i = 0; i < studenti.Count; i++)
                if (studenti[i].Item2 < 16000)
                    studenti.RemoveAt(i);

            // sortiranje preostalih studenata
            for (int i = 0; i < studenti.Count - 1; i++)
            {
                for(int j = i + 1; j < studenti.Count; j++)
                {
                    if (studenti[i].Item2 == studenti[j].Item2)
                    {
                        Console.WriteLine("U listi postoje dva studenta sa istim indeksom - greška!");
                        return;
                    }

                    var rezultat = string.Compare(studenti[i].Item1, studenti[j].Item1);
                    if (rezultat >= 0)
                    {
                        if (studenti[i].Item1 == studenti[j].Item1 &&
                            studenti[i].Item2 < studenti[j].Item2)
                            continue;
                        var pomocni = studenti[i];
                        studenti[i] = studenti[j];
                        studenti[j] = pomocni;
                    }
                }
            }

            // prikaz rezultata
            Console.WriteLine("\nStudenti nakon sortiranja:");
            foreach (var student in studenti)
            {
                Console.WriteLine("Ime: " + student.Item1 + ", Indeks: " + student.Item2.ToString());
            }




            // Korištenje lambda-funkcija u metodama za rad sa kolekcijama podataka

            // brisanje svih studenata koji ispunjavaju kriterij
            noviStudenti.RemoveAll(student => student.Item2 < 16000);

            // pokušaj pronalaska dva studenta s istim indeksom
            if(noviStudenti.Any(student1 => noviStudenti.Where(student2 =>
                student1.Item2 == student2.Item2).ToList().Count > 1))
            {
                Console.WriteLine("U listi postoje dva studenta sa isti indeksom - greška!");
                return;
            }

            // sortiranje preostalih studenata
            noviStudenti = noviStudenti.OrderBy(student => student.Item1)
                .ThenBy(student => student.Item2).ToList();

            // prikaz rezultata
            Console.WriteLine("\nStudenti nakon sortiranja:");
            noviStudenti.ForEach(student =>
                Console.WriteLine("Ime: " + student.Item1 + ", Indeks: " + student.Item2.ToString()));
        }
    }
}
