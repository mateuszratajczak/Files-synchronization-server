"# Files-synchronization-server" 
1. Mamy dwie aplikacje: klienta i serwera 

2. Aplikacja klienta: 
    - Uruchamiana jest z dwoma parametrami: nazwa użytkownika i ścieżka do lokalnego folderu 
    - Każdy klient ma swój lokalny folder z plikami 
    - Aplikacja obserwuje lokalny folder i patrzy na zmiany. Jak pojawią się tam nowe pliki, to wysyła je na serwer 
    - Jak pojawi się nowy plik dla danego użytkownika, to jest on pobierany do lokalnego folderu 
    - Po włączeniu patrzymy czy są nowe pliki i je ściągamy 
    - Wysyłamy / odbieramy w puli wątków 

3. Serwer: 
    - 5 folderów, które symulują 5 serwerów lub 5 dysków 
    - Klient wysyła np. 10 plików, więc serwer uruchamia 5 wątków na których równolegle kopiuje pliki do tych dysków (folderów) 
    - Wymagany jest kontroler, który tak rozłoży ruch tutaj, że do każdego z dysków (folderów) jednocześnie jest kopiowana taka sama liczba plików 
    - Jeżeli dołączymy drugiego klienta, który zacznie wysyłać pliki, to nie może być tak, że będzie on czekał jak skończą się zadania pierwszego klienta. Lista zadań jest mutowalna i musi być tworzona w inteligentny sposób 
    - Dla każdego dysku mamy plik tekstowy (np. csv), w którym opisujemy zawartość i dopasowanie do użytkownika 

Tworzymy puste pliki, czas kopiowania symulujemy usypianiem wątku. Możemy usypiać wątek np. losową liczbą z przedziału 1 - 15, żeby symulować różne rozmiary plików.
