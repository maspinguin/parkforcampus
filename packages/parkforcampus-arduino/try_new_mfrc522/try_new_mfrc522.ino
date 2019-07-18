#include <MFRC522.h>
#include <SPI.h>

#define RST_PIN         9           // Configurable, see typical pin layout above
#define SS_PIN          10          // Configurable, see typical pin layout above

MFRC522 mfrc522(SS_PIN, RST_PIN);   // Create MFRC522 instance.


MFRC522::MIFARE_Key key;
MFRC522::MIFARE_Key newKeyA = {keyByte: {0xA0, 0xA1, 0xA2, 0xA3, 0xA4, 0xA5}};
MFRC522::MIFARE_Key newKeyB = {keyByte: {0xB0, 0xB1, 0xB2, 0xB3, 0xB4, 0xB5}};

void setup() {
  Serial.begin(9600);
  SPI.begin();
  while (!Serial);    // Do nothing if no serial port is opened (added for Arduinos based on ATMEGA32U4)
    SPI.begin();        // Init SPI bus
    mfrc522.PCD_Init(); // Init MFRC522 card

     for (byte i = 0; i < 6; i++) {
        key.keyByte[i] = 0xFF;
    }

    Serial.println(F("Scan a MIFARE Classic PICC to demonstrate read and write."));
    Serial.print(F("Using key (for A and B):"));
    dump_byte_array(key.keyByte, MFRC522::MF_KEY_SIZE);
    Serial.println();

    Serial.println(F("BEWARE: Data will be written to the PICC, in sector #1"));
}
String input;
String inputVars;

