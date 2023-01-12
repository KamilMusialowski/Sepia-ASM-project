; Temat projektu: Filtr Sepia
; Algorytm sumuje wartoœci RGB kolejnych pikseli przemno¿one przez odpowiedni wspó³czynnik.
; Nastêpnie oblicza nowe wartoœci RGB na podstawie powy¿szej sumy i wspó³czynnika si³y filtru.
; Tabela zapisanych, uzyskanych w ten sposób wartoœci stanowi podstawê do wygenerowania grafiki
; z na³o¿onym filtrem.
; Data wykonania projektu: semestr V/rok akademicki 2022/2023, Kamil Musia³owski, Informatyka Katowice AEiI
; Wersja: 1.0

; rcx/xmm0 - piksele
; rdx/xmm1 - wpó³czynniki sepii
; r8/xmm2 - wspó³czynniki rgb
; r9/xmm3 - wartoœci 255: (255 | 255 | 255 | 255)
; r11  - indeks startowy
; r10 - indeks koñcowy

.CODE
sepia proc			
	mov			ebx, dword ptr[rbp + 48]	; Pobieramy ze stosu wartoœæ indeksu startowego
	mov			r11, rbx					; Wpisujemy j¹ do rejestru r11


	mov			ebx, dword ptr[rbp + 56]	; To samo robimy z wartoœci¹ indeksu koñcowego 
	mov			r10, rbx					; Wpisujemy j¹ do rejestru r10


	movdqu		xmm1, oword ptr[rdx]		; pobieramy do xmm1 adres pamiêci, gdzie przechowuje siê
											; dane o rozmiarze 8 bajtów (octalword - 8) - w tym przypadku
											; z rejestru rdx --- wspó³czynniki sepii

	movdqu		xmm2, oword ptr[r8]			; analogicznie --- wspó³czynniki rgb
	movdqu		xmm3, oword ptr[r9]			; analogicznie --- tablica wartoœci 255


	sub			r10, r11					; Odejmujemy indkeks startowy od koñcowego
	mov			rdi, r10					; Wpisujemy do rdi
	shr			edi, 2						; Dzielimy wynik przez 4, bo ka¿dy piksel to 4 wartoœci - bgra
											; Przesuwamy o 2 bity w prawo


	mov			rax, 4h						; do rejestru rax wpisujemy wartoœæ 4 w zapisie hex
	mul			r11							; mno¿ymy wartoœæ indeksu pocz¹tkowego przez rejestr  rax (czyli 4 - bo piksel to 4 wartoœci bgra)
	add			rcx, rax					; dodajemy otrzyman¹ wartoœæ do adresu tablicy wynikowej
											; dziêki temu rejestr rcx wskazuje w tablicy wynikowej pozycjê odpowiadaj¹c¹ pierwszemu pikselowi
											; kawa³ka, który jest aktualnie przetwarzany


sepiaLoop:
	cmp			edi, 0h						; jeœli licznik pêtli dojdzie do 0 - wychodzimy
	je			endLoop						; skok do koñca pêtli
	
	movdqu		xmm0, oword ptr[rcx]		; pobieram do xmm0 wartoœæ piksela wskazywanego przez rejestr rcx

	mulps		xmm0, xmm2					; mno¿ê wartoœci opisuj¹ce pobrany piksel przez odpowiednie wspó³czynniki
	


											; w xmm0 mamy (a | r | g | b)
	movshdup    xmm4, xmm0					; w rejestrze xmm4 otrzymujemy (a | a | g | g)
    addps       xmm0, xmm4					; w xmm0 otrzymujemy (a+a | r+a | g+g | b+g)
    movhlps     xmm4, xmm0					; w xmm4 otrzymujemy (a | a | a+a | r+a)
    addps       xmm0, xmm4					; w xmm0 otrzymujemy (a+a+a | r+a+a | g+g+a+a | a+r+g+b)
	punpckldq	xmm0, xmm0					; w xmm0 otrzymujemy (g+g+a+a | g+g+a+a | a+r+g+b | a+r+g+b)
	punpcklqdq	xmm0, xmm0					; w xmm0 otrzymujemy (a+r+g+b | a+r+g+b | a+r+g+b | a+r+g+b)

	addps		xmm0, xmm1					; dodajemy odpowiednie wspó³czynniki rgb do odpowienich wartoœci piksela

	minps		xmm0, xmm3					; kolejne wartoœci piksela porównujemy z wartoœciami 255 i wybieramy mniejsze
	movdqu		oword ptr[rcx], xmm0		; przetworzony piksel wpisujemy w odpowiednie miejsce w tablicy pikseli
	
	add			rcx, 16						; przesuwamy wskaŸnik na kolejny piksel
	sub			rdi, 1						; dekrementujemy licznik pêtli
	jmp			sepiaLoop					; skok do pocz¹tku pêtli
endLoop:
    ret
sepia endp
END