//knihovna pro oled - https://botland.store/blog/esp32-connecting-oled-display/
#include<Arduino.h>
#include <Wire.h>
#include <Adafruit_GFX.h>
#include "Adafruit_SH1106.h"
#include <BluetoothSerial.h>
#include "picture.h"
#include "esp_adc_cal.h"
//#include <Fonts/FreeMonoBoldOblique9pt7b.h>
//defines the I2C pins to which the display is connected
#define OLED_SDA 21
#define OLED_SCL 22

#define Focus_pin 26
#define Shutt_pin 27
#define bleskPin  32
//define touch pins
#define touchPin_ok T5
#define touchPin_up T0
#define touchPin_down T2
#define touchPin_prev  T4
#define touchPin_next  T3
//definovaní grafiky
#define header_Height 10        //vyška hlavičky
#define Line_Height 8           //výška řadku
//definováni velikosti menu
#define Menu_hlavni_delka 3
#define Menu_dalkova_spoust_delka 2
#define Menu_Kapka_delka 6
#define Menu_Casosber_delka 4
//touch stav
bool touched_ok = false;
bool touched_up = false;
bool touched_down = false;
bool touched_prev = false;
bool touched_next = false;
//doba posledního zmáčknutí
int32_t touched_ok_last = 0;
int32_t touched_up_last = 0;
int32_t touched_down_last = 0;
int32_t touched_prev_last = 0;
int32_t touched_next_last = 0;
//menu
bool uprava_hodnot = false;
int Menu = 0;                     //ukazatel ve kterém menu se nacházíme 0 - hlavní menu, 1 - dálková spošt, 2 - Kapka, 3 - časosběr,
int Menu_poloha[4] = {0,0,0,0};   //ukazatel polohy kde jsme v hlavním menu
String Menu_hlavni[Menu_hlavni_delka] = {"dalkova spoust","Kapka","casosber"};
String Menu_Dalkova_spoust[Menu_dalkova_spoust_delka] = {"Start","Zpet"};
String Menu_Kapka[Menu_Kapka_delka] = {"pocet kapek","velikost kapky","cas mezi kapkami","zpozdeni blesku","start","zpet"};
String Menu_Casosber[Menu_Casosber_delka] = {"pocet fotek", "interval", "start", "zpet"};
//hodnoty
//int Hodnoty_Dalkova_spoust[Menu_dalkova_spoust_delka-2] ={}; 
uint16_t Hodnoty_Kapka[Menu_Kapka_delka-2] = {1,1,500,500};
int Hodnoty_Casosber[Menu_Casosber_delka-2] = {100,1};

uint16_t max_minKapka[Menu_Kapka_delka-2][2] = {{1,10},{1,10},{1,9999},{10,9999}};
uint16_t max_minCasosber[Menu_Casosber_delka-2][2] = {{1,9999},{1,120}}; 

int64_t lastADC = -60000;

Adafruit_SH1106 display(21, 22);
BluetoothSerial SerialBT;

//hlavičky funkcí 
void setInterrupt(); //nastavení pčerušení kapacitních snímačů
void touchInterrupt_OK(); //funkce která se volá při přerušení z prostředního kapacitního snímače
void touchInterrupt_DOWN(); //funkce která se volá při přerušení z spodního kapacitního snímače
void touchInterrupt_UP(); //funkce která se volá při přerušení z horního kapacitního snímače
void printCursor(int pozice);     //funke pro vypsání kurzoru který ukazuje jaká položka je vybrána. Pozice je číslo na jakém řadku má být. 0,1,2... 
void zmen_index_vyberu(int pocet); //mění index pole s menu
void vyhodnot_ok(); //vyhodnotí zmačknutí prostředního tlačítka OK
void vypis_menu(); //vypiše menu na display
void start_bluetooth(); //zapne bluetooth
void vycisti_menu(); //vymaže menu na displeji
void vypis_hodnoty(); //vypíše hodnoty 
void start_kalibrace(); //funkce připravená pro spuštěni kalibrace
void vypis_foto();  //na display vypíše fotku 
void set_Pin(); //nastavení pinů
void Photo(); //funkce která vyrobí fotku
void kapka(); //funkce spustí kapku
void openShuter(); //funkce která otevře závěrku fotoaparátu
void closeShuter(); //funkce zavře závěrku fotoaparátu
void blesk(); //funkce spustí blesk
void ReadBT(); //funkce čte hodnoty z bluetooth
void readBTKapka(); //funkce čte hodnoty z bluetooth pro mod kapka
void readBTCasosber();  //funkce čte hodnoty z bluetooth pro mod časosběr
void Casosber();  //funkce která vytvoří časosběr
void ReadSerial(); //funkce která čte data ze seriového portu
void readSKapka(); //funkce čte data ze serioveho portu pro mod kapka 
void readSCasosber(); //funkce čte data ze seriového portu pro mod časosběr
void readBattery(); //funkce čte kapacitu baterie


