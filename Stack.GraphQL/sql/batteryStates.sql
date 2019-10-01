CREATE TABLE Measure.dbo.BatteryStates (
	Id uniqueidentifier NOT NULL,
	[Timestamp] datetimeoffset NOT NULL,
	State bit NOT NULL,
	Point uniqueidentifier NOT NULL,
	CONSTRAINT BatteryStates_PK PRIMARY KEY (Id),
	CONSTRAINT BatteryStates_MeasurePoints_FK FOREIGN KEY (Point) REFERENCES Measure.dbo.MeasurePoints(Id)
) GO
