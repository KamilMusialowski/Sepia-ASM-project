/*
 Temat projektu: Filtr Sepia
Algorytm sumuje wartości RGB kolejnych pikseli przemnożone przez odpowiedni współczynnik.
Następnie oblicza nowe wartości RGB na podstawie powyższej sumy i współczynnika siły filtru.
Tabela zapisanych, uzyskanych w ten sposób wartości stanowi podstawę do wygenerowania grafiki
z nałożonym filtrem.
Data wykonania projektu: semestr V/rok akademicki 2022/2023, Kamil Musiałowski, Informatyka Katowice AEiI
Wersja: 1.0
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sepia_assembler_JA
{
    internal static class Program
    {
        /// <summary>
        /// Główny punkt wejścia dla aplikacji.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SepiaForm());
        }
    }
}
