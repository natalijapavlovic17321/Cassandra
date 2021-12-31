using Cassandra;
using E_Student.Models;

public class MyMappings : Cassandra.Mapping.Mappings
{
    public MyMappings()
    {
        For<Student>().TableName("student1").PartitionKey(x => x.email)
        .Column(x => x.brojindeksa, cm => cm.WithName("brojindeksa"))
        .Column(x => x.godinaupisa, cm => cm.WithName("godinaupisa"))
        .Column(x => x.ime, cm => cm.WithName("ime"))
        .Column(x => x.prezime, cm => cm.WithName("prezime"))
        .Column(x => x.semestar, cm => cm.WithName("semestar"))
        .Column(x => x.sifra, cm => cm.WithName("sifra"));

    }
}