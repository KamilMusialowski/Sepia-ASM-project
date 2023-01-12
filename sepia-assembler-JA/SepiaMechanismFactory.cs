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
using System.Text;
using System.Threading.Tasks;

namespace sepia_assembler_JA
{
    static class SepiaMechanismFactory
    {
        public static SepiaInterface Create(SepiaMechanismType mechanismType, float[] sepiaRates, int bytesPerPixel, int startIndex, int endIndex)
        {
            switch (mechanismType)
            {
                case SepiaMechanismType.Assembly:
                    return new SepiaAssembly(sepiaRates, bytesPerPixel, startIndex, endIndex);
                case SepiaMechanismType.Cpp:
                    return new SepiaCpp(sepiaRates, bytesPerPixel, startIndex, endIndex);
                default:
                    return null;
            }
        }
    }
}
