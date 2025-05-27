using System;

class Program
{
    static void Main()
    {

        Console.Write("Урожайность пшеницы (ц/га): ");
        double P = double.Parse(Console.ReadLine());

        Console.Write("Урожайность ржи (ц/га): ");
        double T = double.Parse(Console.ReadLine());

        Console.Write("Общая площадь угодий (га): ");
        double C = double.Parse(Console.ReadLine());

        Console.Write("Ежегодный рост площади ржи (%): ");
        double p = double.Parse(Console.ReadLine());

        Console.Write("Количество лет: ");
        int N = int.Parse(Console.ReadLine());


        double S_wheat = C / 2;
        double S_rye = C / 2;


        for (int year = 1; year <= N; year++)
        {
            double Y_wheat = P * S_wheat;
            double Y_rye = T * S_rye;

            Console.WriteLine($"\nГод {year}:");
            Console.WriteLine($"  Пшеница: {Y_wheat:F2} ц (площадь: {S_wheat:F2} га)");
            Console.WriteLine($"  Рожь: {Y_rye:F2} ц (площадь: {S_rye:F2} га)");


            if (year < N)
            {
                S_rye *= (1 + p / 100);
                S_wheat = C - S_rye;
            }
        }
    }
}