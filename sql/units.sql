CREATE TABLE Measure.dbo.Units (
    Id uniqueidentifier NOT NULL,
    Display varchar(100) NOT NULL,
    Name varchar(100) NOT NULL,
    CONSTRAINT Units_PK PRIMARY KEY (Id)
) GO
