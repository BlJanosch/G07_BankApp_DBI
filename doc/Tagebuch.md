# Tagebuch DBI - G05_BankApp (Jannik Längle, Noah Fedele)

|Datum|Zuständiger|Aufgabe|
|-----|-----------|-------|
|21.5.24|Noah Fedele|Basic UI im C# Programm erstellen|
|21.5.24|Noah Fedele|Datenbank erstellen und Test-Einträge machen|
|21.5.24|Jannik Längle|Klassen im C# Programm erstellen|
|04.06.24|Noah Fedele|Abheben Fenster hinzugefügt|
|04.06.24|Jannik Längle|WindowAnmelden designed und User & Eintrag Klasse aktualisiert|
|08.06.24|Jannik Längle|User Löschen und Login/SignIn verbessert|
|10.06.24|Noah Fedele|Geld abheben und überweisen ermöglicht|
|11.06.24|Noah Fedele|Funktion Einträge und Glücksspiel|
|11.06.24|Jannik Längle|User Registrieren falscher Kontostand|
|11.06.24|Noah Fedele|Ladefenster hinzugefügt und Doku|
|11.06.24|Jannik Längle|Windows geupdated|
|11.06.24|Jannik Längle|AnmeldenButton nach Abbrechen gleich|
|11.06.24|Jannik Längle|Login falsche Daten|
|13.06.24|Jannik Längle|Fixed Bug --> Benutzer nicht gefunden|
|13.06.24|Jannik Längle|Eintrag mit Startbonus|
|13.06.24|Jannik Längle|StatisticWindow hinzugefügt|
|13.06.24|Jannik Längle|StatisticWindow verbessert|
|13.06.24|Jannik Längle|Fixed Bug --> StatisticWindow Anzeigefehler|

## 21.5.24
Basic UI wurde erstellt, sowie die Datenbank mit entsprechenden Test-Einträgen. Zudem wurden noch alle Klassen angelegt.

## 04.06.24
Abheben Funktion wurde hinzugefügt und das WindowAnmelden wurde erstellt, sowie designed. Außerdem wurde die User & Eintrag Klassse, da die Daten nicht richtig eingelesen wurden.

## 08.06.24
Benutzer kann jetzt seinen Account löschen und die dazugehörigen Einträge wurden ebenfalls gelöscht. Die Eingabe wird nun bei der Anmeldung bzw. Regestrierung überprüft und ansonsten wird eine Fehlermeldung angezeigt. Solange noch kein User erstellt wurde, kann der Anmelden-Button auch nicht gedrückt werden, um Missverständnisse zu vermeiden.

## 10.06.24
Funktion um Geld abzuheben wurde hinzugefügt und das Überweisen von Geld ist jetzt auch möglich (an einen anderen User).

## 11.06.24
Jetzt werden alle Ein- und Ausgaben in der Eintrag-List auf der rechten Seite vom Screen angezeigt und gespeichert. Zudem wurde das Programm um eine Glücksspiel-Funktion erweitert, mit welcher man sein Geld vermehren, aber auch verbrennen kann.
Fixed Bug --> Beim Regestrieren wurde dem Benutzer ein falscher Kontostand zugewiesen.
Sobald ein User gelöscht wird, wird nun ein Ladefenster angezeigt, da es Zeit braucht, bis der Eintrag aus der DB enterfernt wird (ansonsten könnte es zu Fehlern kommen).
Windows geupdated, da jedem Window jetzt der MainUser mitgegeben wird.
Wenn es noch keinen User gibt ist der AnmeldenButton disabeld und sobald man Regestrieren klickt, sich jedoch anderst entscheidet und wieder auf Abbrechen klickt, ist der AnmeldenButton jetzt immer noch disabled.
Ladefenster um eine ProgessBar erweitert und die Anmeldung gegen falsche Daten gesichert.

## 13.06.24
Fixed Bug --> Benutzer konnte sich nicht anmelden, da sein Name nicht in der Liste gefunden wurde. Dieser Bug entstand aufgrund eines falschen IF-Statements.
Der Startbonus wird nun auch als Eintrag gespeichert und dem User angezeigt, falls sich dieser fragt, wo die 50€ herkommen.
StatisticWindow hinzugefügt, welches die Ausgaben und Einkünfte visualisiert. Dieses wurde noch erweitert, dass man die genauen Werte der Ausgaben oder Einkünfte sieht (einfach darüber hovern).
Fixed Bug --> Sobald mehr als ein Wert doppelt bei den Ausgaben oder Einkünften vorkam, lagen diese Werte übereinander, da wir `IndexOf` verwendet haben.

