CREATE TABLE graphql.dbo.MeasureValues (
    Id uniqueidentifier NOT NULL,
    [Timestamp] datetimeoffset NOT NULL,
    Value real NOT NULL,
    Point uniqueidentifier NOT NULL,
    CONSTRAINT MeasureValues_PK PRIMARY KEY (Id),
    CONSTRAINT MeasureValues_MeasurePoints_FK FOREIGN KEY (Point) REFERENCES graphql.dbo.MeasurePoints(Id) ON DELETE CASCADE ON UPDATE CASCADE
) GO
CREATE INDEX MeasureValues_Timestamp_IDX ON graphql.dbo.MeasureValues ([Timestamp]) GO
