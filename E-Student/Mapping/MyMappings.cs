using Cassandra;
using E_Student.Models;

public class MyMappings : Cassandra.Mapping.Mappings
{
    public MyMappings()
    {
        For<Student>().TableName("student").PartitionKey(x => x.Email)
        .Column(x => x.Indeks)//, cm => cm.WithName("\"BrojIndeksa\""))
        .Column(x => x.GodinaUpisa)//, cm => cm.WithName("\"GodinaUpisa\""))
        .Column(x => x.Ime)//, cm => cm.WithName("\"Ime\""))
        .Column(x => x.Prezime)//, cm => cm.WithName("\"Prezime\""))
        .Column(x => x.Semestar)//, cm => cm.WithName("\"Semestar\""))
        .Column(x => x.Sifra);//, cm => cm.WithName("\"Sifra\""));

    }
}