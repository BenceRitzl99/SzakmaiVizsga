using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChefConsole
{
    public class Berles
    {
        public int Uid { get; set; }
        public int ChefId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal DailyRate { get; set; }
        public string Name { get; set; }
        public string Cuisine { get; set; }

        public int NapokSzama => (EndDate - StartDate).Days + 1;
        public decimal TotalPrice => NapokSzama * DailyRate;

    }
}
