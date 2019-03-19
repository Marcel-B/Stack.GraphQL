CREATE TABLE Measure.dbo.PriorityStates (
	Id uniqueidentifier NOT NULL,
	[Timestamp] datetimeoffset NOT NULL,
	State bit NOT NULL,
	Point uniqueidentifier NOT NULL,
	CONSTRAINT PriorityStates_PK PRIMARY KEY (Id),
	CONSTRAINT PriorityStates_MeasurePoints_FK FOREIGN KEY (Point) REFERENCES Measure.dbo.MeasurePoints(Id)
) GO
