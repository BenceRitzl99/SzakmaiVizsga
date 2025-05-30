using ChefConsole;
using System.Globalization;

class Program
{
    static void Main()
    {
        var berlesek = Betoltes("c:/Users/Diak/..rb/Vizsga/ChefConsole/chef_berlesek_2024.csv");
        Console.WriteLine($"1. Bérlések betöltve: {berlesek.Count} db\n");

        // 2. Havi bevétel kiszámítása
        Console.Write("2. Kérem a hónapot (1-12): ");
        int honap = int.Parse(Console.ReadLine());
        var haviBevetel = berlesek
            .Where(b => b.StartDate.Month <= honap && b.EndDate.Month >= honap)
            .Sum(b => b.TotalPrice);
        Console.WriteLine($"A {honap}. hónap összbevétele: {haviBevetel} EUR\n");

        // 3. Teljes éves bevétel meghatározása
        var evesBevetel = berlesek.Sum(b => b.TotalPrice);
        Console.WriteLine($"3. Teljes éves bevétel: {evesBevetel} EUR\n");

        // 4. A legdrágább bérlés megkeresése
        var legdragabb = berlesek.OrderByDescending(b => b.TotalPrice).First();
        Console.WriteLine($"4. Legdrágább bérlés: {legdragabb.Name}, {legdragabb.TotalPrice} EUR ({legdragabb.StartDate:yyyy-MM-dd} - {legdragabb.EndDate:yyyy-MM-dd})\n");

        // 5. Bérelt séfek száma
        var kulonbozoChefekSzama = berlesek.Select(b => b.ChefId).Distinct().Count();
        Console.WriteLine($"5. Bérelt séfek száma: {kulonbozoChefekSzama}\n");

        // 6. A leggyakrabban bérelt séf megállapítása
        var legtobbszorBereltSef = berlesek
            .GroupBy(b => b.Name)
            .OrderByDescending(g => g.Count())
            .First();
        Console.WriteLine($"6. Leggyakrabban bérelt séf: {legtobbszorBereltSef.Key} ({legtobbszorBereltSef.Count()} alkalommal)\n");

        // 7. Bérlések száma konyhatípusonként
        Console.WriteLine("7. Bérlések száma konyhatípusonként:");
        var konyhak = berlesek
            .GroupBy(b => b.Cuisine)
            .OrderByDescending(g => g.Count());
        foreach (var k in konyhak)
        {
            Console.WriteLine($"   {k.Key}: {k.Count()} bérlés");
        }
        Console.WriteLine();

        // 8. Átlagos bérlési időtartam (napban)
        double atlagNap = berlesek.Average(b => b.NapokSzama);
        Console.WriteLine($"8. Átlagos bérlési időtartam: {atlagNap:F2} nap\n");

        Console.WriteLine("Program vége. Nyomj egy gombot a kilépéshez...");
        Console.ReadKey();
    }

    static List<Berles> Betoltes(string fajl)
    {
        var lista = new List<Berles>();
        var sorok = File.ReadAllLines(fajl).Skip(1);

        foreach (var sor in sorok)
        {
            var m = sor.Split(',');

            lista.Add(new Berles
            {
                Uid = int.Parse(m[0]),
                ChefId = int.Parse(m[1]),
                StartDate = DateTime.ParseExact(m[2], "yyyy-MM-dd", CultureInfo.InvariantCulture),
                EndDate = DateTime.ParseExact(m[3], "yyyy-MM-dd", CultureInfo.InvariantCulture),
                DailyRate = decimal.Parse(m[4], CultureInfo.InvariantCulture),
                Name = m[5],
                Cuisine = m[6]
            });
        }

        return lista;
    }
}

