
#include <SPI.h>
#include <MFRC522.h>
#include <string.h>

constexpr uint8_t NR_OF_READERS = 2;

constexpr uint8_t RST_PIN  = 5;
constexpr uint8_t SS_0_PIN = 53;
constexpr uint8_t SS_1_PIN = 45;

byte ssPins[] = {SS_0_PIN, SS_1_PIN};

byte readedCard[NR_OF_READERS][4];  // Matrix for storing UID over each reader
MFRC522 mfrc522[NR_OF_READERS];     // Create MFRC522 instances


void setup()
{
  Serial.begin(9600); // Initialize serial communications
  while (!Serial);    // Do nothing until serial connection is opened
  SPI.begin();        // Init SPI bus
  for (uint8_t reader = 0; reader < NR_OF_READERS; reader++) 
   {
    mfrc522[reader].PCD_Init(ssPins[reader], RST_PIN); // Init each MFRC522 card
   }
 pinMode(RST_PIN, OUTPUT);
 digitalWrite(RST_PIN, LOW);    // mfrc522 readers hard power down.
}


void loop() 
{
 for (uint8_t reader = 0; reader < NR_OF_READERS; reader++) 
  {
    if(getRFID(reader))
    {
      Serial.print(F("Reader "));
      Serial.print(reader);
      Serial.print(F(": Card UID:"));
      printUID(readedCard[reader]);
      Serial.println();
    }
  }
delay(2000);
}

//******************** Routine for print 4 byte UID to serial ******************************
void printUID(byte *buffer){
for (byte i = 0; i < 4; i++){
Serial.print(buffer[i] < 0x10 ? " 0" : " ");
Serial.print(buffer[i], HEX);}}
//********************************END OF ROUTINE********************************************

//************ Routine for scan readers and store the UIDs in readedCard array *************
bool getRFID(byte readern)
{
  bool isPICCpresent = false;
  digitalWrite(RST_PIN, HIGH);   // Get RC522 reader out of hard low power mode
  mfrc522[readern].PCD_Init();   // Init the reader
  //****** Trying to set antenna gain to max, erratic functioning of the readers ***********
  //mfrc522[readern].PCD_SetRegisterBitMask(mfrc522[readern].RFCfgReg, (0x07<<4)); 
  //mfrc522[readern].PCD_SetAntennaGain(0x04);
  // mfrc522[readern].PCD_ClearRegisterBitMask(mfrc522[readern].RFCfgReg, (0x07<<4));
  // mfrc522[readern].PCD_SetRegisterBitMask(mfrc522[readern].RFCfgReg, 0x07);
  //delay(50); 
  if (mfrc522[readern].PICC_IsNewCardPresent() && mfrc522[readern].PICC_ReadCardSerial())
    {
     memcpy(readedCard[readern], mfrc522[readern].uid.uidByte, 4); 
     isPICCpresent = true;
    }
  mfrc522[readern].PICC_HaltA();
  mfrc522[readern].PCD_StopCrypto1();
  digitalWrite(RST_PIN, LOW);    // return to hard low power mode
  return isPICCpresent;          // returns TRUE if PICC is detected, false if not
}
