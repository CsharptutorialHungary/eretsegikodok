using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace eretsegi_2005_05
{
    public class Megoldas
    {
        private readonly List<int> _utolsoHet;
        private int _bevittHet;
        private readonly List<int[]> _lottoszamok;
        private readonly Dictionary<int, int> _statisztika;

        public Megoldas()
        {
            _utolsoHet = new List<int>(5);
            _lottoszamok = new List<int[]>(51);
            _statisztika = new Dictionary<int, int>();
        }

        public void Feladat01()
        {
            //ellenőrzött bekérés, mivel a feladat nem mondja, hogy ne legyen ellenőrzött
            Console.WriteLine("Kérem adja meg az 52. hét lottószámait. Egy szám/sor!");
            int szam = 1;
            do
            {
                Console.Write("{0}. szam: ", szam);
                if (int.TryParse(Console.ReadLine(), out int beolvasott)
                    && beolvasott > 0
                    && beolvasott < 91)
                {
                        _utolsoHet.Add(beolvasott);
                        ++szam;
                }
            }
            while (szam <= 5);
        }

        public void Feladat02()
        {
            Console.WriteLine("Az 52. hét lottószámai:");
            _utolsoHet.Sort();
            foreach (var szam in _utolsoHet)
            {
                Console.Write("{0} ", szam);
            }
        }

        public void Feladat03()
        {
            Console.WriteLine("Adjon meg egy hetet 1-51 között:");
            _bevittHet = int.Parse(Console.ReadLine());
        }

        public void Feladat04()
        {
            //beolvasás
            using (var file = File.OpenText("lottosz.dat"))
            {
                string? sor = null;
                do
                {
                    sor = file.ReadLine();
                    if (!string.IsNullOrEmpty(sor))
                    {
                        int[] be = sor
                            .Split(' ')
                            .Select(szam => int.Parse(szam))
                            .ToArray();
                        _lottoszamok.Add(be);
                    }
                }
                while (sor != null);
            }

            //kiíratás
            int[] szamok = _lottoszamok[_bevittHet-1];
            Console.WriteLine("A {0}. hét lottó számai: ", _bevittHet);
            foreach (var szam in szamok)
            {
                Console.Write("{0} ", szam);
            }
        }

        public void Feladat05()
        {
            //Halmaz. 1 adott elem csak 1x szerepelhet
            HashSet<int> kihuzott = new HashSet<int>();

            //halmaz feltöltése a kihúzott számokkal
            foreach (int[] het in _lottoszamok)
            {
                foreach (int szam in het)
                {
                    kihuzott.Add(szam);
                }
            }

            //ha a halmaz elemeinek száma nem 90,
            //akkor volt olyan, mit nem húztak ki
            if (kihuzott.Count == 90)
            {
                Console.WriteLine("Nincs");
            }
            else
            {
                Console.WriteLine("Van");
            }
        }

        public void Feladat06()
        {
            var paratlanok = from het in _lottoszamok
                             from szam in het
                             where szam % 2 == 1
                             select szam;

            Console.WriteLine("Páratlan számok száma: {0}", paratlanok.Count());
        }

        public void Feladat07()
        {
            using (var kimmenet = File.CreateText("lotto52.ki"))
            {
                foreach (int[] het in _lottoszamok)
                {
                    kimmenet.WriteLine(string.Join(' ', het));
                }
                kimmenet.WriteLine(string.Join(' ', _utolsoHet));
            }
        }

        public void Feladat08()
        {
            //9. feladatban kell majd még a statisztika
            var osszesszam = new List<int[]>(_lottoszamok);
            osszesszam.Add(_utolsoHet.ToArray());
            //Összeszámolás
            foreach (int[] het in osszesszam)
            {
                foreach (int szam in het)
                {
                    if (_statisztika.ContainsKey(szam))
                    {
                        ++_statisztika[szam];
                    }
                    else
                    {
                        _statisztika.Add(szam, 1);
                    }
                }
            }

            //kiírás
            int darab = 0;
            foreach (KeyValuePair<int, int> stat in _statisztika)
            {
                Console.Write("{0} ", stat.Value);
                darab++;
                if (darab == 15)
                {
                    //15 elem után sortörés
                    Console.WriteLine();
                    darab = 0;
                }
            }
        }

        private HashSet<int> Primek90ig()
        {
            var primek = new HashSet<int> { 1, 2, 3, 5, 7};

            bool prim = true;
            for (int szam=11; szam<91; szam+=2)
            {
                prim = true;
                for (int oszto=2; oszto < (szam / 2); oszto++)
                {
                    if (szam % oszto == 0)
                    {
                        prim = false;
                    }
                }
                if (prim)
                {
                    primek.Add(szam);
                }
            }

            return primek;
        }

        public void Feladat09()
        {
            HashSet<int> primek = Primek90ig();
            Console.WriteLine("Ki nem húzott prímek: ");
            foreach (var prim in primek)
            {
                if (!_statisztika.ContainsKey(prim))
                {
                    Console.Write("{0} ", prim);
                }
            }
        }
    }
}
