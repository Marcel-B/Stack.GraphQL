CREATE TABLE Measure.dbo.MeasureValues (
    Id uniqueidentifier NOT NULL,
    [Timestamp] datetimeoffset NOT NULL,
    Value float NOT NULL,
    Point uniqueidentifier NOT NULL,
    CONSTRAINT MeasureValues_PK PRIMARY KEY (Id),
    CONSTRAINT MeasureValues_MeasurePoints_FK FOREIGN KEY (Point) REFERENCES Measure.dbo.MeasurePoints(Id) ON DELETE CASCADE ON UPDATE CASCADE
) GO
CREATE INDEX MeasureValues_Timestamp_IDX ON Measure.dbo.MeasureValues ([Timestamp]) GO
