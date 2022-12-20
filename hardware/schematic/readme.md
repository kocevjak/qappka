# qappka
## schéma zapojení
<img src="https://github.com/kocevjak/qappka/blob/79aa676ef074b176dab750eec611daa8d9397247/hardware/schematic/Schematic_qappka.png" width=80%>

## ESP32
celé zařízení řídí mikroprocesor ESP32 od firmy espressif
## napájení
Dálková spoušť je napájena z Li-Pol akumulátoru. Akumulátor je nabíjen z USB přes integrovaný obvod TP4056. Napětí akumulátoru (3 – 4,2 V) je zvednuto na 5 V pomocí step-up měniče MT3608. 5 V slouží k napájení ventilu. 5 V je pomocí lineárního regulátoru snížené na 3.3 V.
## programování
Programování mikroprocesoru ESP32 probíhá přes USB konektor. Pro převod USB na UART slouží integrovaný obvod CH340.
## komunikace s ventilem
Qappka s ventilem komunikuje pomocí sběrnice I2C. Ventil se ke Qappce připojuje pomocí konenktoru RJ11.

# ventil