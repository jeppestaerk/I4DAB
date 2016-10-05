using HandIn3.DataAccess;
using HandIn3.DataModel;

namespace HandIn3TestApplication
{
    class HandIn3TestApplication
    {
        static void Main()
        {
            PersonkartotekDataUtil personkartotek = new PersonkartotekDataUtil();

            personkartotek.setCurrentPerson("Michael", "Nicholajsen");
            personkartotek.getCurrentTelefon();
            personkartotek.getCurrentAdresse();

            personkartotek.setCurrentPerson(2);
            personkartotek.getCurrentTelefon();
            personkartotek.getCurrentAdresse();
            Person locPerson = personkartotek.currentPerson;
            locPerson.Mellemnavn = "P";
            locPerson.PersonType = "fjende";
            personkartotek.UpdateCurrentPerson();
            personkartotek.setCurrentPerson(2);
        }
    }
}
