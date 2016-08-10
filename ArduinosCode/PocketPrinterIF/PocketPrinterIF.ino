#include <PocketPrinter.h>
#include <SPI.h>
#include <SPISRAM.h>

const int bandNum = 15;

PocketPrinter printer(6, 5, 13);
SPISRAM spiRam(17);
uint32_t dataSize;
uint8_t beforeMargin = 0;
uint8_t afterMargin = 0;

void setup() {
  Serial.begin(9600);
  while (!Serial) {}
  SPI.begin();
  SPI.setBitOrder(MSBFIRST);
  SPI.setClockDivider(SPI_CLOCK_DIV2);
  SPI.setDataMode(SPI_MODE0);
  dataSize = 0;
}

void loop() {
  if (Serial.available() >= 6) {
    receive();
    print(dataSize / 640);
    Serial.write("R");
    Serial.flush();
  }
  /*if (Serial.available() > 0) {
    char code = Serial.read();
    if (code == 'p') {
      print(bandNum);
    } else if (code == '?') {
      printer.SendInit();
      uint16_t  reply = printer.SendInquiry();
      bool b1 = printer.CheckAcknowledgement(highByte(reply));
      bool b2 = printer.CheckFatalError(lowByte(reply));
      Serial.print(b1);
      Serial.println(b2);
    }
  }*/
}

void receive() {
  digitalWrite(13, HIGH);
  dataSize = 0;
  dataSize += (uint8_t)Serial.read();
  dataSize = dataSize << 8;
  dataSize += (uint8_t)Serial.read();
  dataSize = dataSize << 8;
  dataSize += (uint8_t)Serial.read();
  dataSize = dataSize << 8;
  dataSize += (uint8_t)Serial.read();
  beforeMargin = (uint8_t)Serial.read();
  afterMargin  = (uint8_t)Serial.read();
  
  for (uint32_t i = 0; i < dataSize; i++) {
    while (Serial.available() <= 0) {}
    uint8_t data = Serial.read();
    spiRam.write((unsigned int)i, data);
  }
}

void print(int rowNum) {
  for (int r = 0; r < rowNum; r += 9) {
    while (!printer.SendInit()) {
      delay(100);
    }
    uint8_t buffer[640];
    for (int j = r; j < r + 9 && j < rowNum; j++) {
      for (int i = 0; i < 640; i++) {
        buffer[i] = (uint8_t)spiRam.read(j * 640 + i);
      }
      printer.SendData(buffer, 640);
    }
    printer.SendDataEnd();
    if (rowNum <= 9) {
      printer.SendPrint(beforeMargin, afterMargin, 0x5F);
    } else if (r == 0) {
      printer.SendPrint(beforeMargin, 0, 0x5F);
    } else if (r + 9 >= rowNum) {
      printer.SendPrint(0, afterMargin, 0x5F);
    } else {
      printer.SendPrint(0, 0, 0x5F);
    }
    printer.WaitPrinting();
  }
}
