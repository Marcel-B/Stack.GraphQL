﻿create DATABASE Measure;

drop database Measure;

use Measure;

drop table units;

-- Drop table

-- DROP TABLE Measure.dbo.Units GO

CREATE TABLE Measure.dbo.Units (
    Id uniqueidentifier NOT NULL,
    Display varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    Name varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    Created datetimeoffset DEFAULT sysdatetimeoffset() NULL,
    Updated datetimeoffset DEFAULT sysdatetimeoffset() NULL,
    CONSTRAINT Units_PK PRIMARY KEY (Id)
);

INSERT INTO Measure.dbo.Units (Id,Display,Name,Created,Updated) VALUES 
('8CFF8D7B-00DB-42B2-B78D-057DAB5B5695','Signal','',NULL,NULL)
,('1B475C7F-CD64-4B0B-98F4-0DE5038936A4','NEQ1297587:1:BRIGHTNESS','',NULL,NULL)
,('544A6BB4-7733-4238-B625-16936CE7735A','Zustand Batterie','leer',NULL,NULL)
,('86B0DC19-1CA5-41D7-BAF6-30B4B21D7931','Wert','',NULL,NULL)
,('627B8097-2D09-4D77-BFD9-620B9C5451F5','Zustand Fenster','geöffnet',NULL,NULL)
,('FADBA535-E440-4F6C-BED6-6FEDA0C7FAD1','Spannung','V',NULL,NULL)
,('B5D104E2-F70D-4479-8106-729F346E458E','Fehler','Fehler',NULL,NULL)
,('4E618495-240D-41F3-8B1A-736E1B48CEB8','Anzahl','Stück',NULL,NULL)
,('02A96032-2C2E-4B05-BE17-83A9D03250F4','Feinstaub PM10','µg/m³',NULL,NULL)
,('D079A693-4835-4BC1-BD6B-879485EB2174','Samples','',NULL,NULL)
;
INSERT INTO Measure.dbo.Units (Id,Display,Name,Created,Updated) VALUES 
('1BF8EF7B-EA04-403F-A67B-88B472C10145','Feinstaub PM2.5','µg/m³',NULL,NULL)
,('934CE8D3-0DB9-4279-92BE-91997C49BBD8','Temperatur','°C',NULL,NULL)
,('6A13BD82-9B1D-4E22-B836-9A701C6D648D','MinMicro','',NULL,NULL)
,('4A9CB404-1193-45BC-8C52-AFC708EA2F9B','MaxMicro','',NULL,NULL)
,('52356656-C6A0-4C06-A585-BA8C0E97750E','Leistung','W',NULL,NULL)
,('574EFEB5-A708-46CD-A33F-C1CC6655DB55','Zustand','an/aus',NULL,NULL)
,('FC36426D-05AD-4837-BF78-C415D0705442','Öffnung','%',NULL,NULL)
,('3AF35BE7-5270-49E8-B8E9-C714FFB92307','Luftfeuchte','%',NULL,NULL)
,('DB5F325C-FE3C-4EBD-98E5-D55AD29E411F','Luftdruck','hPa',NULL,NULL)
;

-- Drop table

-- DROP TABLE Measure.dbo.Locations GO

