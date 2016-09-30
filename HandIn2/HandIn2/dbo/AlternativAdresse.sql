--
-- Create Table    : 'AlternativAdresse'   
-- PersonID        :  (references Person.PersonID)
-- AdresseID       :  (references Adresse.AdresseID)
-- AdresseType     :  
--
CREATE TABLE AlternativAdresse (
    PersonID       BIGINT NOT NULL,
    AdresseID      BIGINT NOT NULL,
    AdresseType    NVARCHAR(300) NOT NULL,
CONSTRAINT pk_AlternativAdresse PRIMARY KEY CLUSTERED (PersonID,AdresseID),
CONSTRAINT fk_AlternativAdresse FOREIGN KEY (PersonID)
    REFERENCES Person (PersonID)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
CONSTRAINT fk_AlternativAdresse2 FOREIGN KEY (AdresseID)
    REFERENCES Adresse (AdresseID)
    ON DELETE CASCADE
    ON UPDATE CASCADE)