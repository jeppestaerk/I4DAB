namespace HandIn3DataAccess.Person
{
    class Person
    {
        public string Fornavn { get; set; }
        public string Mellemnavn { get; set; }
        public string Efternavn { get; set; }
        public string PersonType { get; set; }
        public Adresse.Adresse AdresseId { get; set; }
    }
}
