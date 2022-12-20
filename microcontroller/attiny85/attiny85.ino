#include <TinyWireS.h>

#define Address 0x01

uint8_t data = 0;

void setup() {
  pinMode(1,OUTPUT);
  TinyWireS.begin(Address);      // Begin I2C Communication
  TinyWireS.onReceive(ReadData);
  TinyWireS.onRequest(ReadData);         // When requested, call function transmit()

}


void loop() {
  if(data != 0){
    digitalWrite(1,HIGH);
    delay(data);
    digitalWrite(1,LOW);
    data = 0;
  }
  TinyWireS_stop_check();
}

void ReadData(){
  data = TinyWireS.receive();
}
