#include <RFID.h>

#define SDA_DIO 9
#define RESET_DIO 8

RFID RC522(SDA_DIO, RESET_DIO);

void setup() {
  Serial.begin(9600);
  SPI.begin();
  RC522.init();
}
String input;
String inputVars;

String command = "";
String var1="", var2="";
char sz[] = "Here; is some; sample;100;data;1.414;1020";
void loop() {

  if(Serial.available()){
        input = Serial.readStringUntil('\r\n');
       
//        Serial.print("You typed: " );
//        Serial.println(input);
//
//        if(input.indexOf("command")>= 0) {
//          Serial.print((int)input.indexOf("command"));
//        } else {
//          Serial.print("false");
//        }

        char buf[sizeof(sz)];
        input.toCharArray(buf, sizeof(buf));
        char *p = buf;
        char *str;
        int i = 0;
        while ((str = strtok_r(p, ";", &p)) != NULL) {
          String a = String(str);
          Serial.print(int(i));
          Serial.println(str);
          
          if(i == 0) {
            command = a;
          }

          if(i == 1) {
            var1 = a;
          }

          if(i == 2) {
            var2 = a;

          
          }
                   
          i++;
        }
          
    }

    if(command == "writeCard" && var1 != "") {
       Serial.println(var1);
       doWrite(var1, var2);
    }
}

void doWrite(String var1, String var2) {
    if (RC522.isCard())
    {
      RC522.readCardSerial();
//      Serial.println("Card Detected");
//      for(int i=0;i<16;i++)
//      {
//        Serial.print(RC522.serNum[i],HEX);
//        Serial.print(" - ");
//        //Serial.print(RC522.serNum[i],HEX); //to print card detail in Hexa Decimal format
//      }
//    
//      Serial.println();
//      Serial.println();
//      
//     // Serial.println(var1);
//      //RC522.writeMFRC522(RC522.serNum[2], var1.toInt());
//
//      for(int i=0;i<16;i++)
//      {
//        Serial.print(RC522.serNum[i],DEC);
//        Serial.print(" - ");
//        //Serial.print(RC522.serNum[i],HEX); //to print card detail in Hexa Decimal format
//      }
//      Serial.println();
//      Serial.println();
//      
//      

       
      RC522.write(2, 10);
      Serial.println();
      Serial.println();
      Serial.println("test: ");
      Serial.println(RC522.serNum[2], DEC);
       Serial.println(RC522.serNum[3], DEC);
      
      command = "";
      var1 = "";
      var2 = "";
    } else {
      Serial.println("Please tap your card..");
    }
    delay(1000);

}
