DBCC CHECKIDENT(Telefon, RESEED, 0)
DBCC CHECKIDENT(Person, RESEED, 0)
DBCC CHECKIDENT(Adresse, RESEED, 0)

INSERT INTO Adresse(Vejnavn,Husnummer,Postnummer,Bynavn) VALUES('Paludan mullersvej','18','8200','Aarhus')
INSERT INTO Adresse(Vejnavn,Husnummer,Postnummer,Bynavn) VALUES('Jens baggesensvej','112','8200','Aarhus')
INSERT INTO Adresse(Vejnavn,Husnummer,Postnummer,Bynavn) VALUES('Bredgade','15','4200','Kobenhavn')
INSERT INTO Adresse(Vejnavn,Husnummer,Postnummer,Bynavn) VALUES('Bernstoftvej','246','4210','Hanstholm')
INSERT INTO Adresse(Vejnavn,Husnummer,Postnummer,Bynavn) VALUES('Finlandsgade','21','8200','Aarhus')
INSERT INTO Adresse(Vejnavn,Husnummer,Postnummer,Bynavn) VALUES('Arnakvej','15','3000','Blåvandshug')

INSERT INTO Person(Fornavn,Mellemnavn,Efternavn,PersonType,AdresseID) VALUES('Jens','Christian','Hansen','familie',(SELECT Adresse.AdresseID FROM Adresse WHERE Vejnavn = 'Paludan mullersvej' AND Husnummer = '18'))
INSERT INTO Person(Fornavn,Mellemnavn,Efternavn,PersonType,AdresseID) VALUES('Mille','Petersen','Juul','kæreste',(SELECT Adresse.AdresseID FROM Adresse WHERE Vejnavn = 'Bredgade' AND Husnummer = '15'))
INSERT INTO Person(Fornavn,Mellemnavn,Efternavn,PersonType,AdresseID) VALUES('Kristian','Chris','Christensen','familie',(SELECT Adresse.AdresseID FROM Adresse WHERE Vejnavn = 'Bredgade' AND Husnummer = '15'))
INSERT INTO Person(Fornavn,Mellemnavn,Efternavn,PersonType,AdresseID) VALUES('Jakob','Juul','Hansen','familie',(SELECT Adresse.AdresseID FROM Adresse WHERE Vejnavn = 'Bernstoftvej' AND Husnummer = '246' ))
INSERT INTO Person(Fornavn,Mellemnavn,Efternavn,PersonType,AdresseID) VALUES('Michael','','Nicholajsen','kollega',(SELECT Adresse.AdresseID FROM Adresse WHERE Vejnavn = 'Jens baggesensvej' AND Husnummer = '112'))
INSERT INTO Person(Fornavn,Mellemnavn,Efternavn,PersonType,AdresseID) VALUES('Juliane','','Petersen','kollega',(SELECT Adresse.AdresseID FROM Adresse WHERE Vejnavn = 'Jens baggesensvej' AND Husnummer = '112'))
INSERT INTO Person(Fornavn,Mellemnavn,Efternavn,PersonType,AdresseID) VALUES('Chrity','','Sweets','kollega',(SELECT Adresse.AdresseID FROM Adresse WHERE Vejnavn = 'Jens baggesensvej' AND Husnummer = '112'))
INSERT INTO Person(Fornavn,Mellemnavn,Efternavn,PersonType,AdresseID) VALUES('Jesper','','Tørsøe','kollega',(SELECT Adresse.AdresseID FROM Adresse WHERE Vejnavn = 'Finlandsgade' AND Husnummer = '21'))
INSERT INTO Person(Fornavn,Mellemnavn,Efternavn,PersonType,AdresseID) VALUES('Anne','','Jensen','Mutter',(SELECT Adresse.AdresseID FROM Adresse WHERE Vejnavn = 'Arnakvej' AND Husnummer = '15'))
INSERT INTO Person(Fornavn,Mellemnavn,Efternavn,PersonType,AdresseID) VALUES('Kasper','','Jensen','Fatter',(SELECT Adresse.AdresseID FROM Adresse WHERE Vejnavn = 'Arnakvej' AND Husnummer = '15'))

INSERT INTO Telefon(Telefonnummer, TelefonType, PersonID) VALUES('42438811','mobil', (SELECT Person.PersonID FROM Person WHERE Fornavn = 'Jens' AND Efternavn = 'Hansen'))
INSERT INTO Telefon(Telefonnummer, TelefonType, PersonID) VALUES('42438812','mobil', (SELECT Person.PersonID FROM Person WHERE Fornavn = 'Mille' AND Efternavn = 'Juul'))
INSERT INTO Telefon(Telefonnummer, TelefonType, PersonID) VALUES('42438813','arbejde', (SELECT Person.PersonID FROM Person WHERE Fornavn = 'Kristian' AND Efternavn = 'Christensen'))
INSERT INTO Telefon(Telefonnummer, TelefonType, PersonID) VALUES('42438814','hjem', (SELECT Person.PersonID FROM Person WHERE Fornavn = 'Jakob' AND Efternavn = 'Hansen'))
INSERT INTO Telefon(Telefonnummer, TelefonType, PersonID) VALUES('42438815','arbejde', (SELECT Person.PersonID FROM Person WHERE Fornavn = 'Michael' AND Efternavn = 'Nicholajsen'))
INSERT INTO Telefon(Telefonnummer, TelefonType, PersonID) VALUES('42438816','hjem', (SELECT Person.PersonID FROM Person WHERE Fornavn = 'Juliane' AND Efternavn = 'Petersen'))
INSERT INTO Telefon(Telefonnummer, TelefonType, PersonID) VALUES('42438817','mobil', (SELECT Person.PersonID FROM Person WHERE Fornavn = 'Chrity' AND Efternavn = 'Sweets'))
INSERT INTO Telefon(Telefonnummer, TelefonType, PersonID) VALUES('42438818','hjem', (SELECT Person.PersonID FROM Person WHERE Fornavn = 'Jesper' AND Efternavn = 'Tørsøe'))
INSERT INTO Telefon(Telefonnummer, TelefonType, PersonID) VALUES('42438819','mobil', (SELECT Person.PersonID FROM Person WHERE Fornavn = 'Anne' AND Efternavn = 'Jensen'))
INSERT INTO Telefon(Telefonnummer, TelefonType, PersonID) VALUES('42438810','arbejde', (SELECT Person.PersonID FROM Person WHERE Fornavn = 'Kasper' AND Efternavn = 'Jensen'))

SELECT * from Person
SELECT * from Telefon
SELECT * from Adresse

DELETE FROM Person WHERE Person.Fornavn = 'Juliane'

SELECT * from Person
SELECT * from Telefon

UPDATE Person SET Person.PersonType = 'ven' WHERE Person.PersonType = 'kæreste'

SELECT * from Person

DELETE FROM Telefon
DELETE FROM Person
DELETE FROM Adresse