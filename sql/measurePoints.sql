CREATE TABLE Measure.dbo.MeasurePoints (
    Id uniqueidentifier NOT NULL,
    Display varchar(100) NOT NULL,
    Max float DEFAULT 0 NOT NULL,
    Min float DEFAULT 0 NOT NULL,
    Unit uniqueidentifier NOT NULL,
    CONSTRAINT MeasurePoints_PK PRIMARY KEY (Id),
    CONSTRAINT MeasurePoints_Units_FK FOREIGN KEY (Unit) REFERENCES Measure.dbo.Units(Id) ON DELETE CASCADE ON UPDATE CASCADE
) GO
ALTER TABLE Measure.dbo.MeasurePoints ADD Location uniqueidentifier NULL GO
ALTER TABLE Measure.dbo.MeasurePoints ADD CONSTRAINT MeasurePoints_Locations_FK FOREIGN KEY (Location) REFERENCES Measure.dbo.Locations(Id) GO
