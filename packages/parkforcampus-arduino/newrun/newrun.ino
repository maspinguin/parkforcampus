#include <MFRC522.h>
#include <SPI.h>

#define RST_PIN         9           // Configurable, see typical pin layout above
#define SS_PIN          10          // Configurable, see typical pin layout above

MFRC522 mfrc522(SS_PIN, RST_PIN);   // Create MFRC522 instance.


MFRC522::MIFARE_Key oldKeyA;
MFRC522::MIFARE_Key oldKeyB;
MFRC522::MIFARE_Key newKeyA;
MFRC522::MIFARE_Key newKeyB;

void setup() {
  Serial.begin(9600);
  SPI.begin();
  while (!Serial);    // Do nothing if no serial port is opened (added for Arduinos based on ATMEGA32U4)
    SPI.begin();        // Init SPI bus
    mfrc522.PCD_Init(); // Init MFRC522 card

}

String input;
String inputVars;

String command = "";
String var1="", var2="", var3="", var4="";
char sz[] = "writeNewKey;A0A1A2A3A4A5;B0B1B2B3B4B5;A0A1A2A3A4A5;B0B1B2B3B4B5";

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

          if(i == 3) {
            var3 = a;
          }

          if(i == 4) {
            var4 = a;
          }
                   
          i++;
        }
          
    }

    if(command == "writeCard" && var1 != "") {
       //doWrite(var1);
    }

    if(command == "writeNewKey" && var1 != ""&& var2 != "") {
      doWriteKey(var1, var2, var3, var4);
    }

    if(command == "modePortal" && var1 != "") {
    }
  }


void doWriteKey(String _newKeyA, String _newKeyB, String _oldKeyA, String _oldKeyB) {
    if ( ! mfrc522.PICC_IsNewCardPresent()) {
        Serial.println("Please insert your new card ... ");
        delay(1000);
        return;
    }
    // Select one of the cards
    if ( ! mfrc522.PICC_ReadCardSerial()) {
        Serial.println("Please insert your new card ... ");
        delay(1000);
        return;
    } 

    if(_oldKeyA=="" && _oldKeyB == "") {
      oldKeyA = {keyByte: {0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF}};
      oldKeyB = {keyByte: {0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF}};
    } else {
      oldKeyA = convertKeyStringToKeyByte(_oldKeyA);
      oldKeyB = convertKeyStringToKeyByte(_oldKeyB);
      Serial.println(_oldKeyA);
    }

    // converting _newKeyA into &newKeyA
    newKeyA = convertKeyStringToKeyByte(_newKeyA);
    // converting _newKeyB into &newKeyB
    newKeyB = convertKeyStringToKeyByte(_newKeyB);
    
    
  
   // dump_byte_array(oldKeyA.keyByte, oldKeyA.keyByte.size);
    
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
    
     // change keys in section 1 block 7
    if (!MIFARE_SetKeys(&oldKeyA, &oldKeyB, &newKeyA, &newKeyB, 1)) {
      return;
    }

    // Halt PICC
    mfrc522.PICC_HaltA();
    // Stop encryption on PCD
    mfrc522.PCD_StopCrypto1();

    // reset variable
    command = "";
    var1="";
    var2="";
    var3="";
    var4="";
}

////////////////////////
// FUNCTION TO SET KEY//
///////////////////////
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
// END OF FUNCTION TO SET KEY


//////////////////
// HELPER ONLY //
////////////////
void dump_byte_array(byte* buffer, byte bufferSize) {
  for (byte i = 0; i < bufferSize; i++) {
    Serial.print(buffer[i] < 0x10 ? " 0" : " ");
    Serial.print(buffer[i], HEX);
  }
}


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


int x2i(char *s) 
{
 int x = 0;
 for(;;) {
   char c = *s;
   if (c >= '0' && c <= '9') {
      x *= 16;
      x += c - '0'; 
   }
   else if (c >= 'A' && c <= 'F') {
      x *= 16;
      x += (c - 'A') + 10; 
   }
   else break;
   s++;
 }
 return x;
}


MFRC522::MIFARE_Key convertKeyStringToKeyByte(String _newKey) {
   MFRC522::MIFARE_Key newKey;
   int str_len = _newKey.length() + 1; 
    char cstr[str_len];
    _newKey.toCharArray(cstr, str_len);
    int y= 0;
   
    for (int i=0 ; nibble2c(cstr[i])>=0 ; i++)
    {
      char c[4];
          c[0] = '0';
          c[1] = 'x';
          c[2] = cstr[i];
          c[3] = cstr[i+1];
      if(nibble2c(cstr[i+1])>=0){
         if(y < MFRC522::MF_KEY_SIZE) {
          byte  bytes[2];
          sscanf(c, "%04x", &bytes[0]);
          newKey.keyByte[y] = bytes[0];
         
        }
        //Serial.println();
        
        i++;
        y++;
      }
    }
    return newKey;
}


// END OF HELPER //
