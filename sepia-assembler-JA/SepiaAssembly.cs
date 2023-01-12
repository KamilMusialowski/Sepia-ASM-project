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
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace sepia_assembler_JA
{
    internal class SepiaAssembly : SepiaInterface
    {
        public SepiaAssembly(float[] sepiaRates, int bytesPerPixel,
            int startIndex, int endIndex)
            : base(sepiaRates, bytesPerPixel, startIndex, endIndex)
        { }

        [DllImport(@"C:\Users\kmusi\OneDrive\Desktop\JA_projekt\sepia-assembler-JA\x64\Debug\Sepia_ASM.dll")]
        private static extern void sepia(
            float[] pixels, float[] sepiaRates,
            float[] rgbRates, float[] array255, int startIndex, int endIndex);

        public override void ExecuteEffect(float[] allPixels)
        {
            float[] array255 = { 255.0f, 255.0f, 255.0f, 255.0f };
            sepia(allPixels, _sepiaRates,
                _rgbRates, array255, _startIndex, _endIndex);
        }
    }
}