void setup()   {
  adc1_config_width(ADC_WIDTH_10Bit);
  adc1_config_channel_atten(ADC1_CHANNEL_5, ADC_ATTEN_11db);
  setInterrupt();
  pinMode(33,INPUT);
  set_Pin();
  Serial.begin(115200);
  start_bluetooth();
  //define the type of display used and the I2C address
  display.begin(SH1106_SWITCHCAPVCC, 0x3C);
  display.clearDisplay();
  display.display();
  Wire.begin();
  display.setTextColor(WHITE);
  display.setTextSize(1,1);
  display.setCursor(0, 0);
  display.drawBitmap(0,0,uvod,128,64,WHITE);
  display.display();
  delay(1000);
  //delay(500);
  display.clearDisplay();
  display.drawLine(0,8,128,8,1);
  display.setCursor(0,0);
  display.print("Qappka");
  //display.print(map(analogRead(33), 0.0f, 4095.0f, 0, 100));
  display.display();
  display.setCursor(8,10);
  for (int i = 0; i < 3; i++)
  {
    display.setCursor(0,display.getCursorY());
    display.println(Menu_hlavni[i]);
  }
}

void loop() {
  if (touched_ok )
  {
    if ((millis() - touched_ok_last) > 300)
    {
      vyhodnot_ok();
      touched_ok_last = millis();
    }
    touched_ok = false;
  }
  else if (touched_down)
  {
    if ((millis() - touched_down_last) > 300)
    {
      zmen_index_vyberu(1);
      touched_down_last = millis();
    }
    touched_down = false;
  }
  else if (touched_up)
  {
    if ((millis() - touched_up_last) > 300)
    {
      zmen_index_vyberu(-1);
      touched_up_last = millis();
    }
    touched_up = false;
  }
  
  if (SerialBT.available() > 0)
  {
    //Serial.print(1);
    ReadBT();
  }

  if (Serial.available() > 0)
  {
    ReadSerial();
  }
  
  //readBattery();
  
  printCursor(Menu_poloha[Menu]);
  display.display();
  
}

void touchInterrupt_OK(){
  if (millis() > 100){
    touched_ok = true;
  }
}
void touchInterrupt_DOWN(){
  if (millis() > 100)
  {
    touched_down = true;
  }
}
void touchInterrupt_UP(){
  if (millis() > 100)
  {
    touched_up = true; 
  }
}


//nastaví přerušení
void setInterrupt(){ 
  touchAttachInterrupt(touchPin_ok, touchInterrupt_OK, 80);     //Interrupt pro OK
  touchAttachInterrupt(touchPin_down,touchInterrupt_DOWN,77);   //Interrupt pro down
  touchAttachInterrupt(touchPin_up, touchInterrupt_UP, 72);     //Interrupt pro UP  
}   

void printCursor(int pozice){  //pozice - jaká položka je vybrána
  vypis_menu();
  if (Menu == 0)
  {
    display.setCursor(0,header_Height+(pozice*Line_Height));
    display.setTextColor(BLACK,WHITE);
    display.print(Menu_hlavni[pozice]);
    display.setTextColor(WHITE,BLACK);
  }
  if (Menu == 1){
    display.setCursor(0,header_Height+(pozice*Line_Height));
    display.setTextColor(BLACK,WHITE);
    display.print(Menu_Dalkova_spoust[pozice]);
    display.setTextColor(WHITE,BLACK);
  }
  if (Menu == 2){
    if (uprava_hodnot)
    {
      display.setCursor(104,header_Height+(pozice*Line_Height));
      display.setTextColor(BLACK,WHITE);
      display.print(Hodnoty_Kapka[pozice]);
      display.setTextColor(WHITE,BLACK);
    }
    else{
      display.setCursor(0,header_Height+(pozice*Line_Height));
      display.setTextColor(BLACK,WHITE);
      display.print(Menu_Kapka[pozice]);
      display.setTextColor(WHITE,BLACK);
    }
  }
  if (Menu == 3){
    if (uprava_hodnot)
    {
      display.setCursor(104,header_Height+(pozice*Line_Height));
      display.setTextColor(BLACK,WHITE);
      display.print(Hodnoty_Casosber[pozice]);
      display.setTextColor(WHITE,BLACK);
    }
    else{
      display.setCursor(0,header_Height+(pozice*Line_Height));
      display.setTextColor(BLACK,WHITE);
      display.print(Menu_Casosber[pozice]);
      display.setTextColor(WHITE,BLACK);
    }
  }
}

