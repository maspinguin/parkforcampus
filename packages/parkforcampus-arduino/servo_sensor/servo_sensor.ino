#include <NewPing.h>
#include <Servo.h>
#define TRIGGER_PIN   8
#define ECHO_PIN      7
#define TRIGGER_PIN2  10
#define ECHO_PIN2     9
#define MAX_DISTANCE 200

Servo servoAne;
NewPing ultrasonic1(TRIGGER_PIN, ECHO_PIN, MAX_DISTANCE);
NewPing ultrasonic2(TRIGGER_PIN2, ECHO_PIN2, MAX_DISTANCE);

void setup() {
  Serial.begin(115200);
  servoAne.attach(6);
  servoAne.write(0);
}

void loop() {
  delay(50);
  int US1 = ultrasonic1.ping_cm();
  int US2 = ultrasonic2.ping_cm();
  Serial.print("Hasil Sensor 1 :");
  Serial.print(US1);
  Serial.print("cm     |     ");
  Serial.print("Hasil Sensor 2 :");
  Serial.print(US2);
  Serial.println("cm");
  
  if(US1 <= 4 && US2 >= 6){
    servoAne.write(90);
    delay(5);
  }
  else if(US1 <= 5 && US2 <= 5){
    servoAne.write(0);
    delay(5);
  }
  else{
    servoAne.write(0);
    delay(5);
  }
}
