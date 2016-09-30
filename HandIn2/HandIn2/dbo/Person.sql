--
-- Create Table    : 'Person'   
-- PersonID        :  
-- Fornavn         :  
-- Mellemnavn      :  
-- Efternavn       :  
-- PersonType      :  
-- AdresseID       :  (references Adresse.AdresseID)
--
CREATE TABLE Person (
    PersonID       BIGINT IDENTITY(1,1) NOT NULL,
    Fornavn        NVARCHAR(300) NOT NULL,
    Mellemnavn     NVARCHAR(300) NOT NULL,
    Efternavn      NVARCHAR(300) NOT NULL,
    PersonType     NVARCHAR(300) NOT NULL,
    AdresseID      BIGINT NULL,
CONSTRAINT pk_Person PRIMARY KEY CLUSTERED (PersonID),
CONSTRAINT fk_Person FOREIGN KEY (AdresseID)
    REFERENCES Adresse (AdresseID))