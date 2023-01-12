/*
 Temat projektu: Filtr Sepia
Algorytm sumuje wartości RGB kolejnych pikseli przemnożone przez odpowiedni współczynnik.
Następnie oblicza nowe wartości RGB na podstawie powyższej sumy i współczynnika siły filtru.
Tabela zapisanych, uzyskanych w ten sposób wartości stanowi podstawę do wygenerowania grafiki
z nałożonym filtrem.
Data wykonania projektu: semestr V/rok akademicki 2022/2023, Kamil Musiałowski, Informatyka Katowice AEiI
Wersja: 1.0
 */

namespace sepia_assembler_JA
{
    enum SepiaMechanismType
    {
        Undefined,
        Assembly,
        Cpp
    }
}
