/*
 Temat projektu: Filtr Sepia
Algorytm sumuje warto�ci RGB kolejnych pikseli przemno�one przez odpowiedni wsp�czynnik.
Nast�pnie oblicza nowe warto�ci RGB na podstawie powy�szej sumy i wsp�czynnika si�y filtru.
Tabela zapisanych, uzyskanych w ten spos�b warto�ci stanowi podstaw� do wygenerowania grafiki
z na�o�onym filtrem.
Data wykonania projektu: semestr V/rok akademicki 2022/2023, Kamil Musia�owski, Informatyka Katowice AEiI
Wersja: 1.0
 */

using namespace std;

const float max_val = 255.0f;

float min(float x, float y) {
	if (x < y) {
		return x;
	}
	else {
		return y;
	}
}

void Sepia(float* pixels, int size, float* sepia_rates, float* rgb_rates, int bytes_pp, int start, int end) {
	for (int i = start; i < end && i < size; i += bytes_pp) {
		float pixel = pixels[i] * rgb_rates[0] 
					+ pixels[i + 1] * rgb_rates[1] 
					+ pixels[i + 2] * rgb_rates[2];
		pixels[i] = pixel;
		pixels[i + 1] = min(pixel + sepia_rates[1], max_val);
		pixels[i + 2] = min(pixel + sepia_rates[2], max_val);
	}
}

extern "C" __declspec(dllexport) void sepiaCpp(float* pixels, int size,
	float* sepia_rates, float* rgb_rates,
	int bytes_pp, int start, int end) {
	Sepia(pixels, size, sepia_rates, rgb_rates, bytes_pp, start, end);
}