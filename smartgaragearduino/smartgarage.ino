
#include <SPI.h>
#include <Servo.h> 
#include <SoftwareSerial.h>

#include "config.h"
SauroWifi SWifi;
Servo myservo;
void setup() {
  
 Serial.begin(115200);
  Wifi.begin(115200);
  delay(50);
  Serial.println("Client Started");
  myservo.attach(3);
  SWifi.Ssid="";
  SWifi.Pass="";
  SWifi.SubscribeTopic="/smartgarage";
  SWifi.PublishTopic="/smartgarage";
  
  SWifi.setup();
 
}

void loop() { 
  if (Serial.available())
    Wifi.write(Serial.read()); 
  }
void serialEvent3(){
  if(SWifi.Read())
  {
    Serial.print("Gelen Mesaj:");
    Serial.println(SWifi.SerialMessage);
    if(SWifi.SerialMessage.compareTo("open")==0)
    {
      for(int pos = 30; pos <= 150; pos +=15)
      {                                    
        myservo.write(pos);                 
        delay(200);                       
        } 
        delay(5000);
        for(int pos = 150; pos>30; pos -=15)
      {                                    
        myservo.write(pos);                 
        delay(200);                       
        } 
    }    
  }
}
