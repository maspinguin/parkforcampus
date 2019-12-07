



#include <Servo.h>
#include <SPI.h>
#include <MFRC522.h>
 
#define SS_PIN 48
#define RST_PIN 46
#define hijau 5
#define merah 6
#define servo 3

Servo myservo;
MFRC522 mfrc522(SS_PIN, RST_PIN);   // Create MFRC522 instance.
 
void setup() 
{
  Serial.begin(9600); 
  SPI.begin();     
  mfrc522.PCD_Init();
  pinMode(hijau,OUTPUT);
  pinMode(merah,OUTPUT);
  myservo.attach(servo);
  myservo.write(0);  
}
void loop() 
{
  // membaca ID card
  if ( ! mfrc522.PICC_IsNewCardPresent()) 
  {
    return;
  }
  // memilih salah satu card yang terdeteksi
  if ( ! mfrc522.PICC_ReadCardSerial()) 
  {
    return;
  }
  //tampilkan ID card di serial monitor
  Serial.print("UID tag :");
  String content= "";
  byte letter;
  for (byte i = 0; i < mfrc522.uid.size; i++) 
  {
     Serial.print(mfrc522.uid.uidByte[i] < 0x10 ? " 0" : " ");
     Serial.print(mfrc522.uid.uidByte[i], HEX);
     content.concat(String(mfrc522.uid.uidByte[i] < 0x10 ? " 0" : " "));
     content.concat(String(mfrc522.uid.uidByte[i], HEX));
  }
  Serial.println();
  Serial.print("Message : ");
  content.toUpperCase();
  if (content.substring(1) == "B9 30 07 7F") //ganti dengan ID RFID tag kalian
  {
    Serial.println("Authorized access");
    myservo.write(90);  
    digitalWrite(hijau,HIGH);
    delay(1000);
    myservo.write(0);  
    digitalWrite(hijau,LOW);
  }
//  else if(content.substring(1) == "4B 5F 69 0A") //ganti dengan ID RFID tag kalian
//  {
//    Serial.println("Authorized access");
//    myservo.write(180);  
//    digitalWrite(hijau,HIGH);
//    delay(3000);
//    myservo.write(0);  
//    digitalWrite(hijau,LOW);
//  }
 else   {
    Serial.println(" Access denied");
    digitalWrite(merah,HIGH);
    delay(1000);
    digitalWrite(merah,LOW);
  }
}