void zmen_index_vyberu(int pocet){
  if (Menu == 0)
  {
    if ((Menu_poloha[0] + pocet) > (Menu_hlavni_delka-1))
    {
      Menu_poloha[0] = 0;
    }
    else if ((Menu_poloha[0] + pocet) < 0)
    {
      Menu_poloha[0] = Menu_hlavni_delka - 1;
    }
    else{
      Menu_poloha[0] = Menu_poloha[0] + pocet;
    }    
  }
  if (Menu == 1)
  {
    if ((Menu_poloha[1] + pocet) > (Menu_dalkova_spoust_delka-1))
    {
      Menu_poloha[1] = 0;
    }
    else if ((Menu_poloha[1] + pocet) < 0)
    {
      Menu_poloha[1] = Menu_dalkova_spoust_delka - 1;
    }
    else{
      Menu_poloha[1] = Menu_poloha[1] + pocet;
    }    
  }
  if (Menu == 2)
  {
    if (uprava_hodnot)
    {
      if (((Hodnoty_Kapka[Menu_poloha[2]] - pocet) >= max_minKapka[Menu_poloha[2]][0]) && (Hodnoty_Kapka[Menu_poloha[2]] - pocet) <= max_minKapka[Menu_poloha[2]][1])
      {
        Hodnoty_Kapka[Menu_poloha[2]] = Hodnoty_Kapka[Menu_poloha[2]] - pocet;
      }
    }
    else{
      if ((Menu_poloha[2] + pocet) > (Menu_Kapka_delka-1))
      {
        Menu_poloha[2] = 0;
      }
      else if ((Menu_poloha[2] + pocet) < 0)
      {
        Menu_poloha[2] = Menu_Kapka_delka - 1;
      }
      else{
        Menu_poloha[2] = Menu_poloha[2] + pocet;
      }    
    }
  }
  if (Menu == 3)
  {
    if (uprava_hodnot)
    {
      if (((Hodnoty_Casosber[Menu_poloha[3]] - pocet) >= max_minCasosber[Menu_poloha[3]][0]) && (Hodnoty_Casosber[Menu_poloha[3]]  - pocet) <= max_minCasosber[Menu_poloha[3]][1])
      {
        Hodnoty_Casosber[Menu_poloha[3]] = Hodnoty_Casosber[Menu_poloha[3]] - pocet;
      }
    }
    else{
      if ((Menu_poloha[3] + pocet) > (Menu_Casosber_delka-1))
      {
        Menu_poloha[3] = 0;
      }
      else if ((Menu_poloha[3] + pocet) < 0)
      {
        Menu_poloha[3] = Menu_Casosber_delka - 1;
      }
      else{
        Menu_poloha[3] = Menu_poloha[3] + pocet;
      }    
    }
  }
}

void vyhodnot_ok(){
  if (Menu == 0)
  {
    if (Menu_poloha[0] == 3)
    {
      //funkce pro kalibraci
      start_kalibrace();
    }
    else{
      Menu = Menu_poloha[0] + 1;
      vypis_menu();
      Menu_poloha[0] = 0;
      uprava_hodnot = false;
    }
  }

  else if (Menu == 1)
  {
    if (Menu_poloha[1] == (Menu_dalkova_spoust_delka - 2))
    {
      /* funkce pro dálkovou spoust */
      Photo();
      
    }
    if (Menu_poloha[1] == (Menu_dalkova_spoust_delka - 1))
    {
      Menu = 0;
      Menu_poloha[1] = 0;
      vypis_menu();
    }
    
  }
  
  else if (Menu == 2)
  {
    if (Menu_poloha[2] < (Menu_Kapka_delka - 2))
    {
      uprava_hodnot = !uprava_hodnot;
      vypis_menu();
    }
    if (Menu_poloha[2] == (Menu_Kapka_delka - 2))
    {
      /* funkce pro kapku */
      kapka();
      
    }
    if (Menu_poloha[2] == (Menu_Kapka_delka - 1))
    {
      Menu = 0;
      Menu_poloha[2] = 0;
      vypis_menu();
    }
    
  }
  
  else if (Menu == 3)
  {
    if (Menu_poloha[3] < (Menu_Casosber_delka - 2))
    {
      uprava_hodnot = !uprava_hodnot;
      vypis_menu();
    }
    if (Menu_poloha[3] == (Menu_Casosber_delka - 2))
    {
      /* funkce pro časosber*/
      Casosber();
      
    }
    if (Menu_poloha[3] == (Menu_Casosber_delka - 1))
    {
      Menu = 0;
      Menu_poloha[3] = 0;
      vypis_menu();
    }
    
  }
  
}