String command = "";
String var1="", var2="";
char sz[] = "Here; is some; sample;100;data;1.414;1020";
void loop() {

  if(Serial.available()){
        input = Serial.readStringUntil('\r\n');
   
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

    char dataBlock[]    = {
         '3','1','3','2','4','1','4','5'
    };

    String s="313233";
    int str_len = s.length() + 1; 
    char cstr[str_len];
    s.toCharArray(cstr, str_len);
  
    Serial.println(hex2str(cstr));
    delay(1000);
    //doRead();
    //doChangeKey();
}

void doWrite(String var1, String var2) {


}

void doRead() {
   if ( ! mfrc522.PICC_IsNewCardPresent())
        return;

    // Select one of the cards
    if ( ! mfrc522.PICC_ReadCardSerial())
        return;

     Serial.print(F("Card UID:"));
    dump_byte_array(mfrc522.uid.uidByte, mfrc522.uid.size);
    Serial.println();
    Serial.print(F("PICC type: "));
    MFRC522::PICC_Type piccType = mfrc522.PICC_GetType(mfrc522.uid.sak);
    Serial.println(mfrc522.PICC_GetTypeName(piccType));


      // Check for compatibility
    if (    piccType != MFRC522::PICC_TYPE_MIFARE_MINI
        &&  piccType != MFRC522::PICC_TYPE_MIFARE_1K
        &&  piccType != MFRC522::PICC_TYPE_MIFARE_4K) {
        Serial.println(F("This sample only works with MIFARE Classic cards."));
        return;
    }



    
    // In this sample we use the second sector,
    // that is: sector #1, covering block #4 up to and including block #7
    byte sector         = 1;
    byte blockAddr      = 4;
    byte dataBlock[]    = {
        0x62, 0x61, 0x6e, 0x67, //  1,  2,   3,  4,
        0x20, 0x06, 0x07, 0x08, //  5,  6,   7,  8,
        0x09, 0x0a, 0xff, 0x0b, //  9, 10, 255, 11,
        0x0c, 0x0d, 0x0e, 0x0f  // 12, 13, 14, 15
    };
    byte trailerBlock   = 7;
    MFRC522::StatusCode status;
    byte buffer[18];
    byte size = sizeof(buffer);


     // Authenticate using key A
    Serial.println(F("Authenticating using key A..."));
    status = (MFRC522::StatusCode) mfrc522.PCD_Authenticate(MFRC522::PICC_CMD_MF_AUTH_KEY_A, 7, &newKeyA , &(mfrc522.uid));
    if (status != MFRC522::STATUS_OK) {
        Serial.print(F("PCD_Authenticate() failed: "));
        Serial.println(mfrc522.GetStatusCodeName(status));
        return;
    }

    
    // Show the whole sector as it currently is
    Serial.println(F("Current data in sector:"));
    mfrc522.PICC_DumpMifareClassicSectorToSerial(&(mfrc522.uid), &newKeyA, sector);
    Serial.println();


      // Read data from the block
    Serial.print(F("Reading data from block ")); Serial.print(blockAddr);
    Serial.println(F(" ..."));
    status = (MFRC522::StatusCode) mfrc522.MIFARE_Read(blockAddr, buffer, &size);
    if (status != MFRC522::STATUS_OK) {
        Serial.print(F("MIFARE_Read() failed: "));
        Serial.println(mfrc522.GetStatusCodeName(status));
    }
    Serial.print(F("Data in block ")); Serial.print(blockAddr); Serial.println(F(":"));
    dump_byte_array(buffer, 16); Serial.println();
    Serial.println();


       // Authenticate using key B
    Serial.println(F("Authenticating again using key B..."));
    status = (MFRC522::StatusCode) mfrc522.PCD_Authenticate(MFRC522::PICC_CMD_MF_AUTH_KEY_B, trailerBlock, &newKeyB, &(mfrc522.uid));
    if (status != MFRC522::STATUS_OK) {
        Serial.print(F("PCD_Authenticate() failed: "));
        Serial.println(mfrc522.GetStatusCodeName(status));
        return;
    }


    // Write data to the block
    Serial.print(F("Writing data into block ")); Serial.print(blockAddr);
    Serial.println(F(" ..."));
    dump_byte_array(dataBlock, 16); Serial.println();
    status = (MFRC522::StatusCode) mfrc522.MIFARE_Write(blockAddr, dataBlock, 16);
    if (status != MFRC522::STATUS_OK) {
        Serial.print(F("MIFARE_Write() failed: "));
        Serial.println(mfrc522.GetStatusCodeName(status));
    }
    Serial.println();


     // Read data from the block (again, should now be what we have written)
    Serial.print(F("Reading data from block ")); Serial.print(blockAddr);
    Serial.println(F(" ..."));
    status = (MFRC522::StatusCode) mfrc522.MIFARE_Read(blockAddr, buffer, &size);
    if (status != MFRC522::STATUS_OK) {
        Serial.print(F("MIFARE_Read() failed: "));
        Serial.println(mfrc522.GetStatusCodeName(status));
    }
    Serial.print(F("Data in block ")); Serial.print(blockAddr); Serial.println(F(":"));
    dump_byte_array(buffer, 16); Serial.println();



       // Check that data in block is what we have written
    // by counting the number of bytes that are equal
    Serial.println(F("Checking result..."));
    byte count = 0;
    for (byte i = 0; i < 16; i++) {
        // Compare buffer (= what we've read) with dataBlock (= what we've written)
        if (buffer[i] == dataBlock[i])
            count++;
    }
    Serial.print(F("Number of bytes that match = ")); Serial.println(count);
    if (count == 16) {
        Serial.println(F("Success :-)"));
    } else {
        Serial.println(F("Failure, no match :-("));
        Serial.println(F("  perhaps the write didn't work properly..."));
    }
    Serial.println();



    // Dump the sector data
    Serial.println(F("Current data in sector:"));
    mfrc522.PICC_DumpMifareClassicSectorToSerial(&(mfrc522.uid), &newKeyA, sector);
    Serial.println();

    // Halt PICC
    mfrc522.PICC_HaltA();
    // Stop encryption on PCD
    mfrc522.PCD_StopCrypto1();


}

void doChangeKey() {
  // Look for new cards
  if (!mfrc522.PICC_IsNewCardPresent())
    return;

  // Select one of the cards
  if (!mfrc522.PICC_ReadCardSerial())
    return;

  // Show some details of the PICC (that is: the tag/card)
  Serial.print(F("Card UID:"));
  dump_byte_array(mfrc522.uid.uidByte, mfrc522.uid.size);
  Serial.println();
  Serial.print(F("PICC type: "));
  MFRC522::PICC_Type piccType = mfrc522.PICC_GetType(mfrc522.uid.sak);
  Serial.println(mfrc522.PICC_GetTypeName(piccType));

  // Check for compatibility
  if (piccType != MFRC522::PICC_TYPE_MIFARE_MINI
      && piccType != MFRC522::PICC_TYPE_MIFARE_1K
      && piccType != MFRC522::PICC_TYPE_MIFARE_4K) {
    Serial.println(F("This sample only works with MIFARE Classic cards."));
    return;
  }

  // change keys in section 15 block 63
  if (!MIFARE_SetKeys(&key, &key, &newKeyA, &newKeyB, 1)) {
    return;
  }

  // Halt PICC
  mfrc522.PICC_HaltA();
  // Stop encryption on PCD
  mfrc522.PCD_StopCrypto1();
}

unsigned char hexToDecimal(unsigned char hex) {
     if(hex > 0x39) hex -= 7; // adjust for hex letters upper or lower case
      return(hex & 0xf);
}

void dump_byte_array(byte *buffer, byte bufferSize) {
    for (byte i = 0; i < bufferSize; i++) {
        Serial.print(buffer[i] < 0x10 ? " 0" : " ");
        Serial.print(buffer[i], HEX);
    }
}



bool MIFARE_SetKeys(MFRC522::MIFARE_Key* oldKeyA, MFRC522::MIFARE_Key* oldKeyB,
                    MFRC522::MIFARE_Key* newKeyA, MFRC522::MIFARE_Key* newKeyB,
                    int sector) {
  MFRC522::StatusCode status;
  byte trailerBlock = sector * 4 + 3;
  byte buffer[18];
  byte size = sizeof(buffer);

  // Authenticate using key A
  Serial.println(F("Authenticating using key A..."));
  status = (MFRC522::StatusCode)mfrc522.PCD_Authenticate(MFRC522::PICC_CMD_MF_AUTH_KEY_A, trailerBlock, oldKeyA, &(mfrc522.uid));
  if (status != MFRC522::STATUS_OK) {
    Serial.print(F("PCD_Authenticate() failed: "));
    Serial.println(mfrc522.GetStatusCodeName(status));
    return false;
  }

  // Show the whole sector as it currently is
  Serial.println(F("Current data in sector:"));
  mfrc522.PICC_DumpMifareClassicSectorToSerial(&(mfrc522.uid), oldKeyA, sector);
  Serial.println();

  // Read data from the block
  Serial.print(F("Reading data from block ")); Serial.print(trailerBlock);
  Serial.println(F(" ..."));
  status = (MFRC522::StatusCode) mfrc522.MIFARE_Read(trailerBlock, buffer, &size);
  if (status != MFRC522::STATUS_OK) {
    Serial.print(F("MIFARE_Read() failed: "));
    Serial.println(mfrc522.GetStatusCodeName(status));
    return false;
  }
  Serial.print(F("Data in block ")); Serial.print(trailerBlock); Serial.println(F(":"));
  dump_byte_array(buffer, 16); Serial.println();
  Serial.println();

  // Authenticate using key B
  Serial.println(F("Authenticating again using key B..."));
  status = (MFRC522::StatusCode)mfrc522.PCD_Authenticate(MFRC522::PICC_CMD_MF_AUTH_KEY_B, trailerBlock, oldKeyB, &(mfrc522.uid));
  if (status != MFRC522::STATUS_OK) {
    Serial.print(F("PCD_Authenticate() failed: "));
    Serial.println(mfrc522.GetStatusCodeName(status));
    return false;
  }

  if (newKeyA != nullptr || newKeyB != nullptr) {
    for (byte i = 0; i < MFRC522::MF_KEY_SIZE; i++) {
      if (newKeyA != nullptr) {
        buffer[i] = newKeyA->keyByte[i];
      }
      if (newKeyB != nullptr) {
        buffer[i+10] = newKeyB->keyByte[i];
      }
    }
  }

  // Write data to the block
  Serial.print(F("Writing data into block ")); Serial.print(trailerBlock);
  Serial.println(F(" ..."));
  status = (MFRC522::StatusCode) mfrc522.MIFARE_Write(trailerBlock, buffer, 16);
  if (status != MFRC522::STATUS_OK) {
    Serial.print(F("MIFARE_Write() failed: "));
    Serial.println(mfrc522.GetStatusCodeName(status));
    return false;
  }
  Serial.println();

  // Read data from the block (again, should now be what we have written)
  Serial.print(F("Reading data from block ")); Serial.print(trailerBlock);
  Serial.println(F(" ..."));
  status = (MFRC522::StatusCode)mfrc522.MIFARE_Read(trailerBlock, buffer, &size);
  if (status != MFRC522::STATUS_OK) {
    Serial.print(F("MIFARE_Read() failed: "));
    Serial.println(mfrc522.GetStatusCodeName(status));
  }
  Serial.print(F("Data in block ")); Serial.print(trailerBlock); Serial.println(F(":"));
  dump_byte_array(buffer, 16); Serial.println();
  return true;
}



// HEX TO CHAR
// char dataBlock[]    = {
//         '3','1','3','2','4','1','4','5' // 12AE
//    };
//    
//    
//    Serial.println(hex2str(dataBlock));
char nibble2c(char c)
{
   if ((c>='0') && (c<='9'))
      return c-'0' ;
   if ((c>='A') && (c<='F'))
      return c+10-'A' ;
   if ((c>='a') && (c<='a'))
      return c+10-'a' ;
   return -1 ;
}

char hex2c(char c1, char c2)
{
   if(nibble2c(c2) >= 0)
     return nibble2c(c1)*16+nibble2c(c2) ;
   return nibble2c(c1) ;
}

String hex2str(char *data)
{
   String result = "" ;
   for (int i=0 ; nibble2c(data[i])>=0 ; i++)
   {
      result += hex2c(data[i],data[i+1]) ;
      if(nibble2c(data[i+1])>=0)
        i++ ;
   }
   return result;
}

// END OF HEX TO CHAR
