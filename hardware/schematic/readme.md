# qappka
## schéma zapojení
<img src="https://github.com/kocevjak/qappka/blob/79aa676ef074b176dab750eec611daa8d9397247/hardware/schematic/Schematic_qappka.png" width=100%>

## ESP32
celé zařízení řídí mikroprocesor ESP32 od firmy espressif
## napájení
Dálková spoušť je napájena z Li-Pol akumulátoru. Akumulátor je nabíjen z USB přes integrovaný obvod TP4056. Napětí akumulátoru (3 – 4,2 V) je zvednuto na 5 V pomocí step-up měniče MT3608. 5 V slouží k napájení ventilu. 5 V je pomocí lineárního regulátoru snížené na 3.3 V.
## programování
Programování mikroprocesoru ESP32 probíhá přes USB konektor. Pro převod USB na UART slouží integrovaný obvod CH340.
## komunikace s ventilem
Qappka s ventilem komunikuje pomocí sběrnice I2C. Ventil je ke Qappce připojuje pomocí konenktoru RJ11.

# ventil
<img src="https://github.com/kocevjak/qappka/blob/9ca19c7f4309603359a1b8108686b2ecc7ac5988/hardware/schematic/Schematic_valve.png" width=100%>

## popis
Ke spínání a komunikaci s qappkou slouží mokrokontroler ATTINY85, který je napájen s Qappky<br/>
U návrhu DPS pro spínání ventilu jsem se snažil o univerzálnost. Chtěl jsem abych na stejný DPS mohl zapojit i pro jinou funkci než spínání ventilu (např. spínání fotoaparátu pomocí světla).
Jak bude DPS fungovat záleží na tom jaké součástky připájím a na nahraném softwaru v ATTINY85.<br/>

## Komunikace s qappkou
DPS má na sobě dva konektory RJ11 který slouží ke komunikaci přes I2C. Dva konektory má proto jelikož jsem chtěl, aby bylo možné zapojit více modulů za sebe.