void vypis_menu(){
  vycisti_menu();
  vypis_hodnoty();
  if (Menu == 0)
  {
    display.setTextColor(WHITE,BLACK);
    for (int i = 0; i < Menu_hlavni_delka; i++)
    {
      display.setCursor(0,header_Height+(i*Line_Height));
      display.print(Menu_hlavni[i]);
    }
  }
  else if (Menu == 1)
  {
    display.setTextColor(WHITE,BLACK);
    for (int i = 0; i < Menu_dalkova_spoust_delka; i++)
    {
      display.setCursor(0,header_Height+(i*Line_Height));
      display.print(Menu_Dalkova_spoust[i]);
    }
    
  }
  else if (Menu == 2)
  {
    display.setTextColor(WHITE,BLACK);
    for (int i = 0; i < Menu_Kapka_delka; i++)
    {
      display.setCursor(0,header_Height+(i*Line_Height));
      display.print(Menu_Kapka[i]);
    }
    
  }
  else if (Menu == 3)
  {
    display.setTextColor(WHITE,BLACK);
    for (int i = 0; i < Menu_Casosber_delka; i++)
    {
      display.setCursor(0,header_Height+(i*Line_Height));
      display.print(Menu_Casosber[i]);
    }
  }    
}

void vycisti_menu(){
  display.setTextColor(WHITE,BLACK);
  for (int i = 0; i < 64/(Line_Height-2); i++)
  {
    display.setTextColor(WHITE,BLACK);
    display.setCursor(0,header_Height+(i*(Line_Height-2)));
    display.print("                             ");
    //display.setCursor(126,header_Height+(i*Line_Height));
  }
  
}

void start_bluetooth(){
  SerialBT.begin("Quappka");
}

void vypis_hodnoty(){
  if (Menu == 2)
  {
    display.setTextColor(WHITE,BLACK);
    for (int i = 0; i < (Menu_Kapka_delka - 2); i++)
    {
      display.setCursor(104,header_Height+(i*Line_Height));
      display.print(Hodnoty_Kapka[i]);  
    }
  }
  if (Menu == 3)
  {
    display.setTextColor(WHITE,BLACK);
    for (int i = 0; i < (Menu_Casosber_delka - 2); i++)
    {
      display.setCursor(104,header_Height+(i*Line_Height));
      display.print(Hodnoty_Casosber[i]);  
    }
  }
}

void start_kalibrace(){
  vypis_foto();
}

void vypis_foto(){
  vycisti_menu();
  display.drawBitmap(int((128/2)-(Foto_Width/2)),10,Foto,63,54,WHITE);
  display.display();
}

void set_Pin(){
    pinMode(Focus_pin, OUTPUT);
    pinMode(Shutt_pin,OUTPUT);
    pinMode(bleskPin,OUTPUT);
}

void Photo(){
    vypis_foto();
    digitalWrite(Focus_pin,HIGH);
    digitalWrite(Shutt_pin,HIGH);
    delay(3);
    digitalWrite(Shutt_pin,LOW);
    digitalWrite(Focus_pin,LOW);
    vypis_menu();
}

void kapka(){
  vypis_foto();
  uint8_t velikost_kapka = map(Hodnoty_Kapka[1],1,10,80,120);
  uint8_t pocetkapke = Hodnoty_Kapka[0];
  uint16_t meziKapkami = Hodnoty_Kapka[2];
  uint16_t zpozdeniBlesku = Hodnoty_Kapka[3];
  openShuter();
  for (int i = 0; i < pocetkapke; i++)
  {
    Wire.beginTransmission(0x01);
    delay(meziKapkami);
    Wire.write(velikost_kapka);
    Wire.endTransmission();
  }
  delay(zpozdeniBlesku);
  blesk();
  closeShuter();
  Wire.endTransmission();
  vypis_menu();
}
 
void openShuter(){
  digitalWrite(Focus_pin,HIGH);
  digitalWrite(Shutt_pin,HIGH);
}

