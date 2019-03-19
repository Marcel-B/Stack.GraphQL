CREATE TABLE Measure.dbo.ActiveMeasurePoints (
	Id uniqueidentifier NOT NULL,
	IsActive bit NOT NULL,
	LastValue float NOT NULL,
	Updated datetimeoffset NOT NULL,
	Point uniqueidentifier NOT NULL,
	CONSTRAINT ActiveMeasurePoints_PK PRIMARY KEY (Id),
	CONSTRAINT ActiveMeasurePoints_MeasurePoints_FK FOREIGN KEY (Point) REFERENCES Measure.dbo.MeasurePoints(Id)
) GO
