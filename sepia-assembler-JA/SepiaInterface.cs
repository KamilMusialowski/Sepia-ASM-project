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
    public abstract class SepiaInterface
    {
        protected float[] _sepiaRates;
        protected float[] _rgbRates = { 0.114f, 0.587f, 0.299f, 0.0f };
        protected int _startIndex;
        protected int _endIndex;
        protected int _bytesPerPixel;

        public SepiaInterface(float[] sepiaRates, int bytesPerPixel, int startIndex, int endIndex)
        {
            _sepiaRates = sepiaRates;
            _startIndex = startIndex;
            _endIndex = endIndex;
            _bytesPerPixel = bytesPerPixel;
        }

        public abstract void ExecuteEffect(float[] allPixels);
    }
}