void closeShuter(){
  digitalWrite(Shutt_pin,LOW);
  digitalWrite(Focus_pin,LOW);
}

void blesk(){
  digitalWrite(bleskPin,HIGH);
  delay(1);
  digitalWrite(bleskPin,LOW);
}

void ReadBT(){
  int hlavicka = 0;
  hlavicka = (int)SerialBT.read();
  if (hlavicka == 1)
  {
    readBTKapka();
  }

  else if (hlavicka == 2)
  {
    readBTCasosber();
  }

  else if (hlavicka == 3)
  {
    Photo();
  }
    
}

void readBTKapka(){
  byte data[6];
  uint8_t pocet_kapek = 0;
  uint8_t velikost_kapka = 0;
  uint16_t mezi_kapka = 0;
  uint16_t blesk_kapka = 0;

  for (int i = 0; i < 6; i++)
  {
    data[i] = (int)SerialBT.read();
  }
  
  pocet_kapek = data[0];
  velikost_kapka = data[1];
  mezi_kapka = ((data[2] << 8) | data[3]);
  blesk_kapka = ((data[4] << 8) | data[5]);

  Hodnoty_Kapka[0] = pocet_kapek;
  Hodnoty_Kapka[1] = velikost_kapka;
  Hodnoty_Kapka[2] = mezi_kapka;
  Hodnoty_Kapka[3] = blesk_kapka;

  vypis_menu();
  kapka();

}

void readBTCasosber(){
  byte data[3];
  uint16_t pocet_fotek;
  uint8_t mezi_fotkami;

  for (int i = 0; i < 3; i++)
  {
    data[i] = (int)SerialBT.read();
  }

  pocet_fotek = ((data[0] << 8) | data[1]);
  mezi_fotkami = data[2];
  
  Hodnoty_Casosber[0] = pocet_fotek;
  Hodnoty_Casosber[1] = mezi_fotkami;

  vypis_menu();
  Casosber();

}

void Casosber(){
  vypis_foto();
  uint16_t pocet_fotek = Hodnoty_Casosber[0];
  uint8_t interval = Hodnoty_Casosber[1];
  for (int i = 0; i < pocet_fotek + 1; i++)
  {
    Photo();
    if (i != (pocet_fotek))
    {
      delay(interval * 1000);
    }
    
  }
  vypis_menu();
  
}

void ReadSerial(){
  int hlavicka = (int)Serial.read();
  if (hlavicka == 1)
  {
    readSKapka();
  }

  else if (hlavicka == 2)
  {
    readSCasosber();
  }
    
  else if (hlavicka == 3)
  {
    Photo();
  }
  
}

void readSKapka(){
  byte data[6];
  uint8_t pocet_kapek = 0;
  uint8_t velikost_kapka = 0;
  uint16_t mezi_kapka = 0;
  uint16_t blesk_kapka = 0;

  for (int i = 0; i < 6; i++)
  {
    data[i] = (int)Serial.read();
  }
  
  pocet_kapek = data[0];
  velikost_kapka = data[1];
  mezi_kapka = ((data[2] << 8) | data[3]);
  blesk_kapka = ((data[4] << 8) | data[5]);

  Hodnoty_Kapka[0] = pocet_kapek;
  Hodnoty_Kapka[1] = velikost_kapka;
  Hodnoty_Kapka[2] = mezi_kapka;
  Hodnoty_Kapka[3] = blesk_kapka;

  vypis_menu();
  kapka();
}

void readSCasosber(){
  byte data[3];
  uint16_t pocet_fotek;
  uint8_t mezi_fotkami;

  for (int i = 0; i < 3; i++)
  {
    data[i] = (int)Serial.read();
  }

  pocet_fotek = ((data[0] << 8) | data[1]);
  mezi_fotkami = data[2];
  
  Hodnoty_Casosber[0] = pocet_fotek;
  Hodnoty_Casosber[1] = mezi_fotkami;

  vypis_menu();
  Casosber();
}

void readBattery(){
  if ((millis() - lastADC) > 60000)
  {
    lastADC = millis();
    if (map(adc1_get_raw(ADC1_CHANNEL_5),480 , 636, 0, 100) < 6)
    {
      display.drawBitmap(120,0,low_battery,8,8,WHITE);
    }
    else if (map(adc1_get_raw(ADC1_CHANNEL_5),480 , 636, 0, 100) > 9)
    {
      display.setCursor(102,0);
      display.print("        ");
    }
  }
  
}
