CREATE TABLE Measure.dbo.Locations (
	Id uniqueidentifier NOT NULL,
	Display varchar(50) NOT NULL,
	Description varchar(250) NOT NULL,
	Longitude float NULL,
	Latitude float NULL,
	Floor int NULL,
	Created datetimeoffset NOT NULL,
	CONSTRAINT Locations_PK PRIMARY KEY (Id)
) GO
