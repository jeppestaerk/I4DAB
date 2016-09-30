--
-- Create Table    : 'Telefon'   
-- TelefonID       :  
-- Telefonnummer   :  
-- TelefonType     :  
-- PersonID        :  (references Person.PersonID)
--
CREATE TABLE Telefon (
    TelefonID      BIGINT IDENTITY(1,1) NOT NULL,
    Telefonnummer  NVARCHAR(300) NOT NULL,
    TelefonType    NVARCHAR(300) NOT NULL,
    PersonID       BIGINT NULL,
CONSTRAINT pk_Telefon PRIMARY KEY CLUSTERED (TelefonID),
CONSTRAINT fk_Telefon FOREIGN KEY (PersonID)
    REFERENCES Person (PersonID)
	ON DELETE CASCADE)