CREATE TABLE Measure.dbo.Locations (
    Id uniqueidentifier NOT NULL,
    Display varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    Description varchar(250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    Longitude float NULL,
    Latitude float NULL,
    Floor int NULL,
    Created datetimeoffset DEFAULT sysdatetimeoffset() NULL,
    Updated datetimeoffset DEFAULT sysdatetimeoffset() NULL,
    CONSTRAINT Locations_PK PRIMARY KEY (Id)
);

INSERT INTO Measure.dbo.Locations (Id,Display,Description,Longitude,Latitude,Floor,Created,Updated) VALUES 
('08932E10-CC40-4169-ABB9-134374970031','Keller','Scheiblerstr. 111',6.602869,51.349884,-1,NULL,NULL)
,('0AD9B7A6-F1C5-46FB-844D-257E6B587106','Schlafzimmer','Scheiblerstr. 111',6.602869,51.349884,0,NULL,NULL)
,('CBED0495-14CF-4192-962F-2D05ED82059F','Kinderzimmer','Scheiblerstr. 111',6.602869,51.349884,0,NULL,NULL)
,('0A39EABC-ED42-48C6-9938-5F0B8855FE88','Büro','Scheiblerstr. 111',6.602869,51.349884,0,NULL,NULL)
,('DF8A8841-C8E1-438C-982C-77D141EF1D80','Wohnzimmer','Scheiblerstr. 111',6.602869,51.349884,0,NULL,NULL)
,('7D3AB857-59BC-48E6-8019-7B1D5BA29F41','Terrasse','Terrasse im Garten',6.602869,51.349884,0,NULL,NULL)
,('0AF80ED8-BB47-4E78-8D8A-803048253ED0','Bad','Scheiblerstr. 111',6.602869,51.349884,0,NULL,NULL)
,('4641CBEC-CB2D-49FF-A405-AD8B7EA51F5B','Küche','Scheiblerstr. 111',6.602869,51.349884,0,NULL,NULL)
,('F4EAC985-CF73-400C-BB66-B4554E9F31C1','Chili','Scheiblerstr. 111',6.602869,51.349884,0,NULL,NULL)
,('4E98B6BD-D69A-4982-A8CC-D90011F1C898','Gäste WC','Scheiblerstr. 111',6.602869,51.349884,0,NULL,NULL)
;

-- Drop table

-- DROP TABLE Measure.dbo.MeasurePoints GO

CREATE TABLE Measure.dbo.MeasurePoints (
    Id uniqueidentifier NOT NULL,
    Display varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    Max float DEFAULT 0 NOT NULL,
    Min float DEFAULT 0 NOT NULL,
    Unit uniqueidentifier NOT NULL,
    Location uniqueidentifier NULL,
    ExternId varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    Created datetimeoffset DEFAULT sysdatetimeoffset() NULL,
    Updated datetimeoffset DEFAULT sysdatetimeoffset() NULL,
    CONSTRAINT MeasurePoints_PK PRIMARY KEY (Id),
    CONSTRAINT MeasurePoints_Locations_FK FOREIGN KEY (Location) REFERENCES Measure.dbo.Locations(Id),
    CONSTRAINT MeasurePoints_Units_FK FOREIGN KEY (Unit) REFERENCES Measure.dbo.Units(Id) ON DELETE CASCADE ON UPDATE CASCADE
);

INSERT INTO Measure.dbo.MeasurePoints (Id,Display,Max,Min,Unit,Location,ExternId,Created,Updated) VALUES 
('E964AC14-ECE9-40AF-8F9D-03D4C841ABDA','Fenster links',1,0,'627B8097-2D09-4D77-BFD9-620B9C5451F5','DF8A8841-C8E1-438C-982C-77D141EF1D80','OEQ0707467:1:STATE',NULL,NULL)
,('E983E0D4-1B5A-45C1-B71F-0647C0A0F685','Fenster rechts',1,0,'B5D104E2-F70D-4479-8106-729F346E458E','0AD9B7A6-F1C5-46FB-844D-257E6B587106','OEQ0707446:1:ERROR',NULL,NULL)
,('D7D4D587-37BE-41D0-A8F4-0BF3513F72D1','Heat Index',40,5,'934CE8D3-0DB9-4279-92BE-91997C49BBD8','0AD9B7A6-F1C5-46FB-844D-257E6B587106','LSS:01:HEATINDEX','1900-01-01 00:00:00 +00:00','1900-01-01 00:00:00 +00:00')
,('8672AEF3-A80C-41E1-987C-0FB03419BB2E','Ventil',0,0,'FC36426D-05AD-4837-BF78-C415D0705442','DF8A8841-C8E1-438C-982C-77D141EF1D80','NEQ1778676:4:VALVE_STATE',NULL,NULL)
,('FC7BDD17-ECB3-4A90-9FBC-1A8922BAB0F5','Türe rechts',1,0,'544A6BB4-7733-4238-B625-16936CE7735A','DF8A8841-C8E1-438C-982C-77D141EF1D80','OEQ0222677:1:LOWBAT',NULL,NULL)
,('31B6FD9A-EBC3-456A-9CCF-1FBB2FEBA980','Bewegung',1,0,'574EFEB5-A708-46CD-A33F-C1CC6655DB55','7D3AB857-59BC-48E6-8019-7B1D5BA29F41','NEQ1297587:1:MOTION',NULL,NULL)
,('02B2A124-FC09-4C53-9A81-203662B536C7','Fenster links',1,0,'544A6BB4-7733-4238-B625-16936CE7735A','0AD9B7A6-F1C5-46FB-844D-257E6B587106','OEQ0707415:1:LOWBAT',NULL,NULL)
,('C3CB711E-9035-4C91-A6F9-24AA34718933','Fenster',1,0,'B5D104E2-F70D-4479-8106-729F346E458E','0AF80ED8-BB47-4E78-8D8A-803048253ED0','OEQ0222798:1:ERROR',NULL,NULL)
,('8FA026A5-BA9F-476A-AB7F-27406C3CEA91','BMP180',0,0,'934CE8D3-0DB9-4279-92BE-91997C49BBD8','7D3AB857-59BC-48E6-8019-7B1D5BA29F41','2063272:BMP180:TEMPERATURE',NULL,NULL)
,('4AA7109F-D40B-486A-A731-277864031265','Fenster links',1,0,'B5D104E2-F70D-4479-8106-729F346E458E','CBED0495-14CF-4192-962F-2D05ED82059F','OEQ0707459:1:ERROR',NULL,NULL)
;
INSERT INTO Measure.dbo.MeasurePoints (Id,Display,Max,Min,Unit,Location,ExternId,Created,Updated) VALUES 
('21D1E5C7-33CB-4824-BDDA-2B55E4CEF801','Fenster rechts',1,0,'627B8097-2D09-4D77-BFD9-620B9C5451F5','CBED0495-14CF-4192-962F-2D05ED82059F','OEQ0707418:1:STATE',NULL,NULL)
,('795F28B0-77ED-4A57-AF57-32A2C47CDBA0','DHT22',0,0,'3AF35BE7-5270-49E8-B8E9-C714FFB92307','7D3AB857-59BC-48E6-8019-7B1D5BA29F41','2063272:DHT22:HUMIDITY',NULL,NULL)
,('91D88EFE-9D50-44E3-82BC-353513857DC9','Türe rechts',1,0,'627B8097-2D09-4D77-BFD9-620B9C5451F5','DF8A8841-C8E1-438C-982C-77D141EF1D80','OEQ0222677:1:STATE',NULL,NULL)
,('50BB6D22-CAE0-4767-B129-37449AC53A67','Fenster rechts',1,0,'627B8097-2D09-4D77-BFD9-620B9C5451F5','DF8A8841-C8E1-438C-982C-77D141EF1D80','OEQ0707434:1:STATE',NULL,NULL)
,('42093156-563E-4731-8D5B-3E23C2CCB673','DS18B20',35,20,'934CE8D3-0DB9-4279-92BE-91997C49BBD8','F4EAC985-CF73-400C-BB66-B4554E9F31C1','CHILI:DS18B20:TEMPERATURE',NULL,NULL)
,('EC80C083-7A60-40D3-9AF5-4775AE0CBEFD','Ventil',0,0,'934CE8D3-0DB9-4279-92BE-91997C49BBD8','DF8A8841-C8E1-438C-982C-77D141EF1D80','NEQ1778676:4:ACTUAL_TEMPERATURE',NULL,NULL)
,('C912D681-25F6-4562-BC3D-54A9FA04E5E5','Fenster links',1,0,'627B8097-2D09-4D77-BFD9-620B9C5451F5','0AD9B7A6-F1C5-46FB-844D-257E6B587106','OEQ0707415:1:STATE',NULL,NULL)
,('777CECC4-C140-477D-BD94-5A0A611F47FC','SDS011',0,0,'02A96032-2C2E-4B05-BE17-83A9D03250F4','7D3AB857-59BC-48E6-8019-7B1D5BA29F41','2063272:PM10',NULL,NULL)
,('C928D913-67CD-47C6-8C09-5D8B17531D79','Temperatur',100,0,'934CE8D3-0DB9-4279-92BE-91997C49BBD8','DF8A8841-C8E1-438C-982C-77D141EF1D80','OEQ1668708:2:ACTUAL_TEMPERATURE',NULL,NULL)
,('516C6AB3-E615-462E-8718-63FD85220D6A','BMP180',0,0,'DB5F325C-FE3C-4EBD-98E5-D55AD29E411F','7D3AB857-59BC-48E6-8019-7B1D5BA29F41','2063272:BMP180:PRESSURE',NULL,NULL)
;
INSERT INTO Measure.dbo.MeasurePoints (Id,Display,Max,Min,Unit,Location,ExternId,Created,Updated) VALUES 
('78E8A6CB-FFB1-41E7-A5CA-65C9C5F06983','Türe links',1,0,'627B8097-2D09-4D77-BFD9-620B9C5451F5','DF8A8841-C8E1-438C-982C-77D141EF1D80','OEQ0226938:1:STATE',NULL,NULL)
,('9AF4EED9-8E8D-4AFA-A015-6A2CCB4673A3','Fenster',1,0,'627B8097-2D09-4D77-BFD9-620B9C5451F5','0AF80ED8-BB47-4E78-8D8A-803048253ED0','OEQ0222798:1:STATE',NULL,NULL)
,('6EF4C26F-0F36-44FE-9291-6EC8B4D0E694','Fenster rechts',1,0,'B5D104E2-F70D-4479-8106-729F346E458E','CBED0495-14CF-4192-962F-2D05ED82059F','OEQ0707418:1:ERROR',NULL,NULL)
,('FB43A587-8251-4EA1-97B2-6F2F702952A6','SDS011',0,0,'1BF8EF7B-EA04-403F-A67B-88B472C10145','7D3AB857-59BC-48E6-8019-7B1D5BA29F41','2063272:PM2_5',NULL,NULL)
,('985BE34C-4923-4657-817F-751D8F5E23DF','Temperatur',40,5,'934CE8D3-0DB9-4279-92BE-91997C49BBD8','0AD9B7A6-F1C5-46FB-844D-257E6B587106','LSS:01:TEMPERATURE','1900-01-01 00:00:00 +00:00','1900-01-01 00:00:00 +00:00')
,('AE3DC5E7-15CC-48A6-8BC6-755B57A140A3','Fenster rechts',1,0,'544A6BB4-7733-4238-B625-16936CE7735A','CBED0495-14CF-4192-962F-2D05ED82059F','OEQ0707418:1:LOWBAT',NULL,NULL)
,('B0F96C9C-F6FD-44C6-8AF0-7FB79804BD04','Fenster links',1,0,'627B8097-2D09-4D77-BFD9-620B9C5451F5','CBED0495-14CF-4192-962F-2D05ED82059F','OEQ0707459:1:STATE',NULL,NULL)
,('B678C510-9306-4B54-B426-8D81536CCC86','Türe rechts',1,0,'B5D104E2-F70D-4479-8106-729F346E458E','DF8A8841-C8E1-438C-982C-77D141EF1D80','OEQ0222677:1:ERROR',NULL,NULL)
,('38F5700F-1DFC-483E-8320-8F44BE26B251','Helligkeit',255,0,'86B0DC19-1CA5-41D7-BAF6-30B4B21D7931','7D3AB857-59BC-48E6-8019-7B1D5BA29F41','NEQ1297587:1:BRIGHTNESS',NULL,NULL)
,('6ECB2AFB-95E0-4E86-911C-9628F453CCA7','Luftfeuchte',100,0,'3AF35BE7-5270-49E8-B8E9-C714FFB92307','DF8A8841-C8E1-438C-982C-77D141EF1D80','OEQ1668708:2:ACTUAL_HUMIDITY',NULL,NULL)
;
INSERT INTO Measure.dbo.MeasurePoints (Id,Display,Max,Min,Unit,Location,ExternId,Created,Updated) VALUES 
('8F6648C0-2774-4F27-AD7E-96777555EACC','Stromzähler TV',0,0,'52356656-C6A0-4C06-A585-BA8C0E97750E','DF8A8841-C8E1-438C-982C-77D141EF1D80','OEQ0571647:2:POWER',NULL,NULL)
,('AF6B9D7D-5346-45DE-9947-974ED1A04B74','Fenster rechts',1,0,'B5D104E2-F70D-4479-8106-729F346E458E','DF8A8841-C8E1-438C-982C-77D141EF1D80','OEQ0707434:1:ERROR',NULL,NULL)
,('A862865D-CA15-4EFD-B208-9A5D3B6C1C9B','WiFi',0,0,'4A9CB404-1193-45BC-8C52-AFC708EA2F9B','7D3AB857-59BC-48E6-8019-7B1D5BA29F41','2063272',NULL,NULL)
,('D354B6DF-1347-4C6B-9D7B-9BAE5A483DA4','DHT22',100,0,'3AF35BE7-5270-49E8-B8E9-C714FFB92307','F4EAC985-CF73-400C-BB66-B4554E9F31C1','CHILI:DHT22:HUMIDITY',NULL,NULL)
,('032B23E9-1D79-45DB-8FED-A0AC9B1196AC','Fenster rechts',1,0,'544A6BB4-7733-4238-B625-16936CE7735A','DF8A8841-C8E1-438C-982C-77D141EF1D80','OEQ0707434:1:LOWBAT',NULL,NULL)
,('FB54636E-07AD-48EE-A4A8-A1B3C971B195','Fenster links',1,0,'B5D104E2-F70D-4479-8106-729F346E458E','0AD9B7A6-F1C5-46FB-844D-257E6B587106','OEQ0707415:1:ERROR',NULL,NULL)
,('B9E26581-8FDE-4DD2-8092-AAD0B7DA65AD','Heizung',1,0,'574EFEB5-A708-46CD-A33F-C1CC6655DB55','F4EAC985-CF73-400C-BB66-B4554E9F31C1','CHILI:HEATER',NULL,NULL)
,('BEB93B25-212D-4AD1-8BE8-ACBD8B8894C9','Fenster rechts',1,0,'627B8097-2D09-4D77-BFD9-620B9C5451F5','0AD9B7A6-F1C5-46FB-844D-257E6B587106','OEQ0707446:1:STATE',NULL,NULL)
,('2003DDAA-A695-46E0-85A5-AE67E20F5D1E','Fenster links',1,0,'544A6BB4-7733-4238-B625-16936CE7735A','DF8A8841-C8E1-438C-982C-77D141EF1D80','OEQ0707467:1:LOWBAT',NULL,NULL)
,('6B07766C-5AD1-48AE-8B28-AFBA0CFE19EF','Licht Decke',1,0,'574EFEB5-A708-46CD-A33F-C1CC6655DB55','DF8A8841-C8E1-438C-982C-77D141EF1D80','OEQ0182339:1:STATE',NULL,NULL)
;
INSERT INTO Measure.dbo.MeasurePoints (Id,Display,Max,Min,Unit,Location,ExternId,Created,Updated) VALUES 
('AAA6D5BA-0517-485E-AC68-C01C4A3435AB','Fenster links',1,0,'B5D104E2-F70D-4479-8106-729F346E458E','DF8A8841-C8E1-438C-982C-77D141EF1D80','OEQ0707467:1:ERROR',NULL,NULL)
,('70675A80-CF71-4C1C-B812-C28899CED2F2','Türe links',1,0,'B5D104E2-F70D-4479-8106-729F346E458E','DF8A8841-C8E1-438C-982C-77D141EF1D80','OEQ0226938:1:ERROR',NULL,NULL)
,('CCC55F5C-C2DF-4D39-B5D0-C7EE050A1113','WiFi',0,0,'D079A693-4835-4BC1-BD6B-879485EB2174','7D3AB857-59BC-48E6-8019-7B1D5BA29F41','2063272',NULL,NULL)
,('B30221EE-6C56-4BD9-A9EC-D412C982D867','Stromzähler',0,0,'52356656-C6A0-4C06-A585-BA8C0E97750E','08932E10-CC40-4169-ABB9-134374970031','NEQ0863777:1:POWER',NULL,NULL)
,('84E2803E-5C95-4126-999E-D508CD41A47B','WiFi',0,0,'8CFF8D7B-00DB-42B2-B78D-057DAB5B5695','7D3AB857-59BC-48E6-8019-7B1D5BA29F41','2063272',NULL,NULL)
,('A04C9CF8-07CA-42C5-A9A6-DC6D7004F8F1','Ventil Bad',100,0,'FC36426D-05AD-4837-BF78-C415D0705442','0AF80ED8-BB47-4E78-8D8A-803048253ED0','NEQ1774153:4:VALVE_STATE',NULL,NULL)
,('02D26C0B-15FB-4C61-8105-DCFFF5D78E19','Fenster links',1,0,'544A6BB4-7733-4238-B625-16936CE7735A','CBED0495-14CF-4192-962F-2D05ED82059F','OEQ0707459:1:LOWBAT',NULL,NULL)
,('511DE6DB-52D0-475C-8948-E09CE9A7521E','Licht',1,0,'574EFEB5-A708-46CD-A33F-C1CC6655DB55','F4EAC985-CF73-400C-BB66-B4554E9F31C1','CHILI:LIGHT',NULL,NULL)
,('C70334D8-6C77-46EB-9384-E7A51BAEF17B','WiFi',0,0,'6A13BD82-9B1D-4E22-B836-9A701C6D648D','7D3AB857-59BC-48E6-8019-7B1D5BA29F41','2063272',NULL,NULL)
,('6E78294C-0AB6-4E71-A790-EA099D0693A6','DHT22',0,0,'934CE8D3-0DB9-4279-92BE-91997C49BBD8','7D3AB857-59BC-48E6-8019-7B1D5BA29F41','2063272:DHT22:TEMPERATURE',NULL,NULL)
;
INSERT INTO Measure.dbo.MeasurePoints (Id,Display,Max,Min,Unit,Location,ExternId,Created,Updated) VALUES 
('89357810-27F8-4614-B143-EAC0DB12AE27','DHT22',30,10,'934CE8D3-0DB9-4279-92BE-91997C49BBD8','F4EAC985-CF73-400C-BB66-B4554E9F31C1','CHILI:DHT22:TEMPERATURE',NULL,NULL)
,('9D9D4E1C-2FDE-415F-AE94-EBA546F5A95F','Luftfeuchte',100,0,'3AF35BE7-5270-49E8-B8E9-C714FFB92307','0AD9B7A6-F1C5-46FB-844D-257E6B587106','LSS:01:HUMIDITY','2019-04-01 12:18:50.04929 +00:00','2019-04-01 12:18:50.04929 +00:00')
,('F6CF4A57-300E-4ED8-AF71-EF834DE2D111','WiFi',0,0,'D079A693-4835-4BC1-BD6B-879485EB2174','7D3AB857-59BC-48E6-8019-7B1D5BA29F41','2063272',NULL,NULL)
,('41E72BF8-D3FE-44E4-805E-F0778DE87035','Fenster',1,0,'544A6BB4-7733-4238-B625-16936CE7735A','0AF80ED8-BB47-4E78-8D8A-803048253ED0','OEQ0222798:1:LOWBAT',NULL,NULL)
,('0F759D83-F17D-4600-BB46-F4281BCB3C5F','Türe links',1,0,'544A6BB4-7733-4238-B625-16936CE7735A','DF8A8841-C8E1-438C-982C-77D141EF1D80','OEQ0226938:1:LOWBAT',NULL,NULL)
,('EB32A844-BD21-498E-8E50-F7EE9DE9C024','Ventil Bad',0,0,'934CE8D3-0DB9-4279-92BE-91997C49BBD8','0AF80ED8-BB47-4E78-8D8A-803048253ED0','NEQ1774153:4:ACTUAL_TEMPERATURE',NULL,NULL)
,('86FDF1EF-C990-43B3-829F-FB770DDDB580','Fenster rechts',1,0,'544A6BB4-7733-4238-B625-16936CE7735A','0AD9B7A6-F1C5-46FB-844D-257E6B587106','OEQ0707446:1:LOWBAT',NULL,NULL)
;

-- Drop table

-- DROP TABLE Measure.dbo.MeasureValues GO

CREATE TABLE Measure.dbo.MeasureValues (
    Id uniqueidentifier NOT NULL,
    [Timestamp] datetimeoffset NOT NULL,
    Value float NOT NULL,
    Point uniqueidentifier NOT NULL,
    Updated datetimeoffset DEFAULT sysdatetimeoffset() NULL,
    CONSTRAINT MeasureValues_PK PRIMARY KEY (Id),
    CONSTRAINT MeasureValues_MeasurePoints_FK FOREIGN KEY (Point) REFERENCES Measure.dbo.MeasurePoints(Id) ON DELETE CASCADE ON UPDATE CASCADE
) GO
CREATE INDEX MeasureValues_Timestamp_IDX ON Measure.dbo.MeasureValues ([Timestamp]) GO;

-- Drop table

-- DROP TABLE Measure.dbo.BatteryStates GO

CREATE TABLE Measure.dbo.BatteryStates (
    [Timestamp] datetimeoffset NOT NULL,
    State bit NOT NULL,
    Point uniqueidentifier NOT NULL,
    Updated datetimeoffset DEFAULT sysdatetimeoffset() NULL,
    Created datetimeoffset DEFAULT sysdatetimeoffset() NULL,
    Id uniqueidentifier DEFAULT newid() NOT NULL,
    CONSTRAINT BatteryStates_PK PRIMARY KEY (Id),
    CONSTRAINT BatteryStates_MeasurePoints_FK FOREIGN KEY (Point) REFERENCES Measure.dbo.MeasurePoints(Id)
) GO;

-- Drop table

-- DROP TABLE Measure.dbo.PriorityStates GO

CREATE TABLE Measure.dbo.PriorityStates (
    Id uniqueidentifier NOT NULL,
    [Timestamp] datetimeoffset NOT NULL,
    State bit NOT NULL,
    Point uniqueidentifier NOT NULL,
    CONSTRAINT PriorityStates_PK PRIMARY KEY (Id),
    CONSTRAINT PriorityStates_MeasurePoints_FK FOREIGN KEY (Point) REFERENCES Measure.dbo.MeasurePoints(Id)
) GO;

-- Drop table

-- DROP TABLE Measure.dbo.ActiveMeasurePoints GO

CREATE TABLE Measure.dbo.ActiveMeasurePoints (
    Id uniqueidentifier NOT NULL,
    IsActive bit NOT NULL,
    LastValue float NOT NULL,
    Point uniqueidentifier NOT NULL,
    Created datetimeoffset DEFAULT sysdatetimeoffset() NULL,
    Updated datetimeoffset DEFAULT sysdatetimeoffset() NULL,
    CONSTRAINT ActiveMeasurePoints_PK PRIMARY KEY (Id),
    CONSTRAINT ActiveMeasurePoints_MeasurePoints_FK FOREIGN KEY (Point) REFERENCES Measure.dbo.MeasurePoints(Id)
) GO;

-- Drop table

-- DROP TABLE Measure.dbo.Links GO

CREATE TABLE Measure.dbo.Links (
    Id uniqueidentifier DEFAULT newid() NOT NULL,
    Name varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    Created datetimeoffset DEFAULT sysdatetimeoffset() NOT NULL,
    LastEdit datetimeoffset DEFAULT sysdatetimeoffset() NOT NULL,
    LinkValue varchar(1000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    CONSTRAINT Links_PK PRIMARY KEY (Id)
) GO;

-- Drop table

-- DROP TABLE Measure.dbo.Links GO

CREATE TABLE Measure.dbo.Links (
    Id uniqueidentifier DEFAULT newid() NOT NULL,
    Name varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    Created datetimeoffset DEFAULT sysdatetimeoffset() NOT NULL,
    LastEdit datetimeoffset DEFAULT sysdatetimeoffset() NOT NULL,
    LinkValue varchar(1000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    CONSTRAINT Links_PK PRIMARY KEY (Id)
) GO;

INSERT INTO Measure.dbo.ActiveMeasurePoints (Id,IsActive,LastValue,Point,Created,Updated) VALUES 
('4B6B4077-6396-4CEB-9C4D-070F8AAF42FB',1,3607477,'A862865D-CA15-4EFD-B208-9A5D3B6C1C9B','2019-03-29 18:55:54.7162594 +00:00','2019-07-23 19:06:20.3208084 +00:00')
,('52CAAB8C-1225-4BEB-97B4-0AAAA16E678B',1,57.9,'9D9D4E1C-2FDE-415F-AE94-EBA546F5A95F','2019-04-01 12:23:53.0842129 +00:00','2019-07-23 19:07:01.0995498 +00:00')
,('188F926B-971E-40EA-B46D-0B25CD2595E1',1,0,'E964AC14-ECE9-40AF-8F9D-03D4C841ABDA',NULL,'2019-07-23 18:43:19.1228055 +00:00')
,('B9B2A905-DCFF-4453-BD46-15503A19BBA8',1,1.7,'8F6648C0-2774-4F27-AD7E-96777555EACC','2019-03-29 18:55:54.7162594 +00:00','2019-07-23 19:07:18.991133 +00:00')
,('B6A3B0BF-CEB0-43FE-89E4-19EBAE050763',1,0,'6B07766C-5AD1-48AE-8B28-AFBA0CFE19EF','2019-03-29 18:55:54.7162594 +00:00','2019-07-23 06:02:41.5373575 +00:00')
,('709489E6-D780-407E-A1B9-23203CBD4FC8',1,24.9,'985BE34C-4923-4657-817F-751D8F5E23DF','2019-04-01 12:23:53.0842129 +00:00','2019-07-23 19:07:01.0995552 +00:00')
,('24BACEE4-31C7-401A-87C1-2745A58EE74B',1,0,'B0F96C9C-F6FD-44C6-8AF0-7FB79804BD04',NULL,'2019-07-23 18:57:47.2698846 +00:00')
,('0A20F1EB-E480-4460-A02A-2DDA8C2DCF86',1,1014.92,'516C6AB3-E615-462E-8718-63FD85220D6A','2019-03-29 18:55:54.7162594 +00:00','2019-07-23 19:06:19.8418321 +00:00')
,('AD5A7F2F-CB64-41C7-AC86-2DF6ED3835DB',1,0,'A04C9CF8-07CA-42C5-A9A6-DC6D7004F8F1',NULL,'2019-07-23 19:07:10.9673174 +00:00')
,('1AA12DF8-4C79-4BD8-9549-366DB3D91BD2',1,32.2,'6E78294C-0AB6-4E71-A790-EA099D0693A6','2019-03-29 18:55:54.7162594 +00:00','2019-07-23 19:06:17.8751815 +00:00')
;
INSERT INTO Measure.dbo.ActiveMeasurePoints (Id,IsActive,LastValue,Point,Created,Updated) VALUES 
('4948A429-D2B0-44AE-AAC7-43A28950F809',1,17,'795F28B0-77ED-4A57-AF57-32A2C47CDBA0','2019-03-29 18:55:54.7162594 +00:00','2019-07-23 19:06:17.983271 +00:00')
,('645F5549-35E1-4BCA-87B3-43A3F47B073E',0,25.8125,'42093156-563E-4731-8D5B-3E23C2CCB673',NULL,'2019-04-21 11:07:47.886914 +00:00')
,('928220CA-2F99-4556-B2CA-4C61901559DF',1,0,'31B6FD9A-EBC3-456A-9CCF-1FBB2FEBA980','2019-03-29 18:55:54.7162594 +00:00','2019-07-23 14:35:12.1734986 +00:00')
,('9F1F4D5B-2096-4420-8703-5852E915162E',1,32.6,'8FA026A5-BA9F-476A-AB7F-27406C3CEA91','2019-03-29 18:55:54.7162594 +00:00','2019-07-23 19:06:20.0315252 +00:00')
,('4564035B-B223-4DD5-BF29-638F442F4216',1,0,'78E8A6CB-FFB1-41E7-A5CA-65C9C5F06983',NULL,'2019-07-23 18:43:22.7336451 +00:00')
,('722B5C95-0881-4B10-8FB8-7486ABCEFD73',1,1,'511DE6DB-52D0-475C-8948-E09CE9A7521E','2019-03-29 18:55:54.7162594 +00:00','2019-04-21 11:07:47.886914 +00:00')
,('F366682A-AAC2-475D-B357-78B25C03F9D2',1,27.3999996185,'89357810-27F8-4614-B143-EAC0DB12AE27','2019-03-29 18:55:54.7162594 +00:00','2019-04-21 11:07:47.886914 +00:00')
,('9C5F393F-D924-4223-A80A-820206EE129F',1,173,'38F5700F-1DFC-483E-8320-8F44BE26B251','2019-03-29 18:55:54.7162594 +00:00','2019-07-23 19:02:43.1731847 +00:00')
,('560E14DE-CCCD-46B4-BF47-821BCFA55B88',1,0,'F6CF4A57-300E-4ED8-AF71-EF834DE2D111','2019-03-29 18:55:54.7162594 +00:00','2019-03-29 18:55:54.7162594 +00:00')
,('A5E9C876-8059-4287-AB93-8834D2E34596',1,0,'8672AEF3-A80C-41E1-987C-0FB03419BB2E',NULL,'2019-07-23 19:06:24.7082238 +00:00')
;
INSERT INTO Measure.dbo.ActiveMeasurePoints (Id,IsActive,LastValue,Point,Created,Updated) VALUES 
('051890EB-5A3F-46A9-A727-8FF07A7D834E',1,0,'C912D681-25F6-4562-BC3D-54A9FA04E5E5',NULL,'2019-07-23 18:53:05.3512329 +00:00')
,('132E79DA-7F25-4F2E-BDB0-96DEB94343A8',1,24.96,'D7D4D587-37BE-41D0-A8F4-0BF3513F72D1','2019-04-01 12:23:53.0842129 +00:00','2019-07-23 19:07:01.0995394 +00:00')
,('09E0A38D-E0C4-40C7-BF38-9AF9EAE593F8',1,80,'C70334D8-6C77-46EB-9384-E7A51BAEF17B','2019-03-29 18:55:54.7162594 +00:00','2019-07-23 19:06:20.2319085 +00:00')
,('80303211-5758-4EC9-B353-AB82BA891C7C',1,27.4,'C928D913-67CD-47C6-8C09-5D8B17531D79','2019-03-29 18:55:54.7162594 +00:00','2019-07-23 19:07:40.0059591 +00:00')
,('9C23228E-A2BB-4C0C-84C7-B7B4F85C25EA',1,1,'9AF4EED9-8E8D-4AFA-A015-6A2CCB4673A3',NULL,'2019-07-23 18:09:46.425769 +00:00')
,('591F865F-23B2-4A15-B24F-B7EEED809745',1,0,'50BB6D22-CAE0-4767-B129-37449AC53A67',NULL,'2019-07-23 18:23:05.8406238 +00:00')
,('CDC0FA88-D720-4EAA-B492-C59E0B68456C',1,92,'B30221EE-6C56-4BD9-A9EC-D412C982D867','2019-03-29 18:55:54.7162594 +00:00','2019-07-23 19:07:10.8848005 +00:00')
,('980DD5E5-A015-4623-8BE1-CAD1FFB922ED',1,1682071,'CCC55F5C-C2DF-4D39-B5D0-C7EE050A1113','2019-03-29 18:55:54.7162594 +00:00','2019-07-23 19:06:20.1357847 +00:00')
,('79728CC7-650C-41B7-BC05-CD2B40AE2A29',1,1,'21D1E5C7-33CB-4824-BDDA-2B55E4CEF801',NULL,'2019-07-23 18:40:19.9421649 +00:00')
,('7FA1745F-C572-411C-82B4-D4037843774A',1,6.9,'777CECC4-C140-477D-BD94-5A0A611F47FC','2019-03-29 18:55:54.7162594 +00:00','2019-07-23 19:06:17.6474733 +00:00')
;
INSERT INTO Measure.dbo.ActiveMeasurePoints (Id,IsActive,LastValue,Point,Created,Updated) VALUES 
('0F1EA2E8-50B3-48E6-967A-D93347446F19',1,0,'B9E26581-8FDE-4DD2-8092-AAD0B7DA65AD','2019-03-29 18:55:54.7162594 +00:00','2019-04-21 11:07:47.886914 +00:00')
,('C122FD46-9635-493E-A94D-DE4257763890',1,3.2,'FB43A587-8251-4EA1-97B2-6F2F702952A6','2019-03-29 18:55:54.7162594 +00:00','2019-07-23 19:06:17.7668256 +00:00')
,('7E342B79-E0D1-48E1-B298-E13FB58759FD',1,1,'91D88EFE-9D50-44E3-82BC-353513857DC9',NULL,'2019-07-23 19:04:01.9058622 +00:00')
,('033B995E-2AE6-47BA-9795-E5CA7B2BF350',1,99.9000015259,'D354B6DF-1347-4C6B-9D7B-9BAE5A483DA4','2019-03-29 18:55:54.7162594 +00:00','2019-04-21 11:07:47.886914 +00:00')
,('56A812A9-69FF-477C-9ED5-E6F3A560A8ED',1,53,'6ECB2AFB-95E0-4E86-911C-9628F453CCA7','2019-03-29 18:55:54.7162594 +00:00','2019-07-23 19:07:40.0111163 +00:00')
,('A61B7EFA-C6EE-40BE-B824-EA448A8E7108',1,0,'BEB93B25-212D-4AD1-8BE8-ACBD8B8894C9',NULL,'2019-07-23 18:42:27.2958353 +00:00')
,('9780A126-E112-410C-90D4-F38B0713764F',1,-52,'84E2803E-5C95-4126-999E-D508CD41A47B','2019-03-29 18:55:54.7162594 +00:00','2019-07-23 19:06:20.4199182 +00:00')
;

CREATE TRIGGER [UpdateLastValues] ON [dbo].[MeasureValues] AFTER INSERT AS BEGIN SET NOCOUNT ON UPDATE MP SET LastValue = INSERTED.[Value], Updated = INSERTED.[Timestamp] FROM [dbo].[ActiveMeasurePoints] MP INNER JOIN Inserted ON MP.Point  = Inserted.Point END;
