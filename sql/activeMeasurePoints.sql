CREATE TABLE Measure.dbo.ActiveMeasurePoints (
	Id uniqueidentifier NOT NULL,
	IsActive bit NOT NULL,
	LastValue float NOT NULL,
	Point uniqueidentifier NOT NULL,
	Created datetimeoffset DEFAULT sysdatetimeoffset() NULL,
	Updated datetimeoffset DEFAULT sysdatetimeoffset() NULL
);
