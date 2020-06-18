using System;

namespace eretsegi_2005_05
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var megoldas = new Megoldas();
            Console.WriteLine("01. Feladat:");
            megoldas.Feladat01();

            Console.WriteLine("02. Feladat:");
            megoldas.Feladat02();

            Console.WriteLine("03. Feladat:");
            megoldas.Feladat03();

            Console.WriteLine("04. Feladat:");
            megoldas.Feladat04();

            Console.WriteLine("05. Feladat:");
            megoldas.Feladat05();

            Console.WriteLine("06. Feladat:");
            megoldas.Feladat06();

            Console.WriteLine("07. Feladat:");
            megoldas.Feladat07();

            Console.WriteLine("08. Feladat:");
            megoldas.Feladat08();

            Console.WriteLine("09. Feladat:");
            megoldas.Feladat09();

            Console.ReadKey();
        }
    }
}
