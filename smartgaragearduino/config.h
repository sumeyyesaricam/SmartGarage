#ifndef config_h
#define config_h

HardwareSerial & Wifi = Serial3;

class SauroWifi {
  #define BUFFER_SIZE 1000
  char SerialMsg[BUFFER_SIZE];
  int SerialMsgLength = 0;
  
  public :
    char *Ssid="";
    char *Pass="";
    char *SubscribeTopic="";
    char *PublishTopic="";
    
    String SerialMessage="";
    void setup() {
      pinMode(7, OUTPUT);

      digitalWrite(7, LOW);
      delay(100);
      digitalWrite(7, HIGH);
      delay(1000);

      //Serial.println("yazıldı");
      //ssid
      Wifi.print(Ssid);
      delay(1000);
      //pass
      Wifi.print(Pass);
      delay(1000);
      // topic to subscribe
      Wifi.print(SubscribeTopic);
      delay(1000);
      // topic to publish
      Wifi.print(PublishTopic);
      //Serial.print("setup end");
    }
    
    bool Read ()
    {
        SerialMsg[SerialMsgLength] = Wifi.read();
        /*bekleniyor çünkü ilk harften sonra diğer harfler hemen gelmiyor.*/
        if (SerialMsg[SerialMsgLength] == '\n')
        {
            SerialMsg[--SerialMsgLength] = '\0';
            SerialMessage=String(SerialMsg);
            SerialMsgLength = 0;
            
            return true;
        }
      
      SerialMsgLength++;

      return false;
    }
};
#endif